using static FrameBaseHotFix;

// 参数
public class TriggerFireCountSkillHitParam : CharacterTriggerParamT<TriggerFireCountSkillHitParam>
{
	public int mFireCount;         // 技能释放次数
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mFireCount = param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mFireCount = 0;
	}
	protected override void copyInternal(TriggerFireCountSkillHitParam other)
	{
		base.copyInternal(other);
		mFireCount = other.mFireCount;
	}
}

// 释放一定次数技能后的命中时触发
public class TriggerFireCountSkillHit : CharacterTriggerT<TriggerFireCountSkillHitParam>
{
	protected int mNeedFireCount;
	protected int mCurFireCount;
	public override void enter()
	{
		base.enter();
		mNeedFireCount = mCustomParam.mFireCount;
		mCurFireCount = 0;
		mEventSystem.listenEvent<EventPostFireSkill>(mCharacter.getGUID(), onPostFireSkill, this);
		mEventSystem.listenEvent<EventHitCharacter>(mCharacterGame.getGUID(), onHit, this);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedFireCount = 0;
		mCurFireCount = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onPostFireSkill(EventPostFireSkill param)
	{
		++mCurFireCount;
	}
	protected void onHit(EventHitCharacter param)
	{
		if (mCurFireCount < mNeedFireCount)
		{
			return;
		}
		mCurFireCount = 0;
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(mCharacterGame) || !triggerProbability(mCharacterGame))
		{
			return;
		}
		onTrigger();
		using var a = new ClassScope<INT>(out var tempDamage);
		tempDamage.mValue = param.mDamage;
		addBuff(param.mTarget, tempDamage, param.mBullet, param.mBullet.getSkill());
	}
}