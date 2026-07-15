using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityUtility;
using static TimeUtility;
using static MathUtility;
using static FrameBaseHotFix;
using static FrameUtility;
using static GDR;

// 角色的触发类buff
public class CharacterTrigger : CharacterBuff
{
	protected SafeDictionary<CharacterGame, List<CharacterState>> mBuffList = new();// 已添加的buff列表
	protected List<int> mBuffDetailIDList = new();								// 触发所需要添加的buffDetailID列表
	protected BuffTriggerAddBuffCallback mOnTriggerWillAddBuff;					// 触发器即将添加buff的回调,可以用于修改buff参数,每个添加buff都会调用
	protected BuffTriggerCheck mCanTriggerCheck;								// 自定义判断是否可触发的函数
	protected BuffTriggerCallback mWillTriggerCallback;                         // 此触发器已经触发即将附加buff时的回调
	protected BuffTriggerCallback mCustomTriggerCallback;						// 自定义触发逻辑,允许外部实现特殊的附加buff逻辑
	protected long mCDTime;														// 毫秒数的cd
	protected long mLastTriggerTime;											// 上一次成功触发的时间戳
	protected int mMaxOverlap;													// 最大叠加次数
	protected int mProbability;													// 触发几率,万分比
	protected bool mBuffToTarget;												// 是否将buff添加给被操作者,比如被攻击者
	protected bool mDeadCanTrigger;												// 角色死亡以后是否也能触发
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffList.clear();
		mBuffDetailIDList.Clear();
		mOnTriggerWillAddBuff = null;
		mCanTriggerCheck = null;
		mWillTriggerCallback = null;
		mCustomTriggerCallback = null;
		mCDTime = 0;
		mLastTriggerTime = 0;
		mMaxOverlap = 0;
		mProbability = 0;
		mBuffToTarget = false;
		mDeadCanTrigger = false;
	}
	public override void enter()
	{
		base.enter();
		var thisParam = getParam() as CharacterTriggerParam;
		foreach (int id in mBuffDetailIDList.addRange(thisParam.mBuffDetailIDList))
		{
			if (id <= 0)
			{
				logError("配置的ID错误:" + id);
			}
		}
		mBuffToTarget = thisParam.mBuffTarget;
		mLastTriggerTime = 0;
		mCDTime = (long)(thisParam.mCD * 1000);
		mMaxOverlap = thisParam.mMaxOverlap;
		mProbability = thisParam.mProbability;
		mDeadCanTrigger = thisParam.mBuffData.mDeadCanTrigger;
	}
	public void setCanTriggerCheck(BuffTriggerCheck callback) { mCanTriggerCheck = callback; }
	public void setWillTriggerCallback(BuffTriggerCallback callback) { mWillTriggerCallback = callback; }
	public void setCustomTriggerCallback(BuffTriggerCallback callback) { mCustomTriggerCallback = callback; }
	public void setWillAddBuffCallback(BuffTriggerAddBuffCallback callback) { mOnTriggerWillAddBuff = callback; }
	public CharacterGame getBuffToCharacter(CharacterGame tempTarget) { return mBuffToTarget? tempTarget : mCharacterGame; }
	public int getProbability() { return mProbability; }
	public List<int> getBuffDetailIDList() { return mBuffDetailIDList; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected bool triggerProbability(CharacterGame target) 
	{
		int propbability = mProbability;
		CharacterGame buffCharacter = getBuffToCharacter(target);
		if (buffCharacter?.getTriggerProbabilityCallback() != null)
		{
			propbability = buffCharacter.getTriggerProbabilityCallback().Invoke(this);
		}
		return propbability > 0 && randomHit(propbability, ODDS_SCALE); 
	}
	protected bool canTrigger(CharacterGame target)
	{
		// 是否已经死亡,是否已冷却
		if (!isActive() || 
			!mDeadCanTrigger && getBuffToCharacter(target).getHP() <= 0 ||
			getNowTimeStampMS() - mLastTriggerTime < mCDTime * Time.timeScale)
		{
			return false;
		}
		// 是否已经达到叠加上限
		if (mMaxOverlap > 0 && 
			mBuffList.tryGetValue(getBuffToCharacter(target), out var buffList) && 
			buffList.Count >= mMaxOverlap)
		{
			return false;
		}
		if (mCanTriggerCheck != null && !mCanTriggerCheck(mCharacter, this))
		{
			return false;
		}
		return true;
	}
	protected void resetCD()
	{
		mLastTriggerTime = getNowTimeStampMS();
	}
	protected void addBuff(CharacterGame target, INT damage = null, SkillBullet bullet = null, CharacterSkill skill = null, List<CharacterBuff> resultList = null)
	{
		CharacterGame buffCharacter = getBuffToCharacter(target);
		if (mCustomTriggerCallback != null)
		{
			mCustomTriggerCallback(buffCharacter, this);
			return;
		}
		if (mBuffDetailIDList.Count == 0)
		{
			return;
		}
		foreach (int id in mBuffDetailIDList)
		{
			// 这里不能合并为1行,否则可能会因为resultList为空而不执行doAddBuff
			CharacterBuff buff = doAddBuff(id, buffCharacter, damage, bullet, skill);
			resultList?.Add(buff);
		}
	}
	protected CharacterBuff doAddBuff(int buffDetailID, CharacterGame target, INT damage = null, SkillBullet bullet = null, CharacterSkill skill = null)
	{
		using var a = new BuffParamScope(out CharacterBuffParam param, buffDetailID);
		param.mCallback = onBuffAdd;
		param.mBuffTrigger = this;
		param.mSource = mCharacter;
		param.mBullet = bullet;
		param.mSkill = skill;
		param.mDamage = damage;
		param.mTriggerAssignID = mAssignID;
		mOnTriggerWillAddBuff?.Invoke(target, this, param);
		Type stateType = mStateManager.getStateType(param.mBuffData.mID);
		if (target.getStateMachine().addState(stateType, param, 0) is not CharacterBuff buff)
		{
			return null;
		}
		if (!mBuffList.tryGetValue(target, out var buffList))
		{
			LIST_PERSIST(out buffList);
			mBuffList.add(target, buffList);
		}
		buffList.Add(buff);
		buff.addWillRemoveCallback(this, (CharacterState state) => 
		{
			if (!mBuffList.tryGetValue(target, out var buffList))
			{
				return;
			}
			buffList.Remove(state);
			if (buffList.Count == 0)
			{
				UN_LIST(ref buffList);
				mBuffList.remove(target);
			}
		});
		return buff;
	}
	protected void onBuffAdd(bool result, CharacterTrigger triggerBuff, long triggerBuffAssignID)
	{
		if (triggerBuff == null ||
			triggerBuff.getAssignID() != triggerBuffAssignID ||
			!result)
		{
			return;
		}
		// 触发成功后重置CD
		triggerBuff.resetCD();
	}
	// 移除添加到指定角色上的所有buff
	protected void removeCharacterAddedBuff(CharacterGame character)
	{
		if (mBuffList.tryGetValue(character, out var buffList))
		{
			foreach (CharacterState buff in buffList)
			{
				if (!buff.isValid())
				{
					continue;
				}
				buff.removeWillRemoveCallback(this);
				character.getStateMachine().removeState(buff, true);
			}
			UN_LIST(ref buffList);
			mBuffList.remove(character);
		}
	}
	// 移除所有角色的通过当前buff所触发的buff
	protected void removeAllAdded()
	{
		using var a = new SafeDictionaryReader<CharacterGame, List<CharacterState>>(mBuffList);
		foreach (var buffPair in a.mReadList)
		{
			foreach (CharacterState buff in buffPair.Value)
			{
				if (!buff.isValid())
				{
					continue;
				}
				buff.removeWillRemoveCallback(this);
				buffPair.Key.getStateMachine().removeState(buff, true);
			}
			UN_LIST(buffPair.Value);
		}
		mBuffList.clear();
	}
	// 移除所有通过当前buff所触发的buff,仅限添加在自己身上的buff
	protected void removeAllAddedSelf(bool willDestroy)
	{
		using var a = new SafeDictionaryReader<CharacterGame, List<CharacterState>>(mBuffList);
		foreach (var buffList in a.mReadList.Values)
		{
			foreach (CharacterState buff in buffList)
			{
				// 只能移除触发添加在自己身上的buff,添加到别人身上的buff不能移除,否则会出现重复退出状态的问题
				if (!buff.isValid() || buff.getCharacter() != mCharacter)
				{
					continue;
				}
				buff.removeWillRemoveCallback(this);
				if (willDestroy)
				{
					buff.leave(true, true, null);
				}
				else
				{
					mCharacter.getStateMachine().removeState(buff, true);
				}
			}
			UN_LIST(buffList);
		}
		mBuffList.clear();
	}
	protected void onTrigger()
	{
		mWillTriggerCallback?.Invoke(mCharacter, this);
	}
}