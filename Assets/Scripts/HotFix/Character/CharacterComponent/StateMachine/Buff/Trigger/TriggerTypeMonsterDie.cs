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

// 【代表样例：带自定义参数的触发】TriggerTypeMonsterDie——"指定 ID 的怪死亡"时触发
// 【自定义参数】Param 比通用多一个 mMonsterID，registeAllParam/copyInternal/check 都先调 base
//   再处理自己的字段(check 里校验怪物表引用)——这是所有带参触发器的标准写法。
// 【全局监听】listenEvent<EventMonsterDie> 不传 GUID，监听所有怪的死亡(对比 TriggerHit 按自己 GUID 监听)；
//   回调里再按 mTableData.mID == mMonsterID 过滤出目标怪。
// 【注意：自爆技能用的就是这类触发器】配合 MonsterPassiveSkill_SelfExplosion。
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