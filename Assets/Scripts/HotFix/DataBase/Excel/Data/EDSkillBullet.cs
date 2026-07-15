// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// SkillBullet表格
public class EDSkillBullet : ExcelDataT<EDSkillBullet>
{
	public string mName;							// 子弹名
	public BULLET_TYPE mType;						// 子弹类型
	public DAMAGE_ELEMENT mElementType;				// 伤害元素类型
	public int mFlyEffect;							// 子弹飞行时的特效
	public int mHitEffect;							// 子弹击中时的特效
	public int mHitSound0;							// 击中时的音效0
	public int mHitSound1;							// 击中时的音效1
	public HIT_EFFECT_POSITION mHitEffectPosition;	// 子弹击中特效的位置计算方式
	public int mMuzzleEffect;						// 子弹枪口发射的特效
	public int mExplosionEffect;					// 子弹爆炸的特效,特效播放完成会自动销毁,不跟随子弹周期
	public List<int> mWillHitBuffToTarget = new();	// 子弹即将击中时给被命中目标所附加的buff列表
	public List<int> mHitBuffToTarget = new();		// 子弹击中时给被命中目标所附加的buff列表
	public List<int> mHitBuffToSelf = new();		// 子弹击中时给技能释放者所附加的buff列表
	public int mAttack;								// 子弹的攻击力
	public float mAttackPercent;					// 子弹的百分比攻击力,是技能释放者的自身攻击力百分比
	public float mSpeed;							// 子弹的移动速度
	public BULLET_FIRE_POINT mStartPosition;		// 子弹发出的起始位置
	public string mStartPointName;					// 如果StartPosition为SELF_POINT.则需要填写节点名字
	public string mHitPoint;						// 子弹击中目标位置节点名字
	public int mDamageModifier;						// 子弹伤害修改器ID,索引到BulletDamageModifier表格
	public int mEffectiveTarget;					// 生效的目标类型
	public bool mSingleTarget;						// 是否为单体伤害
	public bool mIsDamage;							// 是否会造成伤害
	public string mParam0;							// 参数0
	public string mParamDesc0;						// 参数描述0
	public string mParam1;							// 参数1
	public string mParamDesc1;						// 参数描述1
	public string mParam2;							// 参数2
	public string mParamDesc2;						// 参数描述2
	public string mParam3;							// 参数3
	public string mParamDesc3;						// 参数描述3
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.readEnumByte(out mType);
		result = result && reader.readEnumByte(out mElementType);
		result = result && reader.read(out mFlyEffect);
		result = result && reader.read(out mHitEffect);
		result = result && reader.read(out mHitSound0);
		result = result && reader.read(out mHitSound1);
		result = result && reader.readEnumByte(out mHitEffectPosition);
		result = result && reader.read(out mMuzzleEffect);
		result = result && reader.read(out mExplosionEffect);
		result = result && reader.readList(mWillHitBuffToTarget);
		result = result && reader.readList(mHitBuffToTarget);
		result = result && reader.readList(mHitBuffToSelf);
		result = result && reader.read(out mAttack);
		result = result && reader.read(out mAttackPercent);
		result = result && reader.read(out mSpeed);
		result = result && reader.readEnumByte(out mStartPosition);
		result = result && reader.readString(out mStartPointName);
		result = result && reader.readString(out mHitPoint);
		result = result && reader.read(out mDamageModifier);
		result = result && reader.read(out mEffectiveTarget);
		result = result && reader.read(out mSingleTarget);
		result = result && reader.read(out mIsDamage);
		result = result && reader.readString(out mParam0);
		result = result && reader.readString(out mParamDesc0);
		result = result && reader.readString(out mParam1);
		result = result && reader.readString(out mParamDesc1);
		result = result && reader.readString(out mParam2);
		result = result && reader.readString(out mParamDesc2);
		result = result && reader.readString(out mParam3);
		result = result && reader.readString(out mParamDesc3);
		return result;
	}
}
// auto generate end