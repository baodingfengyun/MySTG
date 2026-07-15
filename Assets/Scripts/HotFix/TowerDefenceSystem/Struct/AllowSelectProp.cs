
// 待选卡列表中的数据
public class AllowSelectProp : ClassObject
{
	public ExcelData mPropData;		// 表格数据
	public bool mUsed;				// 是否已经使用
	public override void resetProperty()
	{
		base.resetProperty();
		mPropData = null;
		mUsed = false;
	}
}