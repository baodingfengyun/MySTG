using UnityEngine;
using static TimeUtility;

// 参数
public class TriggerTimeIntervalParam : CharacterTriggerParamT<TriggerTimeIntervalParam>
{}

// 每隔一定时间触发一次
public class TriggerTimeInterval : CharacterTriggerT<TriggerTimeIntervalParam>
{
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		// 是否已冷却
		if (getNowTimeStampMS() - mLastTriggerTime >= mCDTime * Time.timeScale)
		{
			tryTrigger();
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void tryTrigger()
	{
		// 检查冷却,叠加次数等前提条件,触发几率
		if (!canTrigger(mCharacterGame) || !triggerProbability(mCharacterGame))
		{
			return;
		}
		onTrigger();

		addBuff(mCharacterGame);
	}
}