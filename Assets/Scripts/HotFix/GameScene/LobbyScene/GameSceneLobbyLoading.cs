using System;
using static FrameUtility;
using static FrameBaseHotFix;

// 加载资源阶段
public class GameSceneLobbyLoading : SceneProcedure
{
	public static Type mNextProcedureType = typeof(GameSceneLobbyMain);
	protected override void onInit(SceneProcedure lastProcedure)
	{
		mGameFrameworkHotFix.setFrameRate(30);
		changeProcedure(mNextProcedureType);
		mNextProcedureType = typeof(GameSceneLobbyMain);
	}
}