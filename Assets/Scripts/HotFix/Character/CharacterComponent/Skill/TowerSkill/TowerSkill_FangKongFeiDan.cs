using UnityEngine;
using static GBR;
using static MathUtility;
using static FrameUtility;

// 防空飞弹塔的技能,球形飞弹也是用的这个
public class TowerSkill_FangKongFeiDan : TowerSkill_Bounce
{
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void fireAllBullet()
	{
		if (!mFirePointListInited || mBulletDataList.Count == 0)
		{
			return;
		}
		using var a = new ListScope<CharacterMonster>(out var targetList);
		int bulletCount = mBulletDataList.Count;
		CharacterGame focusTarget = getFocusAttackTarget();
		if (focusTarget != null && targetAvailable(focusTarget))
		{
			for (int i = 0; i < bulletCount; ++i)
			{
				targetList.Add(focusTarget as CharacterMonster);
			}
		}
		else
		{
			if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.ALL_MONSTER)
			{
				mTowerDefenceSystem.getMonstersInRange(mTower.getPosition(), mTower.getRange(), targetList);
			}
			else if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
			{
				mTowerDefenceSystem.getWalkMonstersInRange(mTower.getPosition(), mTower.getRange(), targetList);
			}
			else if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
			{
				mTowerDefenceSystem.getFlyMonstersInRange(mTower.getPosition(), mTower.getRange(), targetList);
			}
			if (targetList.Count == 0)
			{
				return;
			}
			// 按距离对列表进行排序
			quickSort(targetList, (CharacterMonster monster0, CharacterMonster monster1) =>
			{
				Vector3 vec0 = monster0.getPosition() - mTower.getPosition();
				Vector3 vec1 = monster1.getPosition() - mTower.getPosition();
				return (int)sign(getSquaredLength(vec0) - getSquaredLength(vec1));
			});
		}
		// 发射子弹
		for (int i = 0; i < bulletCount; ++i)
		{
			CMD_DELAY(out CmdCharacterFireBullet cmd);
			cmd.mFirePosMap = mFirePointLocalPosition;
			cmd.mBulletData = mBulletDataList[i];
			cmd.mTarget = targetList[clampMax(i, targetList.Count - 1)];
			cmd.mTargetAssignID = cmd.mTarget?.getAssignID() ?? 0;
			cmd.mFireID = mFireID;
			cmd.mWillFireBullet = (bullet) =>
			{
				if (bullet is SkillBulletTrackBounce trackBounce)
				{
					bounceWillFire(trackBounce);
				}
			};
			pushDelayCommand(cmd, mTower, mFireTime[i], this);
		}
	}
}