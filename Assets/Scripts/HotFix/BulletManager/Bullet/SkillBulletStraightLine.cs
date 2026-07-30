using UnityEngine;
using static GBR;
using static MathUtility;

// 子弹参数
public class BulletCustomParam_StraightLine : ParamCopyableT<BulletCustomParam_StraightLine>
{
	public float mRange;    // 命中范围
	public override void registeAllParam()
	{
		registeParam((param) => { mRange = param.SToF(); });
	}
	//------------------------------------------------------------------------------------------------------------------------------
	public override void resetProperty()
	{
		base.resetProperty();
		mRange = 0.0f;
	}
	protected override void initFromCopyInternal(BulletCustomParam_StraightLine other)
	{
		mRange = other.mRange;
	}
}

// 技能的子弹,直线运行
public class SkillBulletStraightLine : SkillBulletT<BulletCustomParam_StraightLine>
{
	protected Vector3 mTargetPosition;								// 目标位置
	protected KeyFrameCallback mOnMoveDone;							// 移动完成的回调
	protected float mRealtimeRange;									// 实时的子弹爆炸范围
	protected int mIncreaseExplosionTimes;							// 增加的爆炸次数
	protected float mIncreaseExplosionChance;						// 多次爆炸的几率
	public SkillBulletStraightLine()
	{
		mOnMoveDone = onMoveDone;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mOnMoveDone不重置
		// mOnMoveDone = null;
		mTargetPosition = Vector3.zero;
		mRealtimeRange = 0.0f;
		mIncreaseExplosionTimes = 0;
		mIncreaseExplosionChance = 0.0f;
	}
	public void setTargetPosition(Vector3 targetPos) { mTargetPosition = targetPos; }
	public override float getRealtimeRange() { return mRealtimeRange; }
	public void setIncreaseExplosionTimes(int value) { mIncreaseExplosionTimes = value; }
	public void setIncreaseExplosionChance(float value) { mIncreaseExplosionChance = value; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		float length = (getPosition() - mTargetPosition).getLength();
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		this.MOVE_EX(getPosition(), mTargetPosition, length.divide(speed), mOnMoveDone);
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
		int increaseExplotionTimes = 1;
		if(randomHit(mIncreaseExplosionChance))
		{
			increaseExplotionTimes += mIncreaseExplosionTimes;
		}
		// 对一定范围内的敌人造成伤害
		using var a = new ListScope<CharacterMonster>(out var monsterList);
		mRealtimeRange = mCustomParam.mRange * (mCharacterGame.getBulletExploRangeIncreasePercent(getFlyDistance()) + 1.0f);
		getRangeEffectiveMonster(mRealtimeRange, monsterList);
		foreach (CharacterMonster monster in monsterList)
		{
			for (int j = 0; j < increaseExplotionTimes; ++j)
			{
				hit(monster);
			}
		}
		// 播放爆炸特效
		explosion();
		// 销毁子弹
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
}