using System;
using System.Collections.Generic;
using static MathUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class TriggerBuffToTypeBuffMonsterParam : CharacterTriggerParamT<TriggerBuffToTypeBuffMonsterParam>
{
	public int mBuffTypeID;
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mBuffTypeID = param.SToI(); });
	}
	public override void check()
	{
		base.check();
		checkDataRefByBuffDetail(mExcelBuff, mBuffTypeID);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffTypeID = 0;
	}
	protected override void copyInternal(TriggerBuffToTypeBuffMonsterParam other)
	{
		base.copyInternal(other);
		mBuffTypeID = other.mBuffTypeID;
	}
}

// 范围内拥有指定buff的敌人会附加指定buff,敌人超出范围时,会移除buff
public class TriggerBuffToTypeBuffMonster : CharacterTriggerT<TriggerBuffToTypeBuffMonsterParam>
{
	protected Type mBuffType;
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffType = null;
	}
	public override void enter()
	{
		base.enter();
		mBuffType = mStateManager.getStateType(mCustomParam.mBuffTypeID);
		mEventSystem.listenEvent<EventMonsterDestroy>(onMonsterDestroy, this);
		mEventSystem.listenEvent<EventMonsterDie>(onMonsterDie, this);
	}
	public override void update(float elapsedTime)
	{
		// 之前附加过,现在不在范围内,则移除buff
		using var a = new SafeDictionaryReader<CharacterGame, List<CharacterState>>(mBuffList);
		foreach (CharacterGame item in a.mReadList.Keys)
		{
			if (lengthGreater(item.getPosition() - mCharacterGame.getPosition(), mCharacterGame.getRange()))
			{
				removeCharacterAddedBuff(item);
			}
		}

		using var b = new ListScope<CharacterMonster>(out var monsterList);
		mTowerDefenceSystem.getMonsterWithTypeBuffInRange(mCharacterGame.getPosition(), mCharacterGame.getRange(), mBuffType, monsterList);
		foreach (CharacterMonster monster in monsterList)
		{
			// 没有附加过的才会附加
			if (mBuffList.containsKey(monster))
			{
				addBuff(monster);
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