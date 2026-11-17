using UnityEngine;
using static FrameUtility;

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

// 波动塔的技能：发射"穿透型直线波",沿一条线持续飞行并碰撞沿途所有怪
// 【机制】和基类的最大差异在子弹命中后的处理——用 mWillFireBullet 回调在子弹出膛瞬间
//   把它的终点从"目标当前位置"改成"起点 + 朝向目标方向 × 飞行距离"，
//   于是子弹不再停在目标身上,而是一条固定长度、可贯穿多个怪的直线波。
// 【参数】mDistance 是配置表的飞行距离;发射时再叠加塔的 mIncreaseFlyDis(词条增益)。
// 【连发】每发子弹 interval=0.1s(refreshBulletCountInterval),让多道波依次推进而非重叠。
// 【实现链】fireAllBullet 排延迟命令 → CmdCharacterFireBullet 造弹 → onBulletFire 改写终点
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
	// 子弹发射回调：把"追踪单目标"的子弹改造成"定长穿透直线波"
	//  ① 算出起点→目标的水平方向(resetY 忽略高度差)
	//  ② 终点 = 起点 + 方向 × (配置飞行距离 + 塔的飞行距离加成)
	//  ③ setTargetPosition 固定终点后,子弹按直线飞行并持续碰撞(SkillBulletStraightLineAlwaysCollide)
	protected void onBulletFire(SkillBullet bullet)
	{
		var thisBullet = bullet as SkillBulletStraightLineAlwaysCollide;
		Vector3 dir = (thisBullet.getTarget().getPosition() - thisBullet.getStartPosition()).resetY();
		thisBullet.setTargetPosition(thisBullet.getStartPosition() + dir.setLength(mCustomParam.mDistance + mTower.getGameData().mIncreaseFlyDis));
	}
	protected override void refreshBulletCount()
	{
		refreshBulletCountInterval(MULTI_BULLET_INTERVAL);
	}
}