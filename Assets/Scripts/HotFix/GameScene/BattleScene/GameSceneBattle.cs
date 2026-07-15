
// 战斗逻辑场景,包含战斗的各个阶段
public class GameSceneBattle : GameScene
{
	public override void assignStartExitProcedure()
	{
		mStartProcedure = typeof(GameSceneBattleLoading);
		mExitProcedure = typeof(GameSceneBattleExit);
	}
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