using static GBR;
using static FrameBaseHotFix;
using static FrameBaseUtility;

// 战斗逻辑场景 - 打怪战斗流程
public class GameSceneBattleGamingFight : SceneProcedure
{
    protected override void onInit(SceneProcedure lastProcedure)
    {
		mGameFrameworkHotFix.resetFrameRate();
		// 进入战斗时确认关闭塔的操作相关界面
		CmdGlobalSelectTowerScene.execute(null);
		// 设置刷怪计时器，战斗状态，UI相关
		mTowerDefenceSystem.getMonsterGenerator().setCurMonsterTimer(1);
		mTowerDefenceSystem.setBattleState(BATTLE_STATE.FIGHTING);
		mUIGaming.notifyStartFight(true);
		mUIBattleItemSelectRogue?.notifyStartFight();
		mTowerDefenceSystem.getBattleModeRogue()?.setRogueSelected(false);
		// 发送了数据后再通知波次改变，发送改变之前的数据
		mTowerDefenceSystem.notifyWaveChanged();

		// 肉鸽模式开始后清空随机天赋
		BATTLE_MODE battleMode = mTowerDefenceSystem.getBattleMode();
		if (battleMode == BATTLE_MODE.ROGUE_LIKE)
		{
			mTowerDefenceSystem.clearAllowSelectPropListRogue();
			mUIBattleItemSelectRogue?.setPropList(mTowerDefenceSystem.getAllowSelectPropListRogue());
			mUIBattleItemSelectRogue?.close();
		}
		mEventSystem.pushEvent<EventWaveChange>();
		logBase("[流程]GameSceneBattleGamingFight onInit 进入战斗");
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		mUIGaming.safe()?.notifyStartFight(false);
        logBase("[流程]GameSceneBattleGamingFight onExit 退出战斗");
    }
}