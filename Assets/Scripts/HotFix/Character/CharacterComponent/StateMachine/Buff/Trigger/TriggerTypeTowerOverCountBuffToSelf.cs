using static FrameBaseHotFix;
using static FrameUtility;
using static GBR;

// 参数
public class TriggerTypeTowerOverCountBuffToSelfParam : CharacterTriggerParamT<TriggerTypeTowerOverCountBuffToSelfParam>
{
	public TOWER_TYPE mTowerType;			// 塔的类型
	public int mNeedTowerCount;             // 所需要的塔数量
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
		registeParam((param) => { mNeedTowerCount = param.SToI(); });
	}
	public override void check()
	{
		base.check();
		checkEnum(mTowerType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerType = TOWER_TYPE.NONE;
		mNeedTowerCount = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void copyInternal(TriggerTypeTowerOverCountBuffToSelfParam other)
	{
		base.copyInternal(other);
		mTowerType = other.mTowerType;
		mNeedTowerCount = other.mNeedTowerCount;
	}
}

// 场上指定类型的塔超过一定数量时,会给自己附加指定buff,塔数量低于一定数量时会移除buff
public class TriggerTypeTowerOverCountBuffToSelf : CharacterTriggerT<TriggerTypeTowerOverCountBuffToSelfParam>
{
	protected int mNeedTowerCount;			// 所需要的塔数量
	protected int mCurTowerCount;			// 当前的塔数量
	protected bool mTowerChanged;			// 场上的塔是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	protected TOWER_TYPE mTowerType;		// 塔的类型
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedTowerCount = 0;
		mCurTowerCount = 0;
		mTowerChanged = false;
		mTowerType = TOWER_TYPE.NONE;
	}
	public override void enter()
	{
		base.enter();
		mNeedTowerCount = mCustomParam.mNeedTowerCount;
		mTowerType = mCustomParam.mTowerType;
		mEventSystem.listenEvent<EventGridTowerChange>(onTowerChanged, this);
		mTowerChanged = true;
		mCurTowerCount = 0;
	}
	public override void update(float elapsedTime)
	{
		bool availableChanged = false;
		if (mTowerChanged)
		{
			mTowerChanged = false;
			// 计算场上的指定宝石数量
			int newTowerCount = mTowerDefenceSystem.getTypeTowerCount(mTowerType);
			availableChanged = newTowerCount >= mNeedTowerCount != mCurTowerCount >= mNeedTowerCount;
			mCurTowerCount = newTowerCount;
		}

		if (availableChanged)
		{
			if (mCurTowerCount >= mNeedTowerCount)
			{
				if (!mBuffList.containsKey(mCharacterGame))
				{
					addBuff(mCharacterGame);
				}
			}
			// 塔数量不足,移除所有buff
			else
			{
				removeAllAdded();
			}
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		removeAllAdded();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
}