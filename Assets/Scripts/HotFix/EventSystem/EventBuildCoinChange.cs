
// 局内的建筑币变化
public class EventBuildCoinChange : GameEvent
{
	public int mOldCoin;
	public int mNewCoin;
	public override void resetProperty()
	{
		base.resetProperty();
		mOldCoin = 0;
		mNewCoin = 0;
	}
}