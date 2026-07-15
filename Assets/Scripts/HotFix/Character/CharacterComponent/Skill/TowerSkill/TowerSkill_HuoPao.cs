using UnityEngine;
using static FrameUtility;
using static MathUtility;
using static FrameBaseHotFix;

// 技能参数
public class SkillCustomParam_HuoPao : ParamCopyableT<SkillCustomParam_HuoPao>
{
	public float mStartHeight;     // 子弹飞行距离
	public override void registeAllParam()
	{
		registeParam((param) => { mStartHeight = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mStartHeight = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(SkillCustomParam_HuoPao other)
	{
		mStartHeight = other.mStartHeight;
	}
}

// 火炮塔的技能
public class TowerSkill_HuoPao : TowerSkillT<SkillCustomParam_HuoPao>
{
	protected Vector3 mTargetPosition;			// 因为火炮下落时可能目标已经不存在了,所以为了位置正确,会提前记录一个目标位置
	protected int mIncreaseExplosionTimes;		// 增加的爆炸次数
	protected float mIncreaseExplosionChance;	// 多次爆炸的几率
	public override void resetProperty()
	{
		base.resetProperty();
		mTargetPosition = Vector3.zero;
		mIncreaseExplosionTimes = 0;
		mIncreaseExplosionChance = 0.0f;
	}
	public void addBulletCount(int increaseCount, float timeInterval)
	{
		for (int i = 0; i < increaseCount; ++i)
		{
			mBulletDataList.Add(mBulletDataList[0]);
			mBulletDataList.Add(mBulletDataList[1]);
			mFireTime.Add(mFireTime[mFireTime.Count - 2] + timeInterval);
			mFireTime.Add(0.0f);
		}
	}
	public void removeBulletCount(int count)
	{
		mBulletDataList.RemoveRange(mBulletDataList.Count - count * 2, count * 2);
		mFireTime.RemoveRange(mFireTime.Count - count * 2, count * 2);
	}
	public void addIncreaseExplosionTimes(int value) { mIncreaseExplosionTimes += value; }
	public void addIncreaseExplosionChance(float value) { mIncreaseExplosionChance += value; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void fireAllBullet()
	{
		if (!mFirePointListInited || mBulletDataList.Count < 2)
		{
			return;
		}
		mTargetPosition = mFacingTarget.getPosition();
		// 发射第一个子弹
		int mBulletDataListCount = mBulletDataList.Count;
		for(int i = 0; i < mBulletDataListCount; ++i)
		{
			if (isEven(i))
			{
				CMD_DELAY(out CmdCharacterFireBullet cmd);
				cmd.mFirePosMap = mFirePointLocalPosition;
				cmd.mBulletData = mBulletDataList[i];
				cmd.mTarget = mFacingTarget;
				int bulletIndex = i;
				cmd.mWillFireBullet = (SkillBullet bullet)=>
				{
					(bullet as SkillBulletStraightLine).setTargetPosition(bullet.getStartPosition() + new Vector3(0.0f, mCustomParam.mStartHeight));
					bullet.setExplosionCallback((SkillBullet bullet) =>
					{
						if (bulletIndex + 1 >= mBulletDataList.Count || bulletIndex + 1 >= mFireTime.Count)
						{
							return;
						}
						// 发射第二个子弹,这发子弹没有间隔,在第一个子弹爆炸时就会发出
						CMD_DELAY(out CmdCharacterFireBullet cmd);
						cmd.mFirePosMap = mFirePointLocalPosition;
						cmd.mBulletData = mBulletDataList[bulletIndex + 1];
						cmd.mTarget = bullet.getTarget();
						cmd.mWillFireBullet = onBulletFire1;
						cmd.mTargetAssignID = cmd.mTarget?.getAssignID() ?? 0;
						cmd.mFireID = mFireID;
						pushDelayCommand(cmd, mTower, 0, this);
					});
				};
				cmd.mTargetAssignID = cmd.mTarget?.getAssignID() ?? 0;
				cmd.mFireID = mFireID;
				pushDelayCommand(cmd, mTower, mFireTime[i], this);
			}
		}
	}
	protected void onBulletFire1(SkillBullet bullet)
	{
		var secondBullet = bullet as SkillBulletStraightLine;
		CharacterGame target = bullet.getTarget();
		if (target.isValid())
		{
			secondBullet.setStartPosition(target.getPosition() + new Vector3(0.0f, mCustomParam.mStartHeight));
			secondBullet.setTargetPosition(target.getPosition());
		}
		else
		{
			secondBullet.setStartPosition(mTargetPosition + new Vector3(0.0f, mCustomParam.mStartHeight));
			secondBullet.setTargetPosition(mTargetPosition);
		}
		secondBullet.setIncreaseExplosionTimes(mIncreaseExplosionTimes);
		secondBullet.setIncreaseExplosionChance(mIncreaseExplosionChance);
		bullet.setExplosionCallback((SkillBullet explosion) =>
		{
			using var a = new ClassScope<EventBulletExplosionHuoPao>(out var param);
			param.mBullet = explosion;
			mEventSystem.pushEvent(param, explosion.getCharacter().getGUID());
		});
	}
}