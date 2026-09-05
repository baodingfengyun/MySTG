using System.Collections.Generic;

// 怪物路点
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