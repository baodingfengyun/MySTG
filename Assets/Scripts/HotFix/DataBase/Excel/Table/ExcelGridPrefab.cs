using System.Collections.Generic;

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