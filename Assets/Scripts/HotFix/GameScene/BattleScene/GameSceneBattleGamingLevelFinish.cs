using static GBR;
using static FrameBaseHotFix;

// 关卡结束显示奖励
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