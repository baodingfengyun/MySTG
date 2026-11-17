using System.Collections.Generic;
using UnityEngine;
using static FrameBaseHotFix;

// 伤害飘字信息基类:记录血量变化类型 mDeltaType、飘字预制体路径 mNumberPath,
// 并缓存该路径对应的位移/缩放/透明度关键帧轨迹(首次访问时从 mPathKeyframeManager 读取)。
// 子类只需实现对象池的新建与回收三个抽象方法。
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
	// 懒加载并缓存位移轨迹,避免每次飘字都重新解析路径关键帧。
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