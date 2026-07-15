using static GBR;

// 技能的子弹表
public class ExcelSkillBullet : ExcelTableT<EDSkillBullet>
{
    public override void checkAllData()
    {
        foreach (EDSkillBullet item in queryAll())
        {
            mExcelEffect.checkData(item.mFlyEffect, item.mID, this);
            mExcelEffect.checkData(item.mHitEffect, item.mID, this);
            mExcelEffect.checkData(item.mMuzzleEffect, item.mID, this);
            mExcelEffect.checkData(item.mExplosionEffect, item.mID, this);
            mExcelBulletDamageModifier.checkData(item.mDamageModifier, item.mID, this);
            mExcelBuffDetail.checkData(item.mWillHitBuffToTarget, item.mID, this);
            mExcelBuffDetail.checkData(item.mHitBuffToTarget, item.mID, this);
            mExcelBuffDetail.checkData(item.mHitBuffToSelf, item.mID, this);
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDSkillBullet item in queryAll())
		{
			checkEnum(item.mType, "mType", item.mID);
			checkEnum(item.mElementType, "mElementType", item.mID);
			checkEnum(item.mHitEffectPosition, "mHitEffectPosition", item.mID);
			checkEnum(item.mStartPosition, "mStartPosition", item.mID);
		}
	}
	// auto generate end
}