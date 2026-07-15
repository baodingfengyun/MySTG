
// 参数
public class BuffImmunityElementDebuffDamageParam : CharacterBuffParamT<BuffImmunityElementDebuffDamageParam>
{
	public override void registeAllParam(){}
	public override void check() { }
}

// 免疫所有元素伤害
public class BuffImmunityElementDebuffDamage : CharacterBuffT<BuffImmunityElementDebuffDamageParam>
{
	public BuffImmunityElementDebuffDamage()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override void enter()
	{
		base.enter();
		++mCharacterGame.getGameData().mImmunityElementDebuffDamage;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		--mCharacterGame.getGameData().mImmunityElementDebuffDamage;
	}
}