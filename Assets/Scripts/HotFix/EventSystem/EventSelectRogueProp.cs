
// 选择一个肉鸽词条
public class EventSelectRogueProp : GameEvent
{
	public EDTowerTalent mData;
	public override void resetProperty()
	{
		base.resetProperty();
		mData = null;
	}
}