using static StringUtility;

// 参数
public class BuffRecoverHPPercentOnceParam : CharacterBuffParamT<BuffRecoverHPPercentOnceParam>
{
	public float mPercent;         // 回血百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffRecoverHPPercentOnceParam other)
	{
		mPercent = other.mPercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
}

// 单次百分比回血
public class BuffRecoverHPPercentOnce : CharacterBuffT<BuffRecoverHPPercentOnceParam>
{
	public override void enter()
	{
		base.enter();
		var monster = mCharacterGame as CharacterMonster;
		if (monster == null)
		{
			return;
		}
		int hp = (int)(mCharacterGame.getMaxHP() * mCustomParam.mPercent);
		CmdMonsterSetHP.execute(monster, null, mCharacterGame.getHP() + hp, hp);
	}
}