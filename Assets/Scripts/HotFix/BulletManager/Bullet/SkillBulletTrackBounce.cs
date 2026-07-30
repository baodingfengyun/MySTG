using UnityEngine;
using System.Collections.Generic;
using static GameUtilityHotFix;
using static GBR;

// 子弹参数
public class BulletCustomParam_TrackBounce : ParamCopyableT<BulletCustomParam_TrackBounce>
{
	public int mBounceTimesMax;								// 最大弹跳次数
	public float mBounceRange;								// 弹跳范围
	public float mBouncePercent;							// 弹跳的伤害衰减
	public override void registeAllParam()
	{
		registeParam((param) => { mBounceTimesMax = param.SToI(); });
		registeParam((param) => { mBounceRange = param.SToF(); });
		registeParam((param) => { mBouncePercent = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBounceTimesMax = 0;
		mBounceRange = 0.0f;
		mBouncePercent = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_TrackBounce other)
	{
		mBounceTimesMax = other.mBounceTimesMax;
		mBounceRange = other.mBounceRange;
		mBouncePercent = other.mBouncePercent;
	}
}

// 技能的子弹,追踪并且弹射周围目标
public class SkillBulletTrackBounce : SkillBulletT<BulletCustomParam_TrackBounce>
{
	protected BoolCallback mOnTrackDone;						// 移动完成的回调
	protected KeyFrameCallback mOnMoveDone;						// 没有目标时移动完成的回调
	protected List<long> mBouncedMonsters = new();				// 已经弹跳过的怪物
	protected int mBounceTimesMax;								// 最大弹跳次数
	protected int mBounceRemain;								// 剩余弹跳次数
	protected float mBounceDamagePercent;						// 弹跳的伤害衰减
	public SkillBulletTrackBounce()
	{
		mOnTrackDone = onTrackDone;
		mOnMoveDone = onMoveDone;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mOnTrackDone,mOnMoveDone不重置
		// mOnTrackDone = null;
		// mOnMoveDone = null;
		mBouncedMonsters.Clear();
		mBounceTimesMax = 0;
		mBounceRemain = 0;
		mBounceDamagePercent = 0.0f;
	}
	public void increaseBounceTimesMax(int value)
	{
		mBounceTimesMax = mCustomParam.mBounceTimesMax + value;
		mBounceRemain = mBounceTimesMax;
	}
	public void increaseBounceDamagePercent(float value)
	{
		mBounceDamagePercent = mCustomParam.mBouncePercent + value;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	public override void initData(EDSkillBullet data, ParamCopyable paramTemplate)
	{
		base.initData(data, paramTemplate);
		mBounceTimesMax = mCustomParam.mBounceTimesMax;
		mBounceDamagePercent = mCustomParam.mBouncePercent;
		mBounceRemain = mBounceTimesMax;
	}
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
		if (mTarget != null)
		{
			mBouncedMonsters.Add(mTarget.getGUID());
            this.TRACK_TARGET(mTarget, speed, mHitPointOffset, mOnTrackDone);
		}
		else
		{
			Vector3 targetPos = mCharacterGame.getPosition() + mCharacterGame.getForward() * 6.0f;
            this.MOVE_EX(mStartPosition, targetPos, (mStartPosition - targetPos).resetY().getLength().divide(speed), mOnMoveDone);
		}
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
		explosion();
		mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
	}
	protected void onTrackDone(bool breakTrack)
	{
		if (mWillDestroy)
		{
			return;
		}
		if (breakTrack)
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
			return;
		}
		// 追踪结束后需要使用追踪组件的目标,可以知道目标是否已经不可访问了
		// 如果直接访问当前子弹记录的目标,可能此目标已经被销毁,访问会报错
		if (mBounceTimesMax > 0)
		{
			int bouncedTimes = mBounceTimesMax - mBounceRemain;
			setDamageCallback((CharacterGame target, CharacterGame attacker, SkillBullet bullet, out bool isHit, out bool isCritical, out HP_DELTA deltaType) =>
			{
				int damage = generateDamage(target, attacker, bullet, out isHit, out isCritical, out deltaType);
				return (damage * mBounceDamagePercent.pow(bouncedTimes)).round().clampMin(1);
			});
		}
		hit(getComponent<ComponentTrackTarget>().getTrackTarget() as CharacterMonster);
		explosion();
		if(mBounceRemain > 0)
		{
			CharacterMonster findMonster = null;
			using var a = new ListScope<CharacterMonster>(out var monsters);
			getRangeEffectiveMonster(mCustomParam.mBounceRange < 0 ? mCharacterGame.getRange() : mCustomParam.mBounceRange, monsters);
			monsters.Sort((x, y) => (getPosition() - x.getPosition()).getSquaredLength().CompareTo((getPosition() - y.getPosition()).getSquaredLength()));
			int monstersCount = monsters.Count;
			if(monstersCount > 0)
			{
				// 寻找最近的没被弹跳过的
				foreach (CharacterMonster monster in monsters)
				{
					if (!mBouncedMonsters.Contains(monster.getGUID()))
					{
						findMonster = monster;
						break;
					}
				}
				// 说明距离内的怪物已经都被弹跳过了
				if (findMonster == null)
				{
					// 周围已经没有 没弹跳过的 怪物了，清理了重新找一次
					// 为了避免范围内有多个目标时，因为清理了弹跳过的目标，还是选择了最后弹射的那一个，保留最后弹跳过的一个
					long lastOne = mBouncedMonsters[^1];
					mBouncedMonsters.Clear();
					// 如果范围内只有一个怪物，而且又正好是最后弹跳过的怪物，那就只能再弹跳一次他了
					if (!(monsters.Count == 1 && monsters[0].getGUID() == lastOne))
					{
						mBouncedMonsters.Add(lastOne);
					}
					// 再次寻找没有弹跳过且最近的
					foreach (CharacterMonster monster in monsters)
					{
						if (!mBouncedMonsters.Contains(monster.getGUID()))
						{
							findMonster = monster;
							break;
						}
					}
				}
			}
			if (findMonster == null)
			{
				mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
				return;
			}
			--mBounceRemain;
			float speed = mBulletData.mSpeed * (mCharacterGame.getGameData().mBulletSpeedIncrease + 1.0f);
			setTarget(findMonster);
			mBouncedMonsters.Add(findMonster.getGUID());
			this.TRACK_TARGET(mTarget, speed, mHitPointOffset, mOnTrackDone);
		}
		else
		{
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
		}
	}
}