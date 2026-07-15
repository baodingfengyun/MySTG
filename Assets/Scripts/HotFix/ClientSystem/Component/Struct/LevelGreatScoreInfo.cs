using System;
using System.Collections.Generic;
using static GameUtilityHotFix;
using static FrameBaseHotFix;
using static GBR;

public class LevelGreatScoreInfo : ClassObject
{
	public List<bool> mHasNotTakeStarReward = new();		// 值表示是否还没有领取此星级的奖励,不考虑是否达到领取条件
	public RedPoint mLevelRewardRedPoint;
	public int mLevelID;
	public int mScore;
	public int mHp;
	public void init(int levelID, int score, int hp, Span<bool> rewardStar)
	{
		mLevelID = levelID;
		mScore = score;
		mHp = hp;
		mHasNotTakeStarReward.Clear();
		mHasNotTakeStarReward.addRange(rewardStar);
		if(mLevelRewardRedPoint == null)
		{
			mLevelRewardRedPoint = mRedPointSystem.createRedPoint();
		}
	}
	public override void destroy()
	{
		base.destroy();
		mRedPointSystem.destroyRedPoint(mLevelRewardRedPoint);
		mLevelRewardRedPoint = null;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mHasNotTakeStarReward.Clear();
		mLevelRewardRedPoint = null;
		mLevelID = 0;
		mScore = 0;
		mHp = 0;
	}
	public bool canTakeStarReward(int starIndex)
	{
		if (starIndex < 0 || starIndex >= mHasNotTakeStarReward.Count)
		{
			return false;
		}
		EDLevel data = mExcelLevel.query(mLevelID, false);
		if (data == null || data.mEndless)
		{
			return false;
		}
		return mHasNotTakeStarReward[starIndex] && getLevelStar(mLevelID) >= starIndex + 1;
	}
	public bool canTakeStarReward()
	{
		EDLevel data = mExcelLevel.query(mLevelID, false);
		if (data == null || data.mEndless)
		{
			return false;
		}
		bool canTake = false;
		int star = 0;
		foreach(bool each in mHasNotTakeStarReward.safe())
		{
			star++;
			canTake |= each && getLevelStar(mLevelID) >= star;
		}
		return canTake;
	}
	public bool hasTakeStarReward(int starIndex)
	{
		if (starIndex < 0 || starIndex >= mHasNotTakeStarReward.Count)
		{
			return false;
		}
		return !mHasNotTakeStarReward[starIndex] && getLevelStar(mLevelID) >= starIndex + 1;
	}
	public void setHasTake(int starIndex, bool take)
	{
		mHasNotTakeStarReward[starIndex] = !take;
	}
}