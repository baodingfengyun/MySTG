using static FrameBaseHotFix;

// 参数
public class TriggerHitParam : CharacterTriggerParamT<TriggerHitParam>
{}

// 【代表样例：命中触发】TriggerHit——"我命中了别人"时触发
// 【范式】所有触发器都长这样：enter 里 listenEvent 监听某事件 → 回调里
//   过 canTrigger(冷却/叠加/概率) → onTrigger 通知 → addBuff 生成配置的 Buff。
// 【本例】监听 EventHitCharacter，只处理"攻击者是自己"的事件(param.mAttacker == mCharacterGame)，
//   把命中伤害通过 tempDamage 传给新 Buff（供"命中造成 X 伤害"类 Buff 读取 mDamage），
//   并传 mBullet/技能——于是触发的 Buff 能追溯这次命中的全部上下文。
// 【给谁加】addBuff(param.mTarget) 默认给被命中者，除非触发器配了 mBuffToTarget=false 给自己。
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