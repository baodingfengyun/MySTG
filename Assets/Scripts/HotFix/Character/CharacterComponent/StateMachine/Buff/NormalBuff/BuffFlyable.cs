
// 参数
public class BuffFlyableParam : CharacterBuffParamT<BuffFlyableParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 具有飞天能力,可以使用飞行路线
public class BuffFlyable : CharacterBuffT<BuffFlyableParam>
{
	public override void enter()
	{
		base.enter();
		(mCharacter as CharacterMonster).getMonsterData().mFlyable = true;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		(mCharacter as CharacterMonster).getMonsterData().mFlyable = false;
	}
}