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

// 震荡塔子弹消耗的特殊暴击加成（叠层族代表，展示 OVERLAP_LAYER 的实现细节）。
// 与"数值加减族"的关键区别：同类型 Buff 再次添加时不是新建，而是走 addSameState 叠加层数——
//   ①构造函数设 mMutexType=OVERLAP_LAYER，②每次叠层 ++mCurLayer 并再 += 一次加成，
//   ③叠加时刷新持续时间 setStateTime，④到 mLayerMax 上限后不再加。
// leave 时按 mCurLayer*mIncrease 一次性扣回全部层数的加成（不是只扣一层）。
// 理解本类就理解了毒/燃烧等所有可叠层 Debuff 的底层机制（BuffPoison/BuffBurn 走同一套状态机能力）。
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