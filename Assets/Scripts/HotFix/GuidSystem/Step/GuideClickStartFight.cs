using UnityEngine;
using static FrameUtility;
using static GBR;

// 点击开始按钮
public class GuideClickStartFight : GuideStep
{
	public override void start()
	{
		base.start();
		mUIGaming.setActiveOnlyStartFight(out Vector3 pos);
		mUIGuide.setHandPosition(mData.mClickStyle, pos);
		startInternal();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mStarted && getCurScene().atProcedure<GameSceneBattleGamingFight>())
		{
			finish();
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
}