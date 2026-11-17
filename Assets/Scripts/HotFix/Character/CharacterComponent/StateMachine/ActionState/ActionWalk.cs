
// 走路动作状态：进入时给怪物播 WALK 动画
// 【互斥】构造里设 mMutexType=REMOVE_OLD——加此状态时顶掉同组(StateGroupAction)的旧动作，
//   保证走路/站立/施法等动画状态不会叠加。本状态由移动组件驱动挂载/移除。
public class ActionWalk : CharacterState
{
	public ActionWalk()
	{
		mMutexType = STATE_MUTEX.REMOVE_OLD;
	}
	public override void enter()
	{
		base.enter();
		// 播放走路动作
		if (mCharacter is CharacterMonster monster)
		{
			monster.getComAvatar().playAnimation((int)MONSTER_ANIMATION.WALK);
		}
	}
}