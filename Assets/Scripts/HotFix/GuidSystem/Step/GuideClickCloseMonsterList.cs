using UnityEngine;
using static GBR;

// 点击关闭怪物信息列表
public class GuideClickCloseMonsterList : GuideStep
{
	public override void start()
	{
		base.start();
		mUIGuide.setHandPosition(mData.mClickStyle, Vector3.zero);
		startInternal();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		// 关闭关卡详情界面就算完成
		if (mStarted)
		{
			finish();
		}
	}
}