
// 塔的等级发生变化
public class EventTowerLevelChange : GameEvent
{
	public CharacterTower mTower;
	public int mOldLevel;
	public int mNewLevel;
	public override void resetProperty()
	{
		base.resetProperty();
		mTower = null;
		mOldLevel = 0;
		mNewLevel = 0;
	}
}