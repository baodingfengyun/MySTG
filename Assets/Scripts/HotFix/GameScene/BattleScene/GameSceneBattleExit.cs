using static GBR;
using static FrameBaseHotFix;
using static GameUtilityHotFix;

// 退出流程,用于清理资源
public class GameSceneBattleExit : SceneProcedure
{
	protected override void onExit(SceneProcedure nextProcedure)
	{
		base.onExit(nextProcedure);
		// 退出时需要确认取消变速
		CmdGlobalTimeScale.execute(false);
		mBulletManager.destroyAllBullet();
		mTowerDefenceSystem.clear();
		mSceneSystem.unloadScene(mBattleScene.getName());
		// 一般在场景的Exit流程中,卸载该场景的所有布局,确保没有资源遗留
		mLayoutManager.unloadAllPartLayout();
		// 因为退出战斗时可能会没有关闭对话框,关闭界面时确认关闭一次
		dialogYesNo();
		dialogOK();
		mGameFrameworkHotFix.resetFrameRate();
	}
}