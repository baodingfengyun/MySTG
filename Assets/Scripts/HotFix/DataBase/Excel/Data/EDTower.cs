// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Tower表格
public class EDTower : ExcelDataT<EDTower>
{
	public const int STONE_TOWER_ID = 450;			// 石墩

	private static EDTower _STONE_TOWER;			// 石墩

	public static EDTower STONE_TOWER { get { return _STONE_TOWER ??= mTable.query(STONE_TOWER_ID); } }// 石墩

	public string mName;							// 名称
	public byte mFunctionType;						// 功能类型
	public string mFunctionTypeName;				// 功能类型名
	public string mDescription;						// 描述
	public int mLocalLang;							// 描述在Localization表下的对应ID
	public string mPrefab;							// GameResources下的相对路径,带后缀
	public string mIcon;							// 塔的图标
	public int mSkill;								// 防御塔所携带的技能
	public float mRange;							// 攻击目标检测范围,需要乘以格子的大小才是半径值
	public List<int> mHexRange = new();				// 范围是六边形地块的哪几个边，可以用Range延长
	public List<int> mDefaultBuff = new();			// 默认Buff
	public int mStar;								// 星级
	public int mLevel;								// 等级
	public TOWER_TYPE mType;						// 类型
	public string mRotateRoot;						// 旋转节点的名字
	public int mPrice;								// 卡牌模式下的金币消耗
	public List<Vector3Int> mRogueLevelUpTowerSilverCost = new();// 肉鸽模式下塔升级消耗
	public List<int> mLevelUpCostID = new();		// 升级消耗资源
	public List<int> mLevelUpCostParam0 = new();	// 升级消耗资源参数1
	public List<int> mLevelUpCostParam1 = new();	// 升级消耗资源参数2
	public List<int> mLevelUpCostParam2 = new();	// 升级消耗资源参数3
	public List<int> mStarUpCostID = new();			// 升星消耗资源
	public List<int> mStarUpCostCount = new();		// 升星消耗资源数量
	public List<int> mSellReward = new();			// 出售返还资源
	public List<int> mSellParam0 = new();			// 出售返还资源参数1
	public List<int> mSellParam1 = new();			// 出售返还资源参数2
	public List<int> mSellParam2 = new();			// 出售返还资源参数3
	public float mCritical;							// 暴击率
	public int mDisplayInOrder;						// 局内显示顺序
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.read(out mFunctionType);
		result = result && reader.readString(out mFunctionTypeName);
		result = result && reader.readString(out mDescription);
		result = result && reader.read(out mLocalLang);
		result = result && reader.readString(out mPrefab);
		result = result && reader.readString(out mIcon);
		result = result && reader.read(out mSkill);
		result = result && reader.read(out mRange);
		result = result && reader.readList(mHexRange);
		result = result && reader.readList(mDefaultBuff);
		result = result && reader.read(out mStar);
		result = result && reader.read(out mLevel);
		result = result && reader.readEnumByte(out mType);
		result = result && reader.readString(out mRotateRoot);
		result = result && reader.read(out mPrice);
		result = result && reader.readList(mRogueLevelUpTowerSilverCost);
		result = result && reader.readList(mLevelUpCostID);
		result = result && reader.readList(mLevelUpCostParam0);
		result = result && reader.readList(mLevelUpCostParam1);
		result = result && reader.readList(mLevelUpCostParam2);
		result = result && reader.readList(mStarUpCostID);
		result = result && reader.readList(mStarUpCostCount);
		result = result && reader.readList(mSellReward);
		result = result && reader.readList(mSellParam0);
		result = result && reader.readList(mSellParam1);
		result = result && reader.readList(mSellParam2);
		result = result && reader.read(out mCritical);
		result = result && reader.read(out mDisplayInOrder);
		return result;
	}
}
// auto generate end