using static FrameBaseHotFix;

// 参数
public class TriggerWillHitHpMaxPercentParam : CharacterTriggerParamT<TriggerWillHitHpMaxPercentParam>
{
	public float mPercent;         // 最大百分比
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mPercent = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
	protected override void copyInternal(TriggerWillHitHpMaxPercentParam other)
	{
		base.copyInternal(other);
		mPercent = other.mPercent;
	}
}

// 命中时，怪物血量小于百分比时触发
public class TriggerWillHitHpMaxPercent : CharacterTriggerT<TriggerWillHitHpMaxPercentParam>
{
	public float mPercent;         // 最大的百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mPercent = mCustomParam.mPercent;
		mEventSystem.listenEvent<EventWillHitCharacter>(mCharacterGame.getGUID(), onHit, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onHit(EventWillHitCharacter param)
	{
		if (param.mAttacker != mCharacterGame || param.mTarget == null)
		{
			return;
		}
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(param.mTarget) || !triggerProbability(param.mTarget))
		{
			return;
		}
		if (param.mTarget.getHPPercent() > mPercent)
		{
			return;
		}
		onTrigger();
		addBuff(param.mTarget, param.mDamage, param.mBullet, param.mBullet.getSkill());
	}
}