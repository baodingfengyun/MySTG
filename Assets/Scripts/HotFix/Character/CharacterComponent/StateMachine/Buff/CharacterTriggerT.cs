
// 角色的触发类buff
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