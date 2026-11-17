using System.Collections.Generic;
using UnityEngine;
using static UnityUtility;

// 全局配置表：以"键(mType) -> 字符串值(mValue)"的散装形式存零星参数。
// 不推荐这种全局参数的配置方式,请使用MyFramework仓库中的示例形式去配置全局参数。
// 实现要点：initList() 把每行读入 mStringValues，再把能转成整数的键额外放进 mIntValues，
// 于是 getIntValue/getStringValue 可按类型取值；取不到的键会 logError 并返回默认值。
// 例如 rogue_add_tower_weight 是一串用 | 分隔的 "起始波|结束波|权重"，
// getRogueAddTowerWeight 解析成区间列表，按当前波次落在哪段返回对应权重（超界取最后一段）。
public class ExcelGlobalConfig : ExcelTableT<EDGlobalConfig>
{
    protected Dictionary<string, string> mStringValues;
    protected Dictionary<string, int> mIntValues;
    protected List<Vector3Int> mRogueAddTowerWeight;
    public override void clearCache()
    {
        base.clearCache();
        mRogueAddTowerWeight = null;
    }
    protected int getIntValue(string name)
    {
        if (mIntValues == null)
        {
            initList();
        }
        if (!mIntValues.TryGetValue(name, out int value))
        {
            logError("找不到全局配置的参数:" + name);
        }
        return value;
    }
    protected string getStringValue(string name)
    {
        if (mStringValues == null)
        {
            initList();
        }
        if (!mStringValues.TryGetValue(name, out string value))
        {
            logError("找不到全局配置的参数:" + name);
        }
        return value;
    }
    public int getRogueAddTowerWeight(int wave)
    {
        if (mRogueAddTowerWeight == null)
        {
            mRogueAddTowerWeight = new();
            string[] infos = getStringValue("rogue_add_tower_weight").split("|");
            foreach (var each in infos)
            {
                mRogueAddTowerWeight.add(each.SToV3I());
            }
        }
        foreach (var each in mRogueAddTowerWeight)
        {
            if (wave >= each.x && wave <= each.y)
            {
                return each.z;
            }
        }
        return mRogueAddTowerWeight[^1].z;
    }
    public int getInitCriticalDamage() { return getIntValue("init_critical_damage"); }
    // 肉鸽塔的种类
    public int getRogueTowerSlotCount() { return getIntValue("rogue_tower_slot_count"); }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void initList()
    {
        mStringValues = new();
        mIntValues = new();
        foreach (EDGlobalConfig data in queryAll())
        {
            mStringValues.Add(data.mType, data.mValue.removeAll('[', ']'));
        }
        foreach (var pair in mStringValues)
        {
            if (int.TryParse(pair.Value, out int value))
            {
                mIntValues.Add(pair.Key, value);
            }
        }
    }
	// auto generate start
	protected override void checkAllDataDefault() {}
	// auto generate end
}