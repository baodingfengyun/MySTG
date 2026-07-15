
// 登录逻辑场景,包含登录界面
public class GameSceneLogin : GameScene
{
	public override void assignStartExitProcedure()
	{
		mStartProcedure = typeof(GameSceneLoginLoading);
		mExitProcedure = typeof(GameSceneLoginExit);
	}
	public override void createSceneProcedure()
	{
		addProcedure<GameSceneLoginLoading>();
		addProcedure<GameSceneLoginGaming>();
		addProcedure<GameSceneLoginExit>();
	}
}