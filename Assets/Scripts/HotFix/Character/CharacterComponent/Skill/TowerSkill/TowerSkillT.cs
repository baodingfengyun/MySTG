
// 防御塔技能,带自定义参数
public class TowerSkillT<T> : TowerSkill where T : ParamCopyable, new()
{
	protected T mCustomParam = new();
	public override void resetProperty()
	{
		base.resetProperty();
		mCustomParam?.resetProperty();
	}
	public override void initData(EDTowerSkill skillData, ParamCopyable paramTemplate)
	{
		base.initData(skillData, paramTemplate);
		mCustomParam?.initFromCopy(paramTemplate);
	}
}