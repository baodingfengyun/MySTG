using static StringUtility;

// 参数
public class BuffCriticalUpParam : CharacterBuffParamT<BuffCriticalUpParam>
{
	public float mIncrease;         // 射程增加的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffCriticalUpParam other)
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

// 暴击率增加
public class BuffCriticalUp : CharacterBuffT<BuffCriticalUpParam>
{
	public float mIncrease;		// 增加的暴击率
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mCharacterGame.getGameData().mCriticalIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mCriticalIncrease -= mIncrease;
	}
}