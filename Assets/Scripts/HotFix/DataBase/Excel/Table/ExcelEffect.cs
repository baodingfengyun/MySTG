
public class ExcelEffect : ExcelTableT<EDEffect>
{
    public override void checkAllData()
    {
        foreach (EDEffect item in queryAll())
        {
            checkPath(item.mPath);
        }
    }
	// auto generate start
	protected override void checkAllDataDefault() {}
	// auto generate end
}