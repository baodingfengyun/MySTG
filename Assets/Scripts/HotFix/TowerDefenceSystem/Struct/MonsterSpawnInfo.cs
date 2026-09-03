using System.Collections.Generic;

/// <summary>
/// 怪物出生信息
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