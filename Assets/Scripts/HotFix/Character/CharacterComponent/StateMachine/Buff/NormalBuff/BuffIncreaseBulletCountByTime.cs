using static FrameUtility;
using static FrameBaseHotFix;

// 参数
public class BuffIncreaseBulletCountByNoDamageTimeParam : CharacterBuffParamT<BuffIncreaseBulletCountByNoDamageTimeParam>
{
	public float mTimeMax;			// 等待时间
	public int mIncreaseCount;		// 增加个数
	public override void registeAllParam()
	{
		registeParam((param) => { mTimeMax = param.SToF(); });
		registeParam((param) => { mIncreaseCount = param.SToI(); });
	}
	protected override void copyInternal(BuffIncreaseBulletCountByNoDamageTimeParam other)
	{
		mTimeMax = other.mTimeMax;
		mIncreaseCount = other.mIncreaseCount;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mTimeMax = 0.0f;
		mIncreaseCount = 0;
	}
}

// 无伤害时等待一定时间，增加子弹数量
public class BuffIncreaseBulletCountByNoDamageTime : CharacterBuffT<BuffIncreaseBulletCountByNoDamageTimeParam>
{
	protected float mTimeMax;			// 等待时间
	protected int mIncreaseCount;		// 增加个数
	protected bool mIncreased;			// 已经增幅
	protected float mTimer;
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventWaveChange>(onWaveChanged, this);
		mEventSystem.listenEvent<EventHitCharacter>(mCharacter.getGUID(), onHitCharacter, this);
		mEventSystem.listenEvent<EventPostFireSkill>(mCharacter.getGUID(), onPostFireSkill, this);
		mTimeMax = mCustomParam.mTimeMax;
		mIncreaseCount = mCustomParam.mIncreaseCount;
		mTimer = mTimeMax;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mIncreased && tickTimerLoop(ref mTimer, elapsedTime, mTimeMax))
		{
			mIncreased = true;
			TowerSkill curSkill = (mCharacterGame as CharacterTower).getComSkill().getCurSkill();
			curSkill.setBulletIncreaseCount(curSkill.getBulletIncreaseCount() + mIncreaseCount);
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		resetCount();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTimeMax = 0.0f;
		mIncreaseCount = 0;
		mIncreased = false;
		mTimer = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void resetCount()
	{
		if (mIncreased)
		{
			mIncreased = false;
			TowerSkill curSkill = (mCharacterGame as CharacterTower).getComSkill().getCurSkill();
			curSkill.setBulletIncreaseCount(curSkill.getBulletIncreaseCount() - mIncreaseCount);
		}
	}
	protected void onHitCharacter(EventHitCharacter param)
	{
		mTimer = mTimeMax;
	}
	protected void onPostFireSkill(EventPostFireSkill param)
	{
		resetCount();
	}
	protected void onWaveChanged(EventWaveChange param)
	{
		resetCount();
	}
}