using static GBR;

// Rogue 模式设置塔的"免费升级模式"开关,改塔数据后刷新塔操作面板按钮状态。
public class CmdGlobalFreeUpLevelRogue
{
	public static void execute(CharacterTower tower, bool newFree)
	{
		tower.getTowerData().setFreeUpModeLevel(newFree);
		mUITowerOperation.safe()?.refreshButtonState();
	}
}