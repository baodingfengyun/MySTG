using static StringUtility;

// 参数
public class BuffTowerExploRangeDownParam : CharacterBuffParamT<BuffTowerExploRangeDownParam>
{
	public float mIncrease;         // 爆炸范围减少的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffTowerExploRangeDownParam other)
	{
		mIncrease = other.mIncrease;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}

// 防御塔子弹爆炸范围减少
public class BuffTowerExploRangeDown : CharacterBuffT<BuffTowerExploRangeDownParam>
{
	protected float mIncrease;      // 爆炸范围减少的百分比
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mCharacterGame.getGameData().mExplosionRangeIncrease -= mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mExplosionRangeIncrease += mIncrease;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}