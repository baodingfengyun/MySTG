
// 带自定义参数的 Buff 泛型基类：绝大多数普通 Buff 的直接父类
// 【为什么需要它】CharacterBuff 只认通用 CharacterBuffParam；而各 Buff 有自己的数值
//   （毒的每秒伤害、减速的比例...），这些放在各自的参数子类里。本类用泛型 T 把参数
//   转成具体类型，子类通过 getCustomParam() 强类型访问自己的配置。
// 【注意】mCustomParam 只在 enter() 期间有效——参数对象来自对象池，enter 执行完就回收，
//   所以子类若要在 update 里用数值，必须在 enter 里把值拷到自己的字段上。
public class CharacterBuffT<T> : CharacterBuff where T : StateParam
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