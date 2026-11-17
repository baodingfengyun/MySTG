using System;
using static UnityUtility;
using static FrameUtility;
using static GBR;

// BuffParamScope 的泛型版:借出参数时校验实际类型是否为 T,不匹配则报错(仍强制转型)。
// 同样配合 using 使用,Dispose 归还对象池,让调用方拿到强类型参数。
public struct BuffParamScopeT<T> : IDisposable where T : CharacterBuffParam
{
	private T mParam;
	public BuffParamScopeT(out T param, int buffDetailID)
	{
		CharacterBuffParam param0 = mStateManagerHotFix.createParam(buffDetailID);
		if (param0.GetType() != typeof(T))
		{
			logError("buff参数类型错误,创建的是" + param0.GetType() + ",转换的是:" + typeof(T) + ", BuffDetailID:" + buffDetailID);
		}
		param = param0 as T;
		mParam = param;
	}
	public void Dispose()
	{
		UN_CLASS(ref mParam);
	}
}