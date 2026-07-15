
// 即将释放技能
public class EventPreFireSkill : GameEvent
{
	public CharacterSkill mSkill;
	public override void resetProperty()
	{
		base.resetProperty();
		mSkill = null;
	}
}