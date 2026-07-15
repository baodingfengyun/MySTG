using static FrameUtility;

// 全部引导结束
public class GuideAllFinish : GuideStep
{
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mStarted)
		{
			startInternal();
			delayCall(2.0f, ()=>{ finish(); });
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
}