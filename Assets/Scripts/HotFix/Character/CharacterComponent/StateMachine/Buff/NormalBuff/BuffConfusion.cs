
// 参数
public class BuffConfusionParam : CharacterBuffParamT<BuffConfusionParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 混乱
public class BuffConfusion : CharacterBuffT<BuffConfusionParam>
{
	public override void enter()
	{
		base.enter();
		mCharacterGame.getComponent<COMMonsterMovement>()?.setConfusion(true);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getComponent<COMMonsterMovement>()?.setConfusion(false);
	}
}