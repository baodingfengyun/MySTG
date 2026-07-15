using static FrameBaseHotFix;
using static FrameUtility;
using static StringUtility;
using static GBR;

public class GuidePutTowerMultiParam : ParamBase
{
	public int mTowerCount;
	public override void registeAllParam()
	{
		registeParam((param) => { mTowerCount = param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTowerCount = 0;
	}
}

// 放置多个防御塔
public class GuidePutTowerMulti : GuideStepT<GuidePutTowerMultiParam>
{
	protected int mPrevCount;
	public override void resetProperty()
	{
		base.resetProperty();
		mPrevCount = 0;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mStarted && getCurScene().atProcedure<GameSceneBattleGamingTowerSetup>())
		{
			mPrevCount = mTowerDefenceSystem.getTowerList().Count;
			mUIClientPackRogue.setActiveOnlyAllTowerDrag();
			mBattleScene.setCanReplaceTower(false);
			startInternal();
			mEventSystem.listenEvent((EventTowerPut param) =>
			{
				// 放置了足够数量的防御塔,引导结束
				if (mTowerDefenceSystem.getTowerList().Count >= mPrevCount + mCustomParam.mTowerCount)
				{
					finish();
				}
			}, this);
		}
	}
	public override void clear()
	{
		base.clear();
		mBattleScene.setCanReplaceTower(true);
		mBattleScene.setDragOnlyGrid(-1);
	}
}