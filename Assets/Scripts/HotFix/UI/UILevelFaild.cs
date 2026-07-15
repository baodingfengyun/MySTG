using Obfuz;
using static GameUtilityHotFix;
using static GBR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UILevelFaild.prefab
// 战斗失败界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UILevelFaild : LayoutScript
{
    protected myUGUIObject mMask;
    protected myUGUIObject mBack;
    protected myUGUIText mBackText;
    protected myUGUIObject mReplay;
    protected myUGUIText mPlayAgainText;
    protected myUGUIText mCostText;
    protected myUGUIText mCostBlockText;
    // auto generate member end
    public UILevelFaild()
	{
		// auto generate constructor start
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out myUGUIObject centerRoot, "CenterRoot", false);
		newObject(out mBack, centerRoot, "Back");
		newObject(out mBackText, mBack, "BackText");
		newObject(out mReplay, centerRoot, "Replay");
		newObject(out mPlayAgainText, mReplay, "PlayAgainText");
		newObject(out mCostText, mReplay, "CostText");
		newObject(out mCostBlockText, mReplay, "CostBlockText");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider();
		mBack.registeCollider(onBackClick);
		mReplay.registeCollider(onReplayClick);
		// auto generate init end
	}
	public override void onGameState()
	{
		base.onGameState();
        mCostText.setActive(false);
        mCostBlockText.setActive(false);
		mPlayAgainText.setText("再次游玩", this);
		mBackText.setText("确认", this);
		refresh();
	}
	public void refresh()
	{
		int cost = mTowerDefenceSystem.getLevelUsePower();
		bool canPlayAgain = true;
		mCostText.setActive(canPlayAgain);
		mCostBlockText.setActive(!canPlayAgain);
		mCostText.setText(cost);
		mCostBlockText.setText(cost);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBackClick()
	{
        exitToLobbyOrMapEditor();
    }
	protected void onReplayClick()
	{
        CmdGlobalEnterLevelContinue.execute();
    }
}