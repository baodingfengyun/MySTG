using static GBR;
using static FrameUtility;

public class CmdGlobalEnterLevel
{
	public static void execute(int levelID)
	{
		EDLevel levelData = mExcelLevel.query(levelID);
		mTowerDefenceSystem.setLevelData(levelData);
		enterScene<GameSceneBattle>();
	}
}