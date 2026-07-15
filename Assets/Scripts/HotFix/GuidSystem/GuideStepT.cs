
// 引导步骤,带自定义参数
public class GuideStepT<T> : GuideStep where T : ParamBase
{
	protected T mCustomParam;    // 参数对象
	public override void init(EDGuide data, ParamBase paramTemplate)
	{
		base.init(data, paramTemplate);
		mCustomParam = paramTemplate as T;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mCustomParam = null;
	}
}