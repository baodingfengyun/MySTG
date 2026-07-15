using System;
using static FrameUtility;
using static GBR;

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