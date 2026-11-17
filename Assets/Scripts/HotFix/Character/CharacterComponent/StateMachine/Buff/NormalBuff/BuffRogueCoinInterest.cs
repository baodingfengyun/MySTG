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

// 肉鸽回合结束时，每有 n 银币额外获得 m 银币，m 有上限（Rogue 经济族代表）。
// 同族：BuffRogueKillMonsterAddBuildCoin(击杀加币)、BuffRogueHitMonsterAddBuildCoin(命中加币)、
// BuffRogueMonsterBreakAddCoin(破防加币)、BuffRogueKillMonsterCureLevelHp 等，都是监听某事件改局内资源。
// 本类监听 EventWaveWillFinish，在波次结算前按"当前银币/每n币"取商、封顶 mMaxCoin 后发钱；
// 用 getBattleMode()==ROGUE_LIKE 守卫，确保只在肉鸽模式生效。
// 与数值族不同：它不改 GameData 字段，而是直接发 CmdGlobalSetGoldCoinRogue 命令改局内金币。
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