using static FrameUtility;
using static StringUtility;

// 参数
public class BuffDamageUpStrengthMonsterParam : CharacterBuffParamT<BuffDamageUpStrengthMonsterParam>
{
	public float mPercent;				// 伤害提升的百分比
	public MONSTER_STRENGTH mStrength;	// 强度
	public BuffDamageUpStrengthMonsterParam()
	{
		mStrength = new();
	}
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
		registeParam((param) => { mStrength = (MONSTER_STRENGTH)param.SToI(); });
	}
	protected override void copyInternal(BuffDamageUpStrengthMonsterParam other)
	{
		mPercent = other.mPercent;
		mStrength = other.mStrength;
	}
	public override void check()
	{
		checkEnum(mStrength);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
		mStrength = MONSTER_STRENGTH.NONE;
	}
}

// 伤害增加
public class BuffDamageUpStrengthMonster : CharacterBuffT<BuffDamageUpStrengthMonsterParam>
{
	protected float mPercent;			// 伤害提升百分比
	public MONSTER_STRENGTH mStrength;	// 强度
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
		mStrength = MONSTER_STRENGTH.NONE;
	}
	public override void enter()
	{
		base.enter();
		mPercent = mCustomParam.mPercent;
		mStrength = mCustomParam.mStrength;
		CharacterTowerData data = (mCharacterGame as CharacterTower).getTowerData();
		if (mStrength == MONSTER_STRENGTH.ELITE)
		{
			data.setEliteMonsterDamageIncrease(data.getEliteMonsterDamageIncrease() + mPercent);
		}
		else if (mStrength == MONSTER_STRENGTH.BOSS)
		{
			data.setBossMonsterDamageIncrease(data.getBossMonsterDamageIncrease() + mPercent);
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		CharacterTowerData data = (mCharacterGame as CharacterTower).getTowerData();
		if (mStrength == MONSTER_STRENGTH.ELITE)
		{
			data.setEliteMonsterDamageIncrease(data.getEliteMonsterDamageIncrease() - mPercent);
		}
		else if (mStrength == MONSTER_STRENGTH.BOSS)
		{
			data.setBossMonsterDamageIncrease(data.getBossMonsterDamageIncrease() - mPercent);
		}
	}
}