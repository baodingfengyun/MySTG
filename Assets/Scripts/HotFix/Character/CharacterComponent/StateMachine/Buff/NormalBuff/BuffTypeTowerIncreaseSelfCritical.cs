using static FrameBaseHotFix;
using static FrameUtility;
using static GBR;

// 参数
public class BuffTypeTowerIncreaseSelfCriticalParam : CharacterBuffParamT<BuffTypeTowerIncreaseSelfCriticalParam>
{
	public TOWER_TYPE mTowerType;		// 塔的类型
	public float mIncreasePerTower;     // 每增加一个塔,提升的暴击率
	public override void registeAllParam()
	{
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
		registeParam((param) => { mIncreasePerTower = param.SToF(); });
	}
	protected override void copyInternal(BuffTypeTowerIncreaseSelfCriticalParam other)
	{
		mTowerType = other.mTowerType;
		mIncreasePerTower = other.mIncreasePerTower;
	}
	public override void check()
	{
		checkEnum(mTowerType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerType = TOWER_TYPE.NONE;
		mIncreasePerTower = 0.0f;
	}
}

// 场上指定类型的塔越多,自身增加的暴击率越多
public class BuffTypeTowerIncreaseSelfCritical : CharacterBuffT<BuffTypeTowerIncreaseSelfCriticalParam>
{
	protected float mIncreasePerTower;      // 每增加一个塔,提升的暴击率
	protected float mLastIncrease;			// 上一次提升的百分比
	protected TOWER_TYPE mTowerType;        // 塔的类型
	protected bool mTowerChanged;			// 场上的塔是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	public override void enter()
	{
		base.enter();
		mTowerType = mCustomParam.mTowerType;
		mIncreasePerTower = mCustomParam.mIncreasePerTower;
		mEventSystem.listenEvent<EventGridTowerChange>(onTowerChanged, this);
		mTowerChanged = true;
	}
	public override void update(float elapsedTime)
	{
		if (mTowerChanged)
		{
			mTowerChanged = false;
			float increase = mTowerDefenceSystem.getTypeTowerCount(mTowerType) * mIncreasePerTower;
			mCharacterGame.getGameData().mCriticalIncrease += increase - mLastIncrease;
			mLastIncrease = increase;
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mCriticalIncrease -= mLastIncrease;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePerTower = 0.0f;
		mLastIncrease = 0.0f;
		mTowerType = TOWER_TYPE.NONE;
		mTowerChanged = false;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
}