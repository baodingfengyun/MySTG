using static FrameBaseHotFix;

// 参数
public class TriggerBuffOnWaveStartParam : CharacterTriggerParamT<TriggerBuffOnWaveStartParam>
{ }

//  每波开始，概率添加某些状态，没随机到就会移除
public class TriggerBuffOnWaveStart : CharacterTriggerT<TriggerBuffOnWaveStartParam>
{
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventWaveChange>(onWaveChange, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	private void onWaveChange(EventWaveChange param)
	{
		removeCharacterAddedBuff(mCharacterGame);
		if (triggerProbability(mCharacterGame))
		{
			onTrigger();
			addBuff(mCharacterGame);
		}
	}
}