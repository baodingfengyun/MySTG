using static GBR;

// buff细节表,每一条buff的属性参数。
// 塔/怪/技能/子弹表存的是"buff细节ID"，运行时据此来这里取具体参数（层数/时长/数值）。
// checkAllData 校验每条细节指向的 mBuffTypeID 在 Buff 主表中存在。
public class ExcelBuffDetail : ExcelTableT<EDBuffDetail>
{
    public override void checkAllData()
    {
        foreach (EDBuffDetail item in queryAll())
        {
            mExcelBuff.checkData(item.mBuffTypeID, item.mID, this);
        }
    }
	// auto generate start
	protected override void checkAllDataDefault() {}
	// auto generate end
}