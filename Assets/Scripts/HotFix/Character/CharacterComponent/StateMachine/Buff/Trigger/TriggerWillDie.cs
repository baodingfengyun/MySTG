using static FrameBaseHotFix;

// 参数
public class TriggerWillDieParam : CharacterTriggerParamT<TriggerWillDieParam>
{}

// 自身即将死亡时触发
public class TriggerWillDie : CharacterTriggerT<TriggerWillDieParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventMonsterWillDie>(mCharacter.getGUID(), onWillDie, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onWillDie(EventMonsterWillDie param)
	{
		if (param.mMonster == null)
		{
			return;
		}
		// 检查冷却,叠加次数等前提条件
		if (!canTrigger(param.mMonster))
		{
			return;
		}
		// 触发几率
		if (!triggerProbability(param.mMonster))
		{
			return;
		}
		onTrigger();
		
		addBuff(param.mMonster);
	}
}