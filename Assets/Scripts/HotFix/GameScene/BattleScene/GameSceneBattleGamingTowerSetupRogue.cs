using static GBR;
using static FrameBaseUtility;

// 战斗逻辑场景 - 可以布置塔的流程（肉鸽版，全链路 ①布阵 的 Rogue 分支）
// 继承 TowerSetup 的布阵准备，额外叠一层"三选一卡牌"：
//   本波 WaveConfig.mCardPool != 0 时，若还没选过且候选为空 → CmdGlobalRandomPropListRogue 抽卡池；
//   若是从服务器存档恢复的候选（非空）→ 直接显示；选过或用完 → 隐藏。
// 玩家从三选一里拿到的天赋/新塔，会写进 BattleModeRogue.mSelectedTalentDic / mAllowUseTowerList，
// 再通过全局角色 mGlobalCharacter 上的 Buff 影响本波及之后的所有战斗（Rogue 构筑的"叠层"就在这）。
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
		int cardPool = waveConfig.mCardPool;
        logBase("[流程]GameSceneBattleGamingTowerSetupRogue onInit 三选一界面 cardPool: " + cardPool);
        if (cardPool != 0)
		{
			// 选过就隐藏且不随机了
			if (mTowerDefenceSystem.getBattleModeRogue().isRogueSelected())
			{
				LT.HIDE<UIBattleItemSelectRogue>();
				logBase("[流程]GameSceneBattleGamingTowerSetupRogue 隐藏：选过就隐藏且不随机了");
			}
			// 放塔流程时，为空时随机一次
			else if (mTowerDefenceSystem.getAllowSelectPropListRogueCount() == 0)
			{
				// 因为卡池界面一直不会隐藏,所以除了第一次以外都不会走onGameState,所以需要手动显示
				LT.LOAD<UIBattleItemSelectRogue>().setListVisible(true);
                logBase("[流程]GameSceneBattleGamingTowerSetupRogue 显示：放塔流程时，为空时随机一次");
				CmdGlobalRandomPropListRogue.execute(0);
            }
			// 如果有说明已经读取了服务器的存档
			else
			{
				// 因为卡池界面一直不会隐藏,所以除了第一次以外都不会走onGameState,所以需要手动显示
				LT.LOAD<UIBattleItemSelectRogue>().setListVisible(true);
                logBase("[流程]GameSceneBattleGamingTowerSetupRogue 显示：读取了服务器的存档");
            }
		}
		else
		{
			LT.HIDE<UIBattleItemSelectRogue>();
            logBase("[流程]GameSceneBattleGamingTowerSetupRogue 隐藏：没有配置卡池");
        }
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		CmdGlobalSelectItemOwnedRogue.execute(null);
        logBase("[流程]GameSceneBattleGamingTowerSetupRogue onExit 取消选中手牌中的塔");
    }
}