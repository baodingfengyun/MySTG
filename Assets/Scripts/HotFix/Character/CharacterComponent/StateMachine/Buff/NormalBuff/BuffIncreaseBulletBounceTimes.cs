using static FrameBaseHotFix;

// 参数
public class BuffIncreaseBulletBounceTimesParam : CharacterBuffParamT<BuffIncreaseBulletBounceTimesParam>
{
	public int mIncreaseCount;						// 弹跳次数调整
	public float mIncreaseDamagePercent;			// 弹跳的伤害衰减调整
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseCount = param.SToI(); });
		registeParam((param) => { mIncreaseDamagePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffIncreaseBulletBounceTimesParam other)
	{
		mIncreaseCount = other.mIncreaseCount;
		mIncreaseDamagePercent = other.mIncreaseDamagePercent;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
		mIncreaseDamagePercent = 0.0f;
	}
}

// 增加弹跳子弹的弹跳次数
public class BuffIncreaseBulletBounceTimes : CharacterBuffT<BuffIncreaseBulletBounceTimesParam>
{
	protected int mIncreaseCount;                   // 弹跳次数调整
	protected float mIncreaseDamagePercent;			// 弹跳的伤害衰减调整
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventTowerSkillChanged>(mCharacter.getGUID(), onTowerSkillChange, this);
		mIncreaseCount = mCustomParam.mIncreaseCount;
		mIncreaseDamagePercent = mCustomParam.mIncreaseDamagePercent;
		doIncrese(mIncreaseCount, mIncreaseDamagePercent);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		doIncrese(-mIncreaseCount, -mIncreaseDamagePercent);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
		mIncreaseDamagePercent = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerSkillChange(EventTowerSkillChanged param)
	{
		doIncrese(mIncreaseCount, mIncreaseDamagePercent);
	}
	protected void doIncrese(int increaseCount, float increaseDamagePercent)
	{
		TowerSkill skill = (mCharacterGame as CharacterTower).getComSkill().getCurSkill();
		if (skill is TowerSkill_Bounce bounceSkill)
		{
			bounceSkill.addBounceTimesIncrease(increaseCount);
			bounceSkill.addBounceDamageIncrease(increaseDamagePercent);
		}
	}
}