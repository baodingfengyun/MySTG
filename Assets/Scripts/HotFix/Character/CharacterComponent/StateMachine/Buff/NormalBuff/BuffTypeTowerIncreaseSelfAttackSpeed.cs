using static FrameBaseHotFix;
using static FrameUtility;
using static GBR;

// 参数
public class BuffTypeTowerIncreaseSelfAttackSpeedParam : CharacterBuffParamT<BuffTypeTowerIncreaseSelfAttackSpeedParam>
{
	public TOWER_TYPE mTowerType;           // 塔的类型
	public float mIncreasePerTower;         // 每增加一个塔,攻速提升的百分比
	public float mMaxIncraese;				// 提升上限
	public override void registeAllParam()
	{
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
		registeParam((param) => { mIncreasePerTower = param.SToF(); });
		registeParam((param) => { mMaxIncraese = param.SToF(); });
	}
	protected override void copyInternal(BuffTypeTowerIncreaseSelfAttackSpeedParam other)
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

// 场上指定类型的塔越多,攻速增加越多
public class BuffTypeTowerIncreaseSelfAttackSpeed : CharacterBuffT<BuffTypeTowerIncreaseSelfAttackSpeedParam>
{
	protected float mIncreasePerTower;      // 每增加一个塔,攻速提升的百分比
	protected float mIncreaseAttackSpeed;	// 当前总的攻速提升百分比
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
			mCharacterGame.getGameData().addAttackSpeed(increase - mIncreaseAttackSpeed);
			mIncreaseAttackSpeed = increase;
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().removeAttackSpeed(mIncreaseAttackSpeed);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePerTower = 0.0f;
		mIncreaseAttackSpeed = 0.0f;
		mTowerChanged = false;
		mTowerType = TOWER_TYPE.NONE;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
}