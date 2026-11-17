using System.Collections.Generic;
using static GBR;

// 触发器参数对象：描述"触发后给谁加哪些 Buff、触发条件如何"
// 【核心字段】mBuffDetailIDList(要生成的 Buff 细分表 ID 列表)、mProbability(万分比概率)、
//   mCD(触发冷却秒)、mMaxOverlap(同目标身上最多叠几个)、mBuffTarget(给自己还是给事件目标)。
// 【registeAllParam】声明表里字符串列 → 字段的解析规则，由配置表自动生成/校验。
public class CharacterTriggerParam : CharacterBuffParam
{
	public List<int> mBuffDetailIDList = new();	// 可触发的buff列表
	public int mMaxOverlap;						// 最大叠加次数
	public bool mBuffTarget;					// buff附加的目标,false表示给自己附加,true表示给被命中的人附加
	public float mCD;							// 每次触发的CD
	public int mProbability;					// 触发几率,万分比,0表示不会触发,大于等于10000表示百分百触发
	public override void registeAllParam()
	{
		registeParam((string stringParam) => { stringParam.SToIs(mBuffDetailIDList); });
		registeParam((param) => { mMaxOverlap = param.SToI(); });
		registeParam((param) => { mBuffTarget = param.SToI() != 0; });
		registeParam((param) => { mCD = param.SToF(); });
		registeParam((param) => { mProbability = param.SToI(); });
	}
	public override void copy(StateParam otherParam)
	{
		var other = otherParam as CharacterTriggerParam;
		mBuffDetailIDList.AddRange(other.mBuffDetailIDList);
		mMaxOverlap = other.mMaxOverlap;
		mBuffTarget = other.mBuffTarget;
		mCD = other.mCD;
		mProbability = other.mProbability;
	}
	public override void check()
	{
		checkDataRefByBuffDetail(mExcelBuffDetail, mBuffDetailIDList);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffDetailIDList.Clear();
		mMaxOverlap = 0;
		mBuffTarget = false;
		mCD = 0.0f;
		mProbability = 0;
	}
};