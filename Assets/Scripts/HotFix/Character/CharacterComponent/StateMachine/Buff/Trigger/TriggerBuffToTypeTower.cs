using static FrameBaseHotFix;
using static FrameUtility;
using static GBR;

// 参数
public class TriggerBuffToTypeTowerParam : CharacterTriggerParamT<TriggerBuffToTypeTowerParam>
{
	public TOWER_TYPE mTowerType;           // 塔的类型
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
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
	}
	protected override void copyInternal(TriggerBuffToTypeTowerParam other)
	{
		base.copyInternal(other);
		mTowerType = other.mTowerType;
	}
}

// 所有指定类型的塔附加buff
public class TriggerBuffToTypeTower : CharacterTriggerT<TriggerBuffToTypeTowerParam>
{
	protected TOWER_TYPE mTowerType;            // 塔的类型
	protected bool mTowerChanged;				// 场上的塔是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	public override void enter()
	{
		base.enter();
		mTowerType = mCustomParam.mTowerType;
		mEventSystem.listenEvent<EventGridTowerChange>(onTowerChanged, this);
		mEventSystem.listenEvent<EventTowerDestroy>(onTowerDestroy, this);
		mTowerChanged = true;
	}
	public override void update(float elapsedTime)
	{
		if (mTowerChanged)
		{
			mTowerChanged = false;
			var newList = mTowerDefenceSystem.getTowerList();
			foreach (CharacterTower tower in newList)
			{
				// 新增的塔
				if (tower.getTowerType() == mTowerType && !mBuffList.containsKey(tower))
				{
					addBuff(tower);
				}
			}
			foreach (var item in mBuffList)
			{
				// 已经不在的塔
				if (!newList.Contains(item.Key as CharacterTower))
				{
					removeCharacterAddedBuff(item.Key);
				}
			}
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		// 移除所有塔的增幅
		removeAllAdded();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerType = TOWER_TYPE.NONE;
		mTowerChanged = false;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
	protected void onTowerDestroy(EventTowerDestroy eventParam)
	{
		if (eventParam.mTower == null)
		{
			return;
		}
		removeCharacterAddedBuff(eventParam.mTower);
	}
}