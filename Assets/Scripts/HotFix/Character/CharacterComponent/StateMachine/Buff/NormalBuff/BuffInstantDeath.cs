
// 参数
public class BuffInstantDeathParam : CharacterBuffParamT<BuffInstantDeathParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 即死
public class BuffInstantDeath : CharacterBuffT<BuffInstantDeathParam>
{
	public override void enter()
	{
		base.enter();
		CmdMonsterSetHP.execute(mCharacterGame as CharacterMonster, null, 0, mCharacterGame.getHP());
	}
}