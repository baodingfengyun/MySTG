using System;
using System.Collections.Generic;

// 怪物技能注册
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