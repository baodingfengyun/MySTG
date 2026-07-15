
// 参数
public class BuffSneakParam : CharacterBuffParamT<BuffSneakParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 潜行,无法被防御塔搜索到
public class BuffSneak : CharacterBuffT<BuffSneakParam>
{
	public override void enter()
	{
		base.enter();
		(mCharacter as CharacterMonster).getMonsterData().mIsInvisible += 1;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		(mCharacter as CharacterMonster).getMonsterData().mIsInvisible -= 1;
	}
}