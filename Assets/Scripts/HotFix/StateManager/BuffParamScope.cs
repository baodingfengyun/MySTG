using System;
using static FrameUtility;
using static GBR;

// Buff 参数作用域(struct):构造时从状态管理器借出一个 Buff 参数对象并通过 out 返回,
// 配合 using 使用,Dispose 时归还对象池。用于"临时取参数改字段"的场景,避免忘记归还。
public struct BuffParamScope : IDisposable
{
	private CharacterBuffParam mParam;
	public BuffParamScope(out CharacterBuffParam param, int buffDetailID)
	{
		param = mStateManagerHotFix.createParam(buffDetailID);
		mParam = param;
	}
	public void Dispose()
	{
		UN_CLASS(ref mParam);
	}
}