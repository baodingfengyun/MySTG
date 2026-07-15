using static FrameBaseHotFix;

// 参数
public class TriggerBuffWhenBulletConsumeParam : CharacterTriggerParamT<TriggerBuffWhenBulletConsumeParam>
{
	public int mMaxCount;					// 子弹消耗次数
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mMaxCount = param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMaxCount = 0;
	}
	protected override void copyInternal(TriggerBuffWhenBulletConsumeParam other)
	{
		base.copyInternal(other);
		mMaxCount = other.mMaxCount;
	}
}

// 消耗n个子弹后触发buff
public class TriggerBuffWhenBulletConsume : CharacterTriggerT<TriggerBuffWhenBulletConsumeParam>
{
	public int mMaxCount;					// 子弹消耗次数
	public int mCurCount;					// 子弹当前消耗的数量
	public override void enter()
	{
		base.enter();
		mMaxCount = mCustomParam.mMaxCount;
		mEventSystem.listenEvent<EventBulletConsume>(mCharacter.getGUID(), onBulletConsume, this);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMaxCount = 0;
		mCurCount = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBulletConsume(EventBulletConsume eventParam)
	{
		if (++mCurCount >= mMaxCount)
		{
			mCurCount = 0;
			addBuff(mCharacterGame);
		}
	}
}