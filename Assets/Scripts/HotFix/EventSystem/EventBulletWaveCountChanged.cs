
// 子弹的累计数量发生改变
public class EventBulletWaveCountChanged : GameEvent
{
	public int mCount;
	public override void resetProperty()
	{
		base.resetProperty();
		mCount = 0;
	}
}