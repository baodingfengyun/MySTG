using UnityEngine;
using static GBR;

// 子弹参数
public class BulletCustomParam_ParabolaTrack : ParamCopyableT<BulletCustomParam_ParabolaTrack>
{
	public float mMaxHeight;			// 子弹飞行的最高高度
	public float mRange;                // 爆炸攻击范围
	public override void registeAllParam()
	{
		registeParam((param) => { mMaxHeight = param.SToF(); });
		registeParam((param) => { mRange = param.SToF(); });
	}
	//------------------------------------------------------------------------------------------------------------------------------
	public override void resetProperty()
	{
		base.resetProperty();
		mMaxHeight = 0.0f;
		mRange = 0.0f;
	}
	protected override void initFromCopyInternal(BulletCustomParam_ParabolaTrack other)
	{
		mMaxHeight = other.mMaxHeight;
		mRange = other.mRange;
	}
}

// 技能的子弹,抛物线轨迹,会一直追踪目标
public class SkillBulletParabolaTrack : SkillBulletT<BulletCustomParam_ParabolaTrack>
{
	protected BoolCallback mOnTrackDone;							// 追踪完成的回调
	protected KeyFrameCallback mOnMoveDone;							// 追踪完成的回调
	protected float mRealtimeRange;									// 实时的子弹爆炸范围
	public SkillBulletParabolaTrack()
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
		mRealtimeRange = 0.0f;
	}
	public override float getRealtimeRange() { return mRealtimeRange; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		if (mTarget == null)
		{
			Vector3 targetPos = mCharacterGame.getPosition() + mCharacterGame.getForward() * 6.0f;
			float time = (mStartPosition - targetPos).resetY().getLength().divide(speed);
			this.MOVE_PARABOLA_EX(mStartPosition, targetPos, mCustomParam.mMaxHeight, time, mOnMoveDone);
		}
		else
		{
			this.TRACK_TARGET_PARABOLA(mTarget, speed, mCustomParam.mMaxHeight, mStartPosition, mHitPointOffset, mOnTrackDone);
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
		// 如果直接访问当前子弹记录的目标,可能此目标已经被销毁,访问会报错,所以访问参数传进来的目标
		if (mBulletData.mSingleTarget)
		{
			hit(getComponent<ComponentTrackTargetParabola>().getTrackTarget() as CharacterMonster);
		}
		else
		{
			using var a = new ListScope<CharacterMonster>(out var monsterList);
			mRealtimeRange = mCustomParam.mRange * (mCharacterGame.getBulletExploRangeIncreasePercent(getFlyDistance()) + 1.0f);
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