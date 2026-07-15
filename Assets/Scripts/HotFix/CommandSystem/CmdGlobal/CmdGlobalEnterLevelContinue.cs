using static GBR;
using static FrameUtility;

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