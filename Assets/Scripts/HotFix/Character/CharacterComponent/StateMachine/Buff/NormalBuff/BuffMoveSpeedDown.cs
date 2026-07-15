using static MathUtility;
using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffMoveSpeedDownParam : CharacterBuffParamT<BuffMoveSpeedDownParam>
{
	public float mPercent;			// 减速的百分比
	public int mEffectID;			// 特效ID
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
		registeParam((param) => { mEffectID = param.SToI(); });
	}
	protected override void copyInternal(BuffMoveSpeedDownParam other)
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

// 百分比减速,固定百分比
public class BuffMoveSpeedDown : CharacterBuffT<BuffMoveSpeedDownParam>
{
	protected float mSlowDown;			// 减速的绝对值
	protected GameEffect mEffect;       // 冰霜减速特效
	public BuffMoveSpeedDown()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mSlowDown = 0.0f;
		mEffect = null;
	}
	public override void enter()
	{
		base.enter();
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		mSlowDown = comMovement.getSpeed() * mCustomParam.mPercent * (1.0f + mCharacterGame.getGameData().mSlowDownIncrease);
		clampMax(ref mSlowDown, comMovement.getSpeed());
		comMovement.setSpeed(comMovement.getSpeed() - mSlowDown);

		// 播放冰冻特效
		EDEffect effectData = mExcelEffect.query(mCustomParam.mEffectID);
		if (effectData != null)
		{
			mEffectManager.createEffectAsyncSafe(effectData.mPath, mCharacterGame, mCharacterGame, effectData.mSupportMoveToHide, (GameEffect effect) =>
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
		COMMonsterMovement comMovement = (mCharacter as CharacterMonster).getComMovement();
		comMovement.setSpeed(comMovement.getSpeed() + mSlowDown);
		mEffectManager.destroyEffect(ref mEffect);
	}
}