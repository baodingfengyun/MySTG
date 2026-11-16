using System.Collections.Generic;

/// <summary>
/// 怪物出生信息（代表一次刷新时刻，而不一定只代表一只怪）。
/// 既能单个出口依次出怪，也能多个出口在同一次计时触发同时出怪。
/// 
/// 【全链路 ③刷怪 的排队单元】MonsterGenerator.generateMonsters 把本波怪物整理成
/// Queue&lt;MonsterSpawnInfo&gt;，update 每隔 mMonsterTimeInterval 出队一个：
/// mMonsters[i] 是怪物 id，mSpawnPointIndex[i] 是它的出怪口下标，两个列表一一对应。
/// 一个 info 内放多只怪 = 同一时刻多口齐出（SYNC 规则），这就是"一批"的含义。
/// 继承 ClassObject 走对象池，出队消费后 UN_CLASS 回收。
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