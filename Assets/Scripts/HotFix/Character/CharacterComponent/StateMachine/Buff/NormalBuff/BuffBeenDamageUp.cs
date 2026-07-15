using static StringUtility;

// 参数
public class BuffBeenDamageUpParam : CharacterBuffParamT<BuffBeenDamageUpParam>
{
	public float mIncrease;     // 提升的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffBeenDamageUpParam other)
	{
		mIncrease = other.mIncrease;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}

// 受到的伤害提升
public class BuffBeenDamageUp : CharacterBuffT<BuffBeenDamageUpParam>
{
	protected float mIncrease;      // 提升的百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mCharacterGame.getGameData().mBeenDamageIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mBeenDamageIncrease -= mIncrease;
	}
}