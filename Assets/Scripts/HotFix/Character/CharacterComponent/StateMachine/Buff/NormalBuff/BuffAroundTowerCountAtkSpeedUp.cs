using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static StringUtility;
using static GBR;

// 参数
public class BuffAroundTowerCountAttackSpeedUpParam : CharacterBuffParamT<BuffAroundTowerCountAttackSpeedUpParam>
{
	public float mPercent;      // 每个塔增加的百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void copyInternal(BuffAroundTowerCountAttackSpeedUpParam other)
	{
		mPercent = other.mPercent;
	}
}

// 增加攻速,实际就是减少技能CD
public class BuffAroundTowerCountAttackSpeedUp : CharacterBuffT<BuffAroundTowerCountAttackSpeedUpParam>
{
	public float mPercent;		// 每个塔增加的百分比
	public int mCount;			// 周围的塔数量
	public bool mTowerChanged;	// 场上的塔是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
		mCount = 0;
		mTowerChanged = false;
	}
	public override void enter()
	{
		base.enter();
		mPercent = mCustomParam.mPercent;
		mEventSystem.listenEvent<EventGridTowerChange>(onTowerChanged, this);
		mTowerChanged = true;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mTowerChanged)
		{
			mTowerChanged = false;
			int newCount = 0;
			foreach (int grid in getHexDiagonalGird((mCharacterGame as CharacterTower).getGridIndex(), 1))
			{
				newCount += mTowerDefenceSystem.hasTowerAtGrid(grid) ? 1 : 0;
			}
			if (newCount != mCount)
			{
				mCharacterGame.getGameData().addAttackSpeed(mPercent * (newCount - mCount));
				mCount = newCount;
			}
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().removeAttackSpeed(mPercent * mCount);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
}