using static StringUtility;
using static GDR;

// 参数
public class BuffTransferBackParam : CharacterBuffParamT<BuffTransferBackParam>
{
	public float mGridCount;         // 传送的距离,单位是一个格子大小
	public override void registeAllParam()
	{
		registeParam((param) => { mGridCount = param.SToI(); });
	}
	protected override void copyInternal(BuffTransferBackParam other)
	{
		mGridCount = other.mGridCount;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mGridCount = 0;
	}
}

// 向后传送一定距离
public class BuffTransferBack : CharacterBuffT<BuffTransferBackParam>
{
	public override void enter()
	{
		base.enter();
		if (mCharacter is not CharacterMonster monster)
		{
			return;
		}
		COMMonsterMovement comMovement = monster.getComMovement();
		// 向后传送前需要改变当前行进方向,以及当前的目标点
		comMovement.generateNextRoadIndex(monster.getPosition(), out int index, false);
		if (index >= 0)
		{
			comMovement.setTargetPointIndex(index);
		}
		comMovement.moveBackward(mCustomParam.mGridCount * GRID_SIZE);
		// 由于传送回去后移动方向跟传送的方向相反,所以需要重新计算移动的目标点
		comMovement.checkRoadPointBetween();
	}
}