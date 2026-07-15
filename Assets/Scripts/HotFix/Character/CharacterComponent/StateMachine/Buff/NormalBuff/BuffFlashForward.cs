using static StringUtility;
using static GDR;

// 参数
public class BuffFlashForwardParam : CharacterBuffParamT<BuffFlashForwardParam>
{
	public float mDistance;         // 闪现的距离,单位是一个格子大小
	public override void registeAllParam()
	{
		registeParam((param) => { mDistance = param.SToF(); });
	}
	protected override void copyInternal(BuffFlashForwardParam other)
	{
		mDistance = other.mDistance;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mDistance = 0.0f;
	}
}

// 向前闪现一定距离
public class BuffFlashForward : CharacterBuffT<BuffFlashForwardParam>
{
	public override void enter()
	{
		base.enter();
		// 有不可移动的状态组时,不能移动
		if (mCharacter is not CharacterMonster monster || monster.hasStateGroup<StateGroupNotAllowMove>())
		{
			return;
		}
		monster.getComMovement().moveForward(GRID_SIZE * mCustomParam.mDistance);
	}
}