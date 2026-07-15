using static FrameBaseHotFix;

// 参数
public class TriggerBuffToBulletExploRangeParam : CharacterTriggerParamT<TriggerBuffToBulletExploRangeParam>
{}

// 子弹爆炸时给范围内所有敌人附加buff
public class TriggerBuffToBulletExploRange : CharacterTriggerT<TriggerBuffToBulletExploRangeParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventBulletExplosion>(mCharacter.getGUID(), onBulletExplosion, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBulletExplosion(EventBulletExplosion param)
	{
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(mCharacterGame) || !triggerProbability(mCharacterGame))
		{
			return;
		}

		onTrigger();

		// 对一定范围内的敌人附加buff
		using var a = new ListScope<CharacterMonster>(out var monsterList);
		param.mBullet.getRangeEffectiveMonster(param.mBullet.getRealtimeRange(), monsterList);
		foreach (CharacterMonster monster in monsterList)
		{
			addBuff(monster, null, param.mBullet);
		}
	}
}