using static GBR;

// 技能子弹表：一颗子弹的完整静态定义——形态类型、元素类型、起点/命中特效、伤害修正，
// 以及命中前/命中后给目标或自身挂的 Buff（对应 SkillBullet.hit 里的严格时序钩子）。
// 本类无查询缓存，职责是 checkAllData 校验所有这些跨表引用（特效/伤害修正/Buff）合法。
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