using static GBR;
using static GDR;
using static MathUtility;

// Rogue模式下出售塔,获得金币
public class CmdGlobalSellTowerRogue : CmdGlobalSellTower
{
	public static void execute(CharacterTower tower)
	{
		CharacterTowerData towerData = tower.getTowerData();
		int sellGoldCount = (int)(towerData.mUseCoin * ROGUE_MODE_SELL_TOWER_PERCENT);
		CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() + sellGoldCount);
		postSellTower(tower);
	}
}