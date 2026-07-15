using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 参数
public class BuffBuildingParam : CharacterBuffParamT<BuffBuildingParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 塔建造中的状态
public class BuffBuilding : CharacterBuffT<BuffBuildingParam>
{
	protected GameEffect mEffect;	// 特效
	public override void resetProperty()
	{
		base.resetProperty();
		mEffect = null;
	}
	public override void enter()
	{
		base.enter();
		string effectPath = mExcelEffect.query(TOWER_SELECT_EFFECT_ID).mPath;
		mEffectManager.createEffectAsyncSafe(effectPath, this, null, true, (GameEffect effect) =>
		{
			mEffect = effect;
			mEffect.setPosition(mCharacter.getPosition());
		});
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mEffectManager.destroyEffect(ref mEffect);
	}
}