using static FrameUtility;
using static GBR;

// 没有行为,只是显示提示,一波结束时显示
public class GuideWaveEndTip : GuideStep
{
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mStarted && 
			(getCurScene().atProcedure<GameSceneBattleGamingTowerSetup>() ||
			getCurScene().atProcedure<GameSceneBattleGamingLevelFinish>()))
		{
			startInternal();
		}
		// 如果引导界面的NPC说话被关闭了,当前引导就结束
		if (mStarted && !mUIGuide.isNPCActive())
		{
			finish();
		}
	}
}