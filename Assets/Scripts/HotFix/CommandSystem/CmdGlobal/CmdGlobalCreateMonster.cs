using SimpleJSON;
using System.Collections.Generic;
using System.Threading;
using static FrameBaseHotFix;
using static FrameBaseUtility;
using static GBR;
using static UnityUtility;
using Newtonsoft.Json;

// 创建一个怪物对象
public class CmdGlobalCreateMonster
{
	public static CharacterMonster execute(EDMonster monsterTableData, int startIndex)
	{
		// 创建怪物,怪物从起点移动到终点
		var monster = mCharacterManager.createCharacter<CharacterMonster>("monster");
		monster.initData(monsterTableData);
		monster.notifyFightStart();
		logBase("[CMD创建怪物] " + monster.ToString() + ", startIndex: " + startIndex);

		COMMonsterMovement comMove = monster.getComMovement();
		preStartMove(comMove, monster, startIndex);
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
    // 移动之前，先设置移动路径
	private static void preStartMove(COMMonsterMovement comMove, CharacterMonster monster, int startIndex)
	{
        List<MonsterRoad> roadList = mTowerDefenceSystem.getMonsterRoadList();
        if (!monster.getMonsterData().mFlyable)
        {
            // 选用哪个预设的路径
            int isStart = -1;
            int roadListCount = roadList.Count;
            for (int i = 0; i < roadListCount; ++i)
            {
                MonsterRoad road = roadList[i];
                // toIndex = y * width + x 如果startIndex等于路点的起始点，就算命中预设的路径。
                if (startIndex == road.mStartPoint.toIndex(mTowerDefenceSystem.getLevelWidth()))
                {
                    isStart = i;
                    break;
                }
            }
            // 如果怪物当前的起点就是整个地图的起点,就使用已经计算好的路线就行
            if (isStart >= 0)
            {
                List<int> roadPointList = mTowerDefenceSystem.getMonsterWalkRoadPoint(isStart);
                comMove.setRoadPointList(roadPointList);
                logBase("[计算地面路径]预设: " + isStart + ", roadPointList: " + JsonConvert.SerializeObject(roadPointList));
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
                logBase("[计算地面路径]计算, roadPointList: " + JsonConvert.SerializeObject(list));
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
                List<int> roadPointList = mTowerDefenceSystem.getMonsterFlyRoadPoint(isStart);
                comMove.setRoadPointList(roadPointList);
                logBase("[计算飞行路径]预设: " + isStart + ", roadPointList: " + JsonConvert.SerializeObject(roadPointList));
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
                logBase("[计算飞行路径]计算, roadPointList: " + JsonConvert.SerializeObject(list));
            }
        }
    }
}