using System;
using System.Collections.Generic;

// 塔实体类的反射注册表：把 TOWER_TYPE 映射到具体的 CharacterTower 子类
// 【设计】数据表里只存塔类型枚举,创建实体时由 getTowerType() 查表得到真正的 C# 类型；
//   这就是"加一座塔大多只填表"的关键——只有需要特殊实体行为的塔才在这里注册。
// 【现状】13 座塔里只注册了 2 个特例：石墩(SHI_DUN)和气球炸弹塔(QI_QIU_ZHA_DAN_TA)，
//   其余 11 座一律回退到基类 CharacterTower,行为差异靠"技能类 + 子弹形态 + Buff"实现。
// 【回退】查不到映射时返回 typeof(CharacterTower),所以新增塔即使忘了注册也不会崩,只是没有专属实体逻辑。
public class TowerRegister
{
	protected static Dictionary<TOWER_TYPE, Type> mRegisteList = new();
	public static void registerAll()
	{
		registerTower<CharacterTowerStone>(TOWER_TYPE.SHI_DUN);
		registerTower<CharacterTowerQiQiuZhaDan>(TOWER_TYPE.QI_QIU_ZHA_DAN_TA);
	}
	// 按塔类型取实体类；未注册的类型统一回退到基类 CharacterTower
	public static Type getTowerType(TOWER_TYPE towerDataType)
	{
		return mRegisteList.get(towerDataType) ?? typeof(CharacterTower);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registerTower<T>(TOWER_TYPE towerType) where T : CharacterTower
	{
		mRegisteList.Add(towerType, typeof(T));
	}
}
