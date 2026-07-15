
// 参数
public class BuffInTunnelParam : CharacterBuffParamT<BuffInTunnelParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 怪物进入隧道后无法被攻击的状态
public class BuffInTunnel : CharacterBuffT<BuffInTunnelParam>
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