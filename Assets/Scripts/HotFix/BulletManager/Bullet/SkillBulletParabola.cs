using UnityEngine;
using static FrameBaseUtility;
using static GBR;

// 子弹参数
public class BulletCustomParam_Parabola : ParamCopyableT<BulletCustomParam_Parabola>
{
	public float mMaxHeight;			// 子弹飞行的最高高度
	public float mRange;                // 爆炸攻击范围
	public override void registeAllParam()
	{
		registeParam((param) => { mMaxHeight = param.SToF(); });
		registeParam((param) => { mRange = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMaxHeight = 0.0f;
		mRange = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_Parabola other)
	{
		mMaxHeight = other.mMaxHeight;
		mRange = other.mRange;
	}
}

// 技能的子弹,抛物线轨迹,不追踪
public class SkillBulletParabola : SkillBulletT<BulletCustomParam_Parabola>
{
	protected KeyFrameCallback mMoveDoneCallback;
	protected float mRealtimeRange;								// 实时的子弹爆炸范围
	public SkillBulletParabola()
	{
		mMoveDoneCallback = onReachTarget;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mMoveDoneCallback不重置
		// mMoveDoneCallback = null;
		mRealtimeRange = 0.0f;
	}
	public override float getRealtimeRange() { return mRealtimeRange; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		Vector3 targetPos;
		if (mTarget != null)
		{
			GameObject hitPoint = findGameObject(mBulletData.mHitPoint, mTarget.getGameObject());
			targetPos = hitPoint != null ? hitPoint.transform.position : mTarget.getPosition();
		}
		else
		{
			targetPos = mCharacterGame.getPosition() + mCharacterGame.getForward() * 6.0f;
		}
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		float time = (mStartPosition - targetPos).resetY().getLength().divide(speed);
        this.MOVE_PARABOLA_EX(mStartPosition, targetPos, mCustomParam.mMaxHeight, time, mMoveDoneCallback);
	}
	protected void onReachTarget(ComponentKeyFrame com, bool isBreak)
	{
		mRealtimeRange = mCustomParam.mRange * (mCharacterGame.getBulletExploRangeIncreasePercent(getFlyDistance()) + 1.0f);
		explosion();
		using var a = new ListScope<CharacterMonster>(out var monsterList);
		getRangeEffectiveMonster(mRealtimeRange, monsterList);
		foreach (CharacterMonster monster in monsterList)
		{
			hit(monster);
		}
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
}