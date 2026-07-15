using Obfuz;
using System;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIQuitBattle.prefab
// 退出战斗的提示界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIQuitBattle : LayoutScript
{
    protected myUGUIObject mMask;
    protected LegendButton mCancel;
    protected LegendButton mConfirm;
    // auto generate member end
    protected Action mOnConfirm;
    public UIQuitBattle()
    {
        // auto generate constructor start
        mCancel = new(this);
        mConfirm = new(this);
        // auto generate constructor end
    }
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out myUGUIObject centerRoot, "CenterRoot", false);
		mCancel.assignWindow(centerRoot, "Cancel");
		mConfirm.assignWindow(centerRoot, "Confirm");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider();
		mCancel.registeCollider(onCancelClick);
		mConfirm.registeCollider(onConfirmClick);
		// auto generate init end
		mMask.registeCollider();
		mConfirm.registeCollider(onConfirmClick);
		mCancel.registeCollider(onCancelClick);
	}
	public override void onGameState()
	{
		base.onGameState();
		mOnConfirm = null;
	}
	public void setConfirm(Action callback)
	{
		mOnConfirm = callback;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onConfirmClick()
	{
		mOnConfirm?.Invoke();
	}
	protected void onCancelClick()
	{
		close();
	}
}
