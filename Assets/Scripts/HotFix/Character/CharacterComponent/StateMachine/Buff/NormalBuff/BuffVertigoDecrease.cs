using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffVertigoDecreaseParam : CharacterBuffParamT<BuffVertigoDecreaseParam>
{
	public float mProbabilityDecreasePercent;   // 眩晕概率降低的百分比
	public float mVertigoTimeDecreasePercent;	// 眩晕时间降低的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mProbabilityDecreasePercent = param.SToF(); });
		registeParam((param) => { mVertigoTimeDecreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffVertigoDecreaseParam other)
	{
		mProbabilityDecreasePercent = other.mProbabilityDecreasePercent;
		mVertigoTimeDecreasePercent = other.mVertigoTimeDecreasePercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mProbabilityDecreasePercent = 0.0f;
		mVertigoTimeDecreasePercent = 0.0f;
	}
}

// 降低被眩晕的概率和眩晕时间
public class BuffVertigoDecrease : CharacterBuffT<BuffVertigoDecreaseParam>
{
	protected float mProbabilityDecreasePercent;   // 眩晕概率降低的百分比
	protected float mVertigoTimeDecreasePercent;   // 眩晕时间降低的百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mProbabilityDecreasePercent = 0.0f;
		mVertigoTimeDecreasePercent = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mProbabilityDecreasePercent = mCustomParam.mProbabilityDecreasePercent;
		mVertigoTimeDecreasePercent = mCustomParam.mVertigoTimeDecreasePercent;
		// 添加触发buff时计算概率的监听
		mCharacterGame.addTriggerProbabilityCallback(onTriggerProbability);
		// 只监听怪物添加buff
		mEventSystem.listenEvent<EventMonsterAddBuff>(onMonsterAddBuff, this);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.removeTriggerProbabilityCallback(onTriggerProbability);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected int onTriggerProbability(CharacterTrigger trigger)
	{
		// 如果触发器要添加的buff中包含眩晕状态,则触发的几率降低
		foreach (int buffID in trigger.getBuffDetailIDList())
		{
			EDBuffDetail detail = mExcelBuffDetail.query(buffID);
			if (mStateManager.getStateType(detail.mBuffTypeID) == typeof(BuffVertigo))
			{
				return (int)(trigger.getProbability() * (1.0f - mProbabilityDecreasePercent));
			}
		}
		return trigger.getProbability();
	}
	protected void onMonsterAddBuff(EventMonsterAddBuff eventParam)
	{
		// 减少眩晕状态的持续时间
		var buff = eventParam.mMonster.getFirstState<BuffVertigo>();
		if (buff != null && buff.getStateTime() > 0.0f)
		{
			buff.setStateTime((buff.getStateTime() * (1.0f - mVertigoTimeDecreasePercent)).clampMin(0.01f));
		}
	}
}