using static FrameBase;
using static GB;
using static FrameBaseUtility;

// 最顶层的管理对象
public class Game : GameFramework
{
    public static void startGame()
    {
        Game framework = new();
        framework.init();
        GameEntryBase.getInstance().setFrameworkAOT(framework);
        logBase("[启动游戏]startGame ok");
    }
    public override void init()
	{
        mOnInitFrameSystem += gameInitFrameSystem;
        mOnRegisteStuff += gameRegiste;

        base.init();
        logBase("[进入场景]StartScene");
        mGameSceneManager.enterScene<StartScene>();
    }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void gameInitFrameSystem() 
	{
        registeFrameSystem<LocalizeResourcesManager>((com) => { mLocalizeResourcesManager = com; });
        logBase("[初始化FrameSystem模块]LocalizeResourcesManager");
    }
    protected void gameRegiste()
    {
        LayoutRegister.registeAllLayout();
        logBase("[注册]LayoutRegister");
    }
}