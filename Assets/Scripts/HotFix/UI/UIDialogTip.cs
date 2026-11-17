using Obfuz;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIDialogTip.prefab
// 纯提示对话框：setInfo 设一段文本，无确认按钮(点遮罩/自动关闭)。常驻(PERSIST)。
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