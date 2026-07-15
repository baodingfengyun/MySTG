using static MathUtility;

// 对地面敌人伤害减少
public class ModifierWalkMonsterDecreaseDamage : BulletDamageModifier
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
		var monster = character as CharacterMonster;
		if (monster == null)
		{
			return;
		}
		if (!monster.getMonsterData().mFlyable)
		{
			damage = clampMin((int)(damage * (1.0f - mPercent)));
		}
	}
}