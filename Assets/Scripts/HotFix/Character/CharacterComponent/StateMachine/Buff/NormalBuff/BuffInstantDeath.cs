
// 参数
public class BuffInstantDeathParam : CharacterBuffParamT<BuffInstantDeathParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 【代表样例：极简 Buff】即死——enter 里直接把 HP 打成 0，走 CmdMonsterSetHP
// 【注意】无参数(Param 的 registeAllParam/check 都是空的)，说明这是"触发即生效"的一次性 Buff；
//   伤害量传的是当前满血(getHP())，delta 缺省。它绕过了护甲/闪避，直接改血。
public class BuffInstantDeath : CharacterBuffT<BuffInstantDeathParam>
{
	public override void enter()
	{
		base.enter();
		CmdMonsterSetHP.execute(mCharacterGame as CharacterMonster, null, 0, mCharacterGame.getHP());
	}
}