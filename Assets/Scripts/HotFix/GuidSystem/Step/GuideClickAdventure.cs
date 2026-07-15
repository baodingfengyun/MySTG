using UnityEngine;
using static FrameUtility;
using static GBR;

// 点击大厅的冒险按钮
public class GuideClickAdventure : GuideStep
{
	protected bool mSendGiveUpLevelWave;
	public override void resetProperty()
	{
		base.resetProperty();
		mSendGiveUpLevelWave = false;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mStarted && getCurScene().atProcedure<GameSceneLobbyMain>() && mUILobby.safe() != null)
		{
			mUIGuide.setHandPosition(mData.mClickStyle, Vector3.zero);
			startInternal();
		}
		// 进入到选择关卡时就认为完成
		if (mStarted && getCurScene().atProcedure<GameSceneLobbySelectLevel>())
		{
			finish();
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
}