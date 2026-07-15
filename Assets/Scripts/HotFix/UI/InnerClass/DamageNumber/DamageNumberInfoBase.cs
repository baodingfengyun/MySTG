using System.Collections.Generic;
using UnityEngine;
using static FrameBaseHotFix;

public abstract class DamageNumberInfoBase
{
	public HP_DELTA mDeltaType;
	public string mNumberPath;
	protected Dictionary<float, Vector3> mTranslatePath;
	protected Dictionary<float, Vector3> mScalePath;
	protected Dictionary<float, float> mAlphaPath;
	public abstract DamageNumber newItem();
	public abstract void unuseAll();
	public abstract void unuseItem(DamageNumber item);
	public Dictionary<float, Vector3> getTranslatePath()
	{
		mTranslatePath ??= mPathKeyframeManager.getTranslatePath(mNumberPath);
		return mTranslatePath;
	}
	public Dictionary<float, Vector3> getScalePath()
	{
		mScalePath ??= mPathKeyframeManager.getScalePath(mNumberPath);
		return mScalePath;
	}
	public Dictionary<float, float> getAlphaPath()
	{
		mAlphaPath ??= mPathKeyframeManager.getAlphaPath(mNumberPath);
		return mAlphaPath;
	}
}