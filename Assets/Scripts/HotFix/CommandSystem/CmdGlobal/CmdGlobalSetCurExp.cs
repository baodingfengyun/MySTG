using static GBR;

// 设置当前经验值
public class CmdGlobalSetCurExp
{
	public static void execute(int exp)
	{
		mTowerDefenceSystem.setCurExp(exp);
	}
}