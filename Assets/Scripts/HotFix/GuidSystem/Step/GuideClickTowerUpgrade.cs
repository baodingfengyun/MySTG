using UnityEngine;
using static FrameBaseHotFix;
using static GBR;

// 点击防御塔的升级按钮
public class GuideClickTowerUpgrade : GuideStep
{
	public override void start()
	{
		base.start();
		mUITowerOperation.setActiveOnlyUpgrade(out Vector3 pos);
		mUIGuide.setHandPosition(mData.mClickStyle, pos);
		startInternal();
		mEventSystem.listenEvent((EventTowerLevelChange eventParam) => 
		{
			CmdGlobalSelectTowerScene.execute(null, false);
			finish();
		}, this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
}