using static MathUtility;
using static StringUtility;

// 参数
public class BuffMoveSpeedDownNoEffectParam : CharacterBuffParamT<BuffMoveSpeedDownNoEffectParam>
{
	public float mPercent;         // 减速的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffMoveSpeedDownNoEffectParam other) 
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
public class BuffMoveSpeedDownNoEffect : CharacterBuffT<BuffMoveSpeedDownNoEffectParam>
{
	protected float mSlowDown;				// 减速的绝对值
	public BuffMoveSpeedDownNoEffect()
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
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		mSlowDown = comMovement.getSpeed() * mCustomParam.mPercent * (1.0f + mCharacterGame.getGameData().mSlowDownIncrease);
		clampMax(ref mSlowDown, comMovement.getSpeed());
		comMovement.setSpeed(comMovement.getSpeed() - mSlowDown);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		comMovement.setSpeed(comMovement.getSpeed() + mSlowDown);
	}
}