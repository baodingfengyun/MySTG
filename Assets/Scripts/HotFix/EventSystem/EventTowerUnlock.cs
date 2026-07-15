
// 防御塔解锁
public class EventTowerUnlock : GameEvent
{
	public TOWER_TYPE mTowerType;
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerType = TOWER_TYPE.NONE;
	}
}