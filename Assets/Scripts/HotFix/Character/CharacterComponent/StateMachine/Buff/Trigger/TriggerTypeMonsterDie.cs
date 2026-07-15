using static FrameBaseHotFix;
using static GBR;

// 参数
public class TriggerTypeMonsterDieParam : CharacterTriggerParamT<TriggerTypeMonsterDieParam>
{
	public int mMonsterID;                   // 指定的怪物ID
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mMonsterID = param.SToI(); });
	}
	public override void check()
	{
		base.check();
		checkDataRefByBuffDetail(mExcelMonster, mMonsterID);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMonsterID = 0;
	}
	protected override void copyInternal(TriggerTypeMonsterDieParam other)
	{
		base.copyInternal(other);
		mMonsterID = other.mMonsterID;
	}
}

// 指定怪物死亡时触发
public class TriggerTypeMonsterDie : CharacterTriggerT<TriggerTypeMonsterDieParam>
{
	protected int mMonsterID;
	public override void resetProperty()
	{
		base.resetProperty();
		mMonsterID = 0;
	}
	public override void enter()
	{
		base.enter();
		mMonsterID = mCustomParam.mMonsterID;
		mEventSystem.listenEvent<EventMonsterDie>(onMonsterDie, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onMonsterDie(EventMonsterDie param)
	{
		if (param.mMonster == null || param.mMonster.getMonsterData().mTableData.mID != mMonsterID)
		{
			return;
		}
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(param.mMonster) || !triggerProbability(param.mMonster))
		{
			return;
		}
		onTrigger();
		addBuff(param.mMonster);
	}
}