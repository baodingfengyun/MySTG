
// 眩晕动作状态：进入时给怪物播 VERTIGO 动画
// 【注意】眩晕的行为禁用(不能动/不能攻击)不是本类做的，而是眩晕 Buff 挂上
//   StateGroupNotAllowMove / NotAllowAttack 组，由移动/攻击组件查询后禁用。
//   本类只负责"视觉表现"，注释原文写"走路"是笔误。
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