using Obfuz;
using static GameUtilityHotFix;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UILevelReward.prefab
// 关卡胜利/奖励界面：与 UILevelComplete 同构的两个出口——
//   onBackClick → CmdGlobalEnterLevelContinue 继续；onNextClick → 退出到大厅/地图编辑器。
// 具体内容(奖励项)由 prefab 静态展示，脚本只管按钮。
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
