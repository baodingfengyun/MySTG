
public class ExcelChapter : ExcelTableT<EDChapter>
{
    // auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDChapter item in queryAll())
		{
			if (!item.mImage.isEmpty())
			{
				checkPath(item.mImage);
			}
		}
	}
	// auto generate end
	public EDChapter getFirstChapter() { return mDataList.first(); }
}