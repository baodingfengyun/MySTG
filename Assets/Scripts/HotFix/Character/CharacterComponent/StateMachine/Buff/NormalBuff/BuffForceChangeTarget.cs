
// 参数
public class BuffForceChangeTargetParam : CharacterBuffParamT<BuffForceChangeTargetParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 强制改变防御塔和英雄的目标选择为指定目标, 技能目标为对自己释放的除外
public class BuffForceChangeTarget : CharacterBuffT<BuffForceChangeTargetParam>
{
	public CharacterGame mForceTarget;
	public override void resetProperty()
	{
		base.resetProperty();
		mForceTarget = null;
	}
	public override void enter()
	{
		base.enter();
		mForceTarget = mCustomParam.mSource as CharacterGame;
		mCharacterGame.setForceTarget(mForceTarget);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		if (mCharacterGame.getForceTarget() == mForceTarget)
		{
			mCharacterGame.setForceTarget(null);
		}
	}
}