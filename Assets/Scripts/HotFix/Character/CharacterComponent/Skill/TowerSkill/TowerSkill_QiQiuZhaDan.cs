using static FrameUtility;

// 气球炸弹塔的技能：发射后让塔上的气球模型播放"装填(Reload)"动画，是表现联动最轻的一类
// 【机制】fireAllBullet 逻辑与基类一致（索敌→延迟命令发弹），唯一的差异有两点：
//   ① 覆写 fireAnimation() 为空 —— 塔本体不放通用 Fire0 攻击动画；
//   ② 每发子弹发射时（mWillFireBullet 回调）触发气球 Animator 的 "Reload" 触发器和装填表现。
// 气球 Animator 由专属实体类 CharacterTowerQiQiuZhaDan 在模型加载后抓取 P_QiQiu 节点得到，
// 这里通过 `mTower as CharacterTowerQiQiuZhaDan` 取用 —— 技能与实体类配套，是唯一需要配套实体的塔。
// 注：本塔能伤害玩家自身（气球爆炸），实际伤害效果主要由子弹形态 + Buff 决定。
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