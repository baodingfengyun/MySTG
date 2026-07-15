using static StringUtility;

// 参数
public class BuffCriticalDamageUpParam : CharacterBuffParamT<BuffCriticalDamageUpParam>
{
	public float mIncrease;         // 增加的暴击伤害百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffCriticalDamageUpParam other)
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

// 暴击伤害增加
public class BuffCriticalDamageUp : CharacterBuffT<BuffCriticalDamageUpParam>
{
	public float mIncrease;     // 增加的暴击伤害百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mCharacterGame.getGameData().mCriticalDamageIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mCriticalDamageIncrease -= mIncrease;
	}
}