using static GBR;
using static FrameBaseUtility;

// 战斗逻辑场景 - 顶层的战斗流程,包含布置塔,战斗,结算子流程
public class GameSceneBattleGaming : SceneProcedure
{
	// 加载公共UI，设置关卡对应的背景音乐
	protected override void onInit(SceneProcedure lastProcedure)
	{
		base.onInit(lastProcedure);
		LT.LOAD<UIGaming>();
		LT.LOAD<UIHPBar>();
		LT.LOAD<UIDamageNumber>();
		AT.MUSIC(mTowerDefenceSystem.getLevelMusic());
		logBase("[流程]GameSceneBattleGaming onInit 加载公共UI，设置关卡对应的背景音乐");
	}
	// 隐藏公共UI，取消选中塔，关闭音乐
	protected override void onExit(SceneProcedure nextProcedure)
	{
		LT.HIDE<UIGaming>();
		LT.HIDE<UIHPBar>();
		LT.HIDE<UIDamageNumber>();
		CmdGlobalSelectTowerScene.execute(null);
		AT.MUSIC();
        logBase("[流程]GameSceneBattleGaming onExit 隐藏公共UI，取消选中塔，关闭音乐");
    }
}