using UnityEngine;
using static GDR;
using static MathUtility;

// 参数
public class BuffStrickBackParam : CharacterBuffParamT<BuffStrickBackParam>
{
	public float mGridCount;         // 击退的距离,单位是一个格子大小
	public override void registeAllParam()
	{
		registeParam((param) => { mGridCount = param.SToF(); });
	}
	protected override void copyInternal(BuffStrickBackParam other)
	{
		mGridCount = other.mGridCount;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mGridCount = 0.0f;
	}
}

// 击退一定距离
public class BuffStrickBack : CharacterBuffT<BuffStrickBackParam>
{
	public override bool canEnter()
	{
		// 如果有无法被击退的buff,则不允许添加击退buff
		return base.canEnter() && !mCharacter.getStateMachine().hasState<BuffAntiStrickBack>();
	}
	public override void enter()
	{
		base.enter();
		if (mCharacter is not CharacterMonster monster)
		{
			return;
		}
		// 固定移动0.3秒
		mStateTime = 0.3f;
		// 往移动方向的反方向击退
		Vector3 curPos = monster.getPosition();
		Vector3 delta = setLength(monster.getComMovement().getTargetPosition() - curPos, GRID_SIZE * mCustomParam.mGridCount);
        mCharacter.MOVE(curPos, curPos - delta, mStateTime);
	}
}