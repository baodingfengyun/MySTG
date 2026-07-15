
// 释放技能以后
public class EventPostFireSkill : GameEvent
{
	public CharacterSkill mSkill;
	public override void resetProperty()
	{
		base.resetProperty();
		mSkill = null;
	}
}