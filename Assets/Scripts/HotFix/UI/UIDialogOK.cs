using Obfuz;
using UnityEngine;
using static FrameBaseHotFix;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIDialogOK.prefab
// 单按钮"确定"对话框：setInfo 设文案(支持两个格式化参数)、setOKCallback 注入确认回调；
//   onOKClick 执行回调并关闭。常驻(PERSIST)界面，供 tip/确认场景复用。
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIDialogOK : LayoutScript
{
	protected myUGUIObject mMask;
	protected myUGUIText mTip;
	protected LegendButton mOK;
	protected myUGUIObject mClose;
    // auto generate member end
	protected OnDialogOKCallback mOKCallback;
	public UIDialogOK()
	{
		// auto generate constructor start
		mOK = new(this);
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out myUGUIObject panel, "Panel", false);
		newObject(out mTip, panel, "Tip");
		mOK.assignWindow(panel, "OK");
		newObject(out mClose, panel, "Close");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider();
		mOK.registeCollider(onOKClick);
		// auto generate init end
		mClose.registeCollider(onOKClick, SOUND_HOTFIX.CLOSE_BUTTON);
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
	public void setInfo(string info, string param0, string param1)
	{
		mTip.setText(info, param0, param1, this);
	}
	public void setOKCallback(OnDialogOKCallback callback) { mOKCallback = callback; }
	public void setActiveOnlyOK(out Vector3 pos)
	{
		pos = mOK.getRoot().getWorldPosition();
		mGlobalTouchSystem.setActiveOnlyObject(mOK.getRoot());
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onOKClick()
	{
		close();
	}
}