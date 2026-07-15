using UnityEngine;
using static MathUtility;
using static GDR;

// 参数
public class BuffFloatToAirParam : CharacterBuffParamT<BuffFloatToAirParam>
{
	public override void registeAllParam() { }
	public override void check() { }
}

// 浮空
public class BuffFloatToAir : CharacterBuffT<BuffFloatToAirParam>
{
	protected float mOriginY;       // 初始的位置高度
	protected bool mFalling;        // 是否正在下落
	public BuffFloatToAir()
	{
		mMutexType = STATE_MUTEX.NO_NEW;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mOriginY = 0.0f;
		mFalling = false;
	}
	public override void enter()
	{
		base.enter();
		var monster = mCharacterGame as CharacterMonster;
		mOriginY = monster.getPosition().y;
        monster.MOVE(monster.getPosition(), monster.getPosition() + new Vector3(0.0f, FLY_MONSTER_HEIGHT), 0.1f);
	}
	public override void update(float elapsedTime)
	{
		// 因为下落有一个过程,所以需要在状态还有一定时间结束时就开始移动
		if (!mFalling && mStateTime < 0.1f)
		{
			mFalling = true;
			var monster = mCharacterGame as CharacterMonster;
            monster.MOVE(monster.getPosition(), replaceY(monster.getPosition(), mOriginY), 0.1f);
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		// 确认停止下落,立即设置的地面
		var monster = mCharacterGame as CharacterMonster;
        monster.MOVE(replaceY(monster.getPosition(), mOriginY));
	}
}