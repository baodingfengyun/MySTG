using static GBR;
using static GameUtilityHotFix;
using static StringUtility;

// 参数
public class TriggerBuffToHexRangeTowerParam : CharacterTriggerParamT<TriggerBuffToHexRangeTowerParam>
{
	public int mRange;					// 范围
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mRange = param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mRange = 0;
	}
	protected override void copyInternal(TriggerBuffToHexRangeTowerParam other)
	{
		base.copyInternal(other);
		mRange = other.mRange;
	}
}

// 对六边形半径范围内的塔触发buff
public class TriggerBuffToHexRangeTower : CharacterTriggerT<TriggerBuffToHexRangeTowerParam>
{
	public override void enter()
	{
		base.enter();
		using var a = new ListScope<int>(out var grids);
		getHexAroundGird(mCharacterGame.getGridIndex(), mCustomParam.mRange, grids);
		foreach (int grid in grids)
		{
			CharacterTower tower = mTowerDefenceSystem.getTowerAtGrid(grid);
			if (tower == null)
			{
				continue;
			}
			addBuff(tower);
		}
		// 给自己加
		addBuff(mCharacterGame);
	}
}