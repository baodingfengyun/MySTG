
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