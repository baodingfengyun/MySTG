using UnityEngine;
using System.Collections.Generic;
using static GBR;

// 怪物表
public class ExcelMonster : ExcelTableT<EDMonster>
{
    protected Dictionary<int, List<Vector2Int>> mWaveExpData;       // 波次经验列表
    public int getWaveExp(int monsterID, int waveIndex)
    {
        if (mWaveExpData == null)
        {
            initWaveExp();
        }
        int exp = 0;
        foreach (Vector2Int item in mWaveExpData.get(monsterID).safe())
        {
            exp = item.y;
            if (waveIndex < item.x)
            {
                break;
            }
        }
        return exp;
    }
    public override void checkAllData()
    {
        foreach (EDMonster item in queryAll())
        {
            mExcelMonsterSkill.checkData(item.mSkill, item.mID, this);
            mExcelBuffDetail.checkData(item.mDefaultBuff, item.mID, this);
            checkPath(item.mPrefab);
        }
    }
    public override void clearCache()
    {
        mWaveExpData = null;
    }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void initWaveExp()
    {
        mWaveExpData = new();
        foreach (EDMonster monster in queryAll())
        {
            var list = mWaveExpData.getOrAddNew(monster.mID);
            foreach (string item in monster.mWaveExp)
            {
                list.Add(item.SToV2I('|'));
            }
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDMonster item in queryAll())
		{
			if (!item.mPrefab.isEmpty())
			{
				checkPath(item.mPrefab);
			}
			checkEnum(item.mStrength, "mStrength", item.mID);
			mExcelLocalization.checkData(item.mBornTalk, item.mID, this);
			mExcelLocalization.checkData(item.mDyingTalk, item.mID, this);
		}
	}
	// auto generate end
}