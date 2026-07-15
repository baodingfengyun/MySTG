using static MathUtility;
using static StringUtility;

// 参数
public class BuffMoveSpeedDownHuoPaoExplosionAreaParam : CharacterBuffParamT<BuffMoveSpeedDownHuoPaoExplosionAreaParam>
{
	public float mPercent;				// 减速的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffMoveSpeedDownHuoPaoExplosionAreaParam other)
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

// 火炮塔子弹爆炸残留提供的减速buff
public class BuffMoveSpeedDownHuoPaoExplosionArea : CharacterBuffT<BuffMoveSpeedDownHuoPaoExplosionAreaParam>
{
	protected float mSlowDown;				// 减速的绝对值
	public BuffMoveSpeedDownHuoPaoExplosionArea()
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
		clampMax(ref mSlowDown, comMovement.getSpeed());
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
		// 不做处理当作只能叠一层，为了保证范围buff在移除时，不影响范围buff上的该buff，效果本身并不叠加
	}
}