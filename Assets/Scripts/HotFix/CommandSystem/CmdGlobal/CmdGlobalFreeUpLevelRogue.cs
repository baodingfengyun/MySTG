using static GBR;

public class CmdGlobalFreeUpLevelRogue
{
	public static void execute(CharacterTower tower, bool newFree)
	{
		tower.getTowerData().setFreeUpModeLevel(newFree);
		mUITowerOperation.safe()?.refreshButtonState();
	}
}