using static GameUtilityHotFix;

// 显示登录界面
public class GameSceneLoginGaming : SceneProcedure
{
	protected override void onInit(SceneProcedure lastProcedure)
	{
		LT.LOAD<UILogin>();
		AT.MUSIC(SOUND_HOTFIX.LOGIN_BGM);
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		dialogTip();
		LT.HIDE<UILogin>();
		AT.MUSIC();
	}
}