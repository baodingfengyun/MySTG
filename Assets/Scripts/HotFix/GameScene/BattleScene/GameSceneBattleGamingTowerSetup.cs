using static GBR;
using static FrameBaseHotFix;

// 可以布置塔的流程
public class GameSceneBattleGamingTowerSetup : SceneProcedure
{
	protected override void onInit(SceneProcedure lastProcedure)
	{
        mGameFrameworkHotFix.setFrameRate(60);
		// 设置为布置塔的状态
		mTowerDefenceSystem.setBattleState(BATTLE_STATE.SETUP_TOWER);
		mTowerDefenceSystem.generateRoadPathAndRefresh();
		// 一开始就要显示路线,每次拖拽修改放置塔的位置时也要刷新显示路线
		mBattleScene.showAllPath();
		// 生成下一波怪物的列表
		mTowerDefenceSystem.generateWaveMonster();
		// 每波刷新monster列表的内容
		LT.LOAD<UIMonsterQueue>().refresh();
	}
}