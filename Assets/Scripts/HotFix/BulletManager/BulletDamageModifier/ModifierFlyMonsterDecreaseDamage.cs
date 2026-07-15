using static MathUtility;

// 对空中敌人伤害减少
public class ModifierFlyMonsterDecreaseDamage : BulletDamageModifier
{
	protected float mPercent;
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
	public override void initData(EDBulletDamageModifier data)
	{
		mPercent = data.mParam0.SToF();
	}
	public override void modify(CharacterGame character, ref int damage)
	{
		if (character is not CharacterMonster monster)
		{
			return;
		}
		if (monster.getMonsterData().mFlyable)
		{
			damage = clampMin((int)(damage * (1.0f - mPercent)));
		}
	}
}