using static StringUtility;

// 参数
public class BuffBulletFlyDisIncreaseDamageParam : CharacterBuffParamT<BuffBulletFlyDisIncreaseDamageParam>
{
	public float mIncreasePercent;         // 每飞行一个格子的距离增加的伤害百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffBulletFlyDisIncreaseDamageParam other)
	{
		mIncreasePercent = other.mIncreasePercent;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercent = 0.0f;
	}
}

// 子弹伤害随着飞行距离增加
public class BuffBulletFlyDisIncreaseDamage : CharacterBuffT<BuffBulletFlyDisIncreaseDamageParam>
{
	protected float mIncreasePercent;      // 每飞行一个格子的距离增加的伤害百分比
	public override void enter()
	{
		base.enter();
		mIncreasePercent = mCustomParam.mIncreasePercent;
		mCharacterGame.getGameData().mDamageIncreaseByFlyDis += mIncreasePercent;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mDamageIncreaseByFlyDis -= mIncreasePercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercent = 0.0f;
	}
}