using static FrameBaseHotFix;

// 参数
public class BuffAttackUpOnceByKillMonsterParam : CharacterBuffParamT<BuffAttackUpOnceByKillMonsterParam>
{
	public int mNeedCount;			// 需要击杀的数量
	public float mIncreasePercent;	// 增加的攻击力百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mNeedCount = param.SToI(); });
		registeParam((param) => { mIncreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffAttackUpOnceByKillMonsterParam other)
	{
		mNeedCount = other.mNeedCount;
		mIncreasePercent = other.mIncreasePercent;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mIncreasePercent = 0.0f;
	}
}

// 击杀n个敌人后，下一次攻击提高
public class BuffAttackUpOnceByKillMonster : CharacterBuffT<BuffAttackUpOnceByKillMonsterParam>
{
	protected int mNeedCount;			// 需要击杀的数量
	protected float mIncreasePercent;	// 增加的攻击力百分比
	protected int mCurCount;			// 该塔当前击杀的个数
	protected bool mActivtingIncrease;	// 是否已经增加
	protected long mFireID;				// 技能实例ID
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventKillMonster>(mCharacterGame.getGUID(), onKillMonster, this);
		mEventSystem.listenEvent<EventBulletWillFire>(mCharacterGame.getGUID(), onBulletWillFire, this);
		mEventSystem.listenEvent<EventPreFireSkill>(mCharacterGame.getGUID(), onPreFireSkill, this);
		mNeedCount = mCustomParam.mNeedCount;
		mIncreasePercent = mCustomParam.mIncreasePercent;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mActivtingIncrease = false;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mIncreasePercent = 0.0f;
		mCurCount = 0;
		mActivtingIncrease = false;
		mFireID = 0L;
	}
	protected void onKillMonster(EventKillMonster eventParam)
	{
		if (++mCurCount >= mNeedCount)
		{
			mCurCount = 0;
			mActivtingIncrease = true;
		}
	}
	protected void onBulletWillFire(EventBulletWillFire eventParam)
	{
		SkillBullet bullet = eventParam.mBullet;
		if (bullet.getFireID() == mFireID)
		{
			bullet.setAttackPercent(bullet.getAttackPercent() + mIncreasePercent);
		}
	}
	protected void onPreFireSkill(EventPreFireSkill eventParam)
	{
		if (mActivtingIncrease)
		{
			mActivtingIncrease = false;
			mFireID = eventParam.mSkill.getFireID();
		}
	}
}