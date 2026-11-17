using System.Collections.Generic;
using static GBR;
using static UnityUtility;

// 新手引导表：每一步引导一个 ID，多个连续步骤用 mGroupID 归为一组。
// getFirstGuide/getNextGuide 沿表内顺序串成主流程；
// getFallbackStep 提供"中断后回到哪一步"——没显式配 mFallbackID 时，
// 退回到同组的第一个步骤（groupList 记录每组首个 ID）。
// checkAllData 校验引导文案与多语言表一致、且 mGroupID 单调不减（组内步骤必须连续）。
public class ExcelGuide : ExcelTableT<EDGuide>
{
    protected Dictionary<int, int> mFallbackIDList;
    public override void checkAllData()
    {
        int groupID = 0;
        foreach (EDGuide item in queryAll())
        {
            if (item.mTipLocID != 0 && item.mTip != mExcelLocalization.query(item.mTipLocID).mChinese)
            {
                logError(mTableName + "中ID:" + item.mID + "的Description, 与ExcelLocalization中ID:" + item.mTipLocID + "的中文不一致");
            }
            if (item.mNPCTalkLocID != 0 && item.mNPCTalk != mExcelLocalization.query(item.mNPCTalkLocID).mChinese)
            {
                logError(mTableName + "中ID:" + item.mID + "的Description, 与ExcelLocalization中ID:" + item.mNPCTalkLocID + "的中文不一致");
            }
            // 组ID检测
            if (item.mGroupID < groupID)
            {
                logError("引导组ID填写错误,步骤ID:" + item.mID.IToS() + ",上一个步骤的组ID:" + groupID.IToS());
            }
            groupID = item.mGroupID;
        }
    }
    public int getFirstGuide()
    {
        return queryAll().get(0)?.mID ?? 0;
    }
    public int getNextGuide(int stepID)
    {
        var list = queryAll();
        int count = list.Count;
        for (int i = 0; i < count - 1; ++i)
        {
            if (list[i].mID == stepID)
            {
                return list[i + 1].mID;
            }
        }
        return 0;
    }
    // 取某步骤中断后的回退目标：优先用显式配置的 mFallbackID，
    // 否则退回本组的第一个步骤（重新走这一组引导）。结果缓存于 mFallbackIDList。
    public int getFallbackStep(int stepID)
    {
        if (mFallbackIDList == null)
        {
            mFallbackIDList = new();
            using var a = new DicScope<int, int>(out var groupList);
            foreach (EDGuide item in queryAll())
            {
                groupList.TryAdd(item.mGroupID, item.mID);
            }
            foreach (EDGuide item in queryAll())
            {
                if (item.mFallbackID != 0)
                {
                    mFallbackIDList.add(item.mID, item.mFallbackID);
                }
                else
                {
                    mFallbackIDList.add(item.mID, groupList.get(item.mGroupID));
                }
            }
        }
        return mFallbackIDList.get(stepID);
    }
	// auto generate start
	protected override void checkAllDataDefault() {}
	// auto generate end
}