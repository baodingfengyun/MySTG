
// 战斗逻辑场景,包含战斗的各个阶段
public class GameSceneBattle : GameScene
{
	// 设置战斗场景的起始和结束流程
	public override void assignStartExitProcedure()
	{
		mStartProcedure = typeof(GameSceneBattleLoading);
		mExitProcedure = typeof(GameSceneBattleExit);
	}
	// 设置战斗场景的所有流程（树形）
	public override void createSceneProcedure()
	{
		addProcedure<GameSceneBattleLoading>();
		addProcedure<GameSceneBattleGaming>();
		addProcedure<GameSceneBattleGamingTowerSetup>(typeof(GameSceneBattleGaming));
		addProcedure<GameSceneBattleGamingTowerSetupRogue>(typeof(GameSceneBattleGamingTowerSetup));
		addProcedure<GameSceneBattleGamingFight>(typeof(GameSceneBattleGaming));
		addProcedure<GameSceneBattleGamingLevelFinish>(typeof(GameSceneBattleGaming));
		addProcedure<GameSceneBattleExit>();
	}
}