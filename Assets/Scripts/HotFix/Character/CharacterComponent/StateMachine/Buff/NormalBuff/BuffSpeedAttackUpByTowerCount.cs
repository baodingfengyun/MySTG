using static FrameBaseHotFix;
using static MathUtility;
using static FrameUtility;
using static GBR;

// 参数
public class BuffSpeedAttackUpByTowerCountParam : CharacterBuffParamT<BuffSpeedAttackUpByTowerCountParam>
{
	public float mAttackSpeed;		// 每个塔提升的攻速百分比
	public float mAttackSpeedMax;	// 提升的攻速百分比上限
	public float mAttack;			// 每个塔提升的攻击力百分比
	public float mAttackMax;		// 提升的攻击力百分比上限
	public TOWER_TYPE mTowerType;	// 塔的类型,填0所有
	public override void registeAllParam()
	{
		registeParam((param) => { mAttackSpeed = param.SToF(); });
		registeParam((param) => { mAttackSpeedMax = param.SToF(); });
		registeParam((param) => { mAttack = param.SToF(); });
		registeParam((param) => { mAttackMax = param.SToF(); });
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
	}
	protected override void copyInternal(BuffSpeedAttackUpByTowerCountParam other)
	{
		mAttackSpeed = other.mAttackSpeed;
		mAttackSpeedMax = other.mAttackSpeedMax;
		mAttack = other.mAttack;
		mAttackMax = other.mAttackMax;
		mTowerType = other.mTowerType;
	}
	public override void check()
	{
		checkEnum(mTowerType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mAttackSpeed = 0.0f;
		mAttackSpeedMax = 0.0f;
		mAttack = 0.0f;
		mAttackMax = 0.0f;
		mTowerType = TOWER_TYPE.NONE;
	}
}

// 根据场上防御塔数量，提升自身攻速和攻击力
public class BuffSpeedAttackUpByTowerCount : CharacterBuffT<BuffSpeedAttackUpByTowerCountParam>
{
	protected float mCurAttackSpeed;	// 已经提升的攻速百分比
	protected float mCurAttack;			// 已经提升的攻击力百分比
	public float mAttackSpeed;			// 每个塔提升的攻速百分比
	public float mAttackSpeedMax;		// 提升的攻速百分比上限
	public float mAttack;				// 每个塔提升的攻击力百分比
	public float mAttackMax;			// 提升的攻击力百分比上限
	public TOWER_TYPE mTowerType;		// 塔的类型,填0所有
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventGridTowerChange>(onGridTowerChange, this);
		mAttackSpeed = mCustomParam.mAttackSpeed;
		mAttackSpeedMax = mCustomParam.mAttackSpeedMax;
		mAttack = mCustomParam.mAttack;
		mAttackMax = mCustomParam.mAttackMax;
		mTowerType = mCustomParam.mTowerType;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mAttackSpeedIncreasePercent -= mCurAttackSpeed;
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mCurAttack;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mCurAttackSpeed = 0.0f;
		mCurAttack = 0.0f;
		mAttackSpeed = 0.0f;
		mAttackSpeedMax = 0.0f;
		mAttack = 0.0f;
		mAttackMax = 0.0f;
		mTowerType = TOWER_TYPE.NONE;
	}
	protected void onGridTowerChange(EventGridTowerChange eventParam)
	{
		mCharacterGame.getGameData().mAttackSpeedIncreasePercent -= mCurAttackSpeed;
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mCurAttack;
		int count = 0;
		foreach (var each in mTowerDefenceSystem.getTowerList().safe())
		{
			if (mTowerType == TOWER_TYPE.NONE || each.getTowerData().mTableData.mType == mTowerType)
			{
				count++;
			}
		}
		mCurAttackSpeed = getMin(mAttackSpeedMax, count * mAttackSpeed);
		mCurAttack = getMin(mAttackMax, count * mAttack);
		mCharacterGame.getGameData().mAttackSpeedIncreasePercent += mCurAttackSpeed;
		mCharacterGame.getGameData().mIncreaseAttackPercent += mCurAttack;
	}
}