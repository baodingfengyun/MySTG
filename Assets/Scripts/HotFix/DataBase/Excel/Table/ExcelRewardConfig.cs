using System.Collections.Generic;

// 奖励表,一个奖励id 按概率选择多个奖励组,每个奖励组 随机"奖励次数"次奖励(假如奖励组101 100%获得1银币,奖励次数=3,则发3个银币)
// getRewards 按 rewardConfigID 分组返回该奖励下的所有候选组，缓存 mRewards 懒加载 + clearCache 置空。
// 概率轮盘与次数随机由调用方（奖励发放逻辑）完成，本表只提供"按ID取候选组"。
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