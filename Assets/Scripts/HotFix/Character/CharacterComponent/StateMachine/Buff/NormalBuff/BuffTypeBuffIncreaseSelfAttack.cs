using System;
using static MathUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffTypeBuffIncreaseSelfAttackParam : CharacterBuffParamT<BuffTypeBuffIncreaseSelfAttackParam>
{
	public int mBuffType;						// buff类型
	public int mIncreasePerEnemy;				// 每增加一个buff,提升的攻击力
	public float mIncreasePercentPerEnemy;      // 每增加一个buff,提升的攻击力百分比
	public int mMaxIncrease;					// 提升上限
	public float mMaxIncreasePercent;           // 提升百分比上限
	public override void registeAllParam()
	{
		registeParam((param) => { mBuffType = param.SToI(); });
		registeParam((param) => { mIncreasePerEnemy = param.SToI(); });
		registeParam((param) => { mIncreasePercentPerEnemy = param.SToF(); });
		registeParam((param) => { mMaxIncrease = param.SToI(); });
		registeParam((param) => { mMaxIncreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffTypeBuffIncreaseSelfAttackParam other)
	{
		mBuffType = other.mBuffType;
		mIncreasePerEnemy = other.mIncreasePerEnemy;
		mIncreasePercentPerEnemy = other.mIncreasePercentPerEnemy;
		mMaxIncrease = other.mMaxIncrease;
		mMaxIncreasePercent = other.mMaxIncreasePercent;
	}
	public override void check()
	{
		checkDataRefByBuffDetail(mExcelBuff, mBuffType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffType = 0;
		mIncreasePerEnemy = 0;
		mIncreasePercentPerEnemy = 0.0f;
		mMaxIncrease = 0;
		mMaxIncreasePercent = 0.0f;
	}
}

// 场上指定类型的buff越多,自身增加的攻击力越多
public class BuffTypeBuffIncreaseSelfAttack : CharacterBuffT<BuffTypeBuffIncreaseSelfAttackParam>
{
	protected float mIncreasePercentPerEnemy;	// 每增加一个buff,提升的攻击力百分比
	protected float mLastIncreasePercent;		// 上一次提升的攻击力百分比
	protected float mMaxIncreasePercent;		// 提升百分比上限
	protected int mIncreasePerEnemy;			// 每增加一个buff,提升的攻击力
	protected int mLastIncrease;				// 上一次提升的攻击力
	protected int mMaxIncrease;					// 提升上限
	protected Type mBuffType;					// buff的类型
	protected bool mBuffChanged;				// 场上的buff是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	public override void enter()
	{
		base.enter();
		mBuffType = mStateManager.getStateType((int)mCustomParam.mBuffType);
		mIncreasePerEnemy = mCustomParam.mIncreasePerEnemy;
		mIncreasePercentPerEnemy = mCustomParam.mIncreasePercentPerEnemy;
		mMaxIncrease = mCustomParam.mMaxIncrease;
		mMaxIncreasePercent = mCustomParam.mMaxIncreasePercent;
		mEventSystem.listenEvent<EventMonsterDestroy>(onMonsterDestroy, this);
		mEventSystem.listenEvent<EventMonsterAddBuff>(onMonsterBuffAdd, this);
		mEventSystem.listenEvent<EventMonsterRemoveBuff>(onMonsterBuffRemove, this);
		mEventSystem.listenEvent<EventMonsterDie>(onMonsterDie, this);
		mBuffChanged = true;
	}
	public override void update(float elapsedTime)
	{
		if (mBuffChanged)
		{
			mBuffChanged = false;
			// 计算场上的指定buff数量,增加攻击力
			int buffCount = mTowerDefenceSystem.getMonsterTypeBuffCount(mBuffType);
			int increase = clampMax(buffCount * mIncreasePerEnemy, mMaxIncrease);
			float increasePercent = clampMax(buffCount * mIncreasePercentPerEnemy, mMaxIncreasePercent);
			mCharacterGame.getGameData().mAttackIncrease += increase - mLastIncrease;
			mCharacterGame.getGameData().mIncreaseAttackPercent += increasePercent - mLastIncreasePercent;
			mLastIncrease = increase;
			mLastIncreasePercent = increasePercent;
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mAttackIncrease -= mLastIncrease;
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mLastIncreasePercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercentPerEnemy = 0.0f;
		mLastIncreasePercent = 0.0f;
		mMaxIncreasePercent = 0.0f;
		mIncreasePerEnemy = 0;
		mLastIncrease = 0;
		mMaxIncrease = 0;
		mBuffType = null;
		mBuffChanged = false;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onMonsterDestroy(EventMonsterDestroy eventParam)
	{
		mBuffChanged = true;
	}
	protected void onMonsterBuffAdd(EventMonsterAddBuff eventParam)
	{
		mBuffChanged = true;
	}
	protected void onMonsterBuffRemove(EventMonsterRemoveBuff eventParam)
	{
		mBuffChanged = true;
	}
	protected void onMonsterDie(EventMonsterDie eventParam)
	{
		mBuffChanged = true;
	}
}