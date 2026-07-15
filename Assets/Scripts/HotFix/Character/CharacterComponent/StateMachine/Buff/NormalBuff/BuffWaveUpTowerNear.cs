using System.Collections.Generic;
using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static GBR;

// 参数
public class BuffWaveUpTowerNearParam : CharacterBuffParamT<BuffWaveUpTowerNearParam>
{
	public int mWave;							// 回合数
	public List<int> mPos = new();				// 附近格子的下标数组，六边形左上角0开始顺时针
	public override void registeAllParam()
	{
		registeParam((param) => { mWave = param.SToI(); });
		registeParam((string stringParam) => { mPos = stringParam.SToIs(); });
	}
	public override void check() { }
	protected override void copyInternal(BuffWaveUpTowerNearParam other)
	{
		mWave = other.mWave;
		mPos.AddRange(other.mPos);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mWave = 0;
		mPos.Clear();
	}
}

// 每过n回合，对旁边的防御塔升级
public class BuffWaveUpTowerNear : CharacterBuffT<BuffWaveUpTowerNearParam>
{
	protected int mWave;						// 回合数
	protected int mCurWave;						// 当前回合
	protected List<int> mPos = new();			// 附近格子的下标数组，六边形左上角0开始顺时针
	public override void enter()
	{
		base.enter();
		mWave = mCustomParam.mWave;
		mPos.AddRange(mCustomParam.mPos);
		mEventSystem.listenEvent<EventWaveChange>(onWaveChange, this);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mWave = 0;
		mCurWave = 0;
		mPos.Clear();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	public void onWaveChange(EventWaveChange param)
	{
		if (++mCurWave < mWave)
		{
			return;
		}
		mCurWave = 0;
		using var a = new ListScope<int>(out var grids);
		getHexAroundGird(mCharacterGame.getGridIndex(), 1, grids);
		for(int i = 0; i < grids.Count; ++i)
		{
			if (mPos.Contains(i))
			{
				CmdGlobalUpgradeLevelTowerRogue.execute(mTowerDefenceSystem.getTowerAtGrid(grids[i]));
			}
		}
	}
}