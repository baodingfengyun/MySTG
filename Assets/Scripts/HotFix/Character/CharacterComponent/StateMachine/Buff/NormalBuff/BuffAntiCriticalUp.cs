using static StringUtility;

// 参数
public class BuffAntiCriticalUpParam : CharacterBuffParamT<BuffAntiCriticalUpParam>
{
	public float mIncrease;     // 提升的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void copyInternal(BuffAntiCriticalUpParam other)
	{
		mIncrease = other.mIncrease;
	}
}

// 暴击抗性提升
public class BuffAntiCriticalUp : CharacterBuffT<BuffAntiCriticalUpParam>
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
		mCharacterGame.getGameData().mAntiCritical += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mAntiCritical -= mIncrease;
	}
}