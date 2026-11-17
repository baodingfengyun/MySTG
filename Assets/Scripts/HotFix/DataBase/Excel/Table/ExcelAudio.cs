
// 音频资源表：集中登记音效/音乐的资源路径，关卡/技能/塔表用音频ID引用。
// checkAllData 校验每个音频的 mPath 资源确实存在。
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