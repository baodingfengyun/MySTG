// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Monster表格
public class EDMonster : ExcelDataT<EDMonster>
{
	public string mName;							// 怪物名称
	public string mPrefab;							// GameResources下的相对路径,带后缀
	public string mIcon;							// 怪物头像
	public string mDescription;						// 怪物简介
	public MONSTER_STRENGTH mStrength;				// 怪物类型
	public int mDefence;							// 怪物防御
	public int mHP;									// 怪物血量
	public float mSpeed;							// 怪物移动速度
	public string mTypeIcon;						// 类型图标
	public List<int> mSkill = new();				// 怪物技能
	public List<int> mDefaultBuff = new();			// buff列表,索引到BuffDetail表
	public int mPopulation;							// 人口
	public int mScore;								// 怪物分数
	public int mDropBox;							// 掉落宝箱
	public List<string> mWaveExp = new();			// 怪物不同波次击败后的得分，"5|3,10|7"代表1-5波得3分，6-10波得7分
	public int mHurtHp;								// 怪物到达终点时减少玩家的血量
	public float mAntiFire;							// 火属性抗性
	public float mAntiIce;							// 冰属性抗性
	public float mAntiDark;							// 暗属性抗性
	public float mAntiLight;						// 光属性抗性
	public float mAntiPoison;						// 毒属性抗性
	public float mAntiLightning;					// 电属性抗性
	public float mEvasion;							// 闪避率
	public int mAntiPenetrating;					// 穿透抵抗值
	public int mHpStar;								// 血量星级
	public int mSpeedStar;							// 速度星级
	public int mDefenceStar;						// 防御力星级
	public int mBornTalk;							// 出场怪物语言
	public int mBornTalkProbability;				// 说话概率（万分比）
	public int mDyingTalk;							// 低血量怪物语言
	public int mDyingTalkProbability;				// 说话概率（万分比）
	public float mDieAnimationLength;				// 死亡动作时长
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.readString(out mPrefab);
		result = result && reader.readString(out mIcon);
		result = result && reader.readString(out mDescription);
		result = result && reader.readEnumByte(out mStrength);
		result = result && reader.read(out mDefence);
		result = result && reader.read(out mHP);
		result = result && reader.read(out mSpeed);
		result = result && reader.readString(out mTypeIcon);
		result = result && reader.readList(mSkill);
		result = result && reader.readList(mDefaultBuff);
		result = result && reader.read(out mPopulation);
		result = result && reader.read(out mScore);
		result = result && reader.read(out mDropBox);
		result = result && reader.readList(mWaveExp);
		result = result && reader.read(out mHurtHp);
		result = result && reader.read(out mAntiFire);
		result = result && reader.read(out mAntiIce);
		result = result && reader.read(out mAntiDark);
		result = result && reader.read(out mAntiLight);
		result = result && reader.read(out mAntiPoison);
		result = result && reader.read(out mAntiLightning);
		result = result && reader.read(out mEvasion);
		result = result && reader.read(out mAntiPenetrating);
		result = result && reader.read(out mHpStar);
		result = result && reader.read(out mSpeedStar);
		result = result && reader.read(out mDefenceStar);
		result = result && reader.read(out mBornTalk);
		result = result && reader.read(out mBornTalkProbability);
		result = result && reader.read(out mDyingTalk);
		result = result && reader.read(out mDyingTalkProbability);
		result = result && reader.read(out mDieAnimationLength);
		return result;
	}
}
// auto generate end