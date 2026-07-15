using static GBR;

// 选中手牌中的塔,Rogue模式下
public class CmdGlobalSelectItemOwnedRogue : CmdGlobalSelectItemOwned
{
	public static void execute(ClientPackRogueItemTower towerItem)
	{
		if (mUIClientPackRogue == null)
		{
			return;
		}
		setSelectItem(mUIClientPackRogue.getReadyToSetupTower() != null, towerItem != null);
	}
}