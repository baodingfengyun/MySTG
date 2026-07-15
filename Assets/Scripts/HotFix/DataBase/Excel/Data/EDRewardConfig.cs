// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// RewardConfig表格
public class EDRewardConfig : ExcelDataT<EDRewardConfig>
{
	public int mRewardConfigID;						// 奖励id,配置奖励时使用这个
	public int mRewardGroup;						// 奖励组
	public int mProbability;						// 概率（万分比）
	public int mCount;								// 奖励次数
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.read(out mRewardConfigID);
		result = result && reader.read(out mRewardGroup);
		result = result && reader.read(out mProbability);
		result = result && reader.read(out mCount);
		return result;
	}
}
// auto generate end