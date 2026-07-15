using static StringUtility;

// 参数
public class BuffAttackSpeedUpParam : CharacterBuffParamT<BuffAttackSpeedUpParam>
{
	public float mIncreaseAttackSpeed;		// 减少的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseAttackSpeed = param.SToF(); });
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseAttackSpeed = 0.0f;
	}
	protected override void copyInternal(BuffAttackSpeedUpParam other)
	{
		mIncreaseAttackSpeed = other.mIncreaseAttackSpeed;
	}
}

// 增加攻速,实际就是减少技能CD
public class BuffAttackSpeedUp : CharacterBuffT<BuffAttackSpeedUpParam>
{
	public float mIncreaseAttackSpeed;   // 减少的百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseAttackSpeed = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mIncreaseAttackSpeed = mCustomParam.mIncreaseAttackSpeed;
		mCharacterGame.getGameData().addAttackSpeed(mIncreaseAttackSpeed);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().removeAttackSpeed(mIncreaseAttackSpeed);
	}
}