// 弹跳类塔技能的公共基类：为"追踪弹跳子弹"(SkillBulletTrackBounce)提供数值增益钩子
// 【定位】本身不实现 fireAllBullet,只维护两个可由 Buff/词条叠加的增益字段：
//   mBounceTimesIncrease —— 最大弹跳次数 +N(多弹几个怪)
//   mBounceDamageIncrease —— 每次弹跳的伤害衰减比例修正(让第 N 跳不那么弱)
// 【用法】子类在发射子弹的 mWillFireBullet 回调里调 bounceWillFire()，
//   把这两个增益写进具体子弹；防空飞弹塔、飞镖发射器都继承它。
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
	// 把技能层累积的弹跳增益写进这一发子弹（发射瞬间调用一次）
	protected void bounceWillFire(SkillBulletTrackBounce trackBounce)
	{
		trackBounce.increaseBounceTimesMax(mBounceTimesIncrease);
		trackBounce.increaseBounceDamagePercent(mBounceDamageIncrease);
	}
}