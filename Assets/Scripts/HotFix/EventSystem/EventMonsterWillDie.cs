
// 怪物即将死亡
public class EventMonsterWillDie : GameEvent
{
	public CharacterMonster mMonster;			// 怪物
	public CharacterGame mKiller;				// 杀死怪物的角色
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
		mKiller = null;
	}
}