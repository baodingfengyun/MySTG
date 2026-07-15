using UnityEngine;
using static MathUtility;
using static GDR;

// 战斗角色基类的数据
public class CharacterGameData
{
	public float mCritical;								// 暴击率
	public float mCriticalIncrease;						// 暴击率增幅绝对值
	public float mAntiCritical;                         // 暴击抗性
	public float mCriticalDamage;						// 暴击伤害
	public float mCriticalDamageIncrease;				// 暴击伤害增幅值
	public float mEvasion;                              // 闪避率
	public float mBeenDamageIncrease;					// 受到的伤害提升百分比
	public float mBeenFireElementDamageIncrease;		// 受到的火属性伤害提升百分比
	public float mBeenDarkElementDamageIncrease;		// 受到的暗属性伤害提升百分比
	public float mBeenLightElementDamageIncrease;		// 受到的光属性伤害提升百分比
	public float mBeenIceElementDamageIncrease;			// 受到的冰属性伤害提升百分比
	public float mBeenPoisonElementDamageIncrease;		// 受到的毒属性伤害提升百分比
	public float mBeenLightningElementDamageIncrease;   // 受到的电属性伤害提升百分比
	public float mAntiFireElement;						// 火属性抗性,降低受到的火属性伤害
	public float mAntiIceElement;                       // 冰属性抗性,降低受到的冰属性伤害
	public float mAntiDarkElement;                      // 暗属性抗性,降低受到的暗属性伤害
	public float mAntiLightElement;                     // 光属性抗性,降低受到的光属性伤害
	public float mAntiPoisonElement;                    // 毒属性抗性,降低受到的毒属性伤害
	public float mAntiLightningElement;                 // 电属性抗性,降低受到的电属性伤害
	public float mBeenBurnDamageIncrease;				// 受到的燃烧伤害增加
	public float mBeenPoisoningDamageIncrease;			// 受到的中毒伤害增加
	public float mBeenShockedDamageIncrease;			// 受到的感电伤害增加
	protected float mAttackSpeed;                       // 自身的攻击速度
	public float mSlowDownIncrease;						// 受到的减速效果增幅
	public float mIncreaseAttackPercent;                // 攻击力提升百分比
	public float mParalysisCD;                          // 麻痹CD,在CD内时不会再次被麻痹
	public float mFreezeCD;                             // 冰冻CD,在CD内时不会再次被冰冻
	public float mExplosionRangeIncrease;               // 爆炸范围提升的百分比,只有会爆炸的子弹才能生效
	public float mExplosionRangeIncreaseByFlyDis;       // 爆炸范围根据实际飞行距离提升的百分比,只有会爆炸的子弹才能生效
	public float mDamageIncreaseByFlyDis;				// 子弹的伤害根据实际飞行距离提升的百分比
	public float mDamageIncrease;                       // 最终伤害的增幅
	public float mBulletSpeedIncrease;					// 子弹飞行速度的增幅百分比
	public float mRangeIncreasePercent;					// 范围增幅倍率
	public float mRangeIncreaseValue;					// 范围增幅数值
	public float mAttackSpeedIncreasePercent;			// 攻速增幅倍率
	public int mAttack;									// 自身的攻击力,实际击中敌人时会跟子弹的攻击力一起计算
	public int mAttackIncrease;							// 攻击力增加值
	public int mDefence;								// 防御力
	public int mMP;										// 释放技能时消耗的魔法值
	public int mImmunityElementDebuffDamage;			// 免疫所有元素debuff的伤害
	public int mImmunityPhysicDamage;					// 免疫所有物理伤害
	public int mAlwaysCriticalHit;                      // 命中时一定会产生暴击,忽略暴击率和被攻击方的抗暴率
	public int mFireImprintCount;						// 火焰印记的数量
	public Vector3 mBulletScale;						// 子弹缩放
	public float mIncreaseFlyDis;						// 子弹飞行距离增加,对塔射程(索敌范围)不影响
	public virtual void resetProperty()
	{
		mCritical = 0.0f;
		mCriticalIncrease = 0.0f;
		mAntiCritical = 0.0f;
		mCriticalDamage = 0.0f;
		mCriticalDamageIncrease = 0.0f;
		mEvasion = 0.0f;
		mBeenDamageIncrease = 0.0f;
		mBeenFireElementDamageIncrease = 0.0f;
		mBeenDarkElementDamageIncrease = 0.0f;
		mBeenLightElementDamageIncrease = 0.0f;
		mBeenIceElementDamageIncrease = 0.0f;
		mBeenPoisonElementDamageIncrease = 0.0f;
		mBeenLightningElementDamageIncrease = 0.0f;
		mAntiFireElement = 0.0f;
		mAntiIceElement = 0.0f;
		mAntiDarkElement = 0.0f;
		mAntiLightElement = 0.0f;
		mAntiPoisonElement = 0.0f;
		mAntiLightningElement = 0.0f;
		mBeenBurnDamageIncrease = 0.0f;
		mBeenPoisoningDamageIncrease = 0.0f;
		mBeenShockedDamageIncrease = 0.0f;
		mAttackSpeed = 0.0f;
		mSlowDownIncrease = 0.0f;
		mIncreaseAttackPercent = 0.0f;
		mParalysisCD = 0.0f;
		mFreezeCD = 0.0f;
		mExplosionRangeIncrease = 0.0f;
		mExplosionRangeIncreaseByFlyDis = 0.0f;
		mDamageIncreaseByFlyDis = 0.0f;
		mDamageIncrease = 0.0f;
		mBulletSpeedIncrease = 0.0f;
		mRangeIncreasePercent = 0.0f;
		mRangeIncreaseValue = 0.0f;
		mAttackSpeedIncreasePercent = 0.0f;
		mAttack = 0;
		mAttackIncrease = 0;
		mDefence = 0;
		mMP = 0;
		mImmunityElementDebuffDamage = 0;
		mImmunityPhysicDamage = 0;
		mAlwaysCriticalHit = 0;
		mFireImprintCount = 0;
		mBulletScale = Vector3.zero;
		mIncreaseFlyDis = 0.0f;
	}
	public float getCriticalDamage() { return mCriticalDamage + mCriticalDamageIncrease; }
	public float getFinalCD(float cd) { return divide(cd, (mAttackSpeed + 1.0f) * (mAttackSpeedIncreasePercent + 1.0f)); }
	public int getAttack() { return round((mAttack + mAttackIncrease) * (1.0f + mIncreaseAttackPercent)); }
	public float getCritical() { return mCritical + mCriticalIncrease; }
	public float getAttackSpeed() { return mAttackSpeed; }
	public void setAttackSpeed(float value) { mAttackSpeed = clampMin(value, ATTACK_SPEED_MIN); }
	public void addAttackSpeed(float value) { setAttackSpeed(mAttackSpeed + value); }
	public void removeAttackSpeed(float value) { setAttackSpeed(mAttackSpeed - value); }
	public void addBulletScale(float value) { mBulletScale += new Vector3(value, value, value); }
	public void removeBulletScale(float value) { mBulletScale -= new Vector3(value, value, value); }
	public void addIncreaseFlyDis(float value) { mIncreaseFlyDis += value; }
	public void removeIncreaseFlyDis(float value) { mIncreaseFlyDis -= value; }
}