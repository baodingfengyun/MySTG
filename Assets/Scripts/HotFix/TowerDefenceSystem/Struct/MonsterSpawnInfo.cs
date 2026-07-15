using System.Collections.Generic;

public class MonsterSpawnInfo : ClassObject
{
	public List<int> mMonsters = new();
	public List<int> mSpawnPointIndex = new();
	public void addMonster(int monsterID, int spawnPoint)
	{
		mMonsters.Add(monsterID);
		mSpawnPointIndex.Add(spawnPoint);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMonsters.Clear();
		mSpawnPointIndex.Clear();
	}
}