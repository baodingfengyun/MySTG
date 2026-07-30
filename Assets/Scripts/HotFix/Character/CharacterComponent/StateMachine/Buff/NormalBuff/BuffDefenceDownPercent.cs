
// 参数
public class BuffDefenceDownPercentParam : CharacterBuffParamT<BuffDefenceDownPercentParam>
{
	public float mPercent;         // 降低的防御力百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffDefenceDownPercentParam other)
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

// 百分比降低防御力
public class BuffDefenceDownPercent : CharacterBuffT<BuffDefenceDownPercentParam>
{
	public int mDecrease;        // 实际降低的防御力
	public override void resetProperty()
	{
		base.resetProperty();
		mDecrease = 0;
	}
	public override void enter()
	{
		base.enter();
		mDecrease = (mCustomParam.mPercent * mCharacterGame.getGameData().mDefence).round();
		mCharacterGame.getGameData().mDefence -= mDecrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mDefence += mDecrease;
	}
}