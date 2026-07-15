using Obfuz;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIDialogYesNo.prefab
// 包含确认取消按钮的对话框
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIDialogYesNo : LayoutScript
{
	protected myUGUIObject mMask;
	protected myUGUIText mTip;
	protected LegendButton mYes;
	protected LegendButton mNo;
	protected myUGUIObject mClose;
    // auto generate member end
    protected OnDialogYesNoCallback mCallback;
	protected OnDialogOKCallback mConfirmCallback;
	protected bool mResult;
	public UIDialogYesNo()
	{
		// auto generate constructor start
		mYes = new(this);
		mNo = new(this);
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out myUGUIObject panel, "Panel", false);
		newObject(out mTip, panel, "Tip");
		mYes.assignWindow(panel, "Yes");
		mNo.assignWindow(panel, "No");
		newObject(out mClose, panel, "Close");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider();
		mYes.registeCollider(onYesClick);
		mNo.registeCollider(onNoClick);
		// auto generate init end
		mClose.registeCollider(onNoClick, SOUND_HOTFIX.CLOSE_BUTTON);
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
	public void setInfo(string info, string param0, string param1)
	{
		mTip.setText(info, param0, param1, this);
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