
// 防御塔即将击中怪物
public class EventWillHitCharacter : GameEvent
{
	public CharacterGame mAttacker;
	public CharacterGame mTarget;
	public SkillBullet mBullet;
	public INT mDamage;
	public HP_DELTA mDeltaType;
	public override void resetProperty()
	{
		base.resetProperty();
		mAttacker = null;
		mTarget = null;
		mBullet = null;
		mDamage = null;
		mDeltaType = HP_DELTA.NONE;
	}
}