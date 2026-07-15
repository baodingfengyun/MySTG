using System.Collections.Generic;
using static GBR;
using static FrameBaseHotFix;

// 参数
public class BuffRangeEnemyBurnToIncreaseParam : CharacterBuffParamT<BuffRangeEnemyBurnToIncreaseParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 范围内敌人身上的燃烧伤害由递减改为递增
public class BuffRangeEnemyBurnToIncrease : CharacterBuffT<BuffRangeEnemyBurnToIncreaseParam>
{
	protected HashSet<CharacterMonster> mTargetList = new();
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventMonsterDestroy>(onMonsterDestroy, this);
		mEventSystem.listenEvent<EventMonsterDie>(onMonsterDie, this);
	}
	public override void update(float elapsedTime)
	{
		// 查找范围内的敌人
		using var a = new ListScope<CharacterMonster>(out var tempList);
		mTowerDefenceSystem.getMonstersInRange(mCharacterGame.getPosition(), mCharacterGame.getRange(), tempList);
		// 恢复已经超出范围的怪物
		foreach (CharacterMonster item in mTargetList)
		{
			if (!tempList.Contains(item))
			{
				item.getFirstState<BuffBurn>()?.increaseToDecreasePercent();
			}
		}
		// 更新列表
		mTargetList.Clear();
		foreach (CharacterMonster item in tempList)
		{
			mTargetList.Add(item);
			// 确保加入列表中的所有怪物的燃烧buff都是伤害递增的,可能有在范围内才刚被附加燃烧buff的
			item.getFirstState<BuffBurn>()?.decreaseToIncreasePercent();
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		// 移除所有怪物的效果
		foreach (CharacterMonster item in mTargetList)
		{
			item.getFirstState<BuffBurn>()?.increaseToDecreasePercent();
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTargetList.Clear();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onMonsterDestroy(EventMonsterDestroy param)
	{
		if (param.mMonster == null)
		{
			return;
		}
		mTargetList.Remove(param.mMonster);
	}
	protected void onMonsterDie(EventMonsterDie param)
	{
		if (param.mMonster == null)
		{
			return;
		}
		mTargetList.Remove(param.mMonster);
	}
}