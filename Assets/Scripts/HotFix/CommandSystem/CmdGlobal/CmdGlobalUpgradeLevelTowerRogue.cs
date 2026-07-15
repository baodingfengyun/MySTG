using static GameUtilityHotFix;
using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 塔升级,Rogue模式
public class CmdGlobalUpgradeLevelTowerRogue
{
	public static void execute(CharacterTower tower, int addLevel = 1, bool noCost = false)
	{
		if (tower == null)
		{
			return;
		}
		CharacterTowerData towerData = tower.getTowerData();
		if(!noCost)
		{
			int cost = 0;
			if (!towerData.getFreeUpModeLevel())
			{
				cost = mExcelTower.getRogueNextLevelCost(towerData.mTableData, towerData.getBattleLevel());
			}
			if (cost > mTowerDefenceSystem.getGoldCoinRogue())
			{
				return;
			}
			CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() - cost);
			tower.getTowerData().addUseCoin(cost);
		}
		int level = towerData.getBattleLevel() + addLevel;
		// 这里会检查升级后是否超过了等级上限
		if (!CmdGlobalUpgradeStarTower.execute(tower))
		{
			return;
		}
		towerData.setBattleLevel(level);
		CmdGlobalFreeUpLevelRogue.execute(tower, false);
		mUITowerInfo?.setTower(tower);
		tip("成功升级到{0}级!", level.IToS());
		mEffectManager.playEffectAsync(mExcelEffect.query(TOWER_LEVEL_UP_EFFECT_ID).mPath, tower, 2.6f, true, 0);

		using var a = new ClassScope<EventTowerLevelChange>(out var eventParam);
		eventParam.mTower = tower;
		eventParam.mOldLevel = level - 1;
		eventParam.mNewLevel = level;
		mEventSystem.pushEvent(eventParam, tower.getGUID());

		mUITowerOperation.safe()?.refreshButtonState();
	}
}