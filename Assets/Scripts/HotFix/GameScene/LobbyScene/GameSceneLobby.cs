
// 大厅逻辑场景,包含大厅显示和关卡选择
public class GameSceneLobby : GameScene
{
	public override void assignStartExitProcedure()
	{
		mStartProcedure = typeof(GameSceneLobbyLoading);
		mExitProcedure = typeof(GameSceneLobbyExit);
	}
	public override void createSceneProcedure()
	{
		addProcedure<GameSceneLobbyLoading>();
		addProcedure<GameSceneLobbyMain>();
		addProcedure<GameSceneLobbySelectLevel>();
		addProcedure<GameSceneLobbyExit>();
	}
}