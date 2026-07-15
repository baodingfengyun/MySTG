
// 子弹被消耗(只限于子弹正常击中敌人并销毁时，管理(destroy/clear...)时统一清理的子弹不发送这个事件)
public class EventBulletConsume : GameEvent
{
	public SkillBullet mBullet;
	public override void resetProperty()
	{
		base.resetProperty();
		mBullet = null;
	}
}