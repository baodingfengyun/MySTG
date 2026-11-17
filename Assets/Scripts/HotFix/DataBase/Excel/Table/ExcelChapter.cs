
// 章节表：关卡的上一级分组单位，主要存章节封面图等展示信息。
// getFirstChapter 取第一张章节作为默认入口；封面图路径在生成区校验。
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