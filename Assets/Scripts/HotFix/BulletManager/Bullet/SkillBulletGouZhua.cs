using UnityEngine;
using static GBR;
using static MathUtility;
using static FrameBaseUtility;

// 子弹参数
public class BulletCustomParam_GouZhua : ParamCopyableT<BulletCustomParam_GouZhua>
{
	public override void registeAllParam() {}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_GouZhua other){}
}

// 钩爪,将目标拉到角色最近的一个可行走的格子中
public class SkillBulletGouZhua : SkillBulletT<BulletCustomParam_GouZhua>
{
	protected KeyFrameCallback mMoveDoneCallback;				// 移动完成的回调
	protected KeyFrameCallback mMovingCallback;					// 移动中的回调
	protected Vector3 mHookStartPosition;						// 钩爪在没有目标时的开始移动的位置
	protected Vector3 mTargetPosition;						    // 指定位置
	protected Transform mLineObject;							// 钩爪的可调节的链条物体
	public SkillBulletGouZhua()
	{
		mMoveDoneCallback = onMoveDone;
		mMovingCallback = onMoving;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mMoveDoneCallback,mMovingCallback不重置
		// mMoveDoneCallback = null;
		// mMovingCallback = null;
		mHookStartPosition = Vector3.zero;
		mTargetPosition = Vector3.zero;
		mLineObject = null;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		// 始终朝向目标
		if (mTarget != null && (mTarget.getAssignID() != mTargetAssignID || mTarget.isDestroy()))
		{
			mTarget = null;
		}
		if (mTarget != null)
		{
			lookAtPoint(mTarget.getPosition());
		}
	}
	public void setTargetPosition(Vector3 target) { mTargetPosition = target; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		// 按直线将目标拉到指定位置
		if (mTarget != null)
		{
			float time = divide(getLength(mTargetPosition - mTarget.getPosition()), speed);
            mTarget.MOVE_EX(mTarget.getPosition(), mTargetPosition, time, mMovingCallback, mMoveDoneCallback);
		}
		else
		{
			// 没有目标时固定向正前方伸出10米
			float moveDistance = 10.0f;
			mHookStartPosition = mTargetPosition + mCharacterGame.getForward() * moveDistance;
			OT.TWEEN_FLOAT(0.0f, 1.0f, divide(moveDistance, speed), mMovingCallback, mMoveDoneCallback);
		}
	}
	protected void onMoving(ComponentKeyFrame com, bool isBreak)
	{
		if (isBreak)
		{
			return;
		}
		// 更新特效,需要检查特效是否已经被销毁,避免访问到已经被销毁的物体
		if (mFlyEffect != null)
		{
			if (mLineObject == null)
			{
				mLineObject = findGameObject("HookPoint", mFlyEffect.getGameObject(), true).transform;
			}
			if (mLineObject != null)
			{
				if (mTarget != null)
				{
					mLineObject.position = mTarget.getPosition();
				}
				else
				{
					mLineObject.position = lerp(mHookStartPosition, mTargetPosition, com.getTremblingPercent());
				}
			}
		}
	}
	protected void onMoveDone(ComponentKeyFrame com, bool isBreak)
	{
		if (mWillDestroy)
		{
			return;
		}
		if (isBreak)
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
			return;
		}
		// 移动完成
		explosion();
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
}