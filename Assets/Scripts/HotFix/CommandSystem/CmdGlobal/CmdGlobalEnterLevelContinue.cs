using static GBR;
using static FrameUtility;

// 继续进入关卡命令:先复位镜头,保存当前关卡数据→清空塔防系统→重设关卡数据并重新初始化地图,
// 然后切回布阵流程,最后刷新战斗主界面与 Rogue 道具栏,实现"续玩/重进本关"的无缝重启。
public class CmdGlobalEnterLevelContinue
{
	public static void execute()
	{
		CmdGlobalCameraScale.execute(false);
		EDLevel levelData = mTowerDefenceSystem.getLevelData();
		mTowerDefenceSystem.clear();
		mTowerDefenceSystem.setLevelData(levelData);
		mTowerDefenceSystem.initLevel();
		changeProcedure(mTowerDefenceSystem.getSetupTowerProcedure());
		mUIGaming.safe()?.refresh();
		mUIClientPackRogue.safe()?.refresh();
	}
}