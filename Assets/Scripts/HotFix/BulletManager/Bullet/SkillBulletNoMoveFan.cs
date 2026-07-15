using UnityEngine;
using static GBR;
using static MathUtility;
using static StringUtility;
using static FrameUtility;
using static FrameBaseUtility;

// 子弹参数
public class BulletCustomParam_NoMoveFan : ParamCopyableT<BulletCustomParam_NoMoveFan>
{
	public float mFanAngle;      // 扇形角度,角度制
	public float mFanRadius;     // 扇形半径
	public float mExistTime;     // 持续时间,大于0表示会在一定时间后才会销毁,小于等于0表示创建后立即销毁
	public override void registeAllParam()
	{
		registeParam((param) => { mFanAngle = param.SToF(); });
		registeParam((param) => { mFanRadius = param.SToF(); });
		registeParam((param) => { mExistTime = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mFanAngle = 0.0f;
		mFanRadius = 0.0f;
		mExistTime = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_NoMoveFan other)
	{
		mFanAngle = other.mFanAngle;
		mFanRadius = other.mFanRadius;
		mExistTime = other.mExistTime;
	}
}

// 技能的子弹,扇形范围的子弹
public class SkillBulletNoMoveFan : SkillBulletT<BulletCustomParam_NoMoveFan>
{
	protected float mCurExistTime = -1.0f;                      // 剩余的持续时间
	protected float mRealtimeRange;								// 实时的子弹爆炸范围
	public override void resetProperty()
	{
		base.resetProperty();
		mCurExistTime = -1.0f;
		mRealtimeRange = 0.0f;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mLoaded && tickTimerOnce(ref mCurExistTime, elapsedTime))
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
		}
	}
	public override float getRealtimeRange() { return mRealtimeRange; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		mCurExistTime = mCustomParam.mExistTime;
		lookAt(mCharacterGame.getFacingDirection());
		float halfRadian = toRadian(mCustomParam.mFanAngle * 0.5f);
		Vector3 bulletForward = getForward();
		mRealtimeRange = mCustomParam.mFanRadius < 0 ? mCharacterGame.getRange() : mCustomParam.mFanRadius;
		mRealtimeRange *= mCharacterGame.getBulletExploRangeIncreasePercent(getFlyDistance()) + 1.0f;
		if (isEditor())
		{
			Debug.DrawLine(firePoint, firePoint + rotateVector3(bulletForward * mRealtimeRange, halfRadian), Color.red, mCurExistTime);
			Debug.DrawLine(firePoint, firePoint + rotateVector3(bulletForward * mRealtimeRange, -halfRadian), Color.red, mCurExistTime);
		}

		// 对扇形范围内的敌人造成伤害
		Vector3 bulletPos = getPosition();
		using var a = new ListScope<CharacterMonster>(out var monsterList);
		getRangeEffectiveMonster(mRealtimeRange, monsterList);
		foreach (CharacterMonster monster in monsterList)
		{
			if (getAngleBetweenVector(resetY(bulletForward), resetY(monster.getPosition() - bulletPos)) < halfRadian)
			{
				hit(monster);
			}
		}

		// 播放爆炸特效
		explosion();
		// 销毁子弹
		if (mCurExistTime <= 0.0f)
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
		}
	}
}