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

// 感电,只有百分比最高的感电属性才会生效产生附加伤害,其余的不会移除,但是也不会产生伤害
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