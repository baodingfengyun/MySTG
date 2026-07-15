
// 怪物所在格子改变
public class EventMonsterGridChange : GameEvent
{
	public CharacterMonster mMonster;
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
	}
}