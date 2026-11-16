using System.Collections.Generic;

// 怪物路点：一个出生口对应的一条路线数据（全链路 ①寻路 的缓存结果）
// initMapDataInternal 为每个出生点建一条 MonsterRoad；BattleModeBase.generateRoadPathAndRefresh
// 在布阵时把 A* 算出的地面/飞行路径分别写进 mMonsterWalkRoadPoint / mMonsterFlyRoadPoint。
// 建怪时 CmdGlobalCreateMonster.preStartMove 按起点匹配到对应 road，直接拷贝这份预算路线
// （避免每只怪都现场 A*）。放塔导致路线变化时，generateRoadPathAndRefresh 会整体重算覆盖。
public class MonsterRoad
{
	public List<int> mMonsterWalkRoadPoint = new();      // 怪物的地面移动路线
	public List<int> mMonsterFlyRoadPoint = new();       // 怪物的飞行移动路线
	public Point mStartPoint;                            // 怪物移动起始的格子
	public void clear()
	{
		mMonsterWalkRoadPoint.Clear();
		mMonsterFlyRoadPoint.Clear();
		mStartPoint = new(0, 0);
	}
}