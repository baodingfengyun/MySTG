using static GBR;
using static FrameBaseHotFix;
using static FrameBaseUtility;

// 战斗逻辑场景 - 可以布置塔的流程（全链路 ①布阵）
// 每次进入本流程代表"新的一波开始布阵"，做三件关键的准备工作：
//   1. generateRoadPathAndRefresh() + showAllPath()：用 A* 算出并显示地面/飞行路线
//   2. generateWaveMonster()：预生成本波怪物队列（只出数据不出实体）+ 刷新"下一波怪物"预览 UI
//   3. 状态设为 SETUP_TOWER：塔的攻击组件保持关闭，玩家可自由拖拽放塔/换塔/卖塔
// 玩家点"开战"后切到 GameSceneBattleGamingFight，才真正进入战斗。
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
		logBase("[流程]GameSceneBattleGamingTowerSetup onInit 进入布阵流程时，系统先生成下一波数据，并刷新预览界面");
	}
}