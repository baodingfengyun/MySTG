using System.Collections.Generic;

public class ExcelMapPortal : ExcelTableT<EDMapPortal>
{
    protected Dictionary<int, List<EDMapPortal>> mMapPortals;
    public override void clearCache()
    {
        base.clearCache();
        mMapPortals.Clear();
    }
    public List<EDMapPortal> getMapPortals(int mapConfigID)
    {
        if (mMapPortals == null)
        {
            mMapPortals = new();
            foreach (EDMapPortal data in queryAll())
            {
                mMapPortals.getOrAddNew(data.mMap).Add(data);
            }
        }
        return mMapPortals.get(mapConfigID);
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDMapPortal item in queryAll())
		{
			checkEnum(item.mEndRule, "mEndRule", item.mID);
		}
	}
	// auto generate end
}