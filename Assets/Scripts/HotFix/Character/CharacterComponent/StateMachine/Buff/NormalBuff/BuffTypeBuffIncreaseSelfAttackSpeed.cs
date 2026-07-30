using System;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffTypeBuffIncreaseSelfAttackSpeedParam : CharacterBuffParamT<BuffTypeBuffIncreaseSelfAttackSpeedParam>
{
	public int mBuffType;				// buff类型
	public float mIncreasePerEnemy;     // 每增加一个buff,提升的攻击速度
	public float mMaxIncrease;          // 提升上限
	public override void registeAllParam()
	{
		registeParam((param) => { mBuffType = param.SToI(); });
		registeParam((param) => { mIncreasePerEnemy = param.SToF(); });
		registeParam((param) => { mMaxIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffTypeBuffIncreaseSelfAttackSpeedParam other)
	{
		mBuffType = other.mBuffType;
		mIncreasePerEnemy = other.mIncreasePerEnemy;
		mMaxIncrease = other.mMaxIncrease;
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
		mMaxIncrease = 0;
	}
}

// 场上指定类型的buff越多,自身增加的攻击速度越多
public class BuffTypeBuffIncreaseSelfAttackSpeed : CharacterBuffT<BuffTypeBuffIncreaseSelfAttackSpeedParam>
{
	protected float mIncreasePerEnemy;      // 每增加一个buff,提升的攻击速度
	protected float mLastIncrease;			// 上一次提升的百分比
	protected float mMaxIncrease;			// 提升上限
	protected Type mBuffType;				// buff的类型
	protected bool mBuffChanged;            // 场上的buff是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	public override void enter()
	{
		base.enter();
		mBuffType = mStateManager.getStateType((int)mCustomParam.mBuffType);
		mIncreasePerEnemy = mCustomParam.mIncreasePerEnemy;
		mMaxIncrease = mCustomParam.mMaxIncrease;
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
			float increase = (mTowerDefenceSystem.getMonsterTypeBuffCount(mBuffType) * mIncreasePerEnemy).clampMax(mMaxIncrease);
			mCharacterGame.getGameData().addAttackSpeed(increase - mLastIncrease);
			mLastIncrease = increase;
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().removeAttackSpeed(mLastIncrease);
	}
	public override void resetProperty()
	{
		base.resetProperty();
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