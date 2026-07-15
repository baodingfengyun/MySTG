using System.Collections.Generic;
using static GBR;

// auto generate classname start
// generate from:Assets/GameResources/UI/UIPrefab/UITip.prefab
// 一条提示信息
public class TipItem : WindowRecyclableUGUI
// auto generate classname end
{
	// auto generate member start
	protected myUGUIText mTip;
	// auto generate member end
	protected string mOriginText;
	public TipItem(IWindowObjectOwner script) : base(script) { }
    protected override void assignWindowInternal()
    {
		// auto generate assignWindowInternal start
		newObject(out mTip, "Tip");
		// auto generate assignWindowInternal end
	}
	public void setTip(string tip, List<string> param)
	{
		if (param != null)
		{
			using var a = new ListScope<string>(out var tempList);
			mTip.setText(tip, tempList.addRange(param), this);
		}
		else
		{
			mTip.setText(tip, this);
		}
		float time = 1.5f;
		mRoot.MOVE_EX(KEY_CURVE.SINE_IN_OUT, mUITip.getTipStartPos(), mUITip.getTipEndPos(), time, onShowDone);
        mRoot.ALPHA(KEY_CURVE.SINE_IN_OUT, 1.0f, 0.0f, time);
		mTip.ALPHA(KEY_CURVE.SINE_IN_OUT, 1.0f, 0.0f, time);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onShowDone(ComponentKeyFrame com, bool breakTrembling)
	{
		if (breakTrembling)
		{
			return;
		}
		mUITip?.notifyTipShowDone(this);
	}
}