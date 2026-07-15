using static UnityUtility;
using static GBR;

// 怪物技能表
public class ExcelMonsterSkill : ExcelTableT<EDMonsterSkill>
{
    public override void checkAllData()
    {
        foreach (EDMonsterSkill item in queryAll())
        {
            mExcelBuffDetail.checkData(item.mDefaultFireBuff, item.mID, this);
            mExcelEffect.checkData(item.mFireEffect, item.mID, this);
            mExcelBuffDetail.checkData(item.mFireBuff0, item.mID, this);
            mExcelBuffDetail.checkData(item.mFireBuff1, item.mID, this);
            mExcelSkillBullet.checkData(item.mBullet, item.mID, this);
            if (item.mDescription != mExcelLocalization.query(item.mDescriptionID).mChinese)
            {
                logError(mTableName + "中ID:" + item.mID + "的Description, 与ExcelLocalization中ID:" + item.mDescriptionID + "的中文不一致");
            }
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDMonsterSkill item in queryAll())
		{
			mExcelLocalization.checkData(item.mDescriptionID, item.mID, this);
			mExcelBuffDetail.checkData(item.mDefaultFireBuff, item.mID, this);
			checkEnum(item.mSearchTarget, "mSearchTarget", item.mID);
		}
	}
	// auto generate end
}