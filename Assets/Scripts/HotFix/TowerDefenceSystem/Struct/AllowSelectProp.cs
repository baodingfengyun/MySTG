
// 待选卡列表中的数据（Rogue 三选一的一张候选牌）
// mPropData 是通用 ExcelData（实际多为 TowerTalent 表的一行），mUsed 标记玩家是否已选。
// BattleModeRogue.mAllowSelectPropList 装着本波的候选；CmdGlobalRandomPropListRogue 填充它，
// UIBattleItemSelectRogue 据此显示，isAllPropUsed() 通过 mUsed 判断本波卡是否已选完。
// 继承 ClassObject 走对象池。
public class AllowSelectProp : ClassObject
{
	public ExcelData mPropData;		// 表格数据
	public bool mUsed;				// 是否已经使用
	public override void resetProperty()
	{
		base.resetProperty();
		mPropData = null;
		mUsed = false;
	}
}