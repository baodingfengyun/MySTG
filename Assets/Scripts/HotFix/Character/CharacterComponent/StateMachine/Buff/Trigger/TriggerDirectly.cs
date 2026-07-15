
// 参数
public class TriggerDirectlyParam : CharacterTriggerParamT<TriggerDirectlyParam>
{}

// 直接触发
public class TriggerDirectly : CharacterTriggerT<TriggerDirectlyParam>
{
	public override void enter()
	{
		base.enter();
		// 检查冷却,叠加次数等前提条件
		if (!canTrigger(mCharacterGame))
		{
			return;
		}
		// 触发几率
		if (!triggerProbability(mCharacterGame))
		{
			return;
		}
		onTrigger();

		addBuff(mCharacterGame, mCustomParam.mDamage, mCustomParam.mBullet, mCustomParam.mSkill);
	}
}