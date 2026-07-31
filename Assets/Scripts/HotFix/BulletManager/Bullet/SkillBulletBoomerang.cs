using UnityEngine;
using System.Collections.Generic;
using static UnityUtility;
using static MathUtility;
using static FrameUtility;
using static GDR;
using static GBR;

// 子弹参数
public class BulletCustomParam_Boomerang : ParamCopyableT<BulletCustomParam_Boomerang>
{
	public float mArcAngle;		// 弧线对应的角度的一半,角度制,也是弧线切线与发射目标点方向的夹角
	public Vector3 mSize;       // 子弹大小
	public override void registeAllParam()
	{
		registeParam((param) => { mArcAngle = param.SToF(); });
		registeParam((param) => { mSize = param.SToV3(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mArcAngle = 0.0f;
		mSize = Vector3.zero;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_Boomerang other)
	{
		mArcAngle = other.mArcAngle;
		mSize = other.mSize;
	}
}

// 回旋镖,会对目标产生两次伤害,轨迹是两段弧形,弧线的圆心过起点和终点的中线
public class SkillBulletBoomerang : SkillBulletT<BulletCustomParam_Boomerang>
{
	protected KeyFrameCallback mMoveDone;						// 移动完成的回调
	protected HashSet<CharacterGame> mHitList = new();			// 已经击中的列表,避免重复命中
	protected List<Vector3> mFlyPath = new();					// 预先计算出的路线,每0.3米计算一个点
	protected Collider[] mTempResult;							// 临时的存储碰撞结果的数组
	protected Collider mCollider;                               // 子弹碰撞体
	protected float mTickTimer = -1.0f;                         // 碰撞计时
	protected const float INTERVAL = 0.1f;                      // 每0.1秒检测一次碰撞
	protected bool mDirection = true;							// true表示往目标飞,false表示往回飞
	public SkillBulletBoomerang()
	{
		mMoveDone = onMoveDone;
		mTempResult = new Collider[8];
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mMoveDone不重置
		// mMoveDone = null;
		mHitList.Clear();
		mFlyPath.Clear();
        mTempResult.setAllValue(null);
		mCollider = null;
		mTickTimer = -1.0f;
		mDirection = true;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mWillDestroy)
		{
			return;
		}
		
		if (mTowerDefenceSystem.getMonsterMainList().Count > 0 && tickTimerLoop(ref mTickTimer, elapsedTime, INTERVAL))
		{
			int hitCount = overlapCollider(mCollider, mTempResult, MASK_MONSTER);
			CharacterMonster monster = null;
			// 找到第一个没有死亡的怪物
			for (int i = 0; i < hitCount; ++i)
			{
				CharacterMonster tempMonster = mTowerDefenceSystem.getMonsterByCollider(mTempResult[i]);
				if (tempMonster != null && checkMonsterCanEffective(tempMonster) && tempMonster.getHP() > 0)
				{
					monster = tempMonster;
					break;
				}
			}
			if (monster != null && mHitList.Add(monster))
			{
				hit(monster);
			}
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		mCollider = tryGetUnityComponent<Collider>();
		if (mCollider == null)
		{
			var collider = getOrAddUnityComponent<BoxCollider>();
			collider.center = Vector3.zero;
			collider.size = mCustomParam.mSize;
			mCollider = collider;
		}
		mDirection = true;
		Vector3 curPos = getPosition();
		Vector3 dest = curPos + (mTarget.getPosition() - curPos).setLength(mCharacterGame.getRange());
		generateFlyPath(curPos, dest.replaceY(curPos.y), out float pathLength);
        this.MOVE_CURVE_EX(KEY_CURVE.CUBIC_OUT, mFlyPath, pathLength.divide(mBulletData.mSpeed), null, mMoveDone);
		mTickTimer = 0.0f;
	}
	protected void onMoveDone(ComponentKeyFrame com, bool isBreak)
	{
		if(mWillDestroy)
		{
			return;
		}
		if (isBreak)
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
			return;
		}
		// 再往回飞
		if (mDirection)
		{
			mHitList.Clear();
			mDirection = false;
			generateFlyPath(getPosition(), mStartPosition.replaceY(getPosition().y), out float pathLength);
			this.MOVE_CURVE_EX(KEY_CURVE.CUBIC_IN, mFlyPath, pathLength.divide(mBulletData.mSpeed), null, mMoveDone);
		}
		else
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
		}
	}
	protected void generateFlyPath(Vector3 start, Vector3 dest, out float arcLength)
	{
		mFlyPath.Clear();
		if (mCustomParam.mArcAngle.isZero())
		{
			arcLength = 0.0f;
			logWarning("回旋镖轨迹为空");
			return;
		}
		float angleRadian = (mCustomParam.mArcAngle.clamp(0.0f, 90.0f) * 2.0f).toRadian();
		// 计算圆心
		generatePerpendicular(new(start.x, start.z), new(dest.x, dest.z), out Vector2 otherPoint);
		// 圆心到起点终点连线的垂线的方向,方向指向圆心
		Vector3 dir = (new Vector3(otherPoint.x, dest.y, otherPoint.y) - dest).normalize();
		// 弧线的圆心坐标
		float length = ((dest - start).getLength() * 0.5f).divide((angleRadian * 0.5f).tan());
		Vector3 center = (dest + start) * 0.5f + dir * length;
		// 起点到圆心的连线向量
		Vector3 startEdge = start - center;
		float radius = startEdge.getLength();
		// 整条轨迹的弧线长度
		arcLength = angleRadian * radius;
		// 每0.6米长度的弧度计算一个点
		float minArcAngle = 0.6f.divide(radius);
		int segmentCount = (int)angleRadian.divide(minArcAngle);
		mFlyPath.Add(start);
		for (int i = 0; i < segmentCount; ++i)
		{
			mFlyPath.Add(center + startEdge.rotate(minArcAngle * (i + 1)));
		}
		mFlyPath.Add(dest);
	}
}