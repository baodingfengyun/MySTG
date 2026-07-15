using Obfuz;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UILoading.prefab
// 加载界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UILoading : LayoutScript
{
	protected myUGUIObject mMask;
	protected myUGUIImageSimple mProgress;
    // auto generate member end
    public UILoading()
	{
		// auto generate constructor start
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out myUGUIObject bottomRoot, "BottomRoot", false);
		newObject(out mProgress, bottomRoot, "Progress");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider();
		// auto generate init end
	}
	public override void onGameState()
	{
		base.onGameState();
		setProgress(0);
	}
	public void setProgress(float progress)
	{
		mProgress.setFillPercent(progress);
	}
}