using static FrameBaseHotFix;

// 参数
public class TriggerKillMonsterParam : CharacterTriggerParamT<TriggerKillMonsterParam>
{}

// 击杀怪物时触发
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