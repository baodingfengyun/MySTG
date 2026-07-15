using System;
using UnityEngine;

// 用于显示怪物属性
[Serializable]
public class MonsterDebug : MonoBehaviour
{
	public CharacterMonster mMonster;
	[CustomLabel("血量")]
	public int HP;
	[CustomLabel("最大血量")]
	public int MaxHP;
	[CustomLabel("是否为飞行怪物")]
	public bool Flyable;
	[CustomLabel("是否潜行")]
	public byte IsInvisible;
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
	public void setMonster(CharacterMonster monster)
	{
		mMonster = monster;
	}
	public void Update()
	{
		CharacterMonsterData monsterData = mMonster.getMonsterData();
		HP = monsterData.mHP;
		MaxHP = monsterData.mHP;
		Defence = monsterData.mDefence;
		Attack = monsterData.mAttack + monsterData.mAttackIncrease;
		Flyable = monsterData.mFlyable;
		IsInvisible = monsterData.mIsInvisible;
		Critical = monsterData.mCritical + monsterData.mCriticalIncrease;
		AntiCritical = monsterData.mAntiCritical;
		Evasion = monsterData.mEvasion;
		BeenDamageIncrease = monsterData.mBeenDamageIncrease;
		BeenFireElementDamageIncrease = monsterData.mBeenFireElementDamageIncrease;
		BeenDarkElementDamageIncrease = monsterData.mBeenDarkElementDamageIncrease;
		BeenLightElementDamageIncrease = monsterData.mBeenLightElementDamageIncrease;
		BeenIceElementDamageIncrease = monsterData.mBeenIceElementDamageIncrease;
		BeenPoisonElementDamageIncrease = monsterData.mBeenPoisonElementDamageIncrease;
		BeenLightningElementDamageIncrease = monsterData.mBeenLightningElementDamageIncrease;
		AntiFireElement = monsterData.mAntiFireElement;
		AntiIceElement = monsterData.mAntiIceElement;
		AntiDarkElement = monsterData.mAntiDarkElement;
		AntiLightElement = monsterData.mAntiLightElement;
		AntiPoisonElement = monsterData.mAntiPoisonElement;
		AntiLightningElement = monsterData.mAntiLightningElement;
		BeenBurnDamageIncrease = monsterData.mBeenBurnDamageIncrease;
		BeenPoisoningDamageIncrease = monsterData.mBeenPoisoningDamageIncrease;
		BeenShockedDamageIncrease = monsterData.mBeenShockedDamageIncrease;
		AttackSpeed = monsterData.getAttackSpeed();
		SlowDownIncrease = monsterData.mSlowDownIncrease;
		IncreaseAttackPercent = monsterData.mIncreaseAttackPercent;
		mMP = monsterData.mMP;
		ImmunityElementDebuffDamage = monsterData.mImmunityElementDebuffDamage;
		ImmunityPhysicDamage = monsterData.mImmunityPhysicDamage;
	}
}