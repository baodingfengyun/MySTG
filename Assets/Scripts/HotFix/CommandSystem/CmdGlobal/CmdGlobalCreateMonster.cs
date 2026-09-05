using System.Collections.Generic;
using static FrameBaseHotFix;
using static UnityUtility;
using static GBR;
using static FrameBaseUtility;

// 创建一个怪物对象
public class CmdGlobalCreateMonster
{
	public static CharacterMonster execute(EDMonster monsterTableData, int startIndex)
	{
		// 创建怪物,怪物从起点移动到终点
		var monster = mCharacterManager.createCharacter<CharacterMonster>("monster");
		monster.initData(monsterTableData);
		monster.notifyFightStart();
		logBase("[CMD创建怪物] " + monster.ToString());

		COMMonsterMovement comMove = monster.getComMovement();
		List<MonsterRoad> roadList = mTowerDefenceSystem.getMonsterRoadList();
		if (!monster.getMonsterData().mFlyable)
		{
			int isStart = -1;
			int roadListCount = roadList.Count;
			for(int i = 0; i < roadListCount; ++i)
			{
				MonsterRoad road = roadList[i];
				if (startIndex == road.mStartPoint.toIndex(mTowerDefenceSystem.getLevelWidth()))
				{
					isStart = i;
					break;
				}
			}
			// 如果怪物当前的起点就是整个地图的起点,就使用已经计算好的路线就行
			if (isStart >= 0)
			{
				comMove.setRoadPointList(mTowerDefenceSystem.getMonsterWalkRoadPoint(isStart));
			}
			// 指定了其他起点,就需要实时计算一下路线,起点无效时不计算
			else if (startIndex >= 0)
			{
				using var a = new ListScope<int>(out var list);
				if (!mTowerDefenceSystem.generateWalkRoadPathCustom(startIndex, list, -1))
				{
					logWarning("生成怪物时路线计算失败,起点:" + startIndex);
				}
				comMove.setRoadPointList(list);
			}
		}
		else
		{
			int isStart = -1;
			int roadListCount = roadList.Count;
			for (int i = 0; i < roadListCount; ++i)
			{
				MonsterRoad road = roadList[i];
				if (startIndex == road.mStartPoint.toIndex(mTowerDefenceSystem.getLevelWidth()))
				{
					isStart = i;
					break;
				}
			}
			// 如果怪物当前的起点就是整个地图的起点,就使用已经计算好的路线就行
			if (isStart >= 0)
			{
				comMove.setRoadPointList(mTowerDefenceSystem.getMonsterFlyRoadPoint(isStart));
			}
			// 指定了其他起点,就需要实时计算一下路线,起点无效时不计算
			else if (startIndex >= 0)
			{
				using var a = new ListScope<int>(out var list);
				if (!mTowerDefenceSystem.generateFlyRoadPathCustom(startIndex, list, -1))
				{
					logWarning("生成怪物时路线计算失败,起点:" + startIndex);
				}
				comMove.setRoadPointList(list);
			}
		}
		comMove.startMove();
		mTowerDefenceSystem.addMonster(monster);
		if (monster.getMonsterData().mTableData.mStrength == MONSTER_STRENGTH.BOSS)
		{
			// 触发生成BOSS怪物事件
			using var a = new ClassScope<EventSpawnMonster>(out var param);
			param.mMonster = monster;
			mEventSystem.pushEvent(param);
		}
		return monster;
	}
}