
// 参数
public class BuffParalysisParam : CharacterBuffParamT<BuffParalysisParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 麻痹,不允许释放技能,不允许移动,有麻痹CD
public class BuffParalysis : CharacterBuffT<BuffParalysisParam>
{
	protected const float PARALYSIS_CD = 3.0f;
	public BuffParalysis()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override bool canEnter()
	{
		return base.canEnter() && mCharacterGame.getGameData().mParalysisCD <= 0.0f;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		// 麻痹结束以后才开始计算CD
		mCharacterGame.getGameData().mParalysisCD = PARALYSIS_CD;
	}
}