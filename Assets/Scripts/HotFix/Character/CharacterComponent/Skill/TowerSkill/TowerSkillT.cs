
// 防御塔技能,带自定义参数（专属塔技能的"参数化"底座）
// 有些塔的技能需要读 TowerSkill 表里 Param0/Param1 之外的专属数值（如震荡塔的最大子弹数、
// 投石机的最小射程、火炮的抛射高度），这些值在表里写成字符串参数，由 T 类型的 ParamCopyable
// 子类解析（各 SkillCustomParam_XXX.registeAllParam 定义"第几个参数转成 float/int"）。
// 机制：initData 时把注册表里的 paramTemplate 深拷贝到 mCustomParam，之后本塔技能子类
// 直接读 mCustomParam.mXxx。用泛型是为了让每个塔技能持有强类型参数，避免到处 cast。
// 不需要专属参数的塔（十字弓/霰弹/天空之矛等）直接用基类 TowerSkill，不经过这一层。
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