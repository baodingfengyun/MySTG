using static StringUtility;

// 参数
public class BuffBeenAllElementDamageDownParam : CharacterBuffParamT<BuffBeenAllElementDamageDownParam>
{
	public float mDecrease;     // 降低的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mDecrease = param.SToF(); });
	}
	protected override void copyInternal(BuffBeenAllElementDamageDownParam other)
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

// 受到的所有属性伤害降低
public class BuffBeenAllElementDamageDown : CharacterBuffT<BuffBeenAllElementDamageDownParam>
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
		CharacterGameData data = mCharacterGame.getGameData();
		data.mAntiFireElement += mDecrease;
		data.mAntiDarkElement += mDecrease;
		data.mAntiLightElement += mDecrease;
		data.mAntiIceElement += mDecrease;
		data.mAntiPoisonElement += mDecrease;
		data.mAntiLightningElement += mDecrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		CharacterGameData data = mCharacterGame.getGameData();
		data.mAntiFireElement -= mDecrease;
		data.mAntiDarkElement -= mDecrease;
		data.mAntiLightElement -= mDecrease;
		data.mAntiIceElement -= mDecrease;
		data.mAntiPoisonElement -= mDecrease;
		data.mAntiLightningElement -= mDecrease;
	}
}