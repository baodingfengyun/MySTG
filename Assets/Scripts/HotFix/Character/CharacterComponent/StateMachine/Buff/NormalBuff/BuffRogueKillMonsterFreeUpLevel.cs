using static FrameBaseHotFix;

// 参数
public class BuffRogueKillMonsterFreeUpLevelParam : CharacterBuffParamT<BuffRogueKillMonsterFreeUpLevelParam>
{
	public int mNeedCount;	// 需要击杀的数量
	public override void registeAllParam()
	{
		registeParam((param) => { mNeedCount = param.SToI(); });
	}
	protected override void copyInternal(BuffRogueKillMonsterFreeUpLevelParam other)
	{
		mNeedCount = other.mNeedCount;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
	}
}

// 肉鸽模式，击杀一定数量敌人后可免费升塔的等级
public class BuffRogueKillMonsterFreeUpLevel : CharacterBuffT<BuffRogueKillMonsterFreeUpLevelParam>
{
	protected int mNeedCount;	// 需要击杀的数量
	protected int mCurCount;	// 该塔当前击杀的个数
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventKillMonster>(mCharacterGame.getGUID(), onKillMonster, this);
		mNeedCount = mCustomParam.mNeedCount;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mCurCount = 0;
	}
	protected void onKillMonster(EventKillMonster eventParam)
	{
		if (mCurCount < mNeedCount)
		{
			++mCurCount;
		}
		var tower = mCharacterGame as CharacterTower;
		if (mCurCount >= mNeedCount && !tower.getTowerData().getFreeUpModeLevel())
		{
			mCurCount = 0;
			CmdGlobalFreeUpLevelRogue.execute(tower, true);
		}
	}
}