using Obfuz;
using static GameUtilityHotFix;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UILevelReward.prefab
// 关卡胜利界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UILevelReward : LayoutScript
{
	protected myUGUIObject mMask;
	protected myUGUIObject mBack;
	protected myUGUIObject mNext;
	// auto generate member end
	public UILevelReward()
	{
		// auto generate constructor start
		// auto generate constructor end
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out myUGUIObject centerRoot, "CenterRoot", false);
		newObject(out mBack, centerRoot, "Back");
		newObject(out mNext, centerRoot, "Next");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider();
		mBack.registeCollider(onBackClick);
		mNext.registeCollider(onNextClick);
		// auto generate init end
	}
	public override void onGameState()
	{
		base.onGameState();
	}
    //--------------------------------------------------------------------------------------------------------------------------------------------
    protected void onBackClick()
    {
        CmdGlobalEnterLevelContinue.execute();
    }
    protected void onNextClick()
    {
        exitToLobbyOrMapEditor();
    }
}
