// 放置塔
public class EventTowerPut : GameEvent
{
	public CharacterTower mTower;
	public override void resetProperty()
	{
		base.resetProperty();
		mTower = null;
	}
}