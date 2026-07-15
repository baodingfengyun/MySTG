using UnityEngine;
using UnityEngine.UI;

public class UIDialogTipResources : GameLayout
{
	protected Transform mPanel;
	protected Text mTip;
	public override void assignWindow()
	{
		base.assignWindow();
        getUIComponent(out mPanel, "Panel");
        getUIComponent(out mTip, mPanel, "Tip");
	}
	public void setInfo(string info)
	{
		mTip.text = info;
	}
}