using System.Collections.Generic;
using UnityEngine;
using static UnityUtility;

// 不推荐这种全局参数的配置方式,请使用MyFramework仓库中的示例形式去配置全局参数
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