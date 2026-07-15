
// 参数
public class ActionSkillStartParam : StateParam
{
	public int mAnimation;		// 动画状态机的参数值
	public float mSpeed;		// 播放速度
	public override void resetProperty()
	{
		base.resetProperty();
		mAnimation = 0;
		mSpeed = 1.0f;
	}
}

// 技能起始状态
public class ActionSkillStart : CharacterStateT<ActionSkillStartParam>
{
	public ActionSkillStart()
	{
		mMutexType = STATE_MUTEX.REMOVE_OLD;
	}
	public override void enter()
	{
		base.enter();
		// 播放释放技能动作
		if (mCharacter is CharacterMonster monster)
		{
			monster.getComAvatar().playAnimation((int)MONSTER_ANIMATION.SKILL);
		}
	}
}