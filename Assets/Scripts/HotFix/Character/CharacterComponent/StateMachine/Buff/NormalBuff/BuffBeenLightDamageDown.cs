using static StringUtility;

// 参数
public class BuffBeenLightDamageDownParam : CharacterBuffParamT<BuffBeenLightDamageDownParam>
{
	public float mDecrease;     // 降低的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mDecrease = param.SToF(); });
	}
	protected override void copyInternal(BuffBeenLightDamageDownParam other)
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

// 受到的光属性伤害降低
public class BuffBeenLightDamageDown : CharacterBuffT<BuffBeenLightDamageDownParam>
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
		mCharacterGame.getGameData().mBeenLightElementDamageIncrease -= mDecrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mBeenLightElementDamageIncrease += mDecrease;
	}
}