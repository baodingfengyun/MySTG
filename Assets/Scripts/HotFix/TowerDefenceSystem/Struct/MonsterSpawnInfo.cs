using System.Collections.Generic;

/// <summary>
/// 怪物出生信息（代表一次刷新时刻，而不一定只代表一只怪）。
/// 既能单个出口依次出怪，也能多个出口在同一次计时触发同时出怪。
/// </summary>
public class MonsterSpawnInfo : ClassObject
{
	// 怪物id列表
	public List<int> mMonsters = new();
	// 出生点索引列表
	public List<int> mSpawnPointIndex = new();
	// 增加出怪
	public void addMonster(int monsterID, int spawnPoint)
	{
		mMonsters.Add(monsterID);
		mSpawnPointIndex.Add(spawnPoint);
	}
	// 重置
	public override void resetProperty()
	{
		base.resetProperty();
		mMonsters.Clear();
		mSpawnPointIndex.Clear();
	}
}