
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