// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// TowerTalent表格
public class EDTowerTalent : ExcelDataT<EDTowerTalent>
{
	public string mName;							// 天赋名
	public string mSimpleDescription;				// 天赋简单描述
	public string mDescription;						// 天赋描述
	public List<int> mBuff = new();					// 携带的buffID
	public string mIcon;							// 图标名字
	public TOWER_TYPE mTowerType;					// 关联的防御塔
	public bool mIsPowerful;						// 是否为强力特性
	public int mMaxSelectCount;						// 可选择的最大次数
	public List<Vector2Int> mPreTalent = new();		// 前置词条
	public List<int> mMutexTalent = new();			// 互斥词条
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.readString(out mSimpleDescription);
		result = result && reader.readString(out mDescription);
		result = result && reader.readList(mBuff);
		result = result && reader.readString(out mIcon);
		result = result && reader.readEnumByte(out mTowerType);
		result = result && reader.read(out mIsPowerful);
		result = result && reader.read(out mMaxSelectCount);
		result = result && reader.readList(mPreTalent);
		result = result && reader.readList(mMutexTalent);
		return result;
	}
}
// auto generate end