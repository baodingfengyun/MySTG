using static GBR;
using static FrameBaseHotFix;

// 肉鸽模式上阵塔
public class CmdGlobalAddRogueTower
{
	public static void execute(TOWER_TYPE tower)
	{
		mTowerDefenceSystem.getBattleModeRogue().getAllowUseTowerList().add(mExcelTower.getTypeTowerData(tower));
		mUIClientPackRogue.safe()?.refresh();
	}
}