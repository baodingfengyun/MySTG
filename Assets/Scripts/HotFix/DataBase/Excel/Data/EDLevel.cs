// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Level表格
public class EDLevel : ExcelDataT<EDLevel>
{
	public BATTLE_MODE mMode;						// 游戏模式BATTLE_MODE:1.常规战斗 2.防线战斗 3.主线抽卡模式 4.无尽抽卡模式,5:肉鸽模式
	public string mName;							// 关卡名字
	public int mChapter;							// 所属章节
	public int mMapID;								// 地图ID Excel MapConfig
	public string mIconNumberName;					// 图标关卡标号名
	public Vector2Int mIconPosition;				// 图标在大地图中显示的坐标
	public LEVEL_TYPE mLevelType;					// 关卡类型
	public string mDesc;							// 关卡说明
	public string mDisplayImage;					// 关卡展示图
	public List<int> mNextLevel = new();			// 下一关卡
	public int mMusic;								// 背景音乐ID
	public int mUnLockByCompleteLevel;				// 完成某个关卡后解锁
	public int mPowerUse;							// 体力消耗
	public int mInitCurrency;						// 初始货币
	public List<int> mRewardPreview = new();		// 奖励预览
	public List<int> mMonsterPreview = new();		// 怪物预览
	public int mNeedExp;							// 通关分数
	public int mRewardFirst;						// 首通奖励
	public int mStar1;								// 关卡1星条件
	public int mStar2;								// 关卡2星条件
	public int mStar3;								// 关卡3星条件
	public int mReward1;							// 1星关卡奖励
	public int mReward2;							// 2星关卡奖励
	public int mReward3;							// 3星关卡奖励
	public bool mEndless;							// 是否为无尽关卡
	public bool mSaveProgress;						// 中途退出时是否存储关卡进度
	public bool mAutoStart;							// 是否自动倒计时开始
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readEnumByte(out mMode);
		result = result && reader.readString(out mName);
		result = result && reader.read(out mChapter);
		result = result && reader.read(out mMapID);
		result = result && reader.readString(out mIconNumberName);
		result = result && reader.read(out mIconPosition);
		result = result && reader.readEnumByte(out mLevelType);
		result = result && reader.readString(out mDesc);
		result = result && reader.readString(out mDisplayImage);
		result = result && reader.readList(mNextLevel);
		result = result && reader.read(out mMusic);
		result = result && reader.read(out mUnLockByCompleteLevel);
		result = result && reader.read(out mPowerUse);
		result = result && reader.read(out mInitCurrency);
		result = result && reader.readList(mRewardPreview);
		result = result && reader.readList(mMonsterPreview);
		result = result && reader.read(out mNeedExp);
		result = result && reader.read(out mRewardFirst);
		result = result && reader.read(out mStar1);
		result = result && reader.read(out mStar2);
		result = result && reader.read(out mStar3);
		result = result && reader.read(out mReward1);
		result = result && reader.read(out mReward2);
		result = result && reader.read(out mReward3);
		result = result && reader.read(out mEndless);
		result = result && reader.read(out mSaveProgress);
		result = result && reader.read(out mAutoStart);
		return result;
	}
}
// auto generate end