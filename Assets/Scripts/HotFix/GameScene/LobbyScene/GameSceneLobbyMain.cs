using static GBR;
using static GDR;
using static FrameBaseHotFix;
using static FrameUtility;

// 大厅界面的显示
public class GameSceneLobbyMain : SceneProcedure
{
	protected override void onInit(SceneProcedure lastProcedure)
	{
		if(mMainScene == null)
		{
			LT.LOAD_TOP<UILoading>(1250);
		}
		// 由于有允许直接从战斗界面跳转到关卡选择界面的需求，所以进入到LobbyScene时可以不用加载场景，等待进入Main流程时才开始加载场景
		mSceneSystem.loadSceneAsync(SCENE_MAIN, true, true, () =>
		{
			LT.LOAD<UILobby>();
			LT.LOAD_TOP<UIGuide>(1240);
			delayCall(1, () => LT.HIDE<UILoading>());
		}, (progress)=>
		{
			mUILoading.setProgress(progress);
		});
		AT.MUSIC(SOUND_HOTFIX.MAIN_BGM);
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		AT.MUSIC();
		LT.HIDE<UILobby>();
		mSceneSystem.hideScene(SCENE_MAIN);
	}
}