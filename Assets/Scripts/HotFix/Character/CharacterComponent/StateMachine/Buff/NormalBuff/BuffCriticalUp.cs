using static StringUtility;

// 参数
public class BuffCriticalUpParam : CharacterBuffParamT<BuffCriticalUpParam>
{
	public float mIncrease;         // 射程增加的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffCriticalUpParam other)
	{
		mIncrease = other.mIncrease;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}

// 暴击率增加（攻防数值族代表，与 BuffAttackUp/BuffDamageUp/BuffDefenceDownPercent 等数十个类同构）。
// 范式：enter 从 mCustomParam 拷值到成员，再加到 GameData 对应字段；leave 精确减回。
// 本类改 mCriticalIncrease，同类还有 mCriticalDamageIncrease(暴伤)、mIncreaseAttackPercent(攻击%)、
// mEvasion(闪避)等——全是"改一个字段"的薄封装，差异只在字段名和加/减方向。
public class BuffCriticalUp : CharacterBuffT<BuffCriticalUpParam>
{
	public float mIncrease;		// 增加的暴击率
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mCharacterGame.getGameData().mCriticalIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mCriticalIncrease -= mIncrease;
	}
}