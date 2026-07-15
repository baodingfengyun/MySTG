using System.Collections.Generic;
using static GBR;
using static UnityUtility;

// 新手引导表
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