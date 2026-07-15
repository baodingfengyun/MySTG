
// 塔添加buff
public class EventTowerAddBuff : GameEvent
{
	public CharacterTower mTower;
	public override void resetProperty()
	{
		base.resetProperty();
		mTower = null;
	}
}