using System;
using System.Collections.Generic;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class TriggerHPUnderPercentMultiParam : CharacterTriggerParamT<TriggerHPUnderPercentMultiParam>
{
	public List<float> mHPPercents = new();     // 血量百分比列表
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((string stringParam) => { stringParam.SToFs(mHPPercents); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mHPPercents.Clear();
	}
	protected override void copyInternal(TriggerHPUnderPercentMultiParam other)
	{
		base.copyInternal(other);
		mHPPercents.AddRange(other.mHPPercents);
	}
}

// 血量低于一定百分比时触发,可设置多个百分比
public class TriggerHPUnderPercentMulti : CharacterTriggerT<TriggerHPUnderPercentMultiParam>
{
	protected List<float> mHPPercents = new();		// 血量百分比列表
	public override void enter()
	{
		base.enter();
		mHPPercents.AddRange(mCustomParam.mHPPercents);
		mEventSystem.listenEvent<EventMonsterHPChange>(mCharacter.getGUID(), onMonsterHPChange, this);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mHPPercents.Clear();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onMonsterHPChange(EventMonsterHPChange param)
	{
		if (param.mMonster == null || mBuffDetailIDList.Count != mHPPercents.Count)
		{
			return;
		}
		// 选择目标
		Character buffCharacter = mBuffToTarget ? param.mMonster : mCharacter;
		// 检查怪物身上是否有其中一个buff
		foreach (int id in mBuffDetailIDList)
		{
			Type type = mStateManager.getStateType(mExcelBuffDetail.query(id).mBuffTypeID);
			if (buffCharacter.getStateMachine().hasState(type))
			{
				return;
			}
		}
		// 检查血量百分比
		int countPercents = mHPPercents.Count;
		int triggerIndex = -1;
		for (int i = 0; i < countPercents; ++i)
		{
			int hpThreashold = (int)(mHPPercents[i] * param.mMonster.getMaxHP());
			if (param.mLastHP > hpThreashold && param.mCurHP <= hpThreashold)
			{
				triggerIndex = i;
				break;
			}
		}
		// 没有满足任何一个buff触发条件
		if (triggerIndex < 0)
		{
			return;
		}
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(param.mMonster) || !triggerProbability(param.mMonster))
		{
			return;
		}
		onTrigger();
		doAddBuff(mBuffDetailIDList[triggerIndex], param.mMonster);
	}
}