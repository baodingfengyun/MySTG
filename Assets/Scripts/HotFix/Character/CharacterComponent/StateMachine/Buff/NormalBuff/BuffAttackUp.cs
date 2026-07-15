using static StringUtility;

// 参数
public class BuffAttackUpParam : CharacterBuffParamT<BuffAttackUpParam>
{
	public int mIncrease;			// 增加的攻击力
	public float mPercent;          // 增加的攻击力百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToI(); });
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffAttackUpParam other)
	{
		mIncrease = other.mIncrease;
		mPercent = other.mPercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0;
		mPercent = 0.0f;
	}
}

// 攻击力提升
public class BuffAttackUp : CharacterBuffT<BuffAttackUpParam>
{
	protected int mIncrease;		// 增加的攻击力
	protected float mPercent;		// 增加的攻击力百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0;
		mPercent = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mPercent = mCustomParam.mPercent;
		mCharacterGame.getGameData().mAttackIncrease += mIncrease;
		mCharacterGame.getGameData().mIncreaseAttackPercent += mPercent;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mAttackIncrease -= mIncrease;
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mPercent;
	}
}