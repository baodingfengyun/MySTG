
// 伤害数字信息(泛型):按具体飘字类型 T 持有对应的无序对象池,实现基类的 newItem/unuseAll/unuseItem,
// 供飘字系统按血量变化类型(HP_DELTA)复用对应池的飘字对象。
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