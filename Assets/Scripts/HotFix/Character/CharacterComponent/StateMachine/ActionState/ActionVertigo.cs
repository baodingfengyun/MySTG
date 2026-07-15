
// 走路
public class ActionVertigo : CharacterState
{
	public ActionVertigo()
	{
		mMutexType = STATE_MUTEX.REMOVE_OLD;
	}
	public override void enter()
	{
		base.enter();
		// 播放眩晕动作
		if (mCharacter is CharacterMonster monster)
		{
			monster.getComAvatar().playAnimation((int)MONSTER_ANIMATION.VERTIGO);
		}
	}
}