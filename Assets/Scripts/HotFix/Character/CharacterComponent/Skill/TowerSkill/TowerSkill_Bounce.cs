
public class TowerSkill_Bounce : TowerSkill
{
	protected int mBounceTimesIncrease;						// 最大弹跳次数调整
	protected float mBounceDamageIncrease;					// 弹跳的伤害衰减调整
	public override void resetProperty()
	{
		base.resetProperty();
		mBounceTimesIncrease = 0;
		mBounceDamageIncrease = 0.0f;
	}
	public void addBounceTimesIncrease(int value) { mBounceTimesIncrease += value; }
	public void addBounceDamageIncrease(float value) { mBounceDamageIncrease += value; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void bounceWillFire(SkillBulletTrackBounce trackBounce)
	{
		trackBounce.increaseBounceTimesMax(mBounceTimesIncrease);
		trackBounce.increaseBounceDamagePercent(mBounceDamageIncrease);
	}
}