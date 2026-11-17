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

// 【代表样例：可叠加持续伤害 Buff】中毒
// 【叠加策略】STATE_MUTEX.OVERLAP_LAYER——同类型中毒不互斥，addSameState 把新层数累加到 mLayerCount；
//   这就是"毒能叠 N 层、每层都加伤害"的实现（对比 BuffBurn 也是 OVERLAP_LAYER 但叠的是伤害值）。
// 【伤害】每秒 tick 一次(tickTimerLoop 1.0s)，伤害 = 层数 × POISON_DAMAGE(10) × (1+怪物中毒易伤)。
//   走 CmdMonsterSetHP 且 deltaType=DEBUFF，与玩家攻击伤害分通道。
// 【免疫】怪物的 mImmunityElementDebuffDamage 标记可完全免疫此类元素 debuff 伤害。
// 【表现】enter 异步加载中毒特效并挂到模型，leave 时销毁。
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