using static StringUtility;

// 参数
public class BuffAttackSpeedUpGongJianShouParam : CharacterBuffParamT<BuffAttackSpeedUpGongJianShouParam>
{
	public float mIncreaseAttackSpeed;		// 减少的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseAttackSpeed = param.SToF(); });
	}
	protected override void copyInternal(BuffAttackSpeedUpGongJianShouParam other)
	{
		mIncreaseAttackSpeed = other.mIncreaseAttackSpeed;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseAttackSpeed = 0.0f;
	}
}

// 弓手特殊攻速增加
public class BuffAttackSpeedUpGongJianShou : CharacterBuffT<BuffAttackSpeedUpGongJianShouParam>
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