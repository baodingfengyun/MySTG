using static FrameBaseHotFix;
using static GDR;

// 退出流程,用于清理资源
public class GameSceneLobbyExit : SceneProcedure
{
	protected override void onExit(SceneProcedure nextProcedure)
	{
		// 一般在场景的Exit流程中,卸载该场景的所有布局,确保没有资源遗留
		mSceneSystem.unloadScene(SCENE_MAIN);
		mLayoutManager.unloadAllPartLayout();
		mGameFrameworkHotFix.resetFrameRate();
	}
}