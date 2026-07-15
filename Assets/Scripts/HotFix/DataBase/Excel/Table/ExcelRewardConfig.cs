using System.Collections.Generic;

// 奖励表,一个奖励id 按概率选择多个奖励组,每个奖励组 随机“奖励次数”次奖励(假如奖励组101 100%获得1银币,奖励次数=3,则发3个银币)
public class ExcelRewardConfig : ExcelTableT<EDRewardConfig>
{
    protected Dictionary<int, List<EDRewardConfig>> mRewards;
    public override void clearCache()
    {
        base.clearCache();
        mRewards = null;
    }
    public List<EDRewardConfig> getRewards(int rewardID)
    {
        if (mRewards == null)
        {
            mRewards = new();
            foreach (EDRewardConfig each in queryAll())
            {
                mRewards.getOrAddNew(each.mRewardConfigID).Add(each);
            }
        }
        return mRewards.get(rewardID);
    }
	// auto generate start
	protected override void checkAllDataDefault() {}
	// auto generate end
}