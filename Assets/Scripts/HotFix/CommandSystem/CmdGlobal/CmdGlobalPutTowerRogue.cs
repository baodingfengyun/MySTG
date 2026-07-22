using static GameUtilityHotFix;
using static GBR;

// 放置塔到场景中,Rogue模式
public class CmdGlobalPutTowerRogue : CmdGlobalPutTower
{
	public static void execute(CharacterTower tower, int gridIndex, int propIndex)
	{
		CharacterGame gridCharacter = mTowerDefenceSystem.getTowerAtGrid(gridIndex);
		if (propIndex < 0)
		{
			// 交换场景中的两个塔,或者塔与英雄交换位置
			swapCharacter(tower, gridCharacter, gridIndex, tower.getGridIndex());
		}
		else
		{
			if (gridCharacter != null)
			{
				// 手牌中的塔不能替换场景中的英雄
				if (gridCharacter is not CharacterTower gridTower)
				{
					return;
				}
				// 替换场景中的塔
				CmdGlobalSellTowerRogue.execute(gridTower);
			}
			// 设置新的塔的下标
			putTower(tower, gridIndex);
		}
		if (propIndex >= 0)
		{
			// 进入建造CD
			mTowerDefenceSystem.startBuildingCD();
			// 给塔添加建造中的状态
			characterAddBuff(EDBuffDetail.TOWER_BUILDING_BUFF_ID, tower, null);
		}

		postPutTower(gridIndex);
	}
}