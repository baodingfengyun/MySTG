
// 参数
public class BuffMoveSpeedDownZhenDownTowerRangeParam : CharacterBuffParamT<BuffMoveSpeedDownZhenDownTowerRangeParam>
{
	public float mPercent;         // 减速的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffMoveSpeedDownZhenDownTowerRangeParam other)
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

// 百分比减速,固定百分比
public class BuffMoveSpeedDownZhenDownTowerRange : CharacterBuffT<BuffMoveSpeedDownZhenDownTowerRangeParam>
{
	protected float mSlowDown;				// 减速的绝对值
	public BuffMoveSpeedDownZhenDownTowerRange()
	{
		mMutexType = STATE_MUTEX.OVERLAP_LAYER;
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
		mSlowDown = comMovement.getSpeed() * mCustomParam.mPercent * (1.0f + mCharacterGame.getGameData().mSlowDownIncrease);
		mSlowDown = mSlowDown.clampMax(comMovement.getSpeed());
		comMovement.setSpeed(comMovement.getSpeed() - mSlowDown);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		comMovement.setSpeed(comMovement.getSpeed() + mSlowDown);
	}
	public override void addSameState(CharacterState newState)
	{
		// 不做处理当作只能叠一层，为了保证此塔在移除时，不影响其他塔上的该buff，效果本身并不叠加
	}
}