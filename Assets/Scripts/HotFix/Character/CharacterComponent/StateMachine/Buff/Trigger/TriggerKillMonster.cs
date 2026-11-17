using static FrameBaseHotFix;

// 参数
public class TriggerKillMonsterParam : CharacterTriggerParamT<TriggerKillMonsterParam>
{}

// 【代表样例：击杀触发】TriggerKillMonster——"我击杀了怪"时给自己加 Buff
// 【与 TriggerHit 对比】监听 EventKillMonster；canTrigger 传 mCharacterGame(自己)，
//   addBuff 也给自己——这是 Rogue 里"击杀 X 只怪 → 攻击力+X / 回血 / 加钱"类词条的实现底座。
//   164 个 Buff 里大量 Rogue 词条都是这个模式的变体。
public class TriggerKillMonster : CharacterTriggerT<TriggerKillMonsterParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventKillMonster>(mCharacterGame.getGUID(), onKillMonster, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onKillMonster(EventKillMonster param)
	{
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(mCharacterGame) || !triggerProbability(mCharacterGame))
		{
			return;
		}
		onTrigger();
		addBuff(mCharacterGame);
	}
}