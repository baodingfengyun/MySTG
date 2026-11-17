using UnityEngine;
using System.Collections.Generic;
using static GBR;

// 怪物表：怪物的静态属性与技能/Buff 引用。
// 这里额外维护"波次经验曲线"：mWaveExpData = 怪物ID -> [(波次, 击杀经验)...]。
// getWaveExp 按当前波次取应得经验——同样的怪，越到后期击杀收益越高，
// 这是 Rogue 模式经验成长（推进关卡）的数据来源之一。曲线懒加载 + clearCache 置空。
public class ExcelMonster : ExcelTableT<EDMonster>
{
    protected Dictionary<int, List<Vector2Int>> mWaveExpData;       // 波次经验列表
    // 取怪物在第 waveIndex 波的击杀经验：曲线按波次升序，
    // 遍历取"波次不超过当前值的最后一条"，实现经验按波分段递增。
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