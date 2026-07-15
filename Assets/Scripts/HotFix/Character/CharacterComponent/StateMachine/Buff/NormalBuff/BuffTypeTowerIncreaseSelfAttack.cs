using static FrameBaseHotFix;
using static FrameUtility;
using static GBR;

// 参数
public class BuffTypeTowerIncreaseSelfAttackParam : CharacterBuffParamT<BuffTypeTowerIncreaseSelfAttackParam>
{
	public TOWER_TYPE mTowerType;           // 塔的类型
	public float mIncreasePerTower;         // 每增加一个塔,攻击力提升的百分比
	public float mMaxIncraese;              // 提升上限
	public override void registeAllParam()
	{
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
		registeParam((param) => { mIncreasePerTower = param.SToF(); });
		registeParam((param) => { mMaxIncraese = param.SToF(); });
	}
	protected override void copyInternal(BuffTypeTowerIncreaseSelfAttackParam other)
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

// 场上指定类型的塔越多,自身攻击力增加越多
public class BuffTypeTowerIncreaseSelfAttack : CharacterBuffT<BuffTypeTowerIncreaseSelfAttackParam>
{
	protected float mIncreasePerTower;      // 每增加一个塔,攻击力提升的百分比
	protected float mIncreaseAttackPercent;	// 当前总的攻击力提升百分比
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
			float increasePercent = mTowerDefenceSystem.getTypeTowerCount(mTowerType) * mIncreasePerTower;
			mCharacterGame.getGameData().mIncreaseAttackPercent += increasePercent - mIncreaseAttackPercent;
			mIncreaseAttackPercent = increasePercent;
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mIncreaseAttackPercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePerTower = 0.0f;
		mIncreaseAttackPercent = 0.0f;
		mTowerChanged = false;
		mTowerType = TOWER_TYPE.NONE;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
}