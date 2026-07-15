using UnityEngine;
using static FrameUtility;
using static GBR;

// 点击关卡的挑战按钮
public class GuideClickEnterLevel : GuideStep
{
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mStarted && mUILevelInfo.safe() != null)
		{
			mUILevelInfo.setActiveOnlyConfirm(out Vector3 pos);
			mUIGuide.setHandPosition(mData.mClickStyle, pos);
			startInternal();
		}
		// 进入战斗流程时就认为完成
		if (mStarted)
		{
			if(getCurScene().atProcedure<GameSceneBattleLoading>())
			{
				mUIGuide.deactiveAllTip();
			}
			else if(getCurScene().atProcedure<GameSceneBattleGamingTowerSetup>())
			{
				finish();
			}
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
}