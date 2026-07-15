// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// WaveConfig表格
public class EDWaveConfig : ExcelDataT<EDWaveConfig>
{
	public int mLevel;								// 关卡id
	public int mWave;								// 波次
	public SPAWN_POINT_RULE mSpawnRule;				// 出怪规则
	public List<int> mSpawnPoint = new();			// 出怪口
	public List<int> mSpawnPointWeight = new();		// 出怪口随机权重
	public int mSpawnPointTimes;					// 出怪口出怪次数
	public List<Vector2Int> mSpawnQueue = new();	// 固定怪物序列
	public int mPopulation;							// 怪物总人口
	public List<int> mMonsterIDs = new();			// 怪物id
	public List<int> mMonsterWeights = new();		// 怪物权重
	public int mInterval;							// 出怪间隔
	public List<int> mRandomBoss = new();			// 随机boss
	public int mBossProbability;					// boss出现概率
	public int mRewardCurrency;						// 奖励货币
	public int mScoreRatio;							// 怪物掉落分数系数
	public int mHpRatio;							// 难度系数
	public int mHpRatioGrow;						// 难度增长
	public List<Vector2Int> mHpRatioMonster = new();// 难度系数单独怪物配置
	public float mHpModifier;						// 难度修正
	public int mCardPool;							// 卡池id
	public int mCardRandomCount;					// 词条展示数量,仅无尽抽卡生效
	public int mHpCapacityRatio;					// 战力系数
	public int mRewardID;							// 关卡奖励
	public int mRewardExp;							// 掉落宠物经验
	public bool mSelectOnlyOne;						// 词条是否单选
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.read(out mLevel);
		result = result && reader.read(out mWave);
		result = result && reader.readEnumByte(out mSpawnRule);
		result = result && reader.readList(mSpawnPoint);
		result = result && reader.readList(mSpawnPointWeight);
		result = result && reader.read(out mSpawnPointTimes);
		result = result && reader.readList(mSpawnQueue);
		result = result && reader.read(out mPopulation);
		result = result && reader.readList(mMonsterIDs);
		result = result && reader.readList(mMonsterWeights);
		result = result && reader.read(out mInterval);
		result = result && reader.readList(mRandomBoss);
		result = result && reader.read(out mBossProbability);
		result = result && reader.read(out mRewardCurrency);
		result = result && reader.read(out mScoreRatio);
		result = result && reader.read(out mHpRatio);
		result = result && reader.read(out mHpRatioGrow);
		result = result && reader.readList(mHpRatioMonster);
		result = result && reader.read(out mHpModifier);
		result = result && reader.read(out mCardPool);
		result = result && reader.read(out mCardRandomCount);
		result = result && reader.read(out mHpCapacityRatio);
		result = result && reader.read(out mRewardID);
		result = result && reader.read(out mRewardExp);
		result = result && reader.read(out mSelectOnlyOne);
		return result;
	}
}
// auto generate end