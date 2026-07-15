using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

// 选中场景中的塔
public class CmdGlobalSelectTowerScene
{
	public static void execute(CharacterTower tower, bool doMoveCamera = true)
	{
		// 选中场景的塔和选中手牌的塔是互斥的操作
		if (tower != null)
		{
			mTowerDefenceSystem.cmdSelectItemOwned(null);
		}
		mTowerDefenceSystem.getSelectedTowerScene()?.showSelect(false);
		mTowerDefenceSystem.setSelectedTowerScene(tower);
		tower?.showSelect(true);
		LT.HIDE<UITowerOperation>();
		LT.HIDE<UITowerInfo>();
		mBattleScene.showTowerSelect(tower);
		mBattleScene.showTowerRange(tower);
		mUICameraDrag?.setEnable(tower == null);
		GameCamera camera = getMainCamera();
		if (tower != null)
		{
			if (doMoveCamera)
			{
                camera.MOVE_EX(KEY_CURVE.EXPO_OUT, camera.getPosition(), mBattleScene.focusCamera(tower.getPosition()), 0.2f, 
				(com, isBreak) =>
				{
					if (isBreak)
					{
						return;
					}
					showOpertion();
				});
			}
			else
			{
				showOpertion();
			}
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void showOpertion()
	{
		CharacterTower tower = mTowerDefenceSystem.getSelectedTowerScene();
		LT.LOAD<UITowerOperation>().setTowerPosition(tower.getPosition());
		LT.LOAD<UITowerInfo>().setTower(tower);
		using var a = new ClassScope<EventTowerSelect>(out var eventParam);
		eventParam.mTower = tower;
		mEventSystem.pushEvent(eventParam);
	}
}