using static FrameBaseHotFix;
using static GBR;

// 参数
public class BuffDisarmParam : CharacterBuffParamT<BuffDisarmParam>
{
	public int mEffectID;         // 特效ID
	public override void registeAllParam()
	{
		registeParam((param) => { mEffectID = param.SToI(); });
	}
	protected override void copyInternal(BuffDisarmParam other)
	{
		mEffectID = other.mEffectID;
	}
	public override void check()
	{
		checkDataRefByBuffDetail(mExcelEffect, mEffectID);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mEffectID = 0;
	}
}

// 缴械,也是不允许释放技能
public class BuffDisarm : CharacterBuffT<BuffDisarmParam>
{
	protected GameEffect mEffect;       // 特效
	public override void resetProperty()
	{
		base.resetProperty();
		mEffect = null;
	}
	public override void enter()
	{
		base.enter();
		// 播放特效
		EDEffect effectData = mExcelEffect.query(mCustomParam.mEffectID);
		if (effectData != null)
		{
			mEffectManager.createEffectAsyncSafe(effectData.mPath, this, null, effectData.mSupportMoveToHide, (GameEffect effect) =>
			{
				if (mCharacterGame == null)
				{
					return;
				}
				mEffect = effect;
				mCharacterGame.getAvatar().addLoadedCallback(_ =>
				{
					mEffect?.play();
					mEffect?.setPosition(mCharacterGame.getPosition());
				});
			}, 0, false);
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mEffectManager.destroyEffect(ref mEffect);
	}
}