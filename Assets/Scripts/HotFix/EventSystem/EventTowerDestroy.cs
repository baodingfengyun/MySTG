
// 防御塔被销毁
public class EventTowerDestroy : GameEvent
{
	public CharacterTower mTower;
	public override void resetProperty()
	{
		base.resetProperty();
		mTower = null;
	}
}