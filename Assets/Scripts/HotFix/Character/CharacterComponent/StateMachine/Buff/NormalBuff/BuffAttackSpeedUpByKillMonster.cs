using static FrameBaseHotFix;
using static MathUtility;

// 参数
public class BuffAttackSpeedUpByKillMonsterParam : CharacterBuffParamT<BuffAttackSpeedUpByKillMonsterParam>
{
	public int mNeedCount;		// 每击杀多少敌人
	public float mAddSpeed;		// 每次增加攻速
	public float mMaxSpeed;		// 最大增加攻速
	public override void registeAllParam()
	{
		registeParam((param) => { mNeedCount = param.SToI(); });
		registeParam((param) => { mAddSpeed = param.SToF(); });
		registeParam((param) => { mMaxSpeed = param.SToF(); });
	}
	protected override void copyInternal(BuffAttackSpeedUpByKillMonsterParam other)
	{
		mNeedCount = other.mNeedCount;
		mAddSpeed = other.mAddSpeed;
		mMaxSpeed = other.mMaxSpeed;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mAddSpeed = 0.0f;
		mMaxSpeed = 0.0f;
	}
}

// 回旋镖塔每击杀n个单位，攻速增加
public class BuffAttackSpeedUpByKillMonster : CharacterBuffT<BuffAttackSpeedUpByKillMonsterParam>
{
	protected int mNeedCount;	// 每击杀多少敌人
	protected int mCurCount;	// 该塔当前击杀的个数
	protected float mAddSpeed;	// 每次增加攻速
	protected float mMaxSpeed;	// 最大增加攻速
	protected float mCurAddSpeed;	// 当前已经增加的攻速
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventKillMonster>(mCharacterGame.getGUID(), onKillMonster, this);
		mNeedCount = mCustomParam.mNeedCount;
		mAddSpeed = mCustomParam.mAddSpeed;
		mMaxSpeed = mCustomParam.mMaxSpeed;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().removeAttackSpeed(mCurAddSpeed);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mCurCount = 0;
		mAddSpeed = 0.0f;
		mMaxSpeed = 0.0f;
		mCurAddSpeed = 0.0f;
	}
	protected void onKillMonster(EventKillMonster eventParam)
	{
		if (mCurAddSpeed < mMaxSpeed && ++mCurCount >= mNeedCount)
		{
			mCurCount = 0;
			float oldAdd = mCurAddSpeed;
			mCurAddSpeed = getMin(mMaxSpeed, mCurAddSpeed + mAddSpeed);
			mCharacterGame.getGameData().addAttackSpeed(mCurAddSpeed - oldAdd);
		}
	}
}