using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static FrameUtility;
using static GBR;

// 参数
public class TriggerBuffToGridRangeTowerByTowerCountParam : CharacterTriggerParamT<TriggerBuffToGridRangeTowerByTowerCountParam>
{
	public TOWER_TYPE mTowerType;				// 塔的类型,填0所有
	public int mCount;							// 所需要的塔数量
	public int mRange;							// 附近几格
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
		registeParam((param) => { mCount = param.SToI(); });
		registeParam((param) => { mRange = param.SToI(); });
	}
	protected override void copyInternal(TriggerBuffToGridRangeTowerByTowerCountParam other)
	{
		mTowerType = other.mTowerType;
		mCount = other.mCount;
		mRange = other.mRange;
	}
	public override void check()
	{
		checkEnum(mTowerType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerType = TOWER_TYPE.NONE;
		mCount = 0;
		mRange = 0;
	}
}

// 根据场上防御塔数量，提升自身攻击力和暴击率
public class TriggerBuffToGridRangeTowerByTowerCount : CharacterTriggerT<TriggerBuffToGridRangeTowerByTowerCountParam>
{
	public TOWER_TYPE mTowerType;				// 塔的类型,填0所有
	public int mCount;							// 所需要的塔数量
	public int mRange;							// 附近几格
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventGridTowerChange>(onGridTowerChange, this);
		mTowerType = mCustomParam.mTowerType;
		mCount = mCustomParam.mCount;
		mRange = mCustomParam.mRange;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		removeAllAdded();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerType = TOWER_TYPE.NONE;
		mCount = 0;
		mRange = 0;
	}
	public override void destroy()
	{
		base.destroy();
		removeAllAdded();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void addBuffs()
	{
		removeAllAdded();
		int count = 0;
		foreach(var each in mTowerDefenceSystem.getTowerList().safe())
		{
			if(mTowerType == TOWER_TYPE.NONE || each.getTowerData().mTableData.mType == mTowerType)
			{
				count++;
			}
		}
		if(count < mCount)
		{
			return;
		}
		using var a = new ListScope<int>(out var girds);
		getHexAroundGird(mCharacterGame.getGridIndex(), mRange, girds);
		foreach (int each in girds)
		{
			CharacterTower tower = mTowerDefenceSystem.getTowerAtGrid(each);
			if (tower == null)
			{
				continue;
			}
			addBuff(tower);
		}
	}
	protected void onGridTowerChange(EventGridTowerChange param)
	{
		addBuffs();
	}
}