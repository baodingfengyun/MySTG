using UnityEngine;
using System.Collections.Generic;
using static MathUtility;
using static UnityUtility;
using static FrameBaseHotFix;
using static GBR;

// 将传入的塔随机变成别的塔，保留等级
public class CmdGlobalRandomChangeTowerRogue : CmdGlobalPutTower
{
	public static bool execute(CharacterTower tower, bool saveLevel, List<TOWER_TYPE> towerList)
	{
		TOWER_TYPE type = towerList[randomInt(0, towerList.Count - 1)];
		int curStar = tower.getTowerData().mTableData.mStar;
		EDTower towerData = mExcelTower.getTowerData(type, curStar);
		// 可能要变成的塔只有1星
		towerData ??= mExcelTower.getTowerData(type, 1);
		if (towerData == null)
		{
			// 配置有问题
			logError("随机变塔失败,该塔没有1星配置 ID:" + type);
			return false;
		}
		// 记录旧塔数据
		Vector3 pos = tower.getPosition();
		int gridIndex = tower.getGridIndex();
		int level = saveLevel ? tower.getTowerData().getBattleLevel().clampMax(10) : 1;
		bool selecting = mTowerDefenceSystem.getSelectedTowerScene() == tower;
		CmdGlobalDestroyTower.execute(tower);
		// 创建新塔
		CharacterTower newTower = CmdGlobalCreateTower.execute(towerData, pos);
		// 等级
		newTower.getTowerData().setBattleLevel(level);
		// 放置
		putTower(newTower, gridIndex);
		// 更新UI
		if(selecting)
		{
			mTowerDefenceSystem.setSelectedTowerScene(newTower);
			mBattleScene.showTowerRange(newTower);
		}
		mEffectManager.playEffectAsync(EDEffect.TOWER_PLACE.mPath, newTower, 2.6f, true);

		mUITowerInfo.safe()?.setTower(newTower);
		return true;
	}
}