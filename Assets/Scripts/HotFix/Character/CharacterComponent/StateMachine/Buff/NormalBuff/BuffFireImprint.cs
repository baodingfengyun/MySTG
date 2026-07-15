
// 参数
public class BuffFireImprintParam : CharacterBuffParamT<BuffFireImprintParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 火焰印记,只是一个标记
public class BuffFireImprint : CharacterBuffT<BuffFireImprintParam>
{
	public BuffFireImprint()
	{
		mMutexType = STATE_MUTEX.OVERLAP_LAYER;
	}
	public override void enter()
	{
		base.enter();
		mCharacterGame.getGameData().mFireImprintCount = 1;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mFireImprintCount = 0;
	}
	public override void addSameState(CharacterState newState)
	{
		base.addSameState(newState);
		++mCharacterGame.getGameData().mFireImprintCount;
	}
}