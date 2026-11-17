using System.Collections.Generic;

// 地格外观表：地图格子按状态(GRID_STATE，如可建/不可建/路径)和主题(theme)配多套 prefab+材质，
// 建图时 getRandomPrefab 从对应分组里随机挑一个，让同类格子外观有变化。
// 分组缓存 mGridPrefabDict = 格子状态 -> 主题 -> prefab列表，懒加载 + clearCache 置空。
public class ExcelGridPrefab : ExcelTableT<EDGridPrefab>
{
    public static Dictionary<GRID_STATE, Dictionary<int, List<EDGridPrefab>>> mGridPrefabDict;
    public EDGridPrefab getRandomPrefab(GRID_STATE state, int theme)
    {
        if (mGridPrefabDict == null)
        {
            mGridPrefabDict = new();
            foreach (EDGridPrefab item in queryAll())
            {
                mGridPrefabDict.getOrAddNew(item.mGridState).getOrAddNew(item.mTheme).Add(item);
            }
        }

        if (mGridPrefabDict.TryGetValue(state, out var themeInfos) && themeInfos.TryGetValue(theme, out var infos))
        {
            return infos.random();
        }
        return null;
    }
    public override void checkAllData()
    {
        foreach (EDGridPrefab item in queryAll())
        {
            checkPath(item.mPrefab);
            checkPath(item.mMaterial);
        }
    }
    public override void clearCache()
    {
        mGridPrefabDict = null;
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDGridPrefab item in queryAll())
		{
			checkEnum(item.mGridState, "mGridState", item.mID);
		}
	}
	// auto generate end
}