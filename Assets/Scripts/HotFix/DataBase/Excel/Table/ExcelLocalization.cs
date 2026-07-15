using System.Collections.Generic;

// 多语言表
public class ExcelLocalization : ExcelTableT<EDLocalization>
{
    protected Dictionary<string, EDLocalization> mDataDict; // 数据列表
    public EDLocalization getData(string chineseStr)
    {
        if (mDataDict == null)
        {
            mDataDict = new();
            foreach (EDLocalization data in queryAll())
            {
                mDataDict.Add(data.mChinese, data);
            }
        }
        return mDataDict.get(chineseStr);
    }
    public override void clearCache()
    {
        mDataDict = null;
    }
	// auto generate start
	protected override void checkAllDataDefault() {}
	// auto generate end
}