
// 参数
public class BuffMoveSpeedDownByLevelParam : CharacterBuffParamT<BuffMoveSpeedDownByLevelParam>
{
	public float mPercent;         // 减速的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffMoveSpeedDownByLevelParam other)
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
public class BuffMoveSpeedDownByLevel : CharacterBuffT<BuffMoveSpeedDownByLevelParam>
{
	protected float mSlowDown;     // 减速的绝对值
	public BuffMoveSpeedDownByLevel()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mSlowDown = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		int towerStar = (mCustomParam.mSource as CharacterTower).getTowerData().mTableData.mStar;
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		float ratio = 5 * (towerStar * 0.2f + 1).sqrt() * 0.01f;
		mSlowDown = comMovement.getSpeed() * ratio * (1.0f + mCharacterGame.getGameData().mSlowDownIncrease);
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