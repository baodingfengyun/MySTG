using System;
using System.Collections.Generic;

public class TowerRegister
{
	protected static Dictionary<TOWER_TYPE, Type> mRegisteList = new();
	public static void registerAll()
	{
		registerTower<CharacterTowerStone>(TOWER_TYPE.SHI_DUN);
		registerTower<CharacterTowerQiQiuZhaDan>(TOWER_TYPE.QI_QIU_ZHA_DAN_TA);
	}
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
