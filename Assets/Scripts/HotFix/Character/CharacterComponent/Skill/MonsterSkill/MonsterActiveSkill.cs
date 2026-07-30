using System.Collections.Generic;
using static FrameUtility;
using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static GBR;
using static GDR;

// 怪物主动技能
public class MonsterActiveSkill : MonsterSkillBase
{
	protected List<EDSkillBullet> mBulletDataList = new();      // 技能初始包含的子弹表格数据列表
	protected List<float> mFireTime = new();                    // 子弹发射的时间点列表的备份
	protected GameEffect mFireEffect;                           // 技能释放时的特效
	protected CharacterGame mTarget;							// 当前目标
	public override void initData(EDMonsterSkill skillData)
	{
		base.initData(skillData);
		// 开始监听怪物的血量改变事件
		mEventSystem.listenEvent<EventMonsterHPChange>(mMonster.getGUID(), onMonsterHPChanged, this);
		initBulletData();
	}
	public override void destroy()
	{
		base.destroy();
		mEffectManager.destroyEffect(ref mFireEffect);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBulletDataList.Clear();
		mFireTime.Clear();
		mFireEffect = null;
		mTarget = null;
	}
	public override bool canFire() { return base.canFire() && isCoolDown(); }
	public void fire(int hpIndex)
	{
		onPreFireSkill();
		mRemainCD = mSkillData.mCD;
		mMonster.setMP((mMonster.getMP() - mSkillData.mMP).clampMin());

		// 播放动作,动作结束后buff生效
		if (mSkillData.mAnimation > 0 && mSkillData.mAnimationDuration > 0.0f)
		{
			using var a = new ClassScope<ActionSkillStartParam>(out var param);
			param.mAnimation = mSkillData.mAnimation;
			param.mSpeed = 1.0f;
			param.mBuffTime = mSkillData.mAnimationDuration;
			var state = mMonster.getStateMachine().addState<ActionSkillStart>(param);
			state.setLeaveCallback((_, isBreak, _, _) =>
			{
				if(isBreak)
				{
					return;
				}
				mTarget = searchTarget();
				buffToTarget(hpIndex);
			});
		}
		// 没有动作,则buff立即生效
		else
		{
			mTarget = searchTarget();
			buffToTarget(hpIndex);
		}

		// 播放技能释放的特效
		if (mSkillData.mFireEffect > 0)
		{
			if (mFireEffect == null)
			{
				EDEffect effectData = mExcelEffect.query(mSkillData.mFireEffect);
				mEffectManager.createEffectAsyncSafe(effectData.mPath, this, mMonster, effectData.mSupportMoveToHide, (GameEffect effect)=>
				{
					mFireEffect = effect;
					mFireEffect.setEffectDestroyCallback((GameEffect effect) =>
					{
						if (mFireEffect == effect)
						{
							mFireEffect = null;
						}
					});
					mFireEffect.setParent(mMonster.getAvatar().getModel());
					delayCall(mSkillData.mFireEffectTime , () => { fireEffect(); }, this);
				}, 0, false);
			}
			else
			{
				delayCall(mSkillData.mFireEffectTime , () => { fireEffect(); }, this);
			}
		}

		fireAllBullet();
		onPostFireSkill();
	}
	protected void fireEffect()
	{
		mFireEffect.play();
		AT.SOUND_2D(mSkillData.mFireSFX);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void buffToTarget(int hpIndex)
	{
		if (mTarget == null)
		{
			return;
		}
		COMCharacterStateMachine stateMachine = mTarget.getStateMachine();
		foreach (int buffDetailID in mSkillData.mDefaultFireBuff)
		{
			using var a = new BuffParamScope(out CharacterBuffParam param, buffDetailID);
			CharacterState newState = stateMachine.addState(mStateManager.getStateType(param.mBuffData.mID), param, 0);
			if (newState == null || newState is not CharacterTrigger trigger)
			{
				continue;
			}
			trigger.setWillAddBuffCallback((_, _, buffParam) => { buffParam.mSource = mMonster; });
		}
		if (hpIndex == 0)
		{
			foreach (int buffID in mSkillData.mFireBuff0)
			{
				characterAddBuff(buffID, mTarget, mMonster, null, this);
			}
		}
		else if (hpIndex == 1)
		{
			foreach (int buffID in mSkillData.mFireBuff1)
			{
				characterAddBuff(buffID, mTarget, mMonster, null, this);
			}
		}
	}
	// 提供的默认发射所有子弹的方法,有特殊需求可以重写此方法
	protected virtual void fireAllBullet()
	{
		// 发射子弹
		int bulletCount = mBulletDataList.Count;
		for (int i = 0; i < bulletCount; ++i)
		{
			CMD_DELAY(out CmdCharacterFireBullet cmd);
			cmd.mBulletData = mBulletDataList[i];
			cmd.mTarget = mTarget;
			cmd.mTargetAssignID = cmd.mTarget?.getAssignID() ?? 0;
			cmd.mFireID = mFireID;
			pushDelayCommand(cmd, mMonster, mFireTime[i], this);
		}
	}
	protected void initBulletData()
	{
		mBulletDataList.Clear();
		foreach (int bulletID in mSkillData.mBullet)
		{
			mBulletDataList.Add(mExcelSkillBullet.query(bulletID));
		}
		mFireTime.setRange(mSkillData.mFireTime);
	}
	protected void onMonsterHPChanged(EventMonsterHPChange param)
	{
		// 怪物死亡就不需要再触发技能了
		if (!canFire() || param.mMonster.getHP() <= 0)
		{
			return;
		}
		int hpThreashold0 = (int)(mSkillData.mHPPercent0 * param.mMonster.getMaxHP());
		int hpThreashold1 = (int)(mSkillData.mHPPercent1 * param.mMonster.getMaxHP());
		if (hpThreashold0 > 0 && param.mLastHP > hpThreashold0 && param.mCurHP <= hpThreashold0)
		{
			fire(0);
		}
		else if (hpThreashold1 > 0 && param.mLastHP > hpThreashold1 && param.mCurHP <= hpThreashold1)
		{
			fire(1);
		}
	}
	protected CharacterGame searchTarget()
	{
		if (mSkillData.mSearchTarget == MONSTER_SEARCH_TARGET.SELF)
		{
			return mMonster;
		}
		if (mSkillData.mSearchTarget == MONSTER_SEARCH_TARGET.RANGE_MIN_HP_PERCENT_MONSTER)
		{
			using var a = new ListScope<CharacterMonster>(out var monsterList);
			mTowerDefenceSystem.getMonstersInRange(mMonster.getPosition(), mSkillData.mParam0.SToF() * GRID_SIZE, monsterList);
			float minHPPercent = 10.0f;
			CharacterMonster target = null;
			foreach (CharacterMonster monster in monsterList)
			{
				float curPercent = monster.getHPPercent();
				if (curPercent < minHPPercent)
				{
					minHPPercent = curPercent;
					target = monster;
				}
			}
			return target;
		}
		if (mSkillData.mSearchTarget == MONSTER_SEARCH_TARGET.HIGHEST_ATTACK_TOWER)
		{
			return mTowerDefenceSystem.getHighestAttackTower();
		}
		return null;
	}
}