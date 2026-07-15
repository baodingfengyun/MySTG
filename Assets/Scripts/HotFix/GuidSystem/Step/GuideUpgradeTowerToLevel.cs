using static StringUtility;
using static FrameBaseHotFix;
using static GBR;

public class GuideUpgradeTowerToLevelParam : ParamBase
{
	public int mLevel;
	public override void registeAllParam()
	{
		registeParam((param) => { mLevel = param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mLevel = 0;
	}
}

// 任意防御塔升级到一定等级
public class GuideUpgradeTowerToLevel : GuideStepT<GuideUpgradeTowerToLevelParam>
{
	public override void start()
	{
		base.start();
		mGlobalTouchSystem.setActiveOnlyObject(null);
		foreach (CharacterTower tower in mTowerDefenceSystem.getTowerList())
		{
			mGlobalTouchSystem.addActiveOnlyObject(tower);
		}
		LT.LOAD_HIDE<UITowerOperation>().addActiveOnlyUpgrade();
		startInternal();
		mEventSystem.listenEvent((EventTowerLevelChange eventParam) => 
		{
			bool isFinish = false;
			foreach (CharacterTower tower in mTowerDefenceSystem.getTowerList())
			{
				if (tower.getTowerData().getBattleLevel() >= mCustomParam.mLevel)
				{
					isFinish = true;
					break;
				}
			}
			if (isFinish)
			{
				finish();
				CmdGlobalSelectTowerScene.execute(null, false);
			}
		}, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
}