using UnityEngine;
using static GBR;

// 设置时间缩放
public class CmdGlobalTimeScale
{
	public static void execute(bool scale)
	{
		Time.timeScale = scale ? 2 : 1;
		mUIGaming.safe()?.setTimeScaled(scale);
	}
}