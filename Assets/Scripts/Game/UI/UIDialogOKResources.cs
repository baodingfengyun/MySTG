using UnityEngine;
using UnityEngine.UI;

public class UIDialogOKResources : GameLayout
{
	protected Transform mPanel;
	protected Button mClose;
	protected Text mTip;
	protected Button mOk;
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