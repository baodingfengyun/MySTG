using UnityEngine;
using static UnityUtility;
using static FrameUtility;
using static GBR;
using static GDR;

// 子弹参数
public class BulletCustomParam_RotateAround : ParamCopyableT<BulletCustomParam_RotateAround>
{
	public float mRange;		// 命中范围
	public Vector3 mSize;       // 子弹大小
	public override void registeAllParam()
	{
		registeParam((param) => { mRange = param.SToF(); });
		registeParam((param) => { mSize = param.SToV3(); });
	}
	//------------------------------------------------------------------------------------------------------------------------------
	public override void resetProperty()
	{
		base.resetProperty();
		mRange = 0.0f;
		mSize = Vector3.zero;
	}
	protected override void initFromCopyInternal(BulletCustomParam_RotateAround other)
	{
		mRange = other.mRange;
		mSize = other.mSize;
	}
}

// 技能的子弹,绕一个点一直水平旋转,会一直检测碰撞,碰到敌人后就销毁
public class SkillBulletRotateAround : SkillBulletT<BulletCustomParam_RotateAround>
{
	protected Collider[] mTempResult;								// 临时对象
	protected Collider mCollider;									// 子弹碰撞体
	protected Vector3 mRotateCenter;								// 旋转圆心
	protected float mTickTimer = -1.0f;								// 碰撞计时
	protected const float INTERVAL = 0.1f;							// 每0.1秒检测一次碰撞
	protected float mRealtimeRange;									// 实时的子弹爆炸范围
	public SkillBulletRotateAround()
	{
		mTempResult = new Collider[8];
	}
	public override void resetProperty()
	{
		base.resetProperty();
        // mOnTrackDone不重置
        // mOnTrackDone = null;
        mTempResult.setAllValue(null);
		mCollider = null;
		mRotateCenter = Vector3.zero;
		mTickTimer = -1.0f;
		mRealtimeRange = 0.0f;
	}
	public void setRotateCenter(Vector3 center) { mRotateCenter = center; }
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mWillDestroy)
		{
			return;
		}

		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		rotateAround(mRotateCenter, Vector3.up, elapsedTime * speed);
		
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
				// 对一定范围内的敌人造成伤害
				using var a = new ListScope<CharacterMonster>(out var monsterList);
				mRealtimeRange = mCustomParam.mRange * (mCharacterGame.getBulletExploRangeIncreasePercent(getFlyDistance()) + 1.0f);
				getRangeEffectiveMonster(mRealtimeRange, monsterList);
				foreach (CharacterMonster item in monsterList)
				{
					hit(item);
				}
				// 碰到有效怪物就爆炸,销毁子弹
				explosion();
				mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
			}
		}
	}
	public override float getRealtimeRange() { return mRealtimeRange; }
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
		mTickTimer = 0.0f;
	}
}