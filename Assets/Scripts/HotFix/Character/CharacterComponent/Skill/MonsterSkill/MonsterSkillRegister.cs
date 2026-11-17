using System;
using System.Collections.Generic;

// 怪物技能反射注册表：只登记"有特殊行为"的技能，其余用默认类
// 【设计】表里存技能 ID，创建技能时 getMonsterType(id) 查表；查不到的走默认
//   (主动→MonsterActiveSkill / 被动→MonsterPassiveSkill，由上层按 mIsPassive 决定)。
//   目前只注册了 2 个自爆技能(15/23)，因为它们要重写 onTrigger，无法用默认被动类。
//   这和 TowerRegister / StateRegister 是同一套"配表 + 特例注册"思路。
public class MonsterSkillRegister
{
	protected static Dictionary<int, Type> mSkillTypeList = new();
	// 只需要注册特殊的技能即可
	public static void registeAll()
	{
		registe<MonsterPassiveSkill_SelfExplosion>(15);
		registe<MonsterPassiveSkill_SelfExplosion>(23);
	}
	public static Type getMonsterType(int id) { return mSkillTypeList.get(id); }
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registe<T>(int id) where T : MonsterSkillBase
	{
		mSkillTypeList.Add(id, typeof(T));
	}
}