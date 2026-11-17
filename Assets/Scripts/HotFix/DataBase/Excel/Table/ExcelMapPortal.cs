using System.Collections.Generic;

// 传送门表：地图的出口/终点配置（一个地图可有多个传送门）。
// getMapPortals 按地图ID分组返回，缓存 mMapPortals 懒加载。
// 注意：clearCache 对 mMapPortals 调用了 Clear() 而非置 null，若缓存尚未初始化
// （mMapPortals 仍为 null）就清理会触发空引用——疑似隐患，此处仅标注不改逻辑。
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