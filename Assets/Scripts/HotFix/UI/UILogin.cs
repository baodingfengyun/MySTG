using Obfuz;
using static FrameUtility;
using static GameUtilityHotFix;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UILogin.prefab
// 登录界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UILogin : LayoutScript
{
    protected myUGUIObject mLogin;
    // auto generate member end
	public UILogin()
	{
		// auto generate constructor start
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out myUGUIObject center, "Center", false);
		newObject(out mLogin, center, "Login");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mLogin.registeCollider(onLoginClick);
		// auto generate init end
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onLoginClick()
	{
		dialogTip("登录中...");
		delayCall(0.5f, () =>
		{
            enterScene<GameSceneLobby>();
        });
	}
}