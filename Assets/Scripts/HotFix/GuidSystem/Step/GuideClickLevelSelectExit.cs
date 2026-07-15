using UnityEngine;
using static FrameUtility;
using static GBR;

// 点击关卡选择的返回,回到大厅
public class GuideClickLevelSelectExit : GuideStep
{
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mStarted && getCurScene().atProcedure<GameSceneLobbySelectLevel>())
		{
			mUISelectLevel.setActiveOnlyNormalBack(out Vector3 pos);
			mUIGuide.setHandPosition(mData.mClickStyle, pos);
			startInternal();
		}
		// 回到大厅时就认为完成
		if (mStarted && getCurScene().atProcedure<GameSceneLobbyMain>())
		{
			finish();
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
}