using static StringUtility;

// 参数
public class BuffAttackSpeedDownParam : CharacterBuffParamT<BuffAttackSpeedDownParam>
{
	public float mDecreaseAttackSpeed;		// 降低的攻速
	public override void registeAllParam()
	{
		registeParam((param) => { mDecreaseAttackSpeed = param.SToF(); });
	}
	protected override void copyInternal(BuffAttackSpeedDownParam other)
	{
		mDecreaseAttackSpeed = other.mDecreaseAttackSpeed;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mDecreaseAttackSpeed = 0.0f;
	}
}

// 降低攻速
public class BuffAttackSpeedDown : CharacterBuffT<BuffAttackSpeedDownParam>
{
	public float mDecreaseAttackSpeed;   // 降低的攻速
	public override void resetProperty()
	{
		base.resetProperty();
		mDecreaseAttackSpeed = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mDecreaseAttackSpeed = mCustomParam.mDecreaseAttackSpeed;
		mCharacterGame.getGameData().removeAttackSpeed(mDecreaseAttackSpeed);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().addAttackSpeed(mDecreaseAttackSpeed);
	}
}