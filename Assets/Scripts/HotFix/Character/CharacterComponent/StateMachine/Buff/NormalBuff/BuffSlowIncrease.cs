using static StringUtility;

// 参数
public class BuffSlowIncreaseParam : CharacterBuffParamT<BuffSlowIncreaseParam>
{
	public float mPercent;         // 减速的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffSlowIncreaseParam other) 
	{
		mPercent = other.mPercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
}

// 受到的减速效果提升
public class BuffSlowIncrease : CharacterBuffT<BuffSlowIncreaseParam>
{
	protected float mPercent;
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mPercent = mCustomParam.mPercent;
		mCharacterGame.getGameData().mSlowDownIncrease += mPercent;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mSlowDownIncrease -= mPercent;
	}
}