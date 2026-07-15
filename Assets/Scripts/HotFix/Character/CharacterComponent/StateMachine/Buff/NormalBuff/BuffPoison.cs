using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffPoisonParam : CharacterBuffParamT<BuffPoisonParam>
{
	public int mLayerCount;         // 叠加的层数
	public int mEffectID;			// 特效ID
	public override void registeAllParam()
	{
		registeParam((param) => { mLayerCount = param.SToI(); });
		registeParam((param) => { mEffectID = param.SToI(); });
	}
	protected override void copyInternal(BuffPoisonParam other)
	{
		mLayerCount = other.mLayerCount;
		mEffectID = other.mEffectID;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mLayerCount = 0;
		mEffectID = 0;
	}
}

// 中毒,可叠加层数,伤害为层数*固定伤害
public class BuffPoison : CharacterBuffT<BuffPoisonParam>
{
	protected float mCurTime;					// 当前计时
	protected int mLayerCount;					// 当前叠加的层数
	protected const int POISON_DAMAGE = 10;     // 每层固定伤害
	protected GameEffect mEffect;				// 中毒特效
	public BuffPoison()
	{
		mMutexType = STATE_MUTEX.OVERLAP_LAYER;
		mCurTime = -1.0f;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mCurTime = -1.0f;
		mLayerCount = 0;
		mEffect = null;
	}
	public override void enter()
	{
		base.enter();
		mCurTime = 0.0f;
		mLayerCount = mCustomParam.mLayerCount;

		// 播放中毒特效
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
		if (tickTimerLoop(ref mCurTime, elapsedTime, 1.0f))
		{
			// 免疫debuff伤害
			if (mCharacter is CharacterMonster monster && monster.getMonsterData().mImmunityElementDebuffDamage == 0)
			{
				int realDamage = (int)(mLayerCount * POISON_DAMAGE * (1.0f + monster.getMonsterData().mBeenPoisoningDamageIncrease));
				CmdMonsterSetHP.execute(monster, null, monster.getMonsterData().mHP - realDamage, -realDamage, true, HP_DELTA.DEBUFF);
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
		mLayerCount += (newState.getParam() as BuffPoisonParam).mLayerCount;
		mStateTime = newState.getStateTime();
	}
}