using static FrameBaseHotFix;

// 参数
public class TriggerWillHitParam : CharacterTriggerParamT<TriggerWillHitParam>
{}

// 即将命中时触发
public class TriggerWillHit : CharacterTriggerT<TriggerWillHitParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventWillHitCharacter>(mCharacter.getGUID(), onWillHit, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onWillHit(EventWillHitCharacter param)
	{
		if (param.mAttacker != mCharacter || param.mTarget == null)
		{
			return;
		}
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(param.mTarget) || !triggerProbability(param.mTarget))
		{
			return;
		}
		onTrigger();
		addBuff(param.mTarget, param.mDamage, param.mBullet, param.mBullet.getSkill());
	}
}