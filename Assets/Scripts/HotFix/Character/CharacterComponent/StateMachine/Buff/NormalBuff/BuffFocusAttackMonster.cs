using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 参数
public class BuffFocusAttackMonsterParam : CharacterBuffParamT<BuffFocusAttackMonsterParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 集火怪物的buff
public class BuffFocusAttackMonster : CharacterBuffT<BuffFocusAttackMonsterParam>
{
	protected GameEffect mEffect;			// 特效
	public override void enter()
	{
		base.enter();
		if (mCharacterGame is not CharacterMonster monster)
		{
			return;
		}
		mTowerDefenceSystem.setFocusedMonster(monster);
		EDEffect effectData = mExcelEffect.query(FOCUS_BUFF_EFFECT);
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
					mEffect?.setParent((mCharacterGame.getAvatar() as COMMonsterAvatar).getHeadPoint().gameObject);
				});
			}, 0, false);
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mEffect = null;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mEffectManager.destroyEffect(ref mEffect);
		mTowerDefenceSystem.setFocusedMonster(null);
	}
}