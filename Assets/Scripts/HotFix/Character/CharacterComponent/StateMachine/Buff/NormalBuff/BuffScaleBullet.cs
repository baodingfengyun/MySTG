using static StringUtility;

// 参数
public class BuffScaleBulletParam : CharacterBuffParamT<BuffScaleBulletParam>
{
	public float mScale;         // 调整的子弹大小
	public override void registeAllParam()
	{
		registeParam((param) => { mScale = param.SToF(); });
	}
	protected override void copyInternal(BuffScaleBulletParam other)
	{
		mScale = other.mScale;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mScale = 0.0f;
	}
}

// 调整子弹大小
public class BuffScaleBullet : CharacterBuffT<BuffScaleBulletParam>
{
	protected float mScale;     // 调整的子弹大小
	public override void resetProperty()
	{
		base.resetProperty();
		mScale = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mScale = mCustomParam.mScale;
		mCharacterGame.getGameData().addBulletScale(mScale);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().removeBulletScale(mScale);
	}
}