using UnityEngine;
using UnityEngine.UI;

// 确认对话UI
public class UIDialogOKResources : GameLayout
{
	// 位置
	protected Transform mPanel;
	// 关闭按钮（右上角默认）
	protected Button mClose;
	// 提示信息
	protected Text mTip;
	// 确认按钮
	protected Button mOk;
	// 点击确认按钮的回调
	protected OnDialogOKCallback mOKCallback;
	public override void assignWindow()
	{
		base.assignWindow();
		getUIComponent(out mPanel, "Panel");
        getUIComponent(out mClose, mPanel, "Close");
        getUIComponent(out mTip, mPanel, "Tip");
        getUIComponent(out mOk, mPanel, "Ok");
	}
	public override void init()
	{
		base.init();
		mClose.onClick.AddListener(onOKClick);
		mOk.onClick.AddListener(onOKClick);
	}
	public override void onGameState()
	{
		base.onGameState();
		mOKCallback = null;
	}
	public override void onHide()
	{
		base.onHide();
		OnDialogOKCallback temp1 = mOKCallback;
		mOKCallback = null;
		temp1?.Invoke();
	}
	public void setInfo(string info)
	{
		mTip.text = info;
	}
	public void setOKCallback(OnDialogOKCallback callback) { mOKCallback = callback; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onOKClick()
	{
		close();
	}
}