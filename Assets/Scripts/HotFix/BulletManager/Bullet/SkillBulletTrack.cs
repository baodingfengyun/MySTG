using UnityEngine;
using static GBR;
using static MathUtility;

// 子弹参数
public class BulletCustomParam_Track : ParamCopyableT<BulletCustomParam_Track>
{
	public override void registeAllParam() { }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_Track other) { }
}

// 技能的子弹,会一直追踪目标
public class SkillBulletTrack : SkillBulletT<BulletCustomParam_Track>
{
	protected BoolCallback mOnTrackDone;					// 移动完成的回调
	protected KeyFrameCallback mOnMoveDone;					// 没有目标时移动完成的回调
	public SkillBulletTrack()
	{
		mOnTrackDone = onTrackDone;
		mOnMoveDone = onMoveDone;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mOnTrackDone,mOnMoveDone不重置
		// mOnTrackDone = null;
		// mOnMoveDone = null;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		if (mTarget != null)
		{
			this.TRACK_TARGET(mTarget, speed, mHitPointOffset, mOnTrackDone);
		}
		else
		{
			Vector3 targetPos = mCharacterGame.getPosition() + mCharacterGame.getForward() * 6.0f;
			this.MOVE_EX(mStartPosition, targetPos, divide(getLength(resetY(mStartPosition - targetPos)), speed), mOnMoveDone);
		}
	}
	protected void onMoveDone(ComponentKeyFrame com, bool isBreak)
	{
		if (mWillDestroy)
		{
			return;
		}
		if(isBreak)
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
		}
		explosion();
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
	protected void onTrackDone(bool breakTrack)
	{
		if (mWillDestroy)
		{
			return;
		}
		if (breakTrack)
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
			return;
		}
		// 追踪结束后需要使用追踪组件的目标,可以知道目标是否已经不可访问了
		// 如果直接访问当前子弹记录的目标,可能此目标已经被销毁,访问会报错
		hit(getComponent<ComponentTrackTargetParabola>().getTrackTarget() as CharacterMonster);
		explosion();
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
}