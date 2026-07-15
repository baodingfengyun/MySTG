using Obfuz;
using static FrameUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UILobby.prefab
// 大厅主界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UILobby : LayoutScript
{
	protected myUGUIObject mStartGame;
	protected myUGUIObject mStartGameRedPoint;
    // auto generate member end
    public UILobby()
	{
		// auto generate constructor start
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out myUGUIObject rightBottomRoot, "RightBottomRoot", false);
		newObject(out mStartGame, rightBottomRoot, "StartGame");
		newObject(out mStartGameRedPoint, mStartGame, "StartGameRedPoint");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mStartGame.registeCollider(onStartGameClick);
		// auto generate init end
		mStartGame.setClickSound(SOUND_HOTFIX.MAIN_FIGHT_BUTTON);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onStartGameClick()
	{
		changeProcedure<GameSceneLobbySelectLevel>();
	}
}