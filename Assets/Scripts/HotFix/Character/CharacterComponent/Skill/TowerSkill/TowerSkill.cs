using System.Collections.Generic;
using UnityEngine;
using static GBR;
using static MathUtility;
using static FrameBaseUtility;
using static FrameBaseHotFix;
using static FrameUtility;

// 防御塔技能
public class TowerSkill : CharacterSkill
{
	protected Dictionary<string, Transform> mFirePointLocalPosition = new(); // 发射子弹的坐标,相对坐标,使用时需要转换为世界坐标
	protected List<EDSkillBullet> mBulletDataList = new();	// 技能初始包含的子弹表格数据列表
	protected List<float> mFireTime = new();				// 子弹发射的时间点列表的备份
	protected List<SEARCH_TARGET> mSearchTarget = new();	// 寻敌方式，取列表最后一个为当前的
	protected EDTowerSkill mSkillData;						// 技能的表格数据
	protected CharacterTower mTower;						// 拥有此技能的塔
	protected GameEffect mFireEffect;						// 释放技能时的特效
	protected CharacterGame mFacingTarget;					// 朝向的目标
	protected int mBulletIncreaseCount;						// 塔子弹数量增加
	protected float mBulletIncreasePercent;					// 塔子弹数量百分比增加
	protected bool mFirePointListInited;					// mFirePointLocalPosition是否已经初始化完毕,因为要等待模型加载完才能初始化,所以无法单独判断mFirePointLocalPosition是否可用
	protected bool mEverHasTarget;							// 是否有过目标
	protected int mWaveBulletCount;							// 波次累计子弹数
	protected int mForceNewTarget;							// 强制选择新目标
	public override void resetProperty()
	{
		base.resetProperty();
		mFirePointLocalPosition.Clear();
		mBulletDataList.Clear();
		mFireTime.Clear();
		mSearchTarget.Clear();
		mSkillData = null;
		mTower = null;
		mFireEffect = null;
		mFacingTarget = null;
		mBulletIncreaseCount = 0;
		mBulletIncreasePercent = 0.0f;
		mFirePointListInited = false;
		mEverHasTarget = false;
		mWaveBulletCount = 0;
		mForceNewTarget = 0;
	}
	public virtual void initData(EDTowerSkill skillData, ParamCopyable paramTemplate)
	{
		mSkillData = skillData;
		// 创建此技能的子弹信息
		refreshBulletCount();
	}
	public EDTowerSkill getSkillData() { return mSkillData; }
	public override void destroy()
	{
		base.destroy();
		mEffectManager.destroyEffect(ref mFireEffect);
	}
	public void onModelLoaded()
	{
		initFirePoint();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mFacingTarget != null)
		{
			if (mFacingTarget is CharacterMonster monster && monster.getMonsterData().mIsInvisible > 0)
			{
				mFacingTarget = null;
				return;
			}
			if (mFacingTarget.getHP() <= 0 || mFacingTarget.isDestroy())
			{
				mFacingTarget = null;
				return;
			}
			mTower.getComAvatar().towerLookPostion(mFacingTarget.getPosition());
		}
		else if (!mEverHasTarget)
		{
			mTower.getComAvatar().towerLookPostion(mBattleScene.getGridPosition(mTowerDefenceSystem.getStartPointIndex(0)));
		}
	}
	public override void setCharacter(CharacterGame character)
	{
		base.setCharacter(character);
		mTower = character as CharacterTower;
	}
	public float getRealCD() { return mTower.getTowerData().getFinalCD(mSkillData.mCD); }
	public List<EDSkillBullet> getBulletDataList() { return mBulletDataList; }
	public int getOriginBulletCount() { return mSkillData.mBullet.Count; }
	public virtual void fire()
	{
		checkSearchTarget();

		// 没有目标则无法攻击
		if (mFacingTarget == null)
		{
			return;
		}
		onPreFireSkill();
		mRemainCD = getRealCD();
		mEverHasTarget = true;
		// 朝向目标,默认朝向第一个目标
		mTower.getComAvatar().towerLookPostion(mFacingTarget.getPosition());
		fireAllBullet();

		// 播放释放技能的特效，如果没有加载就先异步加载再播放
		if (mSkillData.mFireEffect > 0)
		{
			if (mFireEffect == null)
			{
				EDEffect effectData = mExcelEffect.query(mSkillData.mFireEffect);
				mEffectManager.createEffectAsyncSafe(effectData.mPath, this, mTower, effectData.mSupportMoveToHide, (GameEffect effect) =>
				{
					mFireEffect = effect;
					mFireEffect.setPosition(mTower.getPosition());
					mFireEffect.play();
				}, 0);
			}
			else
			{
				mFireEffect.play();
			}
		}

		// 播放开火音效
		if (mSkillData.mFireSound > 0)
		{
			AT.SOUND_2D(mSkillData.mFireSound);
		}

		// 播放攻击动画
		fireAnimation();

		onPostFireSkill();
		if (mForceNewTarget > 0)
		{
			mFacingTarget = null;
		}
	}
	public int getBulletIncreaseCount() { return mBulletIncreaseCount; }
	public void setBulletIncreaseCount(int value)
	{
		mBulletIncreaseCount = value;
		refreshBulletCount();
	}
	public float getBulletIncreasePercent() { return mBulletIncreasePercent; }
	public void setBulletIncreasePercent(float value)
	{
		mBulletIncreasePercent = value;
		refreshBulletCount();
	}
	public int getWaveBulletCount() { return mWaveBulletCount; }
	public void setWaveBulletCount(int value) { mWaveBulletCount = value; }
	public void addSearchTarget(SEARCH_TARGET value) { mSearchTarget.Add(value); }
	public void removeSearchTarget(SEARCH_TARGET value) { mSearchTarget.Remove(value); }
	public SEARCH_TARGET getSearchTarget()
	{
		if (mSearchTarget.Count == 0)
		{
			return mSkillData.mSearchTarget;
		}
		return mSearchTarget[^1];
	}
	public int getForceNewTarget() { return mForceNewTarget; }
	public void setForceNewTarget(int value) { mForceNewTarget = value; }
	public override void notifyWaveChanged()
	{
		base.notifyWaveChanged();
		mWaveBulletCount = 0;
		using var a = new ClassScope<EventBulletWaveCountChanged>(out var paramCountChanged);
		paramCountChanged.mCount = mWaveBulletCount;
		mEventSystem.pushEvent(paramCountChanged, mCharacter.getGUID());
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected virtual void fireAnimation()
	{
		if (mTower.getAnimator() != null)
		{
			mTower.getAnimator().SetTrigger("Fire0");
		}
	}
	protected CharacterGame getFocusAttackTarget()
	{
		CharacterMonster monster = mTowerDefenceSystem.getFocusedMonster();
		if (!monster.isValid())
		{
			return null;
		}
		bool targetFly = mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER;
		if (mSkillData.mEnemyType != TARGET_BEHAVIOUR_TYPE.ALL_MONSTER && monster.getMonsterData().mFlyable != targetFly)
		{
			return null;
		}
		return monster;
	}
	protected void checkSearchTarget()
	{
		CharacterGame focusTarget = getFocusAttackTarget();
		if (focusTarget != null && targetAvailable(focusTarget))
		{
			mFacingTarget = focusTarget;
		}
		// 搜寻目标,并朝目标发射子弹
		if (mFacingTarget != null && mFacingTarget.isDestroy())
		{
			mFacingTarget = null;
		}
		// 是否清空当前目标
		if (mFacingTarget != null)
		{
			if (mSkillData.mClearTarget)
			{
				mFacingTarget = null;
			}
			else
			{
				// 当前目标是否还满足要求,暂时判断还是否在范围内
				if (!targetAvailable())
				{
					mFacingTarget = null;
				}
			}
		}
		// 选择一个新的目标
		if (mFacingTarget == null)
		{
			// 有强制目标时需要选择为强制目标
			bool targetFly = mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER;
			if (getSearchTarget() != SEARCH_TARGET.SELF &&
				mTower.getForceTarget() is CharacterMonster monster &&
				monster.getMonsterData().mFlyable == targetFly)
			{
				mFacingTarget = mTower.getForceTarget();
			}
			// 按照配置的规则选择目标
			else
			{
				searchNewTarget();
			}
		}
	}
	protected virtual bool targetAvailable()
	{
		return targetAvailable(mFacingTarget);
	}
	protected bool targetAvailable(CharacterGame character)
	{
		if (character is CharacterMonster monster && monster.getMonsterData().mIsInvisible > 0)
		{
			return false;
		}
		return character.getHP() > 0 && lengthLess(mTower.getPosition() - character.getPosition(), mTower.getRange());
	}
	protected virtual void searchNewTarget()
	{
		SEARCH_TARGET searchTarget = getSearchTarget();
		Vector3 pos = mTower.getPosition();
		float range = mTower.getRange();
		if (searchTarget == SEARCH_TARGET.NEAREST)
		{
			if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.ALL_MONSTER)
			{
				mFacingTarget = mTowerDefenceSystem.getNearestMonsterInRange(pos, range);
			}
			else if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
			{
				mFacingTarget = mTowerDefenceSystem.getNearestWalkMonsterInRange(pos, range);
			}
			else if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
			{
				mFacingTarget = mTowerDefenceSystem.getNearestFlyMonsterInRange(pos, range);
			}
		}
		else if (searchTarget == SEARCH_TARGET.SELF)
		{
			mFacingTarget = mTower;
		}
		else if(searchTarget == SEARCH_TARGET.RANDOM)
		{
			mFacingTarget = null;
			using var a = new ListScope<CharacterMonster>(out List<CharacterMonster> list);
			if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.ALL_MONSTER)
			{
				mTowerDefenceSystem.getMonstersInRange(pos, range, list);
			}
			else if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
			{
				mTowerDefenceSystem.getWalkMonstersInRange(pos, range, list);
			}
			else if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
			{
				mTowerDefenceSystem.getFlyMonstersInRange(pos, range, list);
			}
			if(list.Count > 0)
			{
				mFacingTarget = list[randomInt(0, list.Count - 1)];
			}
		}
	}
	protected virtual void fireAllBullet()
	{
		if (!mFirePointListInited || mBulletDataList.Count == 0)
		{
			return;
		}
		// 发射子弹
		int bulletCount = mBulletDataList.Count;
		for (int i = 0; i < bulletCount; ++i)
		{
			CMD_DELAY(out CmdCharacterFireBullet cmd);
			cmd.mFirePosMap = mFirePointLocalPosition;
			cmd.mBulletData = mBulletDataList[i];
			cmd.mTarget = mFacingTarget;
			cmd.mTargetAssignID = cmd.mTarget?.getAssignID() ?? 0;
			cmd.mFireID = mFireID;
			pushDelayCommand(cmd, mTower, mFireTime[i], this);
		}
	}
	protected virtual void initFirePoint()
	{
		mFirePointListInited = true;
		foreach (EDSkillBullet bulletData in mBulletDataList)
		{
			string pointName = bulletData.mStartPointName;
			if (pointName.isEmpty() || mFirePointLocalPosition.ContainsKey(pointName))
			{
				continue;
			}
			GameObject point = findGameObject(pointName, mTower.getComAvatar().getModel(), true);
			mFirePointLocalPosition.Add(pointName, point.transform);
		}
	}
	protected virtual void refreshBulletCount()
	{
		refreshBulletCountInterval();
	}
	protected void refreshBulletCountInterval(float interval = -1)
	{
		mBulletDataList.Clear();
		foreach (int bulletID in mSkillData.mBullet)
		{
			mBulletDataList.Add(mExcelSkillBullet.query(bulletID));
		}
		mFireTime.setRange(mSkillData.mFireTime);
		if (mBulletDataList.Count == 0)
		{
			return;
		}
		EDSkillBullet lastBulletData = mBulletDataList[mBulletDataList.Count - 1];
		float startFireTime = mFireTime[mFireTime.Count - 1];
		float fireTime = 0.0f;
		foreach (float each in mFireTime)
		{
			fireTime += each;
		}
		float timeInterval = interval;
		if (interval < 0.0f)
		{
			timeInterval = divide(fireTime, mFireTime.Count);
		}
		int count = getOriginBulletCount();
		count = round((count + mBulletIncreaseCount) * (1 + mBulletIncreasePercent)) - count;
		for (int i = 0; i < count; ++i)
		{
			startFireTime += timeInterval;
			mBulletDataList.Add(lastBulletData);
			mFireTime.Add(startFireTime);
		}
	}
}