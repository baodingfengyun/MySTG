
// 塔移除buff
public class EventTowerRemoveBuff : GameEvent
{
	public CharacterTower mTower;
	public override void resetProperty()
	{
		base.resetProperty();
		mTower = null;
	}
}