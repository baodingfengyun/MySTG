
// 子弹爆炸
public class EventBulletExplosion : GameEvent
{
	public SkillBullet mBullet;
	public override void resetProperty()
	{
		base.resetProperty();
		mBullet = null;
	}
}