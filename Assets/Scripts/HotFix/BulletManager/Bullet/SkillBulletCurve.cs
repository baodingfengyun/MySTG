using UnityEngine;
using System.Collections.Generic;
using static GBR;
using static MathUtility;

// 子弹参数
public class BulletCustomParam_Curve : ParamCopyableT<BulletCustomParam_Curve>
{
	public float mRange;    // 命中范围
	public override void registeAllParam()
	{
		registeParam((param) => { mRange = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mRange = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_Curve other)
	{
		mRange = other.mRange;
	}
}

// 技能的子弹,按折线移动,移动结束后产生一次伤害
public class SkillBulletCurve : SkillBulletT<BulletCustomParam_Curve>
{
	protected List<Vector3> mPath = new();					// 移动路线
	protected KeyFrameCallback mOnMoveDone;					// 移动完成的回调
	protected float mRealtimeRange;							// 实时的子弹爆炸范围
	public SkillBulletCurve()
	{
		mOnMoveDone = onMoveDone;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mPath.Clear();
		// mOnMoveDone不重置
		// mOnMoveDone = null;
		mRealtimeRange = 0.0f;
	}
	public List<Vector3> getPath() { return mPath; }
	public override float getRealtimeRange() { return mRealtimeRange; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
        this.MOVE_CURVE_EX(mPath, generatePathLength(mPath).divide(speed), mOnMoveDone);
	}
	protected void onMoveDone(ComponentKeyFrame com, bool breakTrack)
	{
		if (mWillDestroy || breakTrack)
		{
			return;
		}

		// 对一定范围内的敌人造成伤害
		using var a = new ListScope<CharacterMonster>(out var monsterList);
		mRealtimeRange = mCustomParam.mRange * (mCharacterGame.getBulletExploRangeIncreasePercent(getFlyDistance()) + 1.0f);
		getRangeEffectiveMonster(mRealtimeRange, monsterList);
		foreach (CharacterMonster monster in monsterList)
		{
			hit(monster);
		}

		explosion();
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
}