using static FrameBaseHotFix;

// 参数
public class TriggerBuffByWaveBulletCountParam : CharacterTriggerParamT<TriggerBuffByWaveBulletCountParam>
{
	public int mBulletCount;		// 需要达到的数量
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mBulletCount = param.SToI(); });
	}
	protected override void copyInternal(TriggerBuffByWaveBulletCountParam other)
	{
		base.copyInternal(other);
		mBulletCount = other.mBulletCount;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBulletCount = 0;
	}
}

// 塔释放一定数量子弹,触发buff,直到波次结束
public class TriggerBuffByWaveBulletCount : CharacterTriggerT<TriggerBuffByWaveBulletCountParam>
{
	public int mBulletCount;		// 需要达到的数量
	public int mCurCount;			// 当前的数量
	public override void enter()
	{
		base.enter();
		mBulletCount = mCustomParam.mBulletCount;
		mEventSystem.listenEvent<EventBulletWaveCountChanged>(mCharacter.getGUID(), onBulletWaveCountChanged, this);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		// 移除所有塔的增幅
		removeAllAdded();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBulletCount = 0;
		mCurCount = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBulletWaveCountChanged(EventBulletWaveCountChanged eventParam)
	{
		bool curActive = mCurCount >= mBulletCount;
		if (curActive != eventParam.mCount >= mBulletCount)
		{
			if(curActive)
			{
				removeAllAdded();
			}
			else
			{
				addBuff(mCharacterGame);
			}
		}
		mCurCount = eventParam.mCount;
	}
}