using static StringUtility;

// 参数
public class BuffBulletFlyDisIncreaseExploRangeParam : CharacterBuffParamT<BuffBulletFlyDisIncreaseExploRangeParam>
{
	public float mIncreaseRangePercent;         // 每飞行一个格子的距离增加的爆炸范围百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseRangePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffBulletFlyDisIncreaseExploRangeParam other)
	{
		mIncreaseRangePercent = other.mIncreaseRangePercent;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseRangePercent = 0.0f;
	}
}

// 爆炸范围随着飞行距离增加
public class BuffBulletFlyDisIncreaseExploRange : CharacterBuffT<BuffBulletFlyDisIncreaseExploRangeParam>
{
	protected float mIncreaseRangePercent;      // 每飞行一个格子的距离增加的爆炸范围百分比
	public override void enter()
	{
		base.enter();
		mIncreaseRangePercent = mCustomParam.mIncreaseRangePercent;
		mCharacterGame.getGameData().mExplosionRangeIncreaseByFlyDis += mIncreaseRangePercent;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mExplosionRangeIncreaseByFlyDis -= mIncreaseRangePercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseRangePercent = 0.0f;
	}
}