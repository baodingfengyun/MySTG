using System;
using System.Collections.Generic;
using static BinaryUtility;
using static FrameUtility;
using static MathUtility;
using static FrameBaseHotFix;
using static GBR;

// 通关数据
public class COMClientLevel : GameComponent, IClientSystemComponent
{
	protected Dictionary<int, LevelGreatScoreInfo> mLevelInfoList = new();		// 每个关卡的数据
	protected Dictionary<BATTLE_MODE, List<int>> mCompleteLevels = new();		// 所有模式已完成的关卡
	public void clear()
	{
		foreach(LevelGreatScoreInfo each in mLevelInfoList.Values)
		{
			each.destroy();
		}
		UN_CLASS_LIST(mLevelInfoList);
		foreach (var item in mCompleteLevels.Values)
		{
			UN_LIST(item);
		}
		mCompleteLevels.Clear();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mLevelInfoList.Clear();
		mCompleteLevels.Clear();
	}
	public RedPoint getLevelRedPoint(int levelID)						{ return mLevelInfoList.get(levelID)?.mLevelRewardRedPoint; }
	public List<int> getAllCompleteLevel(BATTLE_MODE mode)				{ return mCompleteLevels.getOrAddListPersist(mode); }
	public int getMaxCompleteLevel(BATTLE_MODE mode)
	{
		int maxLevel = 0;
		foreach (int levelID in mCompleteLevels.get(mode).safe())
		{
			maxLevel = getMax(maxLevel, levelID);
		}
		return maxLevel;
	}
	public void addCompleteLevel(BATTLE_MODE mode, int id)				{ getAllCompleteLevel(mode).addUnique(id); }
	public int getLevelGreatHp(int levelID)								{ return mLevelInfoList.get(levelID)?.mHp ?? 0; }
	// 奖励是否可领取
	public bool canTakeStarReward(int levelID, int index)				{ return mLevelInfoList.TryGetValue(levelID, out var info) && info.canTakeStarReward(index); }
	public bool canTakeStarReward(int levelID)							{ return mLevelInfoList.TryGetValue(levelID, out var info) && info.canTakeStarReward(); }
	public bool hasTakeStarReward(int levelID, int index)				{ return mLevelInfoList.TryGetValue(levelID, out var info) && info.hasTakeStarReward(index); }
	public void setHasTakeStarReward(int levelID, int index, bool take) { mLevelInfoList.get(levelID)?.setHasTake(index, take); }
	public bool isLevelComplete(int id)
	{
		if (id == 0)
		{
			return true;
		}
		foreach (var each in mCompleteLevels.Values)
		{
			if (each.Contains(id))
			{
				return true;
			}
		}
		return false;
	}
	// 关卡是否已经解锁,正在打的关卡,已经通关的关卡,前置关卡已经通关的关卡,都是已经解锁的
	public bool isLevelUnlock(EDLevel data)
	{
		return isLevelComplete(data.mID) ||
			   isLevelComplete(data.mUnLockByCompleteLevel);
	}
}