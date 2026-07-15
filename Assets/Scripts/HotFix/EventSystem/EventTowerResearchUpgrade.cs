
// 防御塔研究所塔的升级
public class EventTowerResearchUpgrade : GameEvent
{
	public TOWER_TYPE mType;
	public override void resetProperty()
	{
		base.resetProperty();
		mType = TOWER_TYPE.NONE;
	}
}