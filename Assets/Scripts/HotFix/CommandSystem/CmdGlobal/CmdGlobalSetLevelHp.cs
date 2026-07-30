using static GBR;

// 设置关卡的血量
public class CmdGlobalSetLevelHp
{
	public static void execute(int hp)
	{
		hp = hp.clampMin();
		mTowerDefenceSystem.setHp(hp);
		mUIGaming.safe()?.setHeartCount(hp);
	}
}