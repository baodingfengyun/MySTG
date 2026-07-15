using static FrameBaseHotFix;

// 参数
public class BuffImmunityPhysicDamageParam : CharacterBuffParamT<BuffImmunityPhysicDamageParam>
{
	public override void registeAllParam(){}
	public override void check() { }
}

// 免疫所有元素伤害
public class BuffImmunityPhysicDamage : CharacterBuffT<BuffImmunityPhysicDamageParam>
{
	public BuffImmunityPhysicDamage()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override void enter()
	{
		base.enter();
		++mCharacterGame.getGameData().mImmunityPhysicDamage;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		--mCharacterGame.getGameData().mImmunityPhysicDamage;
	}
}