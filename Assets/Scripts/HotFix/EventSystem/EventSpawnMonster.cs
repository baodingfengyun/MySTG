
// 添加怪物
public class EventSpawnMonster : GameEvent
{
	public CharacterMonster mMonster;
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
	}
}