using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffRogueHitMonsterAddBuildCoinParam : CharacterBuffParamT<BuffRogueHitMonsterAddBuildCoinParam>
{
	public int mNeedCount;	// 需要攻击的次数
	public int mAddCoin;	// 增加的建造点数
	public override void registeAllParam()
	{
		registeParam((param) => { mNeedCount = param.SToI(); });
		registeParam((param) => { mAddCoin = param.SToI(); });
	}
	protected override void copyInternal(BuffRogueHitMonsterAddBuildCoinParam other)
	{
		mNeedCount = other.mNeedCount;
		mAddCoin = other.mAddCoin;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mAddCoin = 0;
	}
}

// 肉鸽模式，塔每击中n个敌人，获得m肉鸽建造点
public class BuffRogueHitMonsterAddBuildCoin : CharacterBuffT<BuffRogueHitMonsterAddBuildCoinParam>
{
	protected int mNeedCount;   // 需要攻击的次数
	protected int mAddCoin;		// 增加的建造点数
	protected int mCurCount;	// 该塔当前击杀的个数
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventHitCharacter>(mCharacterGame.getGUID(), onHitMonster, this);
		mNeedCount = mCustomParam.mNeedCount;
		mAddCoin = mCustomParam.mAddCoin;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mAddCoin = 0;
		mCurCount = 0;
	}
	protected void onHitMonster(EventHitCharacter eventParam)
	{
		if (++mCurCount >= mNeedCount)
		{
			mCurCount = 0;
			if (mTowerDefenceSystem.getBattleMode() == BATTLE_MODE.ROGUE_LIKE)
			{
				CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() + mAddCoin);
			}
		}
	}
}