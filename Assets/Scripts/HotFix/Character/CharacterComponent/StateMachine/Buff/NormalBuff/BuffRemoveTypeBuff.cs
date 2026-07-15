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

// 移除指定类型的buff
public class BuffRemoveTypeBuff : CharacterBuffT<BuffRemoveTypeBuffParam>
{
	public override void enter()
	{
		base.enter();
		Type classType = mStateManager.getStateType(mCustomParam.mBuffType);
		mCharacterGame.getStateMachine().removeFirstState(classType, true);
	}
}