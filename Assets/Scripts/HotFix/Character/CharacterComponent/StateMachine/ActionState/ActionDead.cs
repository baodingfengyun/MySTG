using static FrameBaseHotFix;

// 死亡状态：播放死亡动画 + 死亡特效，并用 mStateTime 控制"死多久后真正销毁"
// 【延迟销毁】若配了 mDieAnimationLength(死亡动画时长)，则状态持续 时长+0.5s，
//   状态机到期自动移除 → BattleModeBase 检测状态结束才销毁怪物；没配动画则 mStateTime=0 立即销毁。
//   这段延迟窗口是给"死亡时触发的 Buff/亡语"留表现时间的。
// 【互斥】REMOVE_OLD，顶掉其他动作状态，保证死亡动画不被走路/施法打断。
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