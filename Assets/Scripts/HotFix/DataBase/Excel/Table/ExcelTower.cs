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

// 防御塔表：塔的静态属性（名字/图标/prefab/射程/星级/价格/升级·出售消耗/暴击）都在这里。
// 与 ExcelTowerSkill（行为）、ExcelBuffDetail（默认 Buff）通过 ID 关联，
// 三者拼装出一座完整的塔——这正是"加塔大多只填表"的落点。
// 派生缓存（均懒加载 + clearCache 置空）：
//   mTowerDataDict      (塔类型 -> 星级 -> 数据)，供 getTowerData(type, star) 按星级取属性
//   mTypeTowers         (塔类型 -> 该类型所有星级数据)
//   mBehaviourTypeTowers(索敌行为类型 -> 塔类型列表)，供按索敌策略筛选塔
//   mRogueCost          (塔ID, 当前等级) -> 升到下一级的银币消耗
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
    // 肉鸽模式中，塔从 nowLevel 升到下一级需要的银币。
    // 消耗配置在表里是一个 (起始级, 结束级, 每级消耗) 的三元组，initRogueCostAndSell
    // 会把它展开成"每一级 -> 消耗"的字典；这里直接查表。
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
            // 本地化一致性：塔表里冗余存了一份中文描述，同时用 mLocalLang 指向多语言表。
            // 启动时校验两者一致，防止改了文案只改一处导致显示错乱。
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
                    // 配置区间 1-10 中的"10"指的是"9级升到10级"这一跳，
                    // 其消耗应挂在 key=(塔ID, 9) 上，所以这里 i 要 -1。
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