
// 参数
public class BuffDisableSkillParam : CharacterBuffParamT<BuffDisableSkillParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 不允许释放技能
public class BuffDisableSkill : CharacterBuffT<BuffDisableSkillParam>
{
	public BuffDisableSkill()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
}