using static StringUtility;

// 参数
public class BuffMoveSpeedUpValueParam : CharacterBuffParamT<BuffMoveSpeedUpValueParam>
{
	public float mSpeed;         // 加速的数值
	public override void registeAllParam()
	{
		registeParam((param) => { mSpeed = param.SToF(); });
	}
	protected override void copyInternal(BuffMoveSpeedUpValueParam other)
	{
		mSpeed = other.mSpeed;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mSpeed = 0.0f;
	}
}

// 固定数值加速
public class BuffMoveSpeedUpValue : CharacterBuffT<BuffMoveSpeedUpValueParam>
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
		mMoveSpeedUp = mCustomParam.mSpeed;
		comMovement.setSpeed(comMovement.getSpeed() + mMoveSpeedUp);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		comMovement.setSpeed(comMovement.getSpeed() - mMoveSpeedUp);
	}
}