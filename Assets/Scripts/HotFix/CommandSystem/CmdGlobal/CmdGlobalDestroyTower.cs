using static GBR;
using static FrameBaseHotFix;

public class CmdGlobalDestroyTower
{
	public static void execute(CharacterTower tower)
	{
		if (mTowerDefenceSystem.getSelectedTowerScene() == tower)
		{
			CmdGlobalSelectTowerScene.execute(null);
		}
		// 销毁之前广播被销毁的事件
		using var a = new ClassScope<EventTowerDestroy>(out var eventParam);
		eventParam.mTower = tower;
		mEventSystem.pushEvent(eventParam, tower.getGUID());

		mTowerDefenceSystem.removeTower(tower);
		mCharacterManager.destroyCharacter(tower);
		// 销毁以后广播场上塔改变的事件
		mEventSystem.pushEvent<EventGridTowerChange>();
	}
}