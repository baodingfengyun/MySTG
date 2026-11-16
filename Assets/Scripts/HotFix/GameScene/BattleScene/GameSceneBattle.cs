
// 战斗逻辑场景,包含战斗的各个阶段
//
// 【全链路的流程骨架】SceneSystem 用"流程树(Procedure)"组织一次战斗，本场景注册了 7 个流程：
//   Loading  → 异步加载地图场景 + 预加载怪物 prefab + initLevel() → 进入布阵
//   Gaming(父) → 挂载公共 UI(血条/伤害数字)、战斗 BGM，下面挂三个子流程：
//       TowerSetup(Rogue) → 预生成本波怪、预览路线、三选一抽卡、玩家布阵   ← 链路 ①
//       Fight             → 状态切 FIGHTING、通知塔开火、刷怪、战斗          ← 链路 ②③④⑤
//       LevelFinish       → 胜负结算、奖励界面                              ← 链路 ⑦
//   Exit → 销毁全部子弹、clear() 战斗数据、卸载场景、卸载 UI 布局
// 关键理解：布阵↔战斗是一个"每波循环"——一波打完 cmdWaveFinish 把 waveIndex+1 后回到 TowerSetup，
// 直到 isEnded()（经验达标）或血量归零才走 LevelFinish 结束整局。
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