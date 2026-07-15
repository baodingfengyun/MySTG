using System.Collections.Generic;
using static FrameUtility;
using static MathUtility;
using static GBR;

// 关卡表
public class ExcelLevel : ExcelTableT<EDLevel>
{
    protected Dictionary<BATTLE_MODE, Dictionary<int, List<EDLevel>>> mBattleModeChapters;  // 所有模式 章节对应的关卡
    protected Dictionary<BATTLE_MODE, List<EDLevel>> mModeLevelDatas;                       // 战斗模式的关卡列表
    protected Dictionary<int, List<EDLevel>> mChapterLevels;                                // 章节的所有关卡,key为章节ID
    protected Dictionary<BATTLE_MODE, int> mMaxChapterList;
    public int getMaxChapter(BATTLE_MODE mode)
    {
        if (mMaxChapterList == null)
        {
            mMaxChapterList = new();
            foreach (EDLevel level in queryAll())
            {
                if (mMaxChapterList.TryGetValue(level.mMode, out int tempMaxChapter))
                {
                    mMaxChapterList.set(level.mMode, getMax(level.mChapter, tempMaxChapter));
                }
                else
                {
                    mMaxChapterList.Add(level.mMode, level.mChapter);
                }
            }
        }
        return mMaxChapterList.get(mode);
    }
    public List<EDLevel> getModeLevels(BATTLE_MODE mode)
    {
        if (mModeLevelDatas == null)
        {
            initBattleModeLevelList();
        }
        return mModeLevelDatas.get(mode);
    }
    public List<EDLevel> getChapterLevels(BATTLE_MODE mode, int chapter)
    {
        if (mBattleModeChapters == null)
        {
            initBattleModeChapters();
        }
        return mBattleModeChapters.get(mode)?.get(chapter);
    }
    public List<EDLevel> getChapterLevels(int chapter)
    {
        if (mChapterLevels == null)
        {
            initChapterList();
        }
        return mChapterLevels.get(chapter);
    }
    public override void clearCache()
    {
        mModeLevelDatas = null;
        mBattleModeChapters = null;
        mMaxChapterList = null;
    }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void initBattleModeLevelList()
    {
        mModeLevelDatas = new();
        foreach (EDLevel level in queryAll())
        {
            mModeLevelDatas.getOrAddNew(level.mMode).Add(level);
        }
    }
    protected void initChapterList()
    {
        mChapterLevels = new();
        foreach (EDLevel level in queryAll())
        {
            mChapterLevels.getOrAddNew(level.mChapter).Add(level);
        }
    }
    protected void initBattleModeChapters()
    {
        mBattleModeChapters = new();
        foreach (EDLevel level in queryAll())
        {
            if (!isEnumValid(level.mLevelType))
            {
                continue;
            }
            mBattleModeChapters.getOrAddNew(level.mMode).getOrAddNew(level.mChapter).Add(level);
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDLevel item in queryAll())
		{
			checkEnum(item.mMode, "mMode", item.mID);
			mExcelChapter.checkData(item.mChapter, item.mID, this);
			mExcelMapConfig.checkData(item.mMapID, item.mID, this);
			checkEnum(item.mLevelType, "mLevelType", item.mID);
			mExcelAudio.checkData(item.mMusic, item.mID, this);
		}
	}
	// auto generate end
}