
// 对拥有任意debuff的角色伤害增加
public class ModifierAnyDebuffIncreaseDamage : BulletDamageModifier
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
		if (character.hasStateGroup<StateGroupDebuff2>())
		{
			damage = (int)(damage * (1.0f + mPercent));
		}
	}
}