using System;
using System.Collections.Generic;
using static UnityUtility;
using static FrameUtility;
using static GBR;

// 子弹伤害修改器注册
public class BulletDamageModifierRegister
{
	protected static Dictionary<BULLET_DAMAGE_MODIFIER, Type> mTypeList = new();
	protected static Dictionary<int, BulletDamageModifier> mModifierList = new();
	public static void registerAll()
	{
		register<ModifierAnyDebuffIncreaseDamage>(BULLET_DAMAGE_MODIFIER.ANY_DEBUFF_INCREASE_DAMAGE);
		register<ModifierWalkMonsterDecreaseDamage>(BULLET_DAMAGE_MODIFIER.WALK_MONSTER_DECREASE_DAMAGE);
		register<ModifierFlyMonsterDecreaseDamage>(BULLET_DAMAGE_MODIFIER.FLY_MONSTER_DECREASE_DAMAGE);
	}
	public static BulletDamageModifier getModifier(int id)
	{
		if (!mModifierList.TryGetValue(id, out var modifier))
		{
			EDBulletDamageModifier data = mExcelBulletDamageModifier.query(id);
			if (data == null)
			{
				return null;
			}
			Type classType = mTypeList.get(data.mType);
			if (classType == null)
			{
				logError("子弹修改器未注册, Type:" + (int)data.mType);
			}
			modifier = CLASS(classType) as BulletDamageModifier;
			modifier.initData(data);
			mModifierList.Add(id, modifier);
		}
		return modifier;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void register<T>(BULLET_DAMAGE_MODIFIER type) where T : BulletDamageModifier
	{
		mTypeList.Add(type, typeof(T));
	}
}