using static FrameUtility;

// 气球炸弹塔的技能
public class TowerSkill_QiQiuZhaDan : TowerSkill
{
	protected BulletCallback mOnBulletFire;
	public TowerSkill_QiQiuZhaDan()
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
	protected override void fireAnimation()
	{
		// 气球炸弹塔没有攻击动画
	}
	protected override void fireAllBullet()
	{
		if (!mFirePointListInited || mBulletDataList.Count == 0)
		{
			return;
		}
		// 发射子弹
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
		if (mTower is CharacterTowerQiQiuZhaDan qiqiuta && qiqiuta.getBalloonAnimator() != null)
		{
			qiqiuta.getBalloonAnimator().SetTrigger("Reload");
		}
	}
}