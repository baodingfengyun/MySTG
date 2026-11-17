
// 地图配置表：一张地图的静态信息（场景名、格子方向、出怪口列表等）。
// checkAllData 校验场景资源路径存在；具体传送门在 ExcelMapPortal 按地图ID关联。
public class ExcelMapConfig : ExcelTableT<EDMapConfig>
{
    public override void checkAllData()
    {
        foreach (EDMapConfig item in queryAll())
        {
            checkPath(item.mSceneName);
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDMapConfig item in queryAll())
		{
			if (!item.mSceneName.isEmpty())
			{
				checkPath(item.mSceneName);
			}
			checkEnum(item.mGridDirection, "mGridDirection", item.mID);
		}
	}
	// auto generate end
}