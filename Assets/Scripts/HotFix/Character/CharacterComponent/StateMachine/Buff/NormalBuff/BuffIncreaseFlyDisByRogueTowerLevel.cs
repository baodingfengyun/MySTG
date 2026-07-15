using System.Collections.Generic;
using static FrameBaseHotFix;
using static UnityUtility;
using static GDR;

// 参数
public class BuffIncreaseFlyDisByRogueTowerLevelParam : CharacterBuffParamT<BuffIncreaseFlyDisByRogueTowerLevelParam>
{
	public List<int> mLevels = new();		// 等级
	public List<float> mAddDis = new();		// 等级对应增加的飞行距离(格)
	public override void registeAllParam()
	{
		registeParam(stringParam => stringParam.SToIs(mLevels));
		registeParam(stringParam => stringParam.SToFs(mAddDis));
	}
	protected override void copyInternal(BuffIncreaseFlyDisByRogueTowerLevelParam other)
	{
		mLevels.AddRange(other.mLevels);
		mAddDis.AddRange(other.mAddDis);
	}
	public override void check()
	{
		if(mLevels.Count != mAddDis.Count)
		{
			logError("BuffDetail[" + mBuffDetailData + "] 配置的 等级数量 和 效果数量 不匹配");
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mLevels.Clear();
		mAddDis.Clear();
	}
}

// 肉鸽模式，按塔等级增加子弹飞行距离
public class BuffIncreaseFlyDisByRogueTowerLevel : CharacterBuffT<BuffIncreaseFlyDisByRogueTowerLevelParam>
{
	protected List<int> mLevels = new();		// 等级
	protected List<float> mAddDis = new();		// 等级对应增加的飞行距离(格)
	protected float mCurIncrease;				// 当前的效果
	public override void enter()
	{
		base.enter();
		mLevels.AddRange(mCustomParam.mLevels);
		mAddDis.AddRange(mCustomParam.mAddDis);
		mEventSystem.listenEvent<EventTowerLevelChange>(mCharacter.getGUID(), onGridTowerChange, this);
		refreshIncrease();
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		(mCharacterGame as CharacterTower).getTowerData().removeIncreaseFlyDis(mCurIncrease * GRID_SIZE);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mLevels.Clear();
		mAddDis.Clear();
		mCurIncrease = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void refreshIncrease()
	{
		CharacterTowerData towerData = (mCharacterGame as CharacterTower).getTowerData();
		towerData.removeIncreaseFlyDis(mCurIncrease * GRID_SIZE);
		int curLevel = towerData.getBattleLevel();
		for(int i = mLevels.Count - 1; i >= 0; --i)
		{
			if (curLevel >= mLevels[i])
			{
				mCurIncrease = mAddDis[i];
				break;
			}
		}
		towerData.addIncreaseFlyDis(mCurIncrease * GRID_SIZE);
	}
	protected void onGridTowerChange(EventTowerLevelChange param)
	{
		refreshIncrease();
	}
}