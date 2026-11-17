using System;
using static StringUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffRemoveTypeBuffParam : CharacterBuffParamT<BuffRemoveTypeBuffParam>
{
	public int mBuffType;         // buff类型ID
	public override void registeAllParam()
	{
		registeParam((param) => { mBuffType = param.SToI(); });
	}
	protected override void copyInternal(BuffRemoveTypeBuffParam other)
	{
		mBuffType = other.mBuffType;
	}
	public override void check()
	{
		checkDataRefByBuffDetail(mExcelBuff, mBuffType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffType = 0;
	}
}

// 移除指定类型的 buff（Buff 操作族代表，如清除中毒/燃烧）。
// 通过 mStateManager.getStateType(buffType) 把配表里的 Buff 类型 ID 反射成状态类，
// 再让目标状态机 removeFirstState(..., true) 移除第一个匹配状态。
// 对比 BuffClearDebuff(清所有 debuff)：本类是"按类型精确清除"，是词条系统里"驱散"能力的基础。
public class BuffRemoveTypeBuff : CharacterBuffT<BuffRemoveTypeBuffParam>
{
	public override void enter()
	{
		base.enter();
		Type classType = mStateManager.getStateType(mCustomParam.mBuffType);
		mCharacterGame.getStateMachine().removeFirstState(classType, true);
	}
}