using UnityEngine;
using static GBR;

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
	// 子弹装载时
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		// 计算子弹的移动速度
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		if (mTarget != null)	// 如果有攻击目标
		{
			this.TRACK_TARGET(mTarget, speed, mHitPointOffset, mOnTrackDone);
		}
		else	// 如果没有攻击目标（以发射者的位置为基点、方向计算目标位置）
		{
			Vector3 targetPos = mCharacterGame.getPosition() + mCharacterGame.getForward() * 6.0f;
			this.MOVE_EX(mStartPosition, targetPos, (mStartPosition - targetPos).resetY().getLength().divide(speed), mOnMoveDone);
		}
	}
	// 移动结束（无目标）
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
	// 追踪结束（有目标）
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