
// buff参数对象
public abstract class CharacterBuffParamT<T> : CharacterBuffParam where T : CharacterBuffParam
{
	public sealed override void copy(StateParam other)
	{
		copyInternal(other as T);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected virtual void copyInternal(T other) { }
}