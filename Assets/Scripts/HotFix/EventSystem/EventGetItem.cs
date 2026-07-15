
// 获得道具
public class EventGetItem : GameEvent
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