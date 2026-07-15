using static GBR;
using static FrameUtility;

// 拆除塔命令的基类,提供公共函数
public class CmdGlobalSellTower
{
	protected static void postSellTower(CharacterTower tower)
	{
		CmdGlobalDestroyTower.execute(tower);
		// 在设置塔的流程中需要刷新寻路路线的显示
		if (atProcedure(mTowerDefenceSystem.getSetupTowerProcedure()))
		{
			mTowerDefenceSystem.generateRoadPathAndRefresh();
			mBattleScene.showAllPath();
		}
		AT.SOUND_2D(SOUND_HOTFIX.REMOVE_TOWER);
	}
}