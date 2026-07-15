using static FrameBaseHotFix;

// 参数
public class TriggerHPUnderPercentParam : CharacterTriggerParamT<TriggerHPUnderPercentParam>
{
	public float mHPPercent;        // 血量百分比
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mHPPercent = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mHPPercent = 0.0f;
	}
	protected override void copyInternal(TriggerHPUnderPercentParam other)
	{
		base.copyInternal(other);
		mHPPercent = other.mHPPercent;
	}
}

// 血量低于一定百分比时触发
public class TriggerHPUnderPercent : CharacterTriggerT<TriggerHPUnderPercentParam>
{
	protected float mHPPercent;     // 血量百分比
	public override void enter()
	{
		base.enter();
		mHPPercent = mCustomParam.mHPPercent;
		mEventSystem.listenEvent<EventMonsterHPChange>(mCharacter.getGUID(), onMonsterHPChange, this);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mHPPercent = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onMonsterHPChange(EventMonsterHPChange param)
	{
		if (param.mMonster == null)
		{
			return;
		}
		// 检查血量百分比
		int hpThreashold = (int)(mHPPercent * param.mMonster.getMaxHP());
		if (param.mLastHP < hpThreashold || param.mCurHP >= hpThreashold)
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