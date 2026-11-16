using static GBR;

// 卡牌模式的一波结束（全链路 ⑥波次结算，被 BattleModeRogue.cmdWaveFinish 调用）
// 三件事：①按本波 WaveConfig.mRewardCurrency 发金币（抽卡资源）→ ②waveIndex 无条件+1
// → ③setWin 维护连胜/连败计数(mStreakCount，±3 触发"连胜关")。
// 注意：波次结算只推进"波"，不判断整关结束——是否通关由 isEnded()（经验达标）另行判断。
// 发完金币和递增波次后，基类 cmdWaveFinish 会切回 SETUP_TOWER，进入下一波的布阵。
public class CmdGlobalWaveFinishRogue
{
	public static void execute()
	{
		bool waveWin = mTowerDefenceSystem.getHp() > 0;
		// 根据波次加添加用于购买卡的货币
		EDWaveConfig waveConfig = mTowerDefenceSystem.getWaveData();
		CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() + waveConfig.mRewardCurrency);
		// 无论胜利失败，波次固定加1
		int waveIndex = mTowerDefenceSystem.getWaveIndex() + 1;
		mTowerDefenceSystem.setWaveIndex(waveIndex);
		mTowerDefenceSystem.setWin(waveWin);
		mUIGaming.setWaveValue(waveIndex);
	}
}