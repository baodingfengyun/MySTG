
// 子弹即将被发射时,此时子弹已经初始化完毕
public class EventBulletWillFire : GameEvent
{
	public SkillBullet mBullet;
	public override void resetProperty()
	{
		base.resetProperty();
		mBullet = null;
	}
}