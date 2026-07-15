using static GBR;

// 塔的技能表
public class ExcelTowerSkill : ExcelTableT<EDTowerSkill>
{
    public override void checkAllData()
    {
        foreach (EDTowerSkill item in queryAll())
        {
            mExcelSkillBullet.checkData(item.mBullet, item.mID, this);
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDTowerSkill item in queryAll())
		{
			mExcelSkillBullet.checkData(item.mBullet, item.mID, this);
			mExcelEffect.checkData(item.mFireEffect, item.mID, this);
			mExcelAudio.checkData(item.mFireSound, item.mID, this);
			checkEnum(item.mSearchTarget, "mSearchTarget", item.mID);
			checkEnum(item.mEnemyType, "mEnemyType", item.mID);
		}
	}
	// auto generate end
}