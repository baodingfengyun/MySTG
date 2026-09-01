using UnityEngine;
using UnityEngine.UI;

// 提示对话框UI
public class UIDialogTipResources : GameLayout
{
	// 位置
	protected Transform mPanel;
	// 提示内容
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