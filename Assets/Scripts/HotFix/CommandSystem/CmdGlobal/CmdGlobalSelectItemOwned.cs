using static GBR;

// 选中手牌中的物品(塔或者消耗道具)
public class CmdGlobalSelectItemOwned
{
	protected static void setSelectItem(bool lastItemValid, bool curItemValid)
	{
		if (curItemValid != lastItemValid)
		{
			mBattleScene.showWalkableGrid(curItemValid);
		}
		if (curItemValid)
		{
			CmdGlobalSelectTowerScene.execute(null);
		}
	}
}