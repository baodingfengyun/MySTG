using static StringUtility;

// 参数
public class BuffDamageOnceParam : CharacterBuffParamT<BuffDamageOnceParam>
{
	public int mSingleDamage;         // 伤害值
	public override void registeAllParam()
	{
		registeParam((param) => { mSingleDamage = param.SToI(); });
	}
	protected override void copyInternal(BuffDamageOnceParam other)
	{
		mSingleDamage = other.mSingleDamage;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mSingleDamage = 0;
	}
}

// 单次固定伤害
public class BuffDamageOnce : CharacterBuffT<BuffDamageOnceParam>
{
	public override void enter()
	{
		base.enter();
		if (mCharacter is CharacterMonster monster)
		{
			int damage = mCustomParam.mSingleDamage;
			CmdMonsterSetHP.execute(monster, null, monster.getMonsterData().mHP - damage, -damage, true, HP_DELTA.NORMAL_DAMAGE);
		}
	}
}