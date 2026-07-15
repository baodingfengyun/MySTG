
// 角色被击中,关联的角色ID为被击中的角色
public class EventCharacterBeenHit : GameEvent
{
	public CharacterGame mAttacker;	// 攻击者
	public CharacterGame mTarget;	// 命中的目标
	public SkillBullet mBullet;		// 攻击的子弹
	public int mDamage;				// 伤害值
	public bool mCritical;			// 是否为暴击
	public bool mMiss;				// 是否miss
	public override void resetProperty()
	{
		base.resetProperty();
		mAttacker = null;
		mTarget = null;
		mBullet = null;
		mDamage = 0;
		mCritical = false;
		mMiss = false;
	}
}