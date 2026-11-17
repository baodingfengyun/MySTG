
// 特效资源表：集中登记粒子/动画特效的资源路径，其他表用特效ID引用。
// checkAllData 校验每个特效的 mPath 资源确实存在。
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