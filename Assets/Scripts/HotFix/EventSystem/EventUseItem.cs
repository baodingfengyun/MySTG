
// 在战斗中使用或者放置战斗道具
public class EventUseItem : GameEvent
{
	public int mID;
	public int mCount;
	public override void resetProperty()
	{
		base.resetProperty();
		mID = 0;
		mCount = 0;
	}
}