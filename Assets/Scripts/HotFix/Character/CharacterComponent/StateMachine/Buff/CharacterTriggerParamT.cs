
// 参数对象
public class CharacterTriggerParamT<T> : CharacterTriggerParam where T : CharacterTriggerParam
{
	public sealed override void copy(StateParam other)
	{
		base.copy(other);
		copyInternal(other as T);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected virtual void copyInternal(T other) { }
};