using System.Collections.Generic;
using UnityEngine;
using static UnityUtility;
using static GBR;

// 波次配置
public class ExcelWaveConfig : ExcelTableT<EDWaveConfig>
{
    public Dictionary<int, List<EDWaveConfig>> mLevelWaveDict;
    protected Dictionary<int, HashSet<int>> mLevelMonsters;                     // 关卡所有可能出现的怪物
    protected Dictionary<int, Dictionary<int, int>> mWaveMonsterHpRatio;        // 关卡指定的某个怪物的强度
    public List<EDWaveConfig> getLevelWaves(int level)
    {
        if (mLevelWaveDict == null)
        {
            initWaveConfigs();
        }
        return mLevelWaveDict.get(level);
    }
    public EDWaveConfig getWaveConfig(int level, int waveIndex)
    {
        if (level == 0)
        {
            return null;
        }
        waveIndex += 1; // 策划的配置是从1开始的
        if (mLevelWaveDict == null)
        {
            initWaveConfigs();
        }
        EDWaveConfig config = null;
        foreach (EDWaveConfig item in mLevelWaveDict.get(level).safe())
        {
            if (waveIndex < item.mWave)
            {
                break;
            }
            config = item;
        }
        return config;
    }
    public float getWaveMonsterIncreaseValue(int level, int waveIndex, int monsterID)
    {
        EDWaveConfig waveConfig = getWaveConfig(level, waveIndex);
        if (waveConfig == null)
        {
            return 1.0f;
        }
        int hpRatio = waveConfig.mHpRatio;
        int monsterRatio = getMonsterHpRatio(waveConfig.mID, monsterID);
        if (monsterRatio >= 0)
        {
            hpRatio = monsterRatio;
        }
        return (hpRatio + (waveIndex + 1 - waveConfig.mWave) * waveConfig.mHpRatioGrow) * waveConfig.mHpModifier / 10000.0f;
    }
    public int getMonsterHpRatio(int waveConfigID, int monsterID)
    {
        if (mWaveMonsterHpRatio == null)
        {
            initMonsterHpRatioGrow();
        }
        if (!mWaveMonsterHpRatio.TryGetValue(waveConfigID, out var waveInfo) ||
             !waveInfo.TryGetValue(monsterID, out var findValue))
        {
            return -1;
        }
        return findValue;
    }
    public override void checkAllData()
    {
        foreach (EDWaveConfig item in queryAll())
        {
            checkListPair(item.mMonsterIDs, item.mMonsterWeights, item.mID);
            mExcelLevel.checkData(item.mLevel, item.mID, this);
            mExcelCardPoolConfig.checkData(item.mCardPool, item.mID, this);
            mExcelMonster.checkData(item.mMonsterIDs, item.mID, this);

            // 检查waveConfig的出怪口个数是否大于了MapConfig的出怪口
            EDLevel levelData = mExcelLevel.query(item.mLevel, false);
            if (levelData != null)
            {
                EDMapConfig map = mExcelMapConfig.query(levelData.mMapID, false);
                if (map != null)
                {
                    int spawnPointCount = item.mSpawnPoint.Count;
                    if (spawnPointCount > map.mSpawnPoint.Count)
                    {
                        logError("WaveConfig[" + item.mID + "]的SpawnPoint数量[" + spawnPointCount + "]超出范围, ExcelMapConfig[" + map.mID + "]的SpawnPoint数量为[" + map.mSpawnPoint.Count + "]");
                    }
                    if (item.mCardPool != 0 && item.mCardRandomCount == 0)
                    {
                        logError("卡池ID配置不为0,但是显示数量为0, WaveConfig:" + item.mID);
                    }
                }
            }

            // 怪物的难度系数是否已经配置
            if (item.mHpRatio == 0)
            {
                using var a = new ListScope<int>(out var monsterIDList);
                foreach (Vector2Int info in item.mSpawnQueue)
                {
                    monsterIDList.addUnique(info.x);
                    if (info.x <= 0)
                    {
                        logError("固定怪物序列中配置了ID为0的怪物,WaveID:" + item.mID);
                    }
                    if (info.y <= 0)
                    {
                        logError("固定怪物序列中配置了数量小于等于0的怪物,WaveID:" + item.mID + ", 怪物ID:" + info.x);
                    }
                }
                monsterIDList.AddRange(item.mMonsterIDs);
                using var b = new HashSetScope<int>(out var ratioList);
                foreach (Vector2Int info in item.mHpRatioMonster)
                {
                    ratioList.Add(info.x);
                    if (info.x <= 0)
                    {
                        logError("固定怪物序列中配置了ID为0的怪物,WaveID:" + item.mID);
                    }
                    if (info.y <= 0)
                    {
                        logError("怪物单独配置难度系数中中配置了系数小于等于0的怪物,WaveID:" + item.mID + ", 怪物ID:" + info.x);
                    }
                }
                foreach (int monsterID in monsterIDList)
                {
                    if (!ratioList.Contains(monsterID))
                    {
                        logError("波次难度系数为0,且没有找到单独的怪物难度配置,WaveID:" + item.mID + ", 怪物ID:" + monsterID);
                    }
                }
            }
        }
    }
    public override void clearCache()
    {
        mLevelWaveDict = null;
        mLevelMonsters = null;
        mWaveMonsterHpRatio = null;
    }
    public HashSet<int> getLevelMonsterList(int levelID)
    {
        if (mLevelMonsters == null)
        {
            mLevelMonsters = new();
            foreach (EDWaveConfig each in queryAll())
            {
                var newList = mLevelMonsters.getOrAddNew(each.mLevel);
                newList.addRange(each.mMonsterIDs);
                newList.addRange(each.mRandomBoss);
                foreach (Vector2Int item in each.mSpawnQueue.safe())
                {
                    if (item.x == 0)
                    {
                        logError("item.x == 0, each.mLevel:" + each.mLevel + ", waveID:" + each.mID);
                    }
                    newList.Add(item.x);
                }
            }
        }
        return mLevelMonsters.get(levelID);
    }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void initWaveConfigs()
    {
        mLevelWaveDict = new();
        foreach (EDWaveConfig item in queryAll())
        {
            mLevelWaveDict.getOrAddNew(item.mLevel).Add(item);
        }
    }
    protected void initMonsterHpRatioGrow()
    {
        mWaveMonsterHpRatio = new();
        foreach (EDWaveConfig item in queryAll())
        {
            foreach (Vector2Int info in item.mHpRatioMonster)
            {
                mWaveMonsterHpRatio.getOrAddNew(item.mID).Add(info.x, info.y);
            }
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDWaveConfig item in queryAll())
		{
			mExcelLevel.checkData(item.mLevel, item.mID, this);
			checkEnum(item.mSpawnRule, "mSpawnRule", item.mID);
			mExcelCardPoolConfig.checkData(item.mCardPool, item.mID, this);
		}
	}
	// auto generate end
}