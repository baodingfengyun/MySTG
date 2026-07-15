
public class ExcelAudio : ExcelTableT<EDAudio>
{
    public override void checkAllData()
    {
        foreach (EDAudio item in queryAll())
        {
            checkPath(item.mPath);
        }
    }
	// auto generate start
	protected override void checkAllDataDefault() {}
	// auto generate end
}