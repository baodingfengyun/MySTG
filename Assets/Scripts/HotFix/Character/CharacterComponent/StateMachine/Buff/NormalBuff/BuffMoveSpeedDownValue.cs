
// 参数
public class BuffMoveSpeedDownValueParam : CharacterBuffParamT<BuffMoveSpeedDownValueParam>
{
	public float mSpeed;         // 减速的数值
	public override void registeAllParam()
	{
		registeParam((param) => { mSpeed = param.SToF(); });
	}
	protected override void copyInternal(BuffMoveSpeedDownValueParam other)
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

// 固定数值减速。(未做)减速到负数时,可以让怪物后退
public class BuffMoveSpeedDownValue : CharacterBuffT<BuffMoveSpeedDownValueParam>
{
	protected float mSlowDown;             // 减速的绝对值
	public BuffMoveSpeedDownValue()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override void destroy()
	{
		base.destroy();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mSlowDown = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		mSlowDown = mCustomParam.mSpeed;
		mSlowDown = mSlowDown.clampMax(comMovement.getSpeed());
		comMovement.setSpeed(comMovement.getSpeed() - mSlowDown);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		comMovement.setSpeed(comMovement.getSpeed() + mSlowDown);
	}
}