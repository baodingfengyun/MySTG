using System.Collections.Generic;
using static FrameUtility;
using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static GBR;
using static GDR;

// 怪物主动技能：血量跌到阈值时自动释放，可带前摇动画、特效、子弹和 Buff
// 【触发】initData 时监听 EventMonsterHPChange；onMonsterHPChanged 检测血量从阈值上方跌到下方
//   （param.mLastHP > 阈值 且 mCurHP <= 阈值）才 fire，用两个阈值分出 hpIndex=0/1 两套效果。
//   死亡(HP<=0)不再触发。canFire 还要过冷却(isCoolDown)。
// 【释放时序 fire()】扣蓝 → 设 CD →
//   ① 若配了动画：挂 ActionSkillStart，等动作"正常结束"(非打断)的回调里再索敌+加 Buff；
//   ② 没动画：立即索敌+加 Buff；
//   然后播放特效(异步加载后 delayCall 到 mFireEffectTime 才播) + 发子弹 + 后处理。
//   "Buff 在前摇结束后才生效"是关键——给玩家闪避/打断的反应窗口。
// 【索敌 searchTarget】三种：SELF(给自己) / 范围内血量百分比最低的怪 / 攻击最高的塔。
// 【buffToTarget】给目标挂两类：默认触发器(DefaultFireBuff，可改来源) + 按 hpIndex 选 FireBuff0/1。
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
	// 血量跨越阈值检测：只有"上一帧在阈值上方、这一帧在阈值下方"才触发一次，避免重复释放
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