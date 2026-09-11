using System.Collections.Generic;
using UnityEngine;
using static GBR;
using static FrameBaseHotFix;

// 角色发射子弹
public class CmdCharacterFireBullet : Command
{
	public Dictionary<string, Transform> mFirePosMap;	// 发射位置
	public EDSkillBullet mBulletData;					// 子弹数据
	public CharacterGame mTarget;						// 目标
	public BulletCallback mWillFireBullet;				// 子弹发射时
	public HitCallback mHitCallback;					// 子弹击中时
	public DamageCallback mDamageCallback;				// 产生伤害时
	public long mTargetAssignID;
	public long mFireID;
	public override void resetProperty()
	{
		base.resetProperty();
		mFirePosMap = null;
		mBulletData = null;
		mTarget = null;
		mWillFireBullet = null;
		mHitCallback = null;
		mDamageCallback = null;
		mTargetAssignID = 0;
		mFireID = 0;
	}
	public override void execute()
	{
		var character = mReceiver as CharacterGame;
		if (mTarget != null && mTargetAssignID != mTarget.getAssignID())
		{
			return;
		}
		SkillBullet bullet = mBulletManager.createBullet(mBulletData);
		bullet.setCharacter(character);
		bullet.setStartPosition(bullet.generateStartPos(mTarget, mFirePosMap));
		bullet.setHitCallback(mHitCallback);
		bullet.setDamageCallback(mDamageCallback);
		bullet.setTarget(mTarget);
		bullet.setFireID(mFireID);
		mWillFireBullet?.Invoke(bullet);

		using var a = new ClassScope<EventBulletWillFire>(out var param0);
		param0.mBullet = bullet;
		// 子弹发射事件
		mEventSystem.pushEvent(param0, character.getGUID());

		// 子弹发射
		bullet.fire();
		if(character is CharacterTower tower)
		{
			TowerSkill skill = tower.getComSkill().getCurSkill();
			skill.setWaveBulletCount(skill.getWaveBulletCount() + 1);

			using var b = new ClassScope<EventBulletWaveCountChanged>(out var param1);
			param1.mCount = skill.getWaveBulletCount();
			mEventSystem.pushEvent(param1, character.getGUID());
		}
	}
}