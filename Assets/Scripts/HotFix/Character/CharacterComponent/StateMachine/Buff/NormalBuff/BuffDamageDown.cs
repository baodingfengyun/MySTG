using static StringUtility;

// 参数
public class BuffDamageDownParam : CharacterBuffParamT<BuffDamageDownParam>
{
	public float mDecrease;         // 伤害降低的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mDecrease = param.SToF(); });
	}
	protected override void copyInternal(BuffDamageDownParam other)
	{
		mDecrease = other.mDecrease;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0.0f;
	}
}

// 伤害降低
public class BuffDamageDown : CharacterBuffT<BuffDamageDownParam>
{
	protected float mDecrease;		// 伤害降低百分比
	public override void enter()
	{
		base.enter();
		mDecrease = mCustomParam.mDecrease;
		mCharacterGame.getGameData().mDamageIncrease -= mDecrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mDamageIncrease += mDecrease;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0.0f;
	}
}