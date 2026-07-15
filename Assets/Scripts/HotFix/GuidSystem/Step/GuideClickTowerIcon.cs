using static StringUtility;
using static GBR;

public class GuideClickTowerIconParam : ParamBase
{
	public TOWER_TYPE mTowerType;
	public override void registeAllParam()
	{
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerType = TOWER_TYPE.NONE;
	}
}

// 点击选中列表中的一个塔
public class GuideClickTowerIcon : GuideStepT<GuideClickTowerIconParam>
{
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mStarted && mUIClientPackRogue.isMoveDone())
		{
			// 找到第一个指定类型的塔
			mUIClientPackRogue.setActiveOnlyTowerClick(mCustomParam.mTowerType);
			mUIGuide.setHandPosition(mData.mClickStyle, mUIClientPackRogue.getTowerPropPosition(mCustomParam.mTowerType));
			startInternal();
		}
		if (mStarted && mUITowerInfo.safe() != null)
		{
			finish();
		}
	}
}