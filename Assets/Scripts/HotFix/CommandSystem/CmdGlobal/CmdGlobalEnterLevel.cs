using static GBR;
using static FrameUtility;
using static FrameBaseUtility;

/// <summary>
/// 命令：进入关卡
/// </summary>
public class CmdGlobalEnterLevel
{
	public static void execute(int levelID)
	{
		EDLevel levelData = mExcelLevel.query(levelID);
		mTowerDefenceSystem.setLevelData(levelData);
		enterScene<GameSceneBattle>();
		logBase("[进入关卡]levelID: " + levelID + ", 进入战斗场景");
	}
}