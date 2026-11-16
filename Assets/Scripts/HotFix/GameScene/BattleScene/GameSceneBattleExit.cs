using static GBR;
using static FrameBaseHotFix;
using static GameUtilityHotFix;

// 战斗逻辑场景 - 退出流程（全链路 ⑦的收尾）：一局战斗结束后彻底清理，防止跨关残留
// 清理顺序有讲究：先关变速(TimeScale) → 销毁所有在飞的子弹 → TowerDefenceSystem.clear() 释放
// 所有怪物/塔/传送门/全局角色 → 卸载地图场景 → 卸载本场景所有 UI 布局 → 兜底关闭可能未关的对话框。
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