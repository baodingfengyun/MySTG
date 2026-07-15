using static GBR;

// 没有行为,只是显示提示
public class GuideNoAction : GuideStep
{
	public override void start()
	{
		base.start();
		startInternal();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		// 如果引导界面的NPC说话被关闭了,当前引导就结束
		if (mStarted && !mUIGuide.isNPCActive())
		{
			finish();
		}
	}
}