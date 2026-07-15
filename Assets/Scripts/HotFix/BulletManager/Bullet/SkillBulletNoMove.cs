using UnityEngine;
using static GBR;
using static StringUtility;
using static FrameUtility;

// 子弹参数
public class BulletCustomParam_NoMove : ParamCopyableT<BulletCustomParam_NoMove>
{
	public float mRange;			// 爆炸范围
	public bool mUseCharacterRange; // 是否读取角色的攻击范围作为爆炸范围
	public float mExistTime;        // 持续时间,大于0表示会在一定时间后才会销毁,小于等于0表示创建后立即销毁
	public override void registeAllParam()
	{
		registeParam((param) => { mRange = param.SToF(); });
		registeParam((param) => { mUseCharacterRange = param.SToI() > 0; });
		registeParam((param) => { mExistTime = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mRange = 0.0f;
		mUseCharacterRange = false;
		mExistTime = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_NoMove other)
	{
		mRange = other.mRange;
		mUseCharacterRange = other.mUseCharacterRange;
		mExistTime = other.mExistTime;
	}
}

// 技能的子弹,释放后就在原地爆炸
public class SkillBulletNoMove : SkillBulletT<BulletCustomParam_NoMove>
{
	protected float mCurExistTime = -1.0f;                  // 剩余的持续时间
	protected float mRealtimeRange;							// 实时的子弹爆炸范围
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
		// 对一定范围内的敌人造成伤害
		mRealtimeRange = mCustomParam.mUseCharacterRange ? mCharacterGame.getRange() : mCustomParam.mRange;
		mRealtimeRange *= mCharacterGame.getBulletExploRangeIncreasePercent(0.0f) + 1.0f;
		if (mBulletData.mSingleTarget)
		{
			hit(getNearestEffectiveMonster(mRealtimeRange));
		}
		else
		{
			using var a = new ListScope<CharacterMonster>(out var monsterList);
			getRangeEffectiveMonster(mRealtimeRange, monsterList);
			foreach (CharacterMonster monster in monsterList)
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