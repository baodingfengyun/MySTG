using static FrameUtility;

// 飞镖发射器的技能：左右两支飞镖交替发射,是"双手武器"式的表现
// 【机制】用 mLeftFire 布尔在左右之间切换：
//   ① 每次只发一支(fireIndex = 0 或 1),发射后翻转 mLeftFire；
//   ② fireAnimation 同步播放 Fire0/Fire1 两个动画,视觉上左右手轮流出手。
// 【复用】弹道走 TowerSkill_Bounce 基类的"追踪+弹跳",发射回调注入弹跳加成；
//   与防空飞弹的区别是这里只打 mFacingTarget 单目标,不做多目标分配。
public class TowerSkill_FeiBiaoFaSheQi : TowerSkill_Bounce
{
	protected bool mLeftFire = true;
	public override void resetProperty()
	{
		base.resetProperty();
		mLeftFire = true;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	// 交替播放左右手攻击动画：mLeftFire=true 播 Fire0,false 播 Fire1,然后翻转状态
	protected override void fireAnimation()
	{
		if (mTower.getAnimator() != null)
		{
			mTower.getAnimator().SetTrigger(mLeftFire ? "Fire0" : "Fire1");
		}
		mLeftFire = !mLeftFire;
	}
	protected override void fireAllBullet()
	{
		if (!mFirePointListInited || mBulletDataList.Count == 0)
		{
			return;
		}
		int fireIndex = mLeftFire ? 0 : 1;
		CMD_DELAY(out CmdCharacterFireBullet cmd);
		cmd.mFirePosMap = mFirePointLocalPosition;
		cmd.mBulletData = mBulletDataList[fireIndex];
		cmd.mTarget = mFacingTarget;
		cmd.mTargetAssignID = cmd.mTarget?.getAssignID() ?? 0;
		cmd.mFireID = mFireID;
		cmd.mWillFireBullet = (bullet) =>
		{
			if (bullet is SkillBulletTrackBounce trackBounce)
			{
				bounceWillFire(trackBounce);
			}
		};
		pushDelayCommand(cmd, mTower, mFireTime[fireIndex], this);
	}
}