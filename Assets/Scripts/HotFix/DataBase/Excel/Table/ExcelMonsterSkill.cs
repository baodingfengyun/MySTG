using static UnityUtility;
using static GBR;

// 怪物技能表：怪物主动/被动技能的静态配置（索敌方式、子弹ID、释放特效、附带 Buff）。
// 本类无查询缓存，职责是 checkAllData 校验技能引用的 Buff/特效/子弹 ID 都存在，
// 且技能描述与多语言表一致——保证运行时按 ID 取资源不会取空。
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