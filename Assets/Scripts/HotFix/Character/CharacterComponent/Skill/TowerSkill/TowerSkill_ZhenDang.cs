using UnityEngine;
using System.Collections.Generic;
using static FrameUtility;
using static MathUtility;
using static GBR;

// 技能参数
public class SkillCustomParam_ZhenDang : ParamCopyableT<SkillCustomParam_ZhenDang>
{
	public float mInterval;                          // 生成间隔,已废弃,直接使用技能CD
	public int mMaxBulletCount;                      // 最大生成子弹数量
	public override void registeAllParam()
	{
		registeParam((param) => { mInterval = param.SToF(); });
		registeParam((param) => { mMaxBulletCount = param.SToI(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mInterval = 0.0f;
		mMaxBulletCount = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(SkillCustomParam_ZhenDang other)
	{
		mInterval = other.mInterval;
		mMaxBulletCount = other.mMaxBulletCount;
	}
}

// 震荡塔的技能
public class TowerSkill_ZhenDang : TowerSkillT<SkillCustomParam_ZhenDang>
{
	protected List<SkillBulletZhenDang> mBulletList = new();// 已经生成的子弹列表
	protected BulletCallback mOnBulletExplosion;			// 子弹爆炸的回调
	protected float mCurTimer;								// 当前计时
	protected float mRotatedAngle;							// 整体已经旋转过的角度
	protected int mNotDestroyBulletOnHit;					// 子弹碰撞后不消失
	protected const float ROTATE_RADIUS = 2.0f;				// 旋转半径
	public TowerSkill_ZhenDang()
	{
		mOnBulletExplosion = onBulletExplosion;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mOnBulletExplosion不重置
		// mOnBulletExplosion = null;
		mBulletList.Clear();
		mCurTimer = 0.0f;
		mRotatedAngle = 0.0f;
		mNotDestroyBulletOnHit = 0;
	}
	public override void destroy()
	{
		base.destroy();
		foreach (SkillBulletZhenDang bullet in mBulletList)
		{
			mBulletManager.destroyBullet(bullet);
		}
		mBulletList.Clear();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mTower.getGridIndex() < 0)
		{
			return;
		}
		float speed = mBulletDataList[0].mSpeed * (mTower.getGameData().mBulletSpeedIncrease + 1.0f);
		mRotatedAngle = (mRotatedAngle + elapsedTime * speed).adjustAngle180();

		// 每隔一定时间生成一个子弹
		if (mTowerDefenceSystem.getMonsterMainList().Count > 0 && 
			mBulletList.Count < mCustomParam.mMaxBulletCount && 
			tickTimerLoop(ref mCurTimer, elapsedTime, mTower.getTowerData().getFinalCD(mSkillData.mCD)))
		{
			createBullet();
		}

		// 更新所有子弹的位置
		Vector3 center = mTower.getPosition() + new Vector3(0.0f, 1.0f, 0.0f);
		foreach (SkillBulletZhenDang bullet in mBulletList)
		{
			// 只有子弹特效加载完成后才会计算位置,未加载完成时初始位置还没有生效,会导致位置更新错误
			// 而且为了避免由于子弹未加载完而未更新位置导致后续的位置计算错误,所以每次更新位置时不再依赖当前位置插值,而是直接设置位置
			if (bullet.getFlyEffect() == null)
			{
				continue;
			}
			bullet.setPosition(center + (bullet.getBaseAngle() + mRotatedAngle.toRadian()).getVectorFromAngle() * ROTATE_RADIUS);
		}
	}
	// 此处可能是额外的子弹增加,不是自动增加子弹,所以可以设置额外的子弹最多可以增加到多少个
	public void createBullet()
	{
		// 需要调整所有子弹的基准角度,使之能够排列均匀
		int emptyIndex = -1;
		for (int i = 0; i < mCustomParam.mMaxBulletCount; ++i)
		{
			if (!isBulletIndexExist(i))
			{
				emptyIndex = i;
				break;
			}
		}
		if (emptyIndex == -1)
		{
			return;
		}
		Vector3 center = mTower.getPosition() + new Vector3(0.0f, 1.0f, 0.0f);
		var bullet = mBulletList.add(mBulletManager.createBullet(mBulletDataList[0]) as SkillBulletZhenDang);
		bullet.setCharacter(mTower);
		bullet.setFaceForward(false);
		bullet.setBaseAngle(TWO_PI_RADIAN.divide(mCustomParam.mMaxBulletCount) * emptyIndex);
		bullet.setIndex(emptyIndex);
		bullet.setStartPosition(center + (bullet.getBaseAngle() + mRotatedAngle.toRadian()).getVectorFromAngle() * ROTATE_RADIUS);
		bullet.setExplosionCallback(mOnBulletExplosion);
		bullet.setNotDestroyBulletOnHit(mNotDestroyBulletOnHit > 0);
		bullet.fire();
	}
	// 技能释放里面什么也不做,自定义释放子弹
	public override void fire() {}
	public void setMaxBulletCount(int count) 
	{
		if (mCustomParam.mMaxBulletCount == count)
		{
			return;
		}
		mCustomParam.mMaxBulletCount = count;
		// 数量改变时重新计算所有子弹的角度
		int curCount = mBulletList.Count;
		for (int i = 0; i < curCount; ++i)
		{
			SkillBulletZhenDang bullet = mBulletList[i];
			bullet.setIndex(i);
			bullet.setBaseAngle(TWO_PI_RADIAN.divide(mCustomParam.mMaxBulletCount) * i);
		}
	}
	public int getMaxBulletCount() { return mCustomParam.mMaxBulletCount; }
	public int getCurBulletCount() { return mBulletList.Count; }
	public void addNotDestroyBulletOnHit()
	{
		if (mNotDestroyBulletOnHit++ == 0)
		{
			foreach (SkillBulletZhenDang bullet in mBulletList)
			{
				bullet.setNotDestroyBulletOnHit(true);
			}
		}
	}
	public void removeNotDestroyBulletOnHit() 
	{
		if (--mNotDestroyBulletOnHit == 0)
		{
			foreach (SkillBulletZhenDang bullet in mBulletList)
			{
				bullet.setNotDestroyBulletOnHit(false);
			}
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBulletExplosion(SkillBullet bullet)
	{
		if (mNotDestroyBulletOnHit == 0)
		{
			mBulletList.Remove(bullet as SkillBulletZhenDang);
		}
	}
	protected bool isBulletIndexExist(int index)
	{
		foreach (SkillBulletZhenDang bullet in mBulletList)
		{
			if (bullet.getIndex() == index)
			{
				return true;
			}
		}
		return false;
	}
}