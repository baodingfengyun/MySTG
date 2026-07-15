using System;
using UnityEngine;

// 用于显示塔属性
[Serializable]
public class TowerDebug : MonoBehaviour
{
	public CharacterTower mTower;
	[CustomLabel("当前攻击范围")]
	public float Range;
	[CustomLabel("升级增加伤害")]
	public int LevelIncreasedAttack;
	[CustomLabel("升级倒计时")]
	public float LevelUpdateTimer;
	[CustomLabel("原始范围")]
	public float OriginRange;
	[CustomLabel("格子下标")]
	public int GridIndex;
	[CustomLabel("塔类型")]
	public TOWER_TYPE TowerType;
	[CustomLabel("防御力")]
	public int Defence;
	[CustomLabel("攻击力")]
	public int Attack;
	[CustomLabel("攻击力百分比增幅")]
	public float IncreaseAttackPercent;
	[CustomLabel("暴击率")]
	public float Critical;
	[CustomLabel("暴击抗性")]
	public float AntiCritical;
	[CustomLabel("暴击伤害")]
	public float CriticalDamage;
	[CustomLabel("闪避率")]
	public float Evasion;
	[CustomLabel("被攻击伤害的增幅")]
	public float BeenDamageIncrease;
	[CustomLabel("被火属性攻击伤害的增幅")]
	public float BeenFireElementDamageIncrease;
	[CustomLabel("被暗属性攻击伤害的增幅")]
	public float BeenDarkElementDamageIncrease;
	[CustomLabel("被光属性攻击伤害的增幅")]
	public float BeenLightElementDamageIncrease;
	[CustomLabel("被冰属性攻击伤害的增幅")]
	public float BeenIceElementDamageIncrease;
	[CustomLabel("被毒属性攻击伤害的增幅")]
	public float BeenPoisonElementDamageIncrease;
	[CustomLabel("被电属性攻击伤害的增幅")]
	public float BeenLightningElementDamageIncrease;
	[CustomLabel("火属性抗性")]
	public float AntiFireElement;
	[CustomLabel("冰属性抗性")]
	public float AntiIceElement;
	[CustomLabel("暗属性抗性")]
	public float AntiDarkElement;
	[CustomLabel("光属性抗性")]
	public float AntiLightElement;
	[CustomLabel("毒属性抗性")]
	public float AntiPoisonElement;
	[CustomLabel("电属性抗性")]
	public float AntiLightningElement;
	[CustomLabel("被燃烧伤害增幅")]
	public float BeenBurnDamageIncrease;
	[CustomLabel("被中毒伤害增幅")]
	public float BeenPoisoningDamageIncrease;
	[CustomLabel("被感电伤害增幅")]
	public float BeenShockedDamageIncrease;
	[CustomLabel("攻击速度")]
	public float AttackSpeed;
	[CustomLabel("被减速增幅")]
	public float SlowDownIncrease;
	[CustomLabel("魔法值")]
	public int mMP;
	[CustomLabel("是否免疫debuff伤害")]
	public int ImmunityElementDebuffDamage;
	[CustomLabel("是否免疫物理伤害")]
	public int ImmunityPhysicDamage;
	public void setTower(CharacterTower tower)
	{
		mTower = tower;
	}
	public void Update()
	{
		CharacterTowerData towerData = mTower.getTowerData();
		Range = towerData.mOriginRange;
		LevelIncreasedAttack = towerData.mLevelIncreasedAttack;
		LevelUpdateTimer = towerData.mLevelUpdateTimer;
		OriginRange = towerData.mOriginRange;
		GridIndex = towerData.mGridIndex;
		TowerType = towerData.mTableData?.mType ?? TOWER_TYPE.NONE;
		Defence = towerData.mDefence;
		Attack = towerData.mAttack + towerData.mAttackIncrease;
		Critical = towerData.mCritical + towerData.mCriticalIncrease;
		AntiCritical = towerData.mAntiCritical;
		CriticalDamage = towerData.mCriticalDamage + towerData.mCriticalDamageIncrease;
		Evasion = towerData.mEvasion;
		BeenDamageIncrease = towerData.mBeenDamageIncrease;
		BeenFireElementDamageIncrease = towerData.mBeenFireElementDamageIncrease;
		BeenDarkElementDamageIncrease = towerData.mBeenDarkElementDamageIncrease;
		BeenLightElementDamageIncrease = towerData.mBeenLightElementDamageIncrease;
		BeenIceElementDamageIncrease = towerData.mBeenIceElementDamageIncrease;
		BeenPoisonElementDamageIncrease = towerData.mBeenPoisonElementDamageIncrease;
		BeenLightningElementDamageIncrease = towerData.mBeenLightningElementDamageIncrease;
		AntiFireElement = towerData.mAntiFireElement;
		AntiIceElement = towerData.mAntiIceElement;
		AntiDarkElement = towerData.mAntiDarkElement;
		AntiLightElement = towerData.mAntiLightElement;
		AntiPoisonElement = towerData.mAntiPoisonElement;
		AntiLightningElement = towerData.mAntiLightningElement;
		BeenBurnDamageIncrease = towerData.mBeenBurnDamageIncrease;
		BeenPoisoningDamageIncrease = towerData.mBeenPoisoningDamageIncrease;
		BeenShockedDamageIncrease = towerData.mBeenShockedDamageIncrease;
		AttackSpeed = towerData.getAttackSpeed();
		SlowDownIncrease = towerData.mSlowDownIncrease;
		IncreaseAttackPercent = towerData.mIncreaseAttackPercent;
		mMP = towerData.mMP;
		ImmunityElementDebuffDamage = towerData.mImmunityElementDebuffDamage;
		ImmunityPhysicDamage = towerData.mImmunityPhysicDamage;
	}
}