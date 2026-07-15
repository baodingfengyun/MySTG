using static StringUtility;
using static UnityUtility;
using static FrameBaseHotFix;
using static GBR;

public class GuideClickSceneTowerParam : ParamBase
{
	public int mGridIndex;
	public override void registeAllParam()
	{
		registeParam((param) => { mGridIndex = param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mGridIndex = 0;
	}
}

// 点击选中场景中的一个塔
public class GuideClickSceneTower : GuideStepT<GuideClickSceneTowerParam>
{
	public override void start()
	{
		base.start();
		// 找到第一个指定类型的塔
		CharacterTower tower = mTowerDefenceSystem.getTowerAtGrid(mCustomParam.mGridIndex);
		mGlobalTouchSystem.setActiveOnlyObject(tower);
		mUIGuide.setHandPosition(mData.mClickStyle, worldToScreen(tower.getPosition()));
		startInternal();
		mEventSystem.listenEvent((EventTowerSelect eventParam) => 
		{
			if (eventParam.mTower.getGridIndex() == mCustomParam.mGridIndex)
			{
				finish();
			}
		}, this);
	}
}