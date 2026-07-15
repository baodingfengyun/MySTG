using static FrameBaseHotFix;

// 参数
public class BuffDamageUpToDebuffMonsterParam : CharacterBuffParamT<BuffDamageUpToDebuffMonsterParam>
{
	public float mIncrease;			// 伤害提升的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
	}
	protected override void copyInternal(BuffDamageUpToDebuffMonsterParam other)
	{
		mIncrease = other.mIncrease;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
}

// 对处于异常状态下的单位造成的伤害提升
public class BuffDamageUpToDebuffMonster : CharacterBuffT<BuffDamageUpToDebuffMonsterParam>
{
	protected float mIncrease;		// 伤害提升百分比
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mEventSystem.listenEvent<EventWillHitCharacter>(mCharacterGame.getGUID(), onWillHitCharacter, this);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onWillHitCharacter(EventWillHitCharacter eventParam)
	{
		COMCharacterStateMachine target = eventParam.mTarget.getStateMachine();
		if (target.hasStateGroup<StateGroupDebuff2>())
		{
			eventParam.mDamage.mValue += (int)(eventParam.mDamage.mValue * mIncrease);
		}
	}
}