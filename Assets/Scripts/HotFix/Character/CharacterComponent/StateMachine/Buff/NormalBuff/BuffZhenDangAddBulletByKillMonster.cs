using static FrameBaseHotFix;

// 参数
public class BuffZhenDangAddBulletByKillMonsterParam : CharacterBuffParamT<BuffZhenDangAddBulletByKillMonsterParam>
{
	public int mNeedCount;	// 需要击杀的数量
	public int mAddCount;   // 获得子弹的数量
	public int mMaxCount;	// 子弹的数量上限
	public override void registeAllParam()
	{
		registeParam((param) => { mNeedCount = param.SToI(); });
		registeParam((param) => { mAddCount = param.SToI(); });
		registeParam((param) => { mMaxCount = param.SToI(); });
	}
	protected override void copyInternal(BuffZhenDangAddBulletByKillMonsterParam other)
	{
		mNeedCount = other.mNeedCount;
		mAddCount = other.mAddCount;
		mMaxCount = other.mMaxCount;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mAddCount = 0;
		mMaxCount = 0;
	}
}

// 震荡塔击杀敌方单位后，获得能量球
public class BuffZhenDangAddBulletByKillMonster : CharacterBuffT<BuffZhenDangAddBulletByKillMonsterParam>
{
	protected int mNeedCount;	// 需要击杀的数量
	protected int mCurCount;	// 该塔当前击杀的个数
	protected int mAddCount;    // 获得子弹的数量
	protected int mMaxCount;	// 子弹的数量上限
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventKillMonster>(mCharacterGame.getGUID(), onKillMonster, this);
		mNeedCount = mCustomParam.mNeedCount;
		mAddCount = mCustomParam.mAddCount;
		mMaxCount = mCustomParam.mMaxCount;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mCurCount = 0;
		mAddCount = 0;
		mMaxCount = 0;
	}
	protected void onKillMonster(EventKillMonster eventParam)
	{
		if (++mCurCount >= mNeedCount)
		{
			mCurCount = 0;
			if ((mCharacterGame as CharacterTower).getComSkill().getCurSkill() is TowerSkill_ZhenDang zhendang && 
				zhendang.getCurBulletCount() < mMaxCount)
			{
				zhendang.createBullet();
			}
		}
	}
}