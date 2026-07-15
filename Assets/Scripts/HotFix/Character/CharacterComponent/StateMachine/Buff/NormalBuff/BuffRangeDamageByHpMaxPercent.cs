using static GBR;
using static MathUtility;
using static FrameBaseHotFix;

// 参数
public class BuffRangeDamageByHpMaxPercentParam : CharacterBuffParamT<BuffRangeDamageByHpMaxPercentParam>
{
	public float mPercent;						// 最大血量百分比
	public TARGET_BEHAVIOUR_TYPE mTargetType;	// 怪物类型
	public float mRange;						// 范围
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
		registeParam((param) => { mTargetType = (TARGET_BEHAVIOUR_TYPE)param.SToI(); });
		registeParam((param) => { mRange = param.SToF(); });
	}
	protected override void copyInternal(BuffRangeDamageByHpMaxPercentParam other)
	{
		mPercent = other.mPercent;
		mTargetType = other.mTargetType;
		mRange = other.mRange;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0;
		mTargetType = TARGET_BEHAVIOUR_TYPE.NONE;
		mRange = 0.0f;
	}
}

// 击杀怪物时,对周围指定类型的怪物造成当前怪物最大血量百分比的伤害
public class BuffRangeDamageByHpMaxPercent : CharacterBuffT<BuffRangeDamageByHpMaxPercentParam>
{
	protected float mPercent;						// 最大血量百分比
	protected TARGET_BEHAVIOUR_TYPE mTargetType;	// 怪物类型
	protected float mRange;							// 范围
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventKillMonster>(mCharacter.getGUID(), onKillMonster, this);
		mPercent = mCustomParam.mPercent;
		mTargetType = mCustomParam.mTargetType;
		mRange = mCustomParam.mRange;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
		mTargetType = TARGET_BEHAVIOUR_TYPE.NONE;
		mRange = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onKillMonster(EventKillMonster param)
	{
		CharacterMonster monster = param.mMonster;
		int damage = round(monster.getMaxHP() * mPercent);
		var pos = monster.getPosition();
		using var a = new ListScope<CharacterMonster>(out var monsterList);
		if (mTargetType == TARGET_BEHAVIOUR_TYPE.ALL_MONSTER)
		{
			mTowerDefenceSystem.getMonstersInRange(pos, mRange, monsterList);
		}
		else if (mTargetType == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
		{
			mTowerDefenceSystem.getWalkMonstersInRange(pos, mRange, monsterList);
		}
		else if (mTargetType == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
		{
			mTowerDefenceSystem.getFlyMonstersInRange(pos, mRange, monsterList);
		}
		if (monsterList.Count == 0)
		{
			return;
		}
		foreach (CharacterMonster target in monsterList)
		{
			CmdMonsterSetHP.execute(target, null, target.getMonsterData().mHP - damage, -damage, true);
		}
	}
}