using static GBR;

// 显示选择关卡界面
public class GameSceneLobbySelectLevel : SceneProcedure
{
	public static int mBackToEndless = 0;
	protected override void onInit(SceneProcedure lastProcedure)
	{
		LT.LOAD<UISelectLevel>();
		AT.MUSIC(SOUND_HOTFIX.MAIN_BGM);
		// 进入到选择关卡时,将选择的英雄列表同步一下
		if(mBackToEndless != 0)
		{
			EDLevel targetEndlessLevel = mExcelLevel.query(mBackToEndless, false);
			if(targetEndlessLevel != null && targetEndlessLevel.mEndless)
			{
				mUISelectLevel.selectLevel(targetEndlessLevel);
			}
		}
		mBackToEndless = 0;
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		LT.HIDE<UISelectLevel>();
		LT.HIDE<UILevelInfo>();
		AT.MUSIC();
	}
}