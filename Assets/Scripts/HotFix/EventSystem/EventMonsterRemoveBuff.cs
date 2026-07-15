
// 怪物移除buff
public class EventMonsterRemoveBuff : GameEvent
{
	public CharacterMonster mMonster;
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
	}
}