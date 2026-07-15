using static GameUtilityHotFix;
using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class TriggerBuffToGridRangeTowerWhenPlaceParam : CharacterTriggerParamT<TriggerBuffToGridRangeTowerWhenPlaceParam>
{
	public TOWER_TYPE mTowerType;				// 塔的类型,填0所有
	public int mRange;							// 范围，格
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
		registeParam((param) => { mRange = param.SToI(); });
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
		mRange = 0;
	}
	protected override void copyInternal(TriggerBuffToGridRangeTowerWhenPlaceParam other)
	{
		base.copyInternal(other);
		mTowerType = other.mTowerType;
		mRange = other.mRange;
	}
}

// 在放置和移动英雄时,对一定范围内的塔触发buff
public class TriggerBuffToGridRangeTowerWhenPlace : CharacterTriggerT<TriggerBuffToGridRangeTowerWhenPlaceParam>
{
	public TOWER_TYPE mTowerType;				// 塔的类型,填0所有
	public int mRange;							// 范围，格
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerType = TOWER_TYPE.NONE;
		mRange = 0;
	}
	public override void enter()
	{
		base.enter();
		mTowerType = mCustomParam.mTowerType;
		mRange = mCustomParam.mRange;
		mEventSystem.listenEvent<EventGridTowerChange>(onGridTowerChange, this);
		addBuffs();
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		removeAllAdded();
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
		using var a = new ListScope<int>(out var girds);
		getHexAroundGird(mCharacterGame.getGridIndex(), mRange, girds);
		foreach (int each in girds)
		{
			CharacterTower tower = mTowerDefenceSystem.getTowerAtGrid(each);
			if (tower == null)
			{
				continue;
			}
			if (mTowerType == TOWER_TYPE.NONE || mTowerType == tower.getTowerType())
			{
				addBuff(tower);
			}
		}
	}
	protected void onGridTowerChange(EventGridTowerChange param)
	{
		addBuffs();
	}
}