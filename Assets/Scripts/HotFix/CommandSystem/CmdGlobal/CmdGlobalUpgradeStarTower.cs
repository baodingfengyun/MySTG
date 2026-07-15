using static FrameBaseHotFix;
using static GDR;
using static GBR;

// 塔升星
public class CmdGlobalUpgradeStarTower
{
	public static bool execute(CharacterTower tower)
	{
		EDTower newData = tower.getNextStarData();
		if (newData == null)
		{
			return false;
		}
		tower.updateData(newData);
		mEventSystem.pushEvent<EventGridTowerChange>();
		// 更新UI
		mBattleScene.showTowerRange(tower);
		mUITowerInfo.setTower(mTowerDefenceSystem.getSelectedTowerScene());
		mEffectManager.playEffectAsync(mExcelEffect.query(TOWER_STAR_UP_EFFECT_ID).mPath, tower, 2.6f, true, 0);
		using var a = new ClassScope<EventTowerSkillChanged>(out var eventParam);
		eventParam.mTower = tower;
		mEventSystem.pushEvent(eventParam, tower.getGUID());
		return true;
	}
}