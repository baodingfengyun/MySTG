using UnityEngine;
using UnityEngine.UI;

// 选择是/否对话框UI
public class UIDialogYesNoResources : GameLayout
{
	// 位置
	protected Transform mPanel;
	// 关闭按钮
	protected Button mClose;
	// 提示信息
	protected Text mTip;
	// 是按钮
	protected Button mYes;
	// 否按钮
	protected Button mNo;
	protected OnDialogYesNoCallback mCallback;
	protected OnDialogOKCallback mConfirmCallback;
	// 选择结果
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
		// 关闭 = 否
		mClose.onClick.AddListener(onNoClick);
		// 是
		mYes.onClick.AddListener(onYesClick);
		// 否
		mNo.onClick.AddListener(onNoClick);
	}
	public override void onGameState()
	{
		base.onGameState();
		mConfirmCallback = null;
		mCallback = null;
		mResult = false;
	}
	// 隐藏UI
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
	// 设置提示信息
	public void setInfo(string info)
	{
		mTip.text = info;
	}
	public void setConfirmCallback(OnDialogOKCallback callback) { mConfirmCallback = callback; }
	public void setCallback(OnDialogYesNoCallback callback) { mCallback = callback; }
	//------------------------------------------------------------------------------------------------------------------------------
	// 点击是的行为
	protected void onYesClick()
	{
		mResult = true;
		close();
	}
	// 点击否的行为
	protected void onNoClick()
	{
		mResult = false;
		close();
	}
}