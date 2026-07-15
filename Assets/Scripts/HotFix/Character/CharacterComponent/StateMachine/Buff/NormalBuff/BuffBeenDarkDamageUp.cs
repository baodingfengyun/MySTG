using static StringUtility;

// 参数
public class BuffBeenDarkDamageUpParam : CharacterBuffParamT<BuffBeenDarkDamageUpParam>
{
	public float mIncrease;     // 提升的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffBeenDarkDamageUpParam other)
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

// 受到的暗属性伤害提升
public class BuffBeenDarkDamageUp : CharacterBuffT<BuffBeenDarkDamageUpParam>
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
		mCharacterGame.getGameData().mBeenDarkElementDamageIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mBeenDarkElementDamageIncrease -= mIncrease;
	}
}