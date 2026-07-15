using static GBR;

// 卡牌模式的一波结束
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