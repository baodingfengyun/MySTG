using System.Collections.Generic;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class TriggerBuffToAllTowerParam : CharacterTriggerParamT<TriggerBuffToAllTowerParam>
{}

// 给所有的塔附加buff
public class TriggerBuffToAllTower : CharacterTriggerT<TriggerBuffToAllTowerParam>
{
	protected bool mTowerChanged;				// 场上的塔是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventGridTowerChange>(onTowerChanged, this);
		mEventSystem.listenEvent<EventTowerDestroy>(onTowerDestroy, this);
		mTowerChanged = true;
	}
	public override void update(float elapsedTime)
	{
		if (mTowerChanged)
		{
			mTowerChanged = false;
			var newList = mTowerDefenceSystem.getTowerList();
			foreach (CharacterTower tower in newList)
			{
				// 新增的塔
				if (!mBuffList.containsKey(tower))
				{
					addBuff(tower);
				}
			}
			using var a = new SafeDictionaryReader<CharacterGame, List<CharacterState>>(mBuffList);
			foreach (CharacterGame item in a.mReadList.Keys)
			{
				// 已经不在的塔
				if (!newList.Contains(item as CharacterTower))
				{
					removeCharacterAddedBuff(item);
				}
			}
		}
		base.update(elapsedTime);
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
		mTowerChanged = false;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
	protected void onTowerDestroy(EventTowerDestroy eventParam)
	{
		if (eventParam.mTower == null)
		{
			return;
		}
		removeCharacterAddedBuff(eventParam.mTower);
	}
}