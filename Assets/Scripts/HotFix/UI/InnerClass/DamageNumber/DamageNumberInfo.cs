
public class DamageNumberInfo<T> : DamageNumberInfoBase where T : DamageNumber
{
	public WindowStructPoolUnOrder<T> mNumberPool;
	public override DamageNumber newItem()
	{
		return mNumberPool.newItem();
	}
	public override void unuseAll()
	{
		mNumberPool.unuseAll();
	}
	public override void unuseItem(DamageNumber item)
	{
		mNumberPool.unuseItem(item as T);
	}
}