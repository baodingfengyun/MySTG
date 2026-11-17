
// 子弹伤害修正表：子弹表用 mDamageModifier 指向这里，定义对某类目标的伤害倍率修正
// （如"对飞行单位额外伤害"）。校验修正类型枚举合法。
public class ExcelBulletDamageModifier : ExcelTableT<EDBulletDamageModifier>
{
    // auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDBulletDamageModifier item in queryAll())
		{
			checkEnum(item.mType, "mType", item.mID);
		}
	}
    // auto generate end
}