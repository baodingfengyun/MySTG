using UnityEngine;
using static FrameUtility;
using static MathUtility;

// 技能参数
public class SkillCustomParam_BoDong : ParamCopyableT<SkillCustomParam_BoDong>
{
	public float mDistance;     // 子弹飞行距离
	public override void registeAllParam()
	{
		registeParam((param) => { mDistance = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mDistance = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(SkillCustomParam_BoDong other)
	{
		mDistance = other.mDistance;
	}
}

// 波动塔的技能
public class TowerSkill_BoDong : TowerSkillT<SkillCustomParam_BoDong>
{
	protected BulletCallback mOnBulletFire;	// 发射子弹的回调
	protected const float MULTI_BULLET_INTERVAL = 0.1f; // 多子弹时的间隔时间
	public TowerSkill_BoDong()
	{
		mOnBulletFire = onBulletFire;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		// mOnBulletFire不重置
		// mOnBulletFire = null;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void fireAllBullet()
	{
		if (!mFirePointListInited || mBulletDataList.Count == 0)
		{
			return;
		}
		int bulletCount = mBulletDataList.Count;
		for (int i = 0; i < bulletCount; ++i)
		{
			CMD_DELAY(out CmdCharacterFireBullet cmd);
			cmd.mFirePosMap = mFirePointLocalPosition;
			cmd.mBulletData = mBulletDataList[i];
			cmd.mTarget = mFacingTarget;
			cmd.mWillFireBullet = mOnBulletFire;
			cmd.mTargetAssignID = cmd.mTarget?.getAssignID() ?? 0;
			cmd.mFireID = mFireID;
			pushDelayCommand(cmd, mTower, mFireTime[i], this);
		}
	}
	protected void onBulletFire(SkillBullet bullet)
	{
		var thisBullet = bullet as SkillBulletStraightLineAlwaysCollide;
		Vector3 dir = resetY(thisBullet.getTarget().getPosition() - thisBullet.getStartPosition());
		thisBullet.setTargetPosition(thisBullet.getStartPosition() + setLength(dir, mCustomParam.mDistance + mTower.getGameData().mIncreaseFlyDis));
	}
	protected override void refreshBulletCount()
	{
		refreshBulletCountInterval(MULTI_BULLET_INTERVAL);
	}
}