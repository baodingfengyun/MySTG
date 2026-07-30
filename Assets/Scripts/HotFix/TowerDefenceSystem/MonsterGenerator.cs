using UnityEngine;
using System.Collections.Generic;
using static UnityUtility;
using static MathUtility;
using static FrameUtility;
using static GBR;

// 用于处理刷新怪物的逻辑
public class MonsterGenerator
{
	protected Queue<MonsterSpawnInfo> mMonsterGenerateList = new();	// 当前已经随机出的怪物列表,记录了这一波的每一刷怪刻需要刷出的怪
	protected float mCurMonsterTimer;								// 当前怪物刷新计时器
	protected float mMonsterTimeInterval;							// 怪物出现的间隔时间,秒
	protected bool mMonsterGenerateMaxHP;							// 生成怪物时是否设置无限血量,用于提供调试的功能
	protected bool mMonsterGenerateMinHP;							// 生成怪物时是否设置1血量,用于提供调试的功能
	public void clear()
	{
		UN_CLASS_LIST(mMonsterGenerateList);
		mCurMonsterTimer = 0.0f;
		mMonsterTimeInterval = 0.0f;
		mMonsterGenerateMaxHP = false;
		mMonsterGenerateMinHP = false;
	}
	// 打完一波时需要重置一些刷怪的变量
	public void clearWave()
	{
		UN_CLASS_LIST(mMonsterGenerateList);
		mCurMonsterTimer = 0.0f;
	}
	public void init()
	{
		mCurMonsterTimer = 0.0f;
		mMonsterTimeInterval = mTowerDefenceSystem.getWaveData().mInterval * 0.001f;
	}
	public void update(float elapsedTime)
	{
		if (mMonsterGenerateList.Count > 0 && tickTimerLoop(ref mCurMonsterTimer, elapsedTime, mMonsterTimeInterval))
		{
			MonsterSpawnInfo spawnInfo = mMonsterGenerateList.Dequeue();
			List<int> monsters = spawnInfo.mMonsters;
			int monstersCount = monsters.Count;
			for(int i = 0; i < monstersCount; ++i)
			{
				int monsterID = monsters[i];
				int startIndex = mTowerDefenceSystem.getStartPointIndex(spawnInfo.mSpawnPointIndex[i]);
				CharacterMonster monster = CmdGlobalCreateMonster.execute(mExcelMonster.query(monsterID), startIndex);
				CharacterMonsterData monsterData = monster.getMonsterData();
				if (mMonsterGenerateMaxHP)
				{
					monsterData.mMaxHP = 9999999;
				}
				else if(mMonsterGenerateMinHP)
				{
					monsterData.mMaxHP = 1;
				}
				else
				{
					monsterData.mMaxHP = (int)(monster.getMaxHP() * mTowerDefenceSystem.getWaveIntensity(monsterID));
				}
				CmdMonsterSetHP.execute(monster, monster.getMaxHP());
			}
			UN_CLASS(ref spawnInfo);
		}
	}
	public void generateMonsters()
	{
		UN_CLASS_LIST(mMonsterGenerateList);
		EDWaveConfig waveConfig = mTowerDefenceSystem.getWaveData();
		if (waveConfig == null)
		{
			logError("找不到波次配置,LevelID:" + mTowerDefenceSystem.getLevelID() + ", 波次:" + (mTowerDefenceSystem.getWaveIndex() + 1));
			return;
		}
		mMonsterTimeInterval = waveConfig.mInterval * 0.001f;
		using var a = new ListScope<int>(out var monsters);
		foreach (Vector2Int item in waveConfig.mSpawnQueue.safe())
		{
			for (int j = 0; j < item.y; ++j)
			{
				monsters.Add(item.x);
			}
		}
		if (waveConfig.mPopulation != 0 && waveConfig.mMonsterIDs.Count > 0)
		{
			generateRamdomMonsters(waveConfig.mMonsterWeights, waveConfig.mMonsterIDs, waveConfig.mPopulation, monsters);
		}
		SPAWN_POINT_RULE rule = waveConfig.mSpawnRule;
		if (rule == SPAWN_POINT_RULE.RANDOM)
		{
			setSpawnRuleRandom(monsters, waveConfig.mSpawnPoint, waveConfig.mSpawnPointWeight);
		}
		else if (rule == SPAWN_POINT_RULE.SYNC)
		{
			setSpawnRuleSync(monsters, waveConfig.mSpawnPoint);
		}
		else if (rule == SPAWN_POINT_RULE.TIMES)
		{
			setSpawnRuleTimes(monsters, waveConfig.mSpawnPoint, waveConfig.mSpawnPointTimes);
		}
		else if (rule == SPAWN_POINT_RULE.NONE)
		{
			setSpawnRuleTimes(monsters, waveConfig.mSpawnPoint, 1);
		}
	}
	// 只生成一定数量的指定怪物列表,用于调试
	public void regenerateMosnterDataList(int monsterID, int count)
	{
		UN_CLASS_LIST(mMonsterGenerateList);
		for (int i = 0; i < count; ++i)
		{
			CLASS(out MonsterSpawnInfo info);
			info.addMonster(monsterID, 0);
			mMonsterGenerateList.Enqueue(info);
		}
	}
	public bool isSpawnFinish() { return mMonsterGenerateList.Count == 0; }
	public Queue<MonsterSpawnInfo> getMonsterGenerateList() { return mMonsterGenerateList;}
	public void setMonsterGenerateMaxHP(bool generateMax) { mMonsterGenerateMaxHP = generateMax; }
	public void setMonsterGenerateMinHP(bool generateMax) { mMonsterGenerateMinHP = generateMax; }
	public void setMonsterGenerateInterval(float interval) { mMonsterTimeInterval = interval; }
	public void setCurMonsterTimer(float curMonsterTimer) { mCurMonsterTimer = curMonsterTimer; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void generateRamdomMonsters(List<int> monsterWeights, List<int> monsterIDs, int population, List<int> monsters)
	{
		using var a = new ListScope2<int>(out var monsterWeightList, out var monsterIDList);
		monsterWeightList.AddRange(monsterWeights);
		int minMonsterPopulation = int.MaxValue;
		foreach (int id in monsterIDList.addRange(monsterIDs))
		{
			minMonsterPopulation = minMonsterPopulation.clampMax(mExcelMonster.query(id).mPopulation);
		}
		// 为避免配置错误而进入无限循环,添加一个循环次数限制
		int loopLimit = 1000;
		while (population - minMonsterPopulation >= 0 && monsterIDList.Count > 0 && --loopLimit >= 0)
		{
			for (int i = 0; i < monsterWeightList.Count; ++i)
			{
				if (mExcelMonster.query(monsterIDList[i]).mPopulation > population)
				{
					monsterWeightList.RemoveAt(i);
					monsterIDList.RemoveAt(i);
					--i;
				}
			}
			EDMonster monsterData = mExcelMonster.query(monsterIDList[randomHit(monsterWeightList)]);
			monsters.Add(monsterData.mID);
			population -= monsterData.mPopulation;
		}
	}
	// 从权重中随机一个出口
	protected void setSpawnRuleRandom(List<int> monsters, List<int> spawnIndexList, List<int> spawnPointWeight)
	{
		foreach (int monster in monsters)
		{
			CLASS(out MonsterSpawnInfo info);
			info.addMonster(monster, spawnIndexList[randomHit(spawnPointWeight)]);
			mMonsterGenerateList.Enqueue(info);
		}
	}
	// 多个出怪口同时出怪
	protected void setSpawnRuleSync(List<int> monsters, List<int> spawnIndexList)
	{
		int monstersCount = monsters.Count;
		int spawnPointCount = spawnIndexList.Count;
		for (int i = 0; i < monstersCount; ++i)
		{
			CLASS(out MonsterSpawnInfo info);
			mMonsterGenerateList.Enqueue(info);
			for (int j = 0; j < spawnPointCount; ++j)
			{
				int index = i + j;
				if (index >= monstersCount)
				{
					return;
				}
				info.addMonster(monsters[index], spawnIndexList[j]);
			}
			i += spawnPointCount - 1;
		}
	}
	// 每个口刷times个怪再切换下一个口
	protected void setSpawnRuleTimes(List<int> monsters, List<int> spawnIndexList, int times)
	{
		int monstersCount = monsters.Count;
		int spawnPointCount = spawnIndexList.Count;
		for (int i = 0; i < monstersCount; ++i)
		{
			CLASS(out MonsterSpawnInfo info);
			info.addMonster(monsters[i], spawnIndexList[i / times % spawnPointCount]);
			mMonsterGenerateList.Enqueue(info);
		}
	}
}