
// 参数
public class BuffResetLastSkillCDParam : CharacterBuffParamT<BuffResetLastSkillCDParam>
{
	public override void registeAllParam(){}
	public override void check(){}
}

// 重置刚释放过的技能的CD
public class BuffResetLastSkillCD : CharacterBuffT<BuffResetLastSkillCDParam>
{
	public override void enter()
	{
		base.enter();
		mCustomParam.mSkill.resetCD();
	}
}