using static StringUtility;

// 参数
public class BuffEvasionUpParam : CharacterBuffParamT<BuffEvasionUpParam>
{
	public float mIncrease;     // 提升的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffEvasionUpParam other)
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

// 闪避率提升
public class BuffEvasionUp : CharacterBuffT<BuffEvasionUpParam>
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
		mCharacterGame.getGameData().mEvasion += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mEvasion -= mIncrease;
	}
}