using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffBurnParam : CharacterBuffParamT<BuffBurnParam>
{
	public int mDamageFixed;		// 叠加的燃烧伤害值固定值
	public float mDamagePercent;	// 叠加的燃烧伤害值百分比
	public float mInterval;			// 伤害间隔
	public float mDecreasePercent;  // 递减比例
	public int mEffectID;			// 特效ID
	public override void registeAllParam()
	{
		registeParam((param) => { mDamageFixed = param.SToI(); });
		registeParam((param) => { mDamagePercent = param.SToF(); });
		registeParam((param) => { mInterval = param.SToF(); });
		registeParam((param) => { mDecreasePercent = param.SToF(); });
		registeParam((param) => { mEffectID = param.SToI(); });
	}
	protected override void copyInternal(BuffBurnParam other)
	{
		mDamageFixed = other.mDamageFixed;
		mDamagePercent = other.mDamagePercent;
		mInterval = other.mInterval;
		mDecreasePercent = other.mDecreasePercent;
		mEffectID = other.mEffectID;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mDamageFixed = 0;
		mDamagePercent = 0.0f;
		mInterval = 0.0f;
		mDecreasePercent = 0.0f;
		mEffectID = 0;
	}
}

// 伤害递减的燃烧伤害
public class BuffBurn : CharacterBuffT<BuffBurnParam>
{
	protected int mDamage;				// 伤害值
	protected float mCurTime;			// 当前计时
	protected float mInterval;			// 伤害间隔
	protected float mDecreasePercent;	// 递减比例
	protected GameEffect mEffect;		// 显示的燃烧特效
	public BuffBurn()
	{
		mCurTime = -1.0f;
		mMutexType = STATE_MUTEX.OVERLAP_LAYER;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mDamage = 0;
		mCurTime = -1.0f;
		mInterval = 0.0f;
		mDecreasePercent = 0.0f;
		mEffect = null;
	}
	public override void enter()
	{
		base.enter();
		mDamage = mCustomParam.mDamageFixed;
		if (mCustomParam.mSource is CharacterGame character)
		{
			mDamage += (int)(mCustomParam.mDamagePercent * character.getGameData().getAttack());
		}
		mCurTime = mCustomParam.mInterval;
		mInterval = mCustomParam.mInterval;
		mDecreasePercent = mCustomParam.mDecreasePercent;

		// 播放燃烧特效
		EDEffect effectData = mExcelEffect.query(mCustomParam.mEffectID);
		if (effectData != null)
		{
			mEffectManager.createEffectAsyncSafe(effectData.mPath, this, mCharacterGame, effectData.mSupportMoveToHide, (GameEffect effect) =>
			{
				if (mCharacterGame == null)
				{
					return;
				}
				mEffect = effect;
				mCharacterGame.getAvatar().addLoadedCallback(_ =>
				{
					mEffect?.play();
					mEffect?.setParent(mCharacterGame.getAvatar().getModel());
				});
			}, 0, false);
		}
	}
	public override void update(float elapsedTime)
	{
		// 暂时只对怪物造成伤害
		if (mCharacter is CharacterMonster monster && tickTimerLoop(ref mCurTime, elapsedTime, mInterval))
		{
			if (monster.getMonsterData().mImmunityElementDebuffDamage == 0)
			{
				int realDamage = (int)(mDamage * (1.0f + monster.getMonsterData().mBeenBurnDamageIncrease));
				CmdMonsterSetHP.execute(monster, null, monster.getMonsterData().mHP - realDamage, -realDamage, true, HP_DELTA.DEBUFF);
			}
			mDamage = (int)(mDamage * mDecreasePercent);
			// 没有剩余伤害就移除buff
			if (mDamage <= 0)
			{
				removeSelf();
			}
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mEffectManager.destroyEffect(ref mEffect);
	}
	public override void addSameState(CharacterState newState)
	{
		var newStateParam = newState.getParam() as BuffBurnParam;
		int newDamage = newStateParam.mDamageFixed;
		if (newStateParam.mSource is CharacterGame character)
		{
			newDamage += (int)(newStateParam.mDamagePercent * character.getGameData().getAttack());
		}
		mDamage += newDamage;
	}
	// 为了不容易出现错误,将两个分开,虽然具体实现是完全一样的
	public void decreaseToIncreasePercent()
	{
		if (mDecreasePercent >= 1.0f)
		{
			return;
		}
		// 由原来的每次-30%,改变为+30%
		mDecreasePercent = 2.0f - mDecreasePercent;
	}
	public void increaseToDecreasePercent()
	{
		if (mDecreasePercent <= 1.0f)
		{
			return;
		}
		// 由+30%恢复到-30%
		mDecreasePercent = 2.0f - mDecreasePercent;
	}
}