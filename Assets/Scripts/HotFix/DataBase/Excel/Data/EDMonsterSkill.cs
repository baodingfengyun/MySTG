// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// MonsterSkill表格
public class EDMonsterSkill : ExcelDataT<EDMonsterSkill>
{
	public string mDescription;						// 描述
	public int mDescriptionID;						// 描述的多语言ID
	public string mName;							// 技能名
	public string mIcon;							// 技能图标
	public bool mIsPassive;							// 是否为被动技能
	public float mCD;								// 冷却时间
	public int mAnimation;							// 技能动作,填写的是状态机跳转的参数值
	public float mAnimationDuration;				// 技能动作持续时长
	public List<int> mDefaultFireBuff = new();		// 释放被动技能时默认给自己附加的buff
	public int mPassiveTriggerSFX;					// 被动技能触发时播放的音效
	public int mPassiveTriggerEffect;				// 被动技能触发效果时播放的特效
	public float mPassiveTriggerDelay;				// 被动技能生效延迟时间
	public int mFireSFX;							// 释放主动技能时的音效
	public int mFireEffect;							// 释放主动技能时播放的特效
	public float mFireEffectTime;					// 主动技能特效释放的延迟时间
	public float mHPPercent0;						// 血量百分比0
	public List<int> mFireBuff0 = new();			// 满足血量百分比0时释放技能时给自己添加的buff列表
	public float mHPPercent1;						// 血量百分比1
	public List<int> mFireBuff1 = new();			// 满足血量百分比1时释放技能时给自己添加的buff列表
	public int mMP;									// 消耗的mp
	public List<int> mBullet = new();				// 技能的子弹
	public List<float> mFireTime = new();			// 子弹释放时间
	public MONSTER_SEARCH_TARGET mSearchTarget;		// 寻找目标的方式
	public string mParam0;							// 参数0
	public string mParamDesc0;						// 参数描述0
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mDescription);
		result = result && reader.read(out mDescriptionID);
		result = result && reader.readString(out mName);
		result = result && reader.readString(out mIcon);
		result = result && reader.read(out mIsPassive);
		result = result && reader.read(out mCD);
		result = result && reader.read(out mAnimation);
		result = result && reader.read(out mAnimationDuration);
		result = result && reader.readList(mDefaultFireBuff);
		result = result && reader.read(out mPassiveTriggerSFX);
		result = result && reader.read(out mPassiveTriggerEffect);
		result = result && reader.read(out mPassiveTriggerDelay);
		result = result && reader.read(out mFireSFX);
		result = result && reader.read(out mFireEffect);
		result = result && reader.read(out mFireEffectTime);
		result = result && reader.read(out mHPPercent0);
		result = result && reader.readList(mFireBuff0);
		result = result && reader.read(out mHPPercent1);
		result = result && reader.readList(mFireBuff1);
		result = result && reader.read(out mMP);
		result = result && reader.readList(mBullet);
		result = result && reader.readList(mFireTime);
		result = result && reader.readEnumByte(out mSearchTarget);
		result = result && reader.readString(out mParam0);
		result = result && reader.readString(out mParamDesc0);
		return result;
	}
}
// auto generate end