
// 怪物即将死亡
public class EventMonsterWillDie : GameEvent
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