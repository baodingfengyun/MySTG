using static FrameBaseHotFix;
using static MathUtility;
using static GBR;

// 参数
public class BuffRogueCoinInterestParam : CharacterBuffParamT<BuffRogueCoinInterestParam>
{
	public int mEachCoin;	// 每多少个银币
	public int mAddCoin;	// 每额外获得
	public int mMaxCoin;	// 额外上限
	public override void registeAllParam()
	{
		registeParam((param) => { mEachCoin = param.SToI(); });
		registeParam((param) => { mAddCoin = param.SToI(); });
		registeParam((param) => { mMaxCoin = param.SToI(); });
	}
	protected override void copyInternal(BuffRogueCoinInterestParam other)
	{
		mEachCoin = other.mEachCoin;
		mAddCoin = other.mAddCoin;
		mMaxCoin = other.mMaxCoin;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mEachCoin = 0;
		mAddCoin = 0;
		mMaxCoin = 0;
	}
}

// 肉鸽回合结束时，每有n银币，额外获得m银币，m有上限
public class BuffRogueCoinInterest : CharacterBuffT<BuffRogueCoinInterestParam>
{
	protected int mEachCoin;	// 每多少个银币
	protected int mAddCoin;		// 每额外获得
	protected int mMaxCoin;		// 额外上限
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventWaveWillFinish>(onWaveWillFinish, this);
		mEachCoin = mCustomParam.mEachCoin;
		mAddCoin = mCustomParam.mAddCoin;
		mMaxCoin = mCustomParam.mMaxCoin;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mEachCoin = 0;
		mAddCoin = 0;
		mMaxCoin = 0;
	}
	protected void onWaveWillFinish(EventWaveWillFinish eventParam)
	{
		if (mTowerDefenceSystem.getBattleMode() == BATTLE_MODE.ROGUE_LIKE)
		{
			int curCoin = mTowerDefenceSystem.getGoldCoinRogue();
			CmdGlobalSetGoldCoinRogue.execute(curCoin + getMin(curCoin.divideInt(mEachCoin), mMaxCoin));
		}
	}
}