using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffRogueMonsterBreakAddCoinParam : CharacterBuffParamT<BuffRogueMonsterBreakAddCoinParam>
{
	public int mMaxCount;	// 前几个怪物
	public int mAddCoin;	// 每个转化的银币
	public override void registeAllParam()
	{
		registeParam((param) => { mMaxCount = param.SToI(); });
		registeParam((param) => { mAddCoin = param.SToI(); });
	}
	protected override void copyInternal(BuffRogueMonsterBreakAddCoinParam other)
	{
		mMaxCount = other.mMaxCount;
		mAddCoin = other.mAddCoin;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mMaxCount = 0;
		mAddCoin = 0;
	}
}

// 肉鸽模式前n个进入基地的怪物转化为m银币，boss无效
public class BuffRogueMonsterBreakAddCoin : CharacterBuffT<BuffRogueMonsterBreakAddCoinParam>
{
	protected int mMaxCount;	// 前几个怪物
	protected int mAddCoin;		// 每个转化的银币
	protected int mCurCount;	// 已经转化了的怪物
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventMonsterBreak>(onMonsterBreak, this);
		mMaxCount = mCustomParam.mMaxCount;
		mAddCoin = mCustomParam.mAddCoin;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMaxCount = 0;
		mAddCoin = 0;
		mCurCount = 0;
	}
	protected void onMonsterBreak(EventMonsterBreak eventParam)
	{
		EDMonster data = eventParam.mMonster?.getMonsterData()?.mTableData;
		if(data != null && data.mStrength != MONSTER_STRENGTH.BOSS && ++mCurCount <= mMaxCount)
		{
			if (mTowerDefenceSystem.getBattleMode() == BATTLE_MODE.ROGUE_LIKE)
			{
				CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() + mAddCoin);
			}
		}
	}
}