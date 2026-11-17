using static GBR;

// 塔的技能表：一座塔某一星级对应的技能参数（射速/索敌策略/子弹ID/释放特效音效）。
// 塔表用 mSkill 指向这里，这里再用 mBullet 指向子弹表，形成"塔 -> 技能 -> 子弹"三级链。
// 本类仅做引用校验（子弹/特效/音效 ID 合法、枚举有效），无查询缓存。
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