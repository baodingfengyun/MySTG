using static StringUtility;

// 参数
public class BuffMoveSpeedUpParam : CharacterBuffParamT<BuffMoveSpeedUpParam>
{
	public float mPercent;         // 加速的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffMoveSpeedUpParam other)
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

// 百分比加速,固定百分比
public class BuffMoveSpeedUp : CharacterBuffT<BuffMoveSpeedUpParam>
{
	protected float mMoveSpeedUp;		// 加速的绝对值
	public override void resetProperty()
	{
		base.resetProperty();
		mMoveSpeedUp = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		mMoveSpeedUp = comMovement.getSpeed() * mCustomParam.mPercent;
		comMovement.setSpeed(comMovement.getSpeed() + mMoveSpeedUp);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		comMovement.setSpeed(comMovement.getSpeed() - mMoveSpeedUp);
	}
}