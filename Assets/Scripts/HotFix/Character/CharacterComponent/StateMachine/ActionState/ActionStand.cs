
// 站立动作状态：进入时给怪物播 STAND 动画；与走路/施法同属 StateGroupAction 互斥
public class ActionStand : CharacterState
{
	public ActionStand()
	{
		mMutexType = STATE_MUTEX.REMOVE_OLD;
	}
	public override void enter()
	{
		base.enter();
		// 播放站立动作
		if (mCharacter is CharacterMonster monster)
		{
			monster.getComAvatar().playAnimation((int)MONSTER_ANIMATION.STAND);
		}
	}
}