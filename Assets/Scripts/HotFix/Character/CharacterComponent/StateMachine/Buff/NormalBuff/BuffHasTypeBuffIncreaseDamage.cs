using static FrameBaseHotFix;

// 参数
public class BuffHasTypeBuffIncreaseDamageParam : CharacterBuffParamT<BuffHasTypeBuffIncreaseDamageParam>
{
	public int mState;					// 指定buff类型
	public float mIncreasePercent;      // 增加百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mState = param.SToI(); });
		registeParam((param) => { mIncreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffHasTypeBuffIncreaseDamageParam other)
	{
		mState = other.mState;
		mIncreasePercent = other.mIncreasePercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mState = 0;
		mIncreasePercent = 0.0f;
	}
}

// 攻击拥有指定buff类型的敌人时伤害增加
public class BuffHasTypeBuffIncreaseDamage : CharacterBuffT<BuffHasTypeBuffIncreaseDamageParam>
{
	protected int mState;				// 指定buff类型
	protected float mIncreasePercent;   // 增加百分比
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventWillHitCharacter>(mCharacter.getGUID(), onWillHit, this);
		mState = mCustomParam.mState;
		mIncreasePercent = mCustomParam.mIncreasePercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mState = 0;
		mIncreasePercent = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onWillHit(EventWillHitCharacter eventParam)
	{
		if (!eventParam.mTarget.hasState(mStateManager.getStateType(mState)))
		{
			return;
		}
		eventParam.mDamage.mValue += (int)(eventParam.mDamage.mValue * mIncreasePercent);
	}
}