using static GBR;

// 可以布置塔的流程
public class GameSceneBattleGamingTowerSetupRogue : SceneProcedure
{
	protected override void onInit(SceneProcedure lastProcedure)
	{
		// 显示界面
		LT.LOAD<UIClientPackRogue>().setPanelVisible(true);
		mUIClientPackRogue.stopDrag();
		CmdGlobalSelectTowerScene.execute(null);
		CmdGlobalSelectItemOwnedRogue.execute(null);

		EDWaveConfig waveConfig = mTowerDefenceSystem.getWaveData();
		if (waveConfig.mCardPool != 0)
		{
			// 选过就隐藏且不随机了
			if (mTowerDefenceSystem.getBattleModeRogue().isRogueSelected())
			{
				LT.HIDE<UIBattleItemSelectRogue>();
			}
			// 放塔流程时，为空时随机一次
			else if (mTowerDefenceSystem.getAllowSelectPropListRogueCount() == 0)
			{
				// 因为卡池界面一直不会隐藏,所以除了第一次以外都不会走onGameState,所以需要手动显示
				LT.LOAD<UIBattleItemSelectRogue>().setListVisible(true);
				CmdGlobalRandomPropListRogue.execute(0);
			}
			// 如果有说明已经读取了服务器的存档
			else
			{
				// 因为卡池界面一直不会隐藏,所以除了第一次以外都不会走onGameState,所以需要手动显示
				LT.LOAD<UIBattleItemSelectRogue>().setListVisible(true);
			}
		}
		else
		{
			LT.HIDE<UIBattleItemSelectRogue>();
		}
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		CmdGlobalSelectItemOwnedRogue.execute(null);
	}
}