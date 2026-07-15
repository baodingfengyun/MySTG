using static StringUtility;

// 参数
public class BuffBeenDarkDamageDownParam : CharacterBuffParamT<BuffBeenDarkDamageDownParam>
{
	public float mDecrease;     // 降低的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mDecrease = param.SToF(); });
	}
	protected override void copyInternal(BuffBeenDarkDamageDownParam other)
	{
		mDecrease = other.mDecrease;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0.0f;
	}
}

// 受到的暗属性伤害降低
public class BuffBeenDarkDamageDown : CharacterBuffT<BuffBeenDarkDamageDownParam>
{
	protected float mDecrease;      // 提升的百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mDecrease = mCustomParam.mDecrease;
		mCharacterGame.getGameData().mBeenDarkElementDamageIncrease -= mDecrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mBeenDarkElementDamageIncrease += mDecrease;
	}
}