using UnityEngine;
using System.Collections.Generic;
using static UnityUtility;
using static FrameUtility;
using static GBR;
using static GDR;

// 子弹参数
public class BulletCustomParam_StraightLineAlwaysCollide : ParamCopyableT<BulletCustomParam_StraightLineAlwaysCollide>
{
	public bool mCheckMonsterOnly;      // 是否只判断有没有碰到怪物,不考虑障碍
	public bool mExplosionWhenHit;      // 是否在第一次命中后就爆炸
	public bool mDamageRepeat;			// 是否可以对一个目标重复产生伤害
	public int mPenerating;				// 穿透力
	public override void registeAllParam()
	{
		registeParam((param) => { mCheckMonsterOnly = param.SToI() > 0; });
		registeParam((param) => { mExplosionWhenHit = param.SToI() > 0; });
		registeParam((param) => { mDamageRepeat = param.SToI() > 0; });
		registeParam((param) => { mPenerating = param.SToI(); });
	}
	//------------------------------------------------------------------------------------------------------------------------------
	public override void resetProperty()
	{
		base.resetProperty();
		mCheckMonsterOnly = false;
		mExplosionWhenHit = false;
		mDamageRepeat = false;
		mPenerating = 0;
	}
	protected override void initFromCopyInternal(BulletCustomParam_StraightLineAlwaysCollide other)
	{
		mCheckMonsterOnly = other.mCheckMonsterOnly;
		mExplosionWhenHit = other.mExplosionWhenHit;
		mDamageRepeat = other.mDamageRepeat;
		mPenerating = other.mPenerating;
	}
}

// 技能的子弹,直线运行,飞行中会一直检测是否有碰到物体,碰到后就销毁
public class SkillBulletStraightLineAlwaysCollide : SkillBulletT<BulletCustomParam_StraightLineAlwaysCollide>
{
	protected HashSet<CharacterGame> mHitList = new();							// 已经命中过的敌人列表
	protected KeyFrameCallback mOnMoveDone;                                     // 移动完成时的回调
	protected Vector3 mTargetPosition;											// 目标位置
	protected Collider mCollider;												// 子弹碰撞体
	protected Collider[] mTempResult;											// 临时对象
	protected float mTickTimer = -1.0f;											// 碰撞计时
	protected const float INTERVAL = 0.1f;										// 每0.1秒检测一次碰撞
	public SkillBulletStraightLineAlwaysCollide()
	{
		mTempResult = new Collider[8];
		mOnMoveDone = onMoveDone;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mOnMoveDone不重置
		// mOnMoveDone = null;
		mHitList.Clear();
		mTargetPosition = Vector3.zero;
		mCollider = null;
		mTempResult.setAllValue(null);
		mTickTimer = -1.0f;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		bool hitTarget = false;
		if (mCustomParam.mCheckMonsterOnly)
		{
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
				if (monster != null)
				{
					if (mCustomParam.mDamageRepeat || mHitList.Add(monster))
					{
						mCustomParam.mPenerating -= monster.getMonsterData().mTableData.mAntiPenetrating;
						hit(monster);
					}
					// 碰到有效怪物就爆炸,销毁子弹
					hitTarget = true;
				}
			}
		}
		else
		{
			if (tickTimerLoop(ref mTickTimer, elapsedTime, INTERVAL) && 
				overlapCollider(mCollider, mTempResult, MASK_TOWER | MASK_MONSTER | MASK_BLOCK) > 0)
			{
				// 第一个碰到的如果是没有死亡的怪物,则会产生伤害
				CharacterMonster monster = mTowerDefenceSystem.getMonsterByCollider(mTempResult[0]);
				if (monster != null &&
					checkMonsterCanEffective(monster) && 
					monster.getHP() > 0 &&
					(mCustomParam.mDamageRepeat || mHitList.Add(monster)))
				{
					hit(monster);
				}
				// 碰到有效怪物就爆炸,销毁子弹
				hitTarget = true;
			}
		}
		if (mCustomParam.mExplosionWhenHit && hitTarget || mCustomParam.mPenerating <= 0)
		{
			explosion();
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
		}
	}
	public void setTargetPosition(Vector3 targetPos) { mTargetPosition = targetPos; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		mCollider = tryGetUnityComponent<Collider>();
		if (mCollider == null)
		{
			var collider = getOrAddUnityComponent<BoxCollider>();
			collider.center = Vector3.zero;
			collider.size = Vector3.one;
			mCollider = collider;
		}
		float length = (getPosition() - mTargetPosition).getLength();
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
        this.MOVE_EX(getPosition(), mTargetPosition, length.divide(speed), mOnMoveDone);
		mTickTimer = 0.0f;
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
		// 播放爆炸特效
		explosion();
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
}