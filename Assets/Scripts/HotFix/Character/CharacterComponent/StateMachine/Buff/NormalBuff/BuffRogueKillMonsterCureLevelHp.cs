using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 参数
public class BuffRogueKillMonsterCureLevelHpParam : CharacterBuffParamT<BuffRogueKillMonsterCureLevelHpParam>
{
	public int mNeedCount;	// 需要击杀的数量
	public int mCureHp;		// 回复的血量
	public override void registeAllParam()
	{
		registeParam((param) => { mNeedCount = param.SToI(); });
		registeParam((param) => { mCureHp = param.SToI(); });
	}
	protected override void copyInternal(BuffRogueKillMonsterCureLevelHpParam other)
	{
		mNeedCount = other.mNeedCount;
		mCureHp = other.mCureHp;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mCureHp = 0;
	}
}

// 肉鸽模式，每击杀n个敌人，回复m点已损失的羊村生命
public class BuffRogueKillMonsterCureLevelHp : CharacterBuffT<BuffRogueKillMonsterCureLevelHpParam>
{
	protected int mNeedCount;	// 需要击杀的数量
	protected int mCureHp;		// 回复的血量
	protected int mCurCount;	// 该塔当前击杀的个数
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventKillMonster>(mCharacterGame.getGUID(), onKillMonster, this);
		mNeedCount = mCustomParam.mNeedCount;
		mCureHp = mCustomParam.mCureHp;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mCureHp = 0;
		mCurCount = 0;
	}
	protected void onKillMonster(EventKillMonster eventParam)
	{
		if (mCurCount < mNeedCount)
		{
			++mCurCount;
		}
		if (mCurCount >= mNeedCount && mTowerDefenceSystem.getHp() < LEVEL_INIT_HP)
		{
			mCurCount = 0;
			CmdGlobalSetLevelHp.execute(mTowerDefenceSystem.getHp() + mCureHp);
		}
	}
}