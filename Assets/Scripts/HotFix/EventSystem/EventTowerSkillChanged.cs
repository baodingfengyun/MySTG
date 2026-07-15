
// 防御塔的技能有改变,比如替换了技能ID
public class EventTowerSkillChanged : GameEvent
{
	public CharacterTower mTower;
	public override void resetProperty()
	{
		base.resetProperty();
		mTower = null;
	}
}