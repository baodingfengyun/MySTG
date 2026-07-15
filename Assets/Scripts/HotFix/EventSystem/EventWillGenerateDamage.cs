
// 即将计算击中伤害时
public class EventWillGenerateDamage : GameEvent
{
	public CharacterGame mAttacker;
	public CharacterGame mTarget;
	public SkillBullet mBullet;
	public override void resetProperty()
	{
		base.resetProperty();
		mAttacker = null;
		mTarget = null;
		mBullet = null;
	}
}