using static FrameBaseHotFix;

// 参数
public class TriggerHitParam : CharacterTriggerParamT<TriggerHitParam>
{}

// 命中时触发
public class TriggerHit : CharacterTriggerT<TriggerHitParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventHitCharacter>(mCharacterGame.getGUID(), onHit, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onHit(EventHitCharacter param)
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
		onTrigger();
		using var a = new ClassScope<INT>(out var tempDamage);
		tempDamage.mValue = param.mDamage;
		addBuff(param.mTarget, tempDamage, param.mBullet, param.mBullet.getSkill());
	}
}