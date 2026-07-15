using static GBR;

// 顶层的战斗流程,包含布置塔,战斗,结算子流程
public class GameSceneBattleGaming : SceneProcedure
{
	protected override void onInit(SceneProcedure lastProcedure)
	{
		base.onInit(lastProcedure);
		LT.LOAD<UIGaming>();
		LT.LOAD<UIHPBar>();
		LT.LOAD<UIDamageNumber>();
		AT.MUSIC(mTowerDefenceSystem.getLevelMusic());
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		LT.HIDE<UIGaming>();
		LT.HIDE<UIHPBar>();
		LT.HIDE<UIDamageNumber>();
		CmdGlobalSelectTowerScene.execute(null);
		AT.MUSIC();
	}
}