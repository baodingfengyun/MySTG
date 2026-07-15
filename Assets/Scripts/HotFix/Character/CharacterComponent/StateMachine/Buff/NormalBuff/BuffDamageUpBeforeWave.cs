using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffDamageUpBeforeWaveParam : CharacterBuffParamT<BuffDamageUpBeforeWaveParam>
{
	public float mIncrease;         // 伤害增加的百分比
	public int mWaveCount;			// 波次数
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
		registeParam((param) => { mWaveCount = param.SToI(); });
	}
	protected override void copyInternal(BuffDamageUpBeforeWaveParam other)
	{
		mIncrease = other.mIncrease;
		mWaveCount = other.mWaveCount;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
		mWaveCount = 0;
	}
}

// 前一定波数伤害增加
public class BuffDamageUpBeforeWave : CharacterBuffT<BuffDamageUpBeforeWaveParam>
{
	protected float mIncreasedPercent;	// 已经增加的百分比
	protected float mIncrease;			// 伤害增加的百分比
	protected int mWaveCount;			// 波次数
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mWaveCount = mCustomParam.mWaveCount;
		mIncreasedPercent = mTowerDefenceSystem.getWaveIndex() < mWaveCount ? mIncrease : 0.0f;
		mCharacterGame.getGameData().mDamageIncrease += mIncreasedPercent;
		mEventSystem.listenEvent<EventWaveChange>(onWaveChanged, this);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mDamageIncrease -= mIncreasedPercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasedPercent = 0;
		mIncrease = 0.0f;
		mWaveCount = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onWaveChanged(EventWaveChange param)
	{
		float increase = mTowerDefenceSystem.getWaveIndex() < mWaveCount ? mIncrease : 0.0f;
		mCharacterGame.getGameData().mDamageIncrease += increase - mIncreasedPercent;
		mIncreasedPercent = increase;
	}
}