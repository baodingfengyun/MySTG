using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffShockedParam : CharacterBuffParamT<BuffShockedParam>
{
	public float mPercent;		// 伤害的百分比
	public int mEffectID;		// 特效ID
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
		registeParam((param) => { mEffectID = param.SToI(); });
	}
	protected override void copyInternal(BuffShockedParam other)
	{
		mPercent = other.mPercent;
		mEffectID = other.mEffectID;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
		mEffectID = 0;
	}
}

// 感电（元素状态族代表，与 BuffBurn/FireImprint 等同类）：受到普通伤害时追加一笔百分比伤害。
// 独特机制"只最高层生效"：多个来源挂了不同百分比的感电时，全部状态都留着(不互斥移除)，
// 但 onMonsterBeenHit 里遍历自身所有 BuffShocked 找 mPercent 最大的，只有 maxState==this 的那个才结算，
// 避免多份感电叠加导致伤害爆炸。这是"状态共存但效果取最强"的范例（区别于 OVERLAP_LAYER 的叠层相加）。
// 附加伤害 = 本次伤害 × mPercent × (1 + mBeenShockedDamageIncrease)，并受元素异常免疫(mImmunityElementDebuffDamage)拦截。
// 特效走异步加载 createEffectAsyncSafe，回调里校验 mCharacterGame 非空（防止加载完成时才被销毁），leave 时销毁。
public class BuffShocked : CharacterBuffT<BuffShockedParam>
{
	protected float mPercent;		// 提升的百分比
	protected GameEffect mEffect;   // 感电特效
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
		mEffect = null;
	}
	public override void enter()
	{
		base.enter();
		mPercent = mCustomParam.mPercent;
		mEventSystem.listenEvent<EventCharacterBeenHit>(mCharacter.getGUID(), onMonsterBeenHit, this);

		// 播放感电特效
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
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mEffectManager.destroyEffect(ref mEffect);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onMonsterBeenHit(EventCharacterBeenHit eventParam)
	{
		if (eventParam.mTarget is not CharacterMonster monster)
		{
			return;
		}
		if (monster.getGameData().mImmunityElementDebuffDamage != 0)
		{
			return;
		}
		// 检查一下是否是当前角色身上属性最高的感电状态
		BuffShocked maxState = null;
		foreach (CharacterState buff in mCharacter.getStateMachine().getState<BuffShocked>().getMainList())
		{
			if (maxState == null || maxState.mPercent < (buff as BuffShocked).mPercent)
			{
				maxState = buff as BuffShocked;
			}
		}
		if (maxState == this)
		{
			// 附加一定伤害
			int damage = (int)(eventParam.mDamage * mPercent * (monster.getGameData().mBeenShockedDamageIncrease + 1.0f));
			CmdMonsterSetHP.execute(monster, eventParam.mAttacker, monster.getHP() - damage, -damage, true, HP_DELTA.DEBUFF);
		}
	}
}