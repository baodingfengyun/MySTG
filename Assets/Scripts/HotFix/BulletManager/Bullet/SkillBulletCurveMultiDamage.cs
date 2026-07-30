using UnityEngine;
using System.Collections.Generic;
using static FrameUtility;
using static UnityUtility;
using static MathUtility;
using static GBR;
using static GDR;

// 子弹参数
public class BulletCustomParam_CurveMultiDamage : ParamCopyableT<BulletCustomParam_CurveMultiDamage>
{
	public float mInterval;     // 伤害间隔
	public bool mDamageRepeat;  // 是否可以对一个目标重复产生伤害
	public override void registeAllParam()
	{
		registeParam((param) => { mInterval = param.SToF(); });
		registeParam((param) => { mDamageRepeat = param.SToI() > 0; });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mInterval = 0.0f;
		mDamageRepeat = false;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_CurveMultiDamage other)
	{
		mInterval = other.mInterval;
		mDamageRepeat = other.mDamageRepeat;
	}
}

// 技能的子弹,按折线移动,每隔一定时间产生一次
public class SkillBulletCurveMultiDamage : SkillBulletT<BulletCustomParam_CurveMultiDamage>
{
	protected HashSet<CharacterGame> mHitList = new();					// 已经命中过的敌人列表
	protected List<Vector3> mPath = new();								// 移动路线
	protected Collider[] mTempResult;									// 临时对象
	protected KeyFrameCallback mOnMoveDone;								// 移动完成的回调
	protected SphereCollider mCollider;									// 子弹碰撞体
	protected float mCurTime = -1.0f;									// 当前计时
	public SkillBulletCurveMultiDamage()
	{
		mTempResult = new Collider[16];
		mOnMoveDone = onMoveDone;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mHitList.Clear();
		mPath.Clear();
		mTempResult = null;
		// mOnMoveDone不重置
		// mOnMoveDone = null;
		mCollider = null;
		mCurTime = -1.0f;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mTowerDefenceSystem.getMonsterMainList().Count > 0 && tickTimerLoop(ref mCurTime, elapsedTime, mCustomParam.mInterval))
		{
			// 对怪物产生一次伤害
			int hitCount = overlapCollider(mCollider, mTempResult, MASK_MONSTER);
			for (int i = 0; i < hitCount; ++i)
			{
				CharacterMonster monster = mTowerDefenceSystem.getMonsterByCollider(mTempResult[i]);
				if (checkMonsterCanEffective(monster) && mCustomParam.mDamageRepeat || mHitList.Add(monster))
				{
					hit(monster);
				}
			}
		}
	}
	public List<Vector3> getPath() { return mPath; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		mCollider = getOrAddUnityComponent<SphereCollider>();
		mCollider.center = Vector3.zero;
		mCollider.radius = GRID_SIZE * 0.5f;
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
        this.MOVE_CURVE_EX(mPath, generatePathLength(mPath).divide(speed), mOnMoveDone);
		mCurTime = 0;
	}
	protected void onMoveDone(ComponentKeyFrame com, bool breakTrack)
	{
		if (mWillDestroy || breakTrack)
		{
			return;
		}
		explosion();
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
}