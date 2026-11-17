using System.Collections.Generic;

// 多语言表：以中文文案为主键，存各语言的翻译。多数表只冗余存中文+一个 LocID，
// 真正的多语言文案从这里按 ID/中文反查。getData(中文) 建"中文 -> 行"的反查字典（懒加载）。
// 各表的 checkAllData 会比对冗余中文与本表 mChinese 是否一致，防止文案漂移。
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