using static GBR;
using static MathUtility;

// 设置关卡的血量
public class CmdGlobalSetLevelHp
{
	public static void execute(int hp)
	{
		clampMin(ref hp);
		mTowerDefenceSystem.setHp(hp);
		mUIGaming.safe()?.setHeartCount(hp);
	}
}