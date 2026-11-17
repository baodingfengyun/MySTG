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

// 向前闪现一定距离（瞬时位移族代表）。
// 没有 leave/reset——因为它是一次性效果：只在 enter 里调移动组件 moveForward(GRID_SIZE×距离) 立即位移，
// Buff 本身只是"触发一次动作"的载体，时间到了自然移除，无状态需要还原。
// 位移前检查 StateGroupNotAllowMove(被禁锢时不能闪)，体现状态组互斥对行为的约束。
// 与 BuffFloatToAir/BuffPushMove 等同族，都是 enter 里直接改角色位置。
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