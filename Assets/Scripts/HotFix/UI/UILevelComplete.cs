using Obfuz;
using static GameUtilityHotFix;
using static StringUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UILevelComplete.prefab
// 关卡完成界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UILevelComplete : LayoutScript
{
    protected myUGUIObject mMask;
    protected myUGUIText mCurWaveText;
    protected myUGUIObject mBack;
    protected myUGUIText mBackText;
    protected myUGUIObject mNext;
    protected myUGUIText mNextText;
    // auto generate member end
    public UILevelComplete()
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
		newObject(out mCurWaveText, centerRoot, "CurWaveText");
		newObject(out mBack, centerRoot, "Back");
		newObject(out mBackText, mBack, "BackText");
		newObject(out mNext, centerRoot, "Next");
		newObject(out mNextText, mNext, "NextText");
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
		mCurWaveText.setText(EMPTY);
	}
	public void setWave(int wave) { mCurWaveText.setText("到达波次: {0}", wave.IToS(), this); }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBackClick()
	{
         CmdGlobalEnterLevelContinue.execute();
    }
	protected void onNextClick()
	{
        exitToLobbyOrMapEditor();
    }
}