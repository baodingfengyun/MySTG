
// 技能的子弹基类
public class SkillBulletT<T> : SkillBullet where T : ParamCopyable, new()
{
	protected T mCustomParam = new();           // 用于解析子弹自定义的参数
	public override void initData(EDSkillBullet data, ParamCopyable paramTemplate)
	{
		base.initData(data, paramTemplate);
		mCustomParam.initFromCopy(paramTemplate);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mCustomParam.resetProperty();
	}
}