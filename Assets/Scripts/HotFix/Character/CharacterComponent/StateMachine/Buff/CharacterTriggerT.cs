
// 带自定义参数的触发类 Buff 泛型基类：各具体 Trigger 的直接父类
// 【与 CharacterBuffT 同理】用泛型 T 把通用参数转成触发器自己的参数类型，
//   子类用 getCustomParam() 强类型读取；mCustomParam 同样只在 enter() 期间有效。
public class CharacterTriggerT<T> : CharacterTrigger where T : StateParam
{
	protected T mCustomParam;                   // 此参数只能在enter中使用,执行完enter后就会回收销毁
	public override void setParam(StateParam param)
	{
		base.setParam(param);
		mCustomParam = param as T;
	}
	public T getCustomParam() { return mCustomParam; }
	public override void resetProperty()
	{
		base.resetProperty();
		mCustomParam = null;
	}
}