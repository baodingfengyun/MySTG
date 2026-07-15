
// 参数
public class BuffAlwaysCriticalHitParam : CharacterBuffParamT<BuffAlwaysCriticalHitParam>
{
	public override void registeAllParam() {}
	public override void check(){}
}

// 攻击必定暴击
public class BuffAlwaysCriticalHit : CharacterBuffT<BuffAlwaysCriticalHitParam>
{
	public override void enter()
	{
		base.enter();
		++mCharacterGame.getGameData().mAlwaysCriticalHit;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		--mCharacterGame.getGameData().mAlwaysCriticalHit;
	}
}