using Obfuz;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIDialogTip.prefab
// 提示信息对话框
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIDialogTip : LayoutScript
{
	protected myUGUIObject mMask;
	protected myUGUIText mTip;
    // auto generate member end
    public UIDialogTip()
	{
		// auto generate constructor start
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out myUGUIObject panel, "Panel", false);
		newObject(out mTip, panel, "Tip");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider();
		// auto generate init end
	}
	public void setInfo(string info)
	{
		mTip.setText(info, this);
	}
}