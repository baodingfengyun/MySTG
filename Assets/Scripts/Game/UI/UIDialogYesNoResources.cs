using UnityEngine;
using UnityEngine.UI;

public class UIDialogYesNoResources : GameLayout
{
	protected Transform mPanel;
	protected Button mClose;
	protected Text mTip;
	protected Button mYes;
	protected Button mNo;
	protected OnDialogYesNoCallback mCallback;
	protected OnDialogOKCallback mConfirmCallback;
	protected bool mResult;
	public override void assignWindow()
	{
		base.assignWindow();
		getUIComponent(out mPanel, "Panel");
		getUIComponent(out mClose, mPanel, "Close");
		getUIComponent(out mTip, mPanel, "Tip");
		getUIComponent(out mYes, mPanel, "Yes");
        getUIComponent(out mNo, mPanel, "No");
	}
	public override void init()
	{
		base.init();
		mClose.onClick.AddListener(onNoClick);
		mYes.onClick.AddListener(onYesClick);
		mNo.onClick.AddListener(onNoClick);
	}
	public override void onGameState()
	{
		base.onGameState();
		mConfirmCallback = null;
		mCallback = null;
		mResult = false;
	}
	public override void onHide()
	{
		base.onHide();
		OnDialogYesNoCallback temp0 = mCallback;
		OnDialogOKCallback temp1 = mConfirmCallback;
		mCallback = null;
		mConfirmCallback = null;
		temp0?.Invoke(mResult);
		if (mResult)
		{
			temp1?.Invoke();
		}
	}
	public void setInfo(string info)
	{
		mTip.text = info;
	}
	public void setConfirmCallback(OnDialogOKCallback callback) { mConfirmCallback = callback; }
	public void setCallback(OnDialogYesNoCallback callback) { mCallback = callback; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onYesClick()
	{
		mResult = true;
		close();
	}
	protected void onNoClick()
	{
		mResult = false;
		close();
	}
}