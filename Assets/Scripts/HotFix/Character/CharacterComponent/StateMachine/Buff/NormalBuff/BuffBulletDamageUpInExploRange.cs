using static FrameBaseHotFix;

// 参数
public class BuffBulletDamageUpInExploRangeParam : CharacterBuffParamT<BuffBulletDamageUpInExploRangeParam>
{
	public float mRangePercent;         // 距离爆炸中心的范围百分比
	public float mIncreaseDamage;		// 增加的伤害百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mRangePercent = param.SToF(); });
		registeParam((param) => { mIncreaseDamage = param.SToF(); });
	}
	protected override void copyInternal(BuffBulletDamageUpInExploRangeParam other)
	{
		mRangePercent = other.mRangePercent;
		mIncreaseDamage = other.mIncreaseDamage;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mRangePercent = 0.0f;
		mIncreaseDamage = 0.0f;
	}
}

// 对子弹爆炸一定范围内的敌人伤害增加
public class BuffBulletDamageUpInExploRange : CharacterBuffT<BuffBulletDamageUpInExploRangeParam>
{
	protected float mRangePercent;         // 距离爆炸中心的范围百分比
	protected float mIncreaseDamage;       // 增加的伤害百分比
	public override void enter()
	{
		base.enter();
		mRangePercent = mCustomParam.mRangePercent;
		mIncreaseDamage = mCustomParam.mIncreaseDamage;
		mEventSystem.listenEvent<EventWillHitCharacter>(mCharacterGame.getGUID(), onWillHit, this);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mRangePercent = 0.0f;
		mIncreaseDamage = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onWillHit(EventWillHitCharacter eventParam)
	{
		float curDis = (eventParam.mBullet.getPosition() - eventParam.mTarget.getPosition()).resetY().getLength();
		if (curDis < mRangePercent * eventParam.mBullet.getRealtimeRange())
		{
			eventParam.mDamage.mValue = (int)(eventParam.mDamage.mValue * (1.0f + mIncreaseDamage));
		}
	}
}