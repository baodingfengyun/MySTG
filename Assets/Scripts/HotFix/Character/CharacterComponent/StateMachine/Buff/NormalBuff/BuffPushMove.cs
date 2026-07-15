using UnityEngine;
using static GBR;
using static StringUtility;

// 参数
public class BuffPushMoveParam : CharacterBuffParamT<BuffPushMoveParam>
{
	public float mSpeed;         // 推动的速度
	public override void registeAllParam()
	{
		registeParam((param) => { mSpeed = param.SToF(); });
	}
	protected override void copyInternal(BuffPushMoveParam other)
	{
		mSpeed = other.mSpeed;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mSpeed = 0.0f;
	}
}

// 百分比加速,固定百分比
public class BuffPushMove : CharacterBuffT<BuffPushMoveParam>
{
	protected Vector3 mPushMove;		// 推动的方向和速度
	public override void resetProperty()
	{
		base.resetProperty();
		mPushMove = Vector3.zero;
	}
	public override void enter()
	{
		base.enter();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		Vector3 newPos = mCharacterGame.getPosition() + mPushMove * elapsedTime;
		if(mBattleScene.worldPointToGridIndex(newPos) == (mCharacterGame as CharacterMonster).getComMovement().getGridIndex())
		{
			mCharacterGame.setPosition(newPos);
		}
	}
}