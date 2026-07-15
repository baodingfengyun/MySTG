using static FrameBaseHotFix;
using static MathUtility;
using static GBR;

// 参数
public class BuffAttackCriticalUpByTowerCountParam : CharacterBuffParamT<BuffAttackCriticalUpByTowerCountParam>
{
	public float mAttack;			// 每个塔提升的攻击力百分比
	public float mAttackMax;		// 提升的攻击力百分比上限
	public float mCritical;			// 每个塔提升的暴击率百分比
	public float mCriticalMax;		// 提升的暴击率百分比上限
	public override void registeAllParam()
	{
		registeParam((param) => { mAttack = param.SToF(); });
		registeParam((param) => { mAttackMax = param.SToF(); });
		registeParam((param) => { mCritical = param.SToF(); });
		registeParam((param) => { mCriticalMax = param.SToF(); });
	}
	protected override void copyInternal(BuffAttackCriticalUpByTowerCountParam other)
	{
		mAttack = other.mAttack;
		mAttackMax = other.mAttackMax;
		mCritical = other.mCritical;
		mCriticalMax = other.mCriticalMax;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mAttack = 0.0f;
		mAttackMax = 0.0f;
		mCritical = 0.0f;
		mCriticalMax = 0.0f;
	}
}

// 根据场上防御塔数量，提升自身攻击力和暴击率
public class BuffAttackCriticalUpByTowerCount : CharacterBuffT<BuffAttackCriticalUpByTowerCountParam>
{
	protected float mCurAttack;		// 已经提升的攻击力百分比
	protected float mCurCritical;	// 已经提升的暴击率百分比
	public float mAttack;			// 每个塔提升的攻击力百分比
	public float mAttackMax;		// 提升的攻击力百分比上限
	public float mCritical;			// 每个塔提升的暴击率百分比
	public float mCriticalMax;		// 提升的暴击率百分比上限
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventGridTowerChange>(onGridTowerChange, this);
		mAttack = mCustomParam.mAttack;
		mAttackMax = mCustomParam.mAttackMax;
		mCritical = mCustomParam.mCritical;
		mCriticalMax = mCustomParam.mCriticalMax;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mCurAttack;
		mCharacterGame.getGameData().mCriticalIncrease -= mCurCritical;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mCurAttack = 0.0f;
		mCurCritical = 0.0f;
		mAttack = 0.0f;
		mAttackMax = 0.0f;
		mCritical = 0.0f;
		mCriticalMax = 0.0f;
	}
	protected void onGridTowerChange(EventGridTowerChange eventParam)
	{
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mCurAttack;
		mCharacterGame.getGameData().mCriticalIncrease -= mCurCritical;
		int count = mTowerDefenceSystem.getTowerList().Count;
		mCurAttack = getMin(mAttackMax, count * mAttack);
		mCurCritical = getMin(mCriticalMax, count * mCritical);
		mCharacterGame.getGameData().mIncreaseAttackPercent += mCurAttack;
		mCharacterGame.getGameData().mCriticalIncrease += mCurCritical;
	}
}