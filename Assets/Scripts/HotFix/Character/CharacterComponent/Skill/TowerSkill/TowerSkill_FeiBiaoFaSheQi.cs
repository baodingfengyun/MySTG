using static FrameUtility;

// 飞镖发射器的技能
public class TowerSkill_FeiBiaoFaSheQi : TowerSkill_Bounce
{
	protected bool mLeftFire = true;
	public override void resetProperty()
	{
		base.resetProperty();
		mLeftFire = true;
	}
	//------------------------------------------------------------------------------------------------------------------------------
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