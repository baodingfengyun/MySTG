using static StringUtility;

// 参数
public class BuffBulletSpeedUpParam : CharacterBuffParamT<BuffBulletSpeedUpParam>
{
	public float mIncreasePercent;         // 增加的速度百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffBulletSpeedUpParam other)
	{
		mIncreasePercent = other.mIncreasePercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercent = 0.0f;
	}
}

// 增加子弹飞行速度
public class BuffBulletSpeedUp : CharacterBuffT<BuffBulletSpeedUpParam>
{
	public float mSpeedUp;			// 加速的绝对值
	public override void resetProperty()
	{
		base.resetProperty();
		mSpeedUp = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mSpeedUp = mCustomParam.mIncreasePercent;
		mCharacterGame.getGameData().mBulletSpeedIncrease += mSpeedUp;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mBulletSpeedIncrease -= mSpeedUp;
	}
}