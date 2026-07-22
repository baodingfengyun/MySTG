using static FrameBaseHotFix;
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
		mEffectManager.playEffectAsync(EDEffect.TOWER_PLACE.mPath, tower, 2.6f, true);
		using var a = new ClassScope<EventTowerSkillChanged>(out var eventParam);
		eventParam.mTower = tower;
		mEventSystem.pushEvent(eventParam, tower.getGUID());
		return true;
	}
}