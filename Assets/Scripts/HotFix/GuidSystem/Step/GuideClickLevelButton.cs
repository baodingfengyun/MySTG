using UnityEngine;
using static FrameUtility;
using static StringUtility;
using static GBR;

public class GuideClickLevelButtonParam : ParamBase
{
	public int mLevelID;
	public override void registeAllParam()
	{
		registeParam((param) => { mLevelID = param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mLevelID = 0;
	}
}

// 选择关卡界面点击关卡按钮
public class GuideClickLevelButton : GuideStepT<GuideClickLevelButtonParam>
{
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mStarted)
		{
			// 从主界面开始的此步骤,需要自动进入到关卡选择流程
			if (getCurScene().atProcedure<GameSceneLobbyMain>() && mMainScene.getState() == LOAD_STATE.LOADED)
			{
				changeProcedure<GameSceneLobbySelectLevel>();
			}
			mUISelectLevel?.selectLevel(mExcelLevel.query(mCustomParam.mLevelID));
			if (getCurScene().atProcedure<GameSceneLobbySelectLevel>() &&
				mUISelectLevel.setActiveOnlyLevel(mCustomParam.mLevelID, out Vector3 pos))
			{
				mUIGuide.setHandPosition(mData.mClickStyle, pos);
				startInternal();
			}
		}
		// 关卡信息界面打开时就认为完成
		if (mStarted && mUILevelInfo.safe() != null)
		{
			finish();
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
}