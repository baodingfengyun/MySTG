// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// CardPoolConfig表格
public class EDCardPoolConfig : ExcelDataT<EDCardPoolConfig>
{
	public List<int> mItemID = new();				// 物品id
	public List<BATTLE_ITEM_TYPE> mItemType = new();// 物品类型BATTLE_ITEM_TYPE,1塔,2道具,3天赋
	public List<int> mItemWeight = new();			// 物品权重
	public List<int> mMustItemID = new();			// 必出物品id
	public List<BATTLE_ITEM_TYPE> mMustItemType = new();// 必出物品类型,1塔,2道具
	public bool mUseAddTowerTalent;					// 是否自动添加防御塔上阵词条(肉鸽模式生效)
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readList(mItemID);
		result = result && reader.readEnumByteList(mItemType);
		result = result && reader.readList(mItemWeight);
		result = result && reader.readList(mMustItemID);
		result = result && reader.readEnumByteList(mMustItemType);
		result = result && reader.read(out mUseAddTowerTalent);
		return result;
	}
}
// auto generate end