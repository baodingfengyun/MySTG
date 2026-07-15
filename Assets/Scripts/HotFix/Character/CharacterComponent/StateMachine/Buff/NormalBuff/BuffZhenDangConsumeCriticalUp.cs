using static StringUtility;

// 参数
public class BuffZhenDangConsumeCriticalUpParam : CharacterBuffParamT<BuffZhenDangConsumeCriticalUpParam>
{
	public float mIncrease;		// 增加的暴击率
	public int mLayerMax;		// 最大叠层数
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToF(); });
		registeParam((param) => { mLayerMax = param.SToI(); });
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
		mLayerMax = 0;
	}
	protected override void copyInternal(BuffZhenDangConsumeCriticalUpParam other)
	{
		mIncrease = other.mIncrease;
		mLayerMax = other.mLayerMax;
	}
}

// 震荡塔子弹消耗的特殊暴击加成buff
public class BuffZhenDangConsumeCriticalUp : CharacterBuffT<BuffZhenDangConsumeCriticalUpParam>
{
	protected float mIncrease;		// 增加的暴击率
	protected int mLayerMax;		// 最大叠层数
	protected int mCurLayer;		// 已经叠加的层数
	public BuffZhenDangConsumeCriticalUp()
	{
		mMutexType = STATE_MUTEX.OVERLAP_LAYER;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0.0f;
		mLayerMax = 0;
		mCurLayer = 0;
	}
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mLayerMax = mCustomParam.mLayerMax;
		++mCurLayer;
		mCharacterGame.getGameData().mCriticalIncrease += mIncrease;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mCriticalIncrease -= mCurLayer * mIncrease;
	}
	public override void addSameState(CharacterState newState)
	{
		base.addSameState(newState);
		setStateTime(getStateMaxTime());
		if(mCurLayer >= mLayerMax)
		{
			return;
		}
		++mCurLayer;
		mCharacterGame.getGameData().mCriticalIncrease += mIncrease;
	}
}