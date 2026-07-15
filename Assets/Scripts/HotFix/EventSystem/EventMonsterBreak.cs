// 怪物突破了防线进入基地
public class EventMonsterBreak : GameEvent
{
	public CharacterMonster mMonster;
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
	}
}