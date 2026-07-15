using static FrameBaseHotFix;

// 参数
public class TriggerCriticalParam : CharacterTriggerParamT<TriggerCriticalParam>
{}

// 暴击时触发
public class TriggerCritical : CharacterTriggerT<TriggerCriticalParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventHitCharacter>(mCharacterGame.getGUID(), onHit, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onHit(EventHitCharacter param)
	{
		if (param.mAttacker != mCharacterGame || param.mTarget == null || !param.mCritical)
		{
			return;
		}
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(param.mTarget) || !triggerProbability(param.mTarget))
		{
			return;
		}
		onTrigger();
		addBuff(param.mTarget, null, param.mBullet, param.mBullet.getSkill());
	}
}