using static FrameBaseHotFix;
using static FrameUtility;
using static GBR;

// 参数
public class BuffTypeTowerIncreaseSelfDamageParam : CharacterBuffParamT<BuffTypeTowerIncreaseSelfDamageParam>
{
	public TOWER_TYPE mTowerType;           // 塔的类型
	public float mIncreasePerTower;         // 每增加一个塔,伤害提升的百分比
	public float mMaxIncraese;              // 提升上限
	public override void registeAllParam()
	{
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
		registeParam((param) => { mIncreasePerTower = param.SToF(); });
		registeParam((param) => { mMaxIncraese = param.SToF(); });
	}
	protected override void copyInternal(BuffTypeTowerIncreaseSelfDamageParam other)
	{
		mTowerType = other.mTowerType;
		mIncreasePerTower = other.mIncreasePerTower;
		mMaxIncraese = other.mMaxIncraese;
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
		mMaxIncraese = 0.0f;
	}
}

// 场上指定类型的塔越多,自身伤害增加越多
public class BuffTypeTowerIncreaseSelfDamage : CharacterBuffT<BuffTypeTowerIncreaseSelfDamageParam>
{
	protected float mIncreasePerTower;      // 每增加一个塔,伤害提升的百分比
	protected float mIncreaseDamage;		// 当前总的伤害提升百分比
	protected bool mTowerChanged;           // 场上的塔是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	protected TOWER_TYPE mTowerType;        // 塔的类型
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
			mCharacterGame.getGameData().mDamageIncrease += increase - mIncreaseDamage;
			mIncreaseDamage = increase;
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mDamageIncrease -= mIncreaseDamage;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePerTower = 0.0f;
		mIncreaseDamage = 0.0f;
		mTowerChanged = false;
		mTowerType = TOWER_TYPE.NONE;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
}