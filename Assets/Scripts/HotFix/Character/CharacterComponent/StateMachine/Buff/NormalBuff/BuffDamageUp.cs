using static StringUtility;

// 参数
public class BuffDamageUpParam : CharacterBuffParamT<BuffDamageUpParam>
{
	public float mIncrease;         // 伤害提升的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffDamageUpParam other)
	{
		mIncrease = other.mIncrease;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}

// 伤害增加
public class BuffDamageUp : CharacterBuffT<BuffDamageUpParam>
{
	protected float mIncrease;		// 伤害提升百分比
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mCharacterGame.getGameData().mDamageIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mDamageIncrease -= mIncrease;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}