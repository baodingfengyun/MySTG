using static GBR;
using static FrameBaseHotFix;

// 战斗逻辑场景 - 关卡结束显示奖励（全链路 ⑦结算）
// 由 isEnded()（经验达标 = 通关）或血量归零（失败）触发进入。
// 先 TimeScale(false) 冻结战斗、状态切 FINISH，再按血量分流：>0 播胜利 BGM，≤0 弹 UILevelFaild 播失败 BGM。
public class GameSceneBattleGamingLevelFinish: SceneProcedure
{
    protected override void onInit(SceneProcedure lastProcedure)
    {
		CmdGlobalTimeScale.execute(false);
        mGameFrameworkHotFix.setFrameRate(60);
		mTowerDefenceSystem.setBattleState(BATTLE_STATE.FINISH);
		if (mTowerDefenceSystem.getHp() <= 0)
		{
			LT.LOAD<UILevelFaild>();
			AT.MUSIC(SOUND_HOTFIX.LEVEL_DEFEAT_BGM, false);
		}
		else
		{
			AT.MUSIC(SOUND_HOTFIX.LEVEL_VICTORY_BGM, false);
		}
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		LT.HIDE<UILevelFaild>();
		LT.HIDE<UILevelComplete>();
		AT.MUSIC();
	}
}