using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

// 战斗逻辑场景 - 加载资源流程（全链路起点）
// onInit 里延迟一帧再加载，避免上一关残留 UI（尤其新手引导）干扰；然后依次：
//   异步加载地图场景 → BattleScene.loadResourceAsync → initData →
//   预加载所有怪物 prefab 到对象池（避免战斗中首次出怪卡顿）→
//   TowerDefenceSystem.initLevel()（建传送门+全局角色）→ 切到布阵流程。
public class GameSceneBattleLoading : SceneProcedure
{
	protected override void onInit(SceneProcedure lastProcedure)
	{
		mGameFrameworkHotFix.setFrameRate(60);
		LT.LOAD<UILoading>();
		LT.LOAD_TOP<UIDraging>();
		LT.LOAD<UICameraDrag>(0, LAYOUT_ORDER.FIXED);

		// 加载资源场景,延迟一帧,等待之前的所有操作完成,界面已经隐藏等等,否则可能会残留之前的界面,比如新手引导界面
		delayCall(()=>
		{
			mSceneSystem.loadSceneAsync(mTowerDefenceSystem.getMapSceneName(), true, true, () =>
			{
				mBattleScene.loadResourceAsync(() =>
				{
					// 可能还没有加载完场景就已经退出了关卡
					if (!mTowerDefenceSystem.isLevelValid())
					{
						return;
					}
					mBattleScene.initData();

					// 预加载怪物模型资源
					foreach (EDMonster item in mExcelMonster.queryAll())
					{
						mPrefabPoolManager.initObjectToPool(item.mPrefab, 1, true);
					}

					// 场景加载完以后才能进入下一个流程
					mTowerDefenceSystem.initLevel();
					changeProcedure(mTowerDefenceSystem.getSetupTowerProcedure());
				});
			});
		});
	}
	protected override void onExit(SceneProcedure nextProcedure)
	{
		LT.HIDE<UILoading>();
	}
}
