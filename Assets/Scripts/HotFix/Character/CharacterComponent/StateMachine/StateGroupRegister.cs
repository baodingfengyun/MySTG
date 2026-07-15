using System;
using static FrameBaseHotFix;
using static GBR;

public class StateGroupRegister
{
	protected static int mRegisteDebuff= 1;
	public static void registerAll()
	{
		// 状态组
		Type groupAction = typeof(StateGroupAction);
		Type groupNotAllowMove = typeof(StateGroupNotAllowMove);
		Type groupNotAllowAttack = typeof(StateGroupNotAllowAttack);
		Type groupBurnSlowDownFreeze = typeof(StateGroupBurnSlowDownFreeze);
		Type groupPoisonShocked = typeof(StateGroupPoisonShocked);

		registeGroup(groupAction, GROUP_MUTEX.REMOVE_OTHERS);
		registeGroup(groupNotAllowMove, GROUP_MUTEX.COEXIST);
		registeGroup(groupNotAllowAttack, GROUP_MUTEX.COEXIST);
		registeGroup(groupBurnSlowDownFreeze, GROUP_MUTEX.MUTEX_INVERSE_MAIN);
		registeGroup(groupPoisonShocked, GROUP_MUTEX.MUTEX_WITH_MAIN);

		// 行为动作状态
		assignGroup<ActionWalk>(groupAction);
		assignGroup<ActionStand>(groupAction);
		assignGroup<ActionSkillStart>(groupAction);
		assignGroup<ActionSkillContinuous>(groupAction);
		assignGroup<ActionSkillEnd>(groupAction);
		assignGroup<ActionDead>(groupAction);
		assignGroup<ActionVertigo>(groupAction);

		// 不允许移动的状态
		assignGroup<ActionDead>(groupNotAllowMove);
		assignGroup<BuffFloatToAir>(groupNotAllowMove);
		assignGroup<BuffHoldPosition>(groupNotAllowMove);
		assignGroup<BuffStrickBack>(groupNotAllowMove);
		assignGroup<BuffVertigo>(groupNotAllowMove);
		assignGroup<BuffFreeze>(groupNotAllowMove);
		assignGroup<BuffParalysis>(groupNotAllowMove);
		assignGroup<ActionSkillStart>(groupNotAllowMove);
		assignGroup<ActionSkillContinuous>(groupNotAllowMove);
		assignGroup<ActionSkillEnd>(groupNotAllowMove);

		// 不允许释放技能的状态
		assignGroup<ActionSkillStart>(groupNotAllowAttack);
		assignGroup<ActionSkillContinuous>(groupNotAllowAttack);
		assignGroup<ActionSkillEnd>(groupNotAllowAttack);
		assignGroup<ActionDead>(groupNotAllowAttack);
		assignGroup<BuffBuilding>(groupNotAllowAttack);
		assignGroup<BuffDisableSkill>(groupNotAllowAttack);
		assignGroup<BuffDisarm>(groupNotAllowAttack);
		assignGroup<BuffFloatToAir>(groupNotAllowAttack);
		assignGroup<BuffVertigo>(groupNotAllowAttack);
		assignGroup<BuffFreeze>(groupNotAllowAttack);
		assignGroup<BuffParalysis>(groupNotAllowAttack);

		// 燃烧和冰霜减速,冰冻互斥
		assignGroup<BuffBurn>(groupBurnSlowDownFreeze, true);
		assignGroup<BuffMoveSpeedDown>(groupBurnSlowDownFreeze);
		assignGroup<BuffFreeze>(groupBurnSlowDownFreeze);

		// 中毒和感电互斥
		assignGroup<BuffShocked>(groupPoisonShocked, true);
		assignGroup<BuffPoison>(groupPoisonShocked);
	}
	// 单独注册debuff状态组,根据表格配置判断哪个是debuff
	public static void registeDebuff()
	{
		if (mRegisteDebuff-- <= 0)
		{
			return;
		}
		Type groupDebuff1 = typeof(StateGroupDebuff1);
		Type groupDebuff2 = typeof(StateGroupDebuff2);
		registeGroup(groupDebuff1, GROUP_MUTEX.MUTEX_WITH_MAIN_ONLY);
		assignGroup(groupDebuff1, typeof(BuffClearDebuff), true);

		registeGroup(groupDebuff2, GROUP_MUTEX.MUTEX_WITH_MAIN_ONLY);
		assignGroup(groupDebuff2, typeof(BuffClearDebuff), true);

		foreach (EDBuff item in mExcelBuff.queryAll())
		{
			if (item.mDebuffGroupID == 1)
			{
				assignGroup(groupDebuff1, mStateManager.getStateType(item.mID));
			}
			else if (item.mDebuffGroupID == 2)
			{
				assignGroup(groupDebuff2, mStateManager.getStateType(item.mID));
			}
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registeGroup(Type groupType, GROUP_MUTEX mutex = GROUP_MUTEX.COEXIST)
	{
		mStateManager.registeGroup(groupType, mutex);
	}
	protected static void assignGroup<T>(Type groupType, bool mainState = false) where T : CharacterState
	{
		mStateManager.assignGroup(groupType, typeof(T), mainState);
	}
	protected static void assignGroup(Type groupType, Type stateType, bool mainState = false)
	{
		mStateManager.assignGroup(groupType, stateType, mainState);
	}
}