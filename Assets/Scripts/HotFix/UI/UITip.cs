using Obfuz;
using System.Collections.Generic;
using UnityEngine;
using static FrameUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UITip.prefab
// 屏幕提示飘字(tip)：showTip 显示一条带参数的短提示，用 TipItem 对象池铺开；
//   "内容+参数都相同"的提示会被去重(不重复显示)，显示完 notifyTipShowDone 回收。
//   常驻(PERSIST)，全局 tip(...) 调用最终落到这里。
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UITip : LayoutScript
{
    protected myUGUIObject mTipRootEnd;
    protected WindowStructPool<TipItem> mTipItemPool;
    // auto generate member end
    protected Vector3 mTipStartPosition;
	protected Vector3 mTipEndPosition;
	protected LinkedList<KeyValuePair<string, List<string>>> mTipQueue = new();      // 显示的信息队列,用于控制信息显示间隔
	protected float mTipCD;
	public UITip()
	{
		// auto generate constructor start
		mTipItemPool = new(this);
		// auto generate constructor end
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mTipRootEnd, "TipRootEnd");
		mTipItemPool.assignTemplate(mRoot, "TipItem");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		// auto generate init end
		mTipStartPosition = mTipItemPool.getTemplate().getPosition();
		mTipEndPosition = mTipRootEnd.getPosition();
	}
	public override void onGameState()
	{
		base.onGameState();
        mTipItemPool.unuseAll();
		mTipQueue.Clear();
		mTipCD = -1;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (tickTimerOnce(ref mTipCD, elapsedTime) && mTipQueue.Count > 0)
		{
			mTipCD = 0.5f;
			var first = mTipQueue.First.Value;
            mTipItemPool.newItem().setTip(first.Key, first.Value);
			UN_LIST(first.Value);
			mTipQueue.RemoveFirst();
		}
	}
	public void showTip(string tip, List<string> param)
	{
		// 不放入重复的提示信息
		do
		{
			if (mTipQueue.Count == 0)
			{
				break;
			}
			var last = mTipQueue.Last.Value;
			if (last.Key != tip)
			{
				break;
			}
			// 字符串内容一致,参数一致,为重复的提示信息
			if (param == null && last.Value == null)
			{
				return;
			}
			if (param == null || last.Value == null || param.Count != last.Value.Count)
			{
				break;
			}
		} while (false);
		List<string> temp = null;
		if (!param.isEmpty())
		{
			LIST_PERSIST(out temp);
			temp.AddRange(param);
		}
		mTipQueue.AddLast(new KeyValuePair<string, List<string>>(tip, temp));
		// 如果还未在计时中,则开启计时,避免无法显示
		mTipCD = mTipCD.clampMin();
	}
	public void notifyTipShowDone(TipItem tip)
	{
        mTipItemPool.unuseItem(tip);
	}
	public Vector3 getTipStartPos() { return mTipStartPosition; }
	public Vector3 getTipEndPos() { return mTipEndPosition; }
}