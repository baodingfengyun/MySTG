using System.Collections.Generic;
using static FrameBaseHotFix;
using static FrameUtility;
using static GBR;

// 参数
public class TriggerBuffByEnterTowerRangeParam : CharacterTriggerParamT<TriggerBuffByEnterTowerRangeParam>
{
	public bool mRemoveWhenLeave;				// 怪物离开时是否移除
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mRemoveWhenLeave = param.SToI() != 0; });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mRemoveWhenLeave = false;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void copyInternal(TriggerBuffByEnterTowerRangeParam other)
	{
		base.copyInternal(other);
		mRemoveWhenLeave = other.mRemoveWhenLeave;
	}
}

// 对进入 塔/英雄 范围的敌人附加buff
public class TriggerBuffByEnterTowerRange : CharacterTriggerT<TriggerBuffByEnterTowerRangeParam>
{
	public bool mRemoveWhenLeave;				// 怪物离开时是否移除
	public HashSet<long> mEnterMonsters = new();// 范围内的怪物
	public float mTimer;						// 计时器
	public const float INTERVAL = 0.1f;			// 检查间隔
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventWaveChange>(onWaveChange, this);
		mRemoveWhenLeave = mCustomParam.mRemoveWhenLeave;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if(mCharacter == null)
		{
			return;
		}
		if (tickTimerLoop(ref mTimer, elapsedTime, INTERVAL))
		{
			using var a = new ListScope<CharacterMonster>(out var inRangeMonsters);
			mTowerDefenceSystem.getMonstersInRange(mCharacter.getPosition(), mCharacterGame.getRange(), inRangeMonsters);
			foreach (CharacterMonster each in inRangeMonsters)
			{
				if(mEnterMonsters.Add(each.getGUID()))
				{
					addBuff(each);
				}
			}
			if (mRemoveWhenLeave)
			{
				foreach (CharacterMonster monster in mTowerDefenceSystem.getMonsterMainList())
				{
					if (mEnterMonsters.Contains(monster.getGUID()) && !inRangeMonsters.Contains(monster))
					{
						removeCharacterAddedBuff(monster);
					}
				}
			}
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		removeAllAdded();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mRemoveWhenLeave = false;
		mEnterMonsters.Clear();
		mTimer = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onWaveChange(EventWaveChange param)
	{
		mEnterMonsters.Clear();
	}
}