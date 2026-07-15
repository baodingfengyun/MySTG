using static StringUtility;

// 参数
public class BuffTowerExploRangeUpParam : CharacterBuffParamT<BuffTowerExploRangeUpParam>
{
	public float mIncrease;         // 爆炸范围的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffTowerExploRangeUpParam other)
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

// 防御塔子弹爆炸范围增加
public class BuffTowerExploRangeUp : CharacterBuffT<BuffTowerExploRangeUpParam>
{
	protected float mIncrease;      // 爆炸范围的百分比
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mCharacterGame.getGameData().mExplosionRangeIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mExplosionRangeIncrease -= mIncrease;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}