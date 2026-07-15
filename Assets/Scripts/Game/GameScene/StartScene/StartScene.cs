
// 初始启动逻辑场景
public class StartScene : GameScene
{
	public override void assignStartExitProcedure()
	{
		mStartProcedure = typeof(StartSceneLoading);
		mExitProcedure = typeof(StartSceneExit);
	}
	public override void createSceneProcedure()
	{
		addProcedure<StartSceneLoading>();
		addProcedure<StartSceneVersion>();
		addProcedure<StartSceneFileList>();
		addProcedure<StartSceneDownload>();
		addProcedure<StartSceneExit>();
	}
}