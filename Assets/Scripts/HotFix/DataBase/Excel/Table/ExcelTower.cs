using System.Collections.Generic;
using UnityEngine;
using static GBR;
using static UnityUtility;

// 物品信息
public struct ItemCount
{
    public int mID;
    public int mCount;
    public ItemCount(int id, int count)
    {
        mID = id;
        mCount = count;
    }
    public ItemCount(Vector2Int vec2)
    {
        mID = vec2.x;
        mCount = vec2.y;
    }
}

// 防御塔表
public class ExcelTower : ExcelTableT<EDTower>
{
    protected Dictionary<TOWER_TYPE, Dictionary<int, EDTower>> mTowerDataDict;          // type,star 数据列表
    protected Dictionary<TARGET_BEHAVIOUR_TYPE, List<TOWER_TYPE>> mBehaviourTypeTowers; // 塔目标类型的分类
    protected Dictionary<TOWER_TYPE, List<EDTower>> mTypeTowers;                        // 塔类型对应数据
    protected Dictionary<Vector2Int, int> mRogueCost;                                   // 肉鸽模式 升级到下一级需要的货币
    public override void clearCache()
    {
        mTowerDataDict = null;
        mBehaviourTypeTowers = null;
        mTypeTowers = null;
        mRogueCost = null;
    }
    public EDTower getTowerData(TOWER_TYPE towerType, int star)
    {
        if (mTowerDataDict == null)
        {
            initTowerDataDict();
        }
        return mTowerDataDict.get(towerType)?.get(star);
    }
    public int getTowerLevelAttack(EDTower towerData, int nowLevel)
    {
        return 1;
    }
    public int getRogueNextLevelCost(EDTower towerData, int nowLevel)
    {
        if (mRogueCost == null)
        {
            initRogueCostAndSell();
        }
        return mRogueCost.get(new(towerData.mID, nowLevel));
    }
    public override void checkAllData()
    {
        foreach (EDTower item in queryAll())
        {
            mExcelTowerSkill.checkData(item.mSkill, item.mID, this);
            mExcelBuffDetail.checkData(item.mDefaultBuff, item.mID, this);
            if (item.mDescription != mExcelLocalization.query(item.mLocalLang).mChinese)
            {
                logError(mTableName + "中ID:" + item.mID + "的Description, 与ExcelLocalization中ID:" + item.mLocalLang + "的中文不一致");
            }
            checkPath(item.mPrefab);
        }
    }
    public Dictionary<TOWER_TYPE, List<EDTower>> getTowerTypes()
    {
        if (mTypeTowers == null)
        {
            initTowerTypes();
        }
        return mTypeTowers;
    }
    public EDTower getTypeTowerData(TOWER_TYPE type)
    {
        return getTowerTypes().get(type).get(0);
    }
    public string getTowerName(TOWER_TYPE type)
    {
        return getTypeTowerData(type).mName;
    }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void initRogueCostAndSell()
    {
        mRogueCost = new();
        foreach (EDTower data in queryAll())
        {
            foreach (Vector3Int info in data.mRogueLevelUpTowerSilverCost)
            {
                int startLevel = info.x;
                int endLevel = info.y;
                int eachCost = info.z;
                for (int i = startLevel; i <= endLevel; ++i)
                {
                    // 配置1-10 的10代表的是9升级到10的配置，所以要-1
                    mRogueCost.add(new(data.mID, i - 1), eachCost);
                }
            }
        }
    }
    protected void initTowerTypes()
    {
        mTypeTowers = new();
        foreach (EDTower item in queryAll())
        {
            mTypeTowers.getOrAddNew(item.mType).Add(item);
        }
    }
    protected void initTowerDataDict()
    {
        mTowerDataDict = new();
        foreach (EDTower data in queryAll())
        {
            mTowerDataDict.getOrAddNew(data.mType).Add(data.mStar, data);
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDTower item in queryAll())
		{
			checkEnum(item.mType, "mType", item.mID);
		}
	}
	// auto generate end
}