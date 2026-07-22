using static FrameBaseHotFix;

// 死亡
public class ActionDead : CharacterState
{
	public ActionDead()
	{
		mMutexType = STATE_MUTEX.REMOVE_OLD;
	}
	public override void enter()
	{
		base.enter();
		// 播放死亡特效和死亡动作
		if (mCharacter is CharacterMonster monster)
		{
			monster.getComAvatar().playAnimation((int)MONSTER_ANIMATION.DEAD);
			if (monster.getMonsterData().mTableData.mDieAnimationLength > 0.0f)
			{
				mStateTime = monster.getMonsterData().mTableData.mDieAnimationLength + 0.5f;
			}
			else
			{
				// 死亡后没有死亡动作就马上会被销毁
				mStateTime = 0.0f;
			}
			mEffectManager.playEffectAsyncAtPosition(EDEffect.MONSTER_DEAD.mPath, mCharacter.getPosition(), 1.0f, true);
		}
		else
		{
			mStateTime = 0.0f;
		}
	}
}