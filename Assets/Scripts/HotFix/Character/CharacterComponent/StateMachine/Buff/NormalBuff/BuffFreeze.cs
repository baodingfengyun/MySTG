
// 参数
public class BuffFreezeParam : CharacterBuffParamT<BuffFreezeParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 冰冻,不允许释放技能,不允许移动,有冰冻CD
public class BuffFreeze : CharacterBuffT<BuffFreezeParam>
{
	protected const float FREEZE_CD = 3.0f;	// 冰冻CD
	public BuffFreeze()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override bool canEnter()
	{
		return base.canEnter() && mCharacterGame.getGameData().mFreezeCD <= 0.0f;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		// 冰冻结束以后才开始计算CD
		mCharacterGame.getGameData().mFreezeCD = FREEZE_CD;
	}
}