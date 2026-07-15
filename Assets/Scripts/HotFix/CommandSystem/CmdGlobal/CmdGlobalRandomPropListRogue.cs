using System;
using static GBR;
using static GDR;
using static MathUtility;
using static GameUtilityHotFix;
using static UnityUtility;

// 随机卡池中的物品,Rogue模式
public class CmdGlobalRandomPropListRogue
{
	public static void execute(int costCoin)
	{
		if (costCoin > 0)
		{
			if (mTowerDefenceSystem.getGoldCoinRogue() < costCoin)
			{
				tip("金币不足");
				return;
			}
			CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() - costCoin);
		}

		EDWaveConfig waveConfig = mTowerDefenceSystem.getWaveData();
		if (waveConfig.mCardPool == 0)
		{
			mTowerDefenceSystem.setAllowSelectPropListRogue(null);
			mUIBattleItemSelectRogue.setPropList(mTowerDefenceSystem.getAllowSelectPropListRogue());
			return;
		}
		int cardPoolID = waveConfig.mCardPool;
		EDCardPoolConfig cardPoolConfig = mExcelCardPoolConfig.query(cardPoolID);
		using var a = new ListScope2<int>(out var weightList, out var tempItemIDList);
		using var b = new ListScope2T<ExcelData, BATTLE_ITEM_TYPE>(out var selectedDataList, out var tempItemTypeList);
		// 先获取必出的物品
		int mustHaveCount = cardPoolConfig.mMustItemID.Count;
		for (int i = 0; i < mustHaveCount; ++i)
		{
			if (cardPoolConfig.mMustItemType[i] == BATTLE_ITEM_TYPE.TOWER_TALENT)
			{
				EDTowerTalent talentData = mExcelTowerTalent.query(cardPoolConfig.mMustItemID[i]);
			}
		}
		// 获取上阵的塔
		var currentOwnedTowers = mTowerDefenceSystem.getBattleModeRogue().getAllowUseTowerList();
		bool towerSelectComplete = currentOwnedTowers.Count >= mExcelGlobalConfig.getRogueTowerSlotCount();

		// 从剩下的随机次数中在有权重的卡池中抽取
		tempItemIDList.AddRange(cardPoolConfig.mItemID);
		tempItemTypeList.AddRange(cardPoolConfig.mItemType);
		weightList.AddRange(cardPoolConfig.mItemWeight);
		for (int i = 0; i < tempItemIDList.Count; ++i)
		{
			// 把所有非天赋卡牌的删除掉,不满足当前条件的天赋也删除
			if (tempItemTypeList[i] != BATTLE_ITEM_TYPE.TOWER_TALENT)
			{
				tempItemIDList.RemoveAt(i);
				tempItemTypeList.RemoveAt(i);
				weightList.RemoveAt(i);
				--i;
			}
			// 如果上阵的塔满了，则只随机上阵的塔相关的天赋 
			else if (towerSelectComplete && !currentOwnedTowers.Contains(mExcelTower.getTypeTowerData(mExcelTowerTalent.query(tempItemIDList[i]).mTowerType)))
			{
				tempItemIDList.RemoveAt(i);
				tempItemTypeList.RemoveAt(i);
				weightList.RemoveAt(i);
				--i;
			}
		}

		// 根据英雄携带和场上已有的防御塔,对权重进行调整
		using var e = new ListScope<TOWER_TYPE>(out var existTowerList);
		foreach (CharacterTower item in mTowerDefenceSystem.getTowerList())
		{
			existTowerList.addUnique(item.getTowerType());
		}
		int idCount = tempItemIDList.Count;
		for (int i = 0; i < idCount; ++i)
		{
			if (existTowerList.Contains(mExcelTowerTalent.query(tempItemIDList[i]).mTowerType))
			{
				// 暂时固定加500
				weightList[i] += ROGUE_RANDOM_ADD_PROBABILITY;
			}
		}
		// 加入防御塔上阵词条
		if (cardPoolConfig.mUseAddTowerTalent && !towerSelectComplete)
		{
			foreach (var each in ADD_TOWER_TALENT.safe())
			{
				EDTower towerData = mExcelTower.getTypeTowerData(each.Key);
				if (!currentOwnedTowers.Contains(towerData))
				{
					// 如果是上阵了的塔，即使没解锁也加入
					continue;
				}
				if (mTowerDefenceSystem.getBattleModeRogue().getAllowUseTowerList().Contains(towerData))
				{
					continue;
				}
				if(!tempItemIDList.addUnique(each.Value))
				{
					continue;
				}
				tempItemTypeList.add(BATTLE_ITEM_TYPE.TOWER_TALENT);
				weightList.add(mExcelGlobalConfig.getRogueAddTowerWeight(mTowerDefenceSystem.getWaveIndex()));
			}
		}

		int remainCount = ROGUE_RANDOM_PROP_COUNT - selectedDataList.Count;
		if (remainCount > 0 && weightList.Count > 0)
		{
			Span<int> indexList = stackalloc int[getMin(weightList.Count, remainCount)];
			randomSelect(weightList, remainCount, indexList);
			foreach (int index in indexList)
			{
				if (!selectedDataList.addUnique(mExcelTowerTalent.query(tempItemIDList[index])))
				{
					logError("随机出了重复词条");
				}
			}
		}
		mTowerDefenceSystem.setAllowSelectPropListRogue(selectedDataList);
		mUIBattleItemSelectRogue.setPropList(mTowerDefenceSystem.getAllowSelectPropListRogue());
	}
}