using static FrameBaseHotFix;

// 参数
public class TriggerFireSkillParam : CharacterTriggerParamT<TriggerFireSkillParam>
{}

// 释放后触发
public class TriggerFireSkill : CharacterTriggerT<TriggerFireSkillParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventPostFireSkill>(mCharacter.getGUID(), onFireSkill, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onFireSkill(EventPostFireSkill param)
	{
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(mCharacterGame) || !triggerProbability(mCharacterGame))
		{
			return;
		}
		onTrigger();
		addBuff(mCharacterGame, null, null, param.mSkill);
	}
}