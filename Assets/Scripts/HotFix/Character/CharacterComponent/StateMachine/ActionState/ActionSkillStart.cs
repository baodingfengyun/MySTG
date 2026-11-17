
// 技能三段动作状态之一：起始(start)→持续(continuous)→结束(end)
// 【三段式】一个完整施法动画被拆成 3 个状态，分别在不同时机挂载，配合技能 CD 精确控制节奏。
// 【Param】带 mAnimation(动画参数值)/mSpeed(播放速度)，但当前 enter 里实际只播固定 SKILL 动画，
//   参数字段是预留扩展。
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

// 技能起始状态：进入时播 SKILL 动画（施法前摇），REMOVE_OLD 互斥
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