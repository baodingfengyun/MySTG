
// 怪物死亡
public class EventMonsterDie : GameEvent
{
	public CharacterMonster mMonster;
	public CharacterGame mKiller;
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
		mKiller = null;
	}
}