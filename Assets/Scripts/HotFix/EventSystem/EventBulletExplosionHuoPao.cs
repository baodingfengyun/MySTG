
// 火炮子弹爆炸(火炮下来的那发子弹才触发)
public class EventBulletExplosionHuoPao : GameEvent
{
	public SkillBullet mBullet;
	public override void resetProperty()
	{
		base.resetProperty();
		mBullet = null;
	}
}