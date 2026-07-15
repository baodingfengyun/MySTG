using static GBR;

// buff细节表,每一条buff的属性参数
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