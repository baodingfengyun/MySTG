using static FrameBase;
using static GB;

// 最顶层的管理对象
public class Game : GameFramework
{
    public static void startGame()
    {
        Game framework = new();
        framework.init();
        GameEntryBase.getInstance().setFrameworkAOT(framework);
    }
    public override void init()
	{
        mOnInitFrameSystem += gameInitFrameSystem;
        mOnRegisteStuff += gameRegiste;

        base.init();
        mGameSceneManager.enterScene<StartScene>();
    }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void gameInitFrameSystem() 
	{
        registeFrameSystem<LocalizeResourcesManager>((com) => { mLocalizeResourcesManager = com; });
    }
    protected void gameRegiste()
    {
        LayoutRegister.registeAllLayout();
    }
}