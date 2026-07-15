
// 怪物添加buff
public class EventMonsterAddBuff : GameEvent
{
	public CharacterMonster mMonster;
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
	}
}