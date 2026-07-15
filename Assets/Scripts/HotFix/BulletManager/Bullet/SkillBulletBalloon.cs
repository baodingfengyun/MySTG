using UnityEngine;
using static GBR;
using static MathUtility;

// 子弹参数
public class BulletCustomParam_Balloon : ParamCopyableT<BulletCustomParam_Balloon>
{
	public float mFlyHeight;		// 升空高度
	public float mNearRange;		// 靠近的范围
	public float mExplosionRange;   // 爆炸的范围
	public override void registeAllParam()
	{
		registeParam((param) => { mFlyHeight = param.SToF(); });
		registeParam((param) => { mNearRange = param.SToF(); });
		registeParam((param) => { mExplosionRange = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mFlyHeight = 0.0f;
		mNearRange = 0.0f;
		mExplosionRange = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_Balloon other)
	{
		mFlyHeight = other.mFlyHeight;
		mNearRange = other.mNearRange;
		mExplosionRange = other.mExplosionRange;
	}
}

// 技能的子弹,气球,先升空,升空一定高度后开始追踪目标,接近目标一定范围内时爆炸
public class SkillBulletBalloon : SkillBulletT<BulletCustomParam_Balloon>
{
	protected KeyFrameCallback mOnMoveDone;					// 升空移动完成的回调
	protected BoolCallback mOnTrackDone;					// 追踪完成的回调
	protected float mRealtimeRange;							// 实时的子弹爆炸范围
	public SkillBulletBalloon()
	{
		mOnMoveDone = onMoveDone;
		mOnTrackDone = onTrackDone;
		mFaceForward = false;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mOnMoveDone,mOnTrackDone不重置
		// mOnMoveDone = null;
		// mOnTrackDone = null;
		mRealtimeRange = 0.0f;
		mFaceForward = false;
	}
	public override float getRealtimeRange() { return mRealtimeRange; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		if (mTarget == null)
		{
			mOnMoveDone?.Invoke(null, true);
			return;
		}
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		float time = divide(mCustomParam.mFlyHeight, speed);
        this.MOVE_EX(getPosition(), getPosition() + new Vector3(0.0f, mCustomParam.mFlyHeight), time, mOnMoveDone);
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
		if (mTarget == null)
		{
			explosion();
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
			return;
		}
		// 升空完成,开始追踪
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
        this.TRACK_TARGET(mTarget, speed, mCustomParam.mNearRange, mHitPointOffset, mOnTrackDone);
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
		if (mBulletData.mSingleTarget)
		{
			hit(getComponent<ComponentTrackTarget>().getTrackTarget() as CharacterMonster);
		}
		else
		{
			using var a = new ListScope<CharacterMonster>(out var monsterList);
			mRealtimeRange = mCustomParam.mExplosionRange * (mCharacterGame.getBulletExploRangeIncreasePercent(getFlyDistance()) + 1.0f);
			getRangeEffectiveMonster(mRealtimeRange, monsterList);
			foreach (CharacterMonster monster in monsterList)
			{
				hit(monster);
			}
		}

		explosion();
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
}