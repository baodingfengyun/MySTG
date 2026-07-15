using static FrameBaseHotFix;
using static FrameUtility;

// 参数
public class BuffAttackSpeedUpSteppedParam : CharacterBuffParamT<BuffAttackSpeedUpSteppedParam>
{
	public float mDecrease;		// 初始降低的攻速
	public float mIncrease;		// 每次增加的攻速
	public int mLayerMax;		// 增加的次数上限
	public float mTimeMax;		// 连击等待时间
	public override void registeAllParam()
	{
		registeParam((param) => { mDecrease = param.SToF(); });
		registeParam((param) => { mIncrease = param.SToF(); });
		registeParam((param) => { mLayerMax = param.SToI(); });
		registeParam((param) => { mTimeMax = param.SToF(); });
	}
	protected override void copyInternal(BuffAttackSpeedUpSteppedParam other)
	{
		mDecrease = other.mDecrease;
		mIncrease = other.mIncrease;
		mLayerMax = other.mLayerMax;
		mTimeMax = other.mTimeMax;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0.0f;
		mIncrease = 0.0f;
		mLayerMax = 0;
		mTimeMax = 0.0f;
	}
}

// 降低攻速
public class BuffAttackSpeedUpStepped : CharacterBuffT<BuffAttackSpeedUpSteppedParam>
{
	protected float mDecrease;		// 初始降低的攻速
	protected float mIncrease;		// 每次增加的攻速
	protected int mLayerMax;		// 增加的次数上限
	protected float mTimeMax;		// 连击等待时间
	protected float mCurIncrease;	// 当前攻速增幅
	protected float mTimer;			// 连击等待时间计时
	protected int mCurLayer;		// 当前增加次数
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0.0f;
		mIncrease = 0.0f;
		mLayerMax = 0;
		mTimeMax = 0.0f;
		mCurIncrease = 0.0f;
		mTimer = 0.0f;
		mCurLayer = 0;
	}
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventWaveChange>(onWaveChanged, this);
		mEventSystem.listenEvent<EventPostFireSkill>(mCharacter.getGUID(), onFireSkill, this);
		mDecrease = mCustomParam.mDecrease;
		mIncrease = mCustomParam.mIncrease;
		mLayerMax = mCustomParam.mLayerMax;
		mTimeMax = mCustomParam.mTimeMax;
		mTimer = mTimeMax;
		resetIncrese();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if(tickTimerLoop(ref mTimer, elapsedTime, mTimeMax))
		{
			resetIncrese();
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().removeAttackSpeed(mCurIncrease);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void resetIncrese()
	{
		mCurLayer = 0;
		mCharacterGame.getGameData().removeAttackSpeed(mCurIncrease + mDecrease);
		mCurIncrease = -mDecrease;
	}
	protected void onFireSkill(EventPostFireSkill param)
	{
		mTimer = mTimeMax;
		if (mCurLayer < mLayerMax)
		{
			++mCurLayer;
			mCurIncrease += mIncrease;
			mCharacterGame.getGameData().addAttackSpeed(mIncrease);
		}
	}
	protected void onWaveChanged(EventWaveChange param)
	{
		resetIncrese();
	}
}