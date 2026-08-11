using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class TriggerBuffToInvisibleMonsterParam : CharacterTriggerParamT<TriggerBuffToInvisibleMonsterParam>
{}

// 范围内隐身的敌人会附加指定buff,敌人超出范围时,会移除buff
public class TriggerBuffToInvisibleMonster : CharacterTriggerT<TriggerBuffToInvisibleMonsterParam>
{
	protected float mTickTimer;
	public override void resetProperty()
	{
		base.resetProperty();
		mTickTimer = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventMonsterDestroy>(onMonsterDestroy, this);
		mEventSystem.listenEvent<EventMonsterDie>(onMonsterDie, this);
	}
	public override void update(float elapsedTime)
	{
		// 每0.2秒才检测一次
		if (tickTimerLoop(ref mTickTimer, elapsedTime, 0.2f))
		{
			// 之前附加过,现在不在范围内,则移除buff
			foreach (var item in mBuffList)
			{
				if ((item.Key.getPosition() - mCharacterGame.getPosition()).lengthGreater(mCharacterGame.getRange()))
				{
					removeCharacterAddedBuff(item.Key);
				}
			}

			using var b = new ListScope<CharacterMonster>(out var monsterList);
			mTowerDefenceSystem.getInvisibleMonsterInRange(mCharacterGame.getPosition(), mCharacterGame.getRange(), monsterList);
			foreach (CharacterMonster monster in monsterList)
			{
				// 没有附加过的才会附加
				if (!mBuffList.containsKey(monster))
				{
					addBuff(monster);
				}
			}
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		removeAllAdded();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onMonsterDestroy(EventMonsterDestroy eventParam)
	{
		removeCharacterAddedBuff(eventParam.mMonster);
	}
	protected void onMonsterDie(EventMonsterDie eventParam)
	{
		removeCharacterAddedBuff(eventParam.mMonster);
	}
}