using UnityEngine;
using static GBR;
using static MathUtility;
using static FrameUtility;

// 防空飞弹塔 / 球形飞弹塔共用的技能：一次齐射多发"追踪弹",逐弹锁定不同目标
// 【机制】与基类最大的差异在"目标分配"——不再所有子弹打同一个 mFacingTarget，
//   而是先收集射程内的一批怪,按距离排序,每发子弹分一个目标,实现多发覆盖多个怪。
// 【目标来源两条路】
//   ① 有集火目标(focusTarget)且有效 → 所有子弹都打它(集中火力,如 Boss 场景)；
//   ② 否则按 mEnemyType(ALL/WALK/FLY_MONSTER)取射程内对应怪 → 距离升序排序 →
//      第 i 发子弹锁定排序后第 i 个目标(i 越界则 clamp 到最后一个,保证每发都有目标)。
// 【弹道】子弹是 SkillBulletTrackBounce(追踪+弹跳),发射回调里注入 bounceWillFire
//   把技能的弹跳次数/伤害衰减加成写进子弹。
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
		// 按到塔的距离升序排序：越近的目标越优先被前面的子弹锁定（近的先打）
			quickSort(targetList, (CharacterMonster monster0, CharacterMonster monster1) =>
		{
			Vector3 vec0 = monster0.getPosition() - mTower.getPosition();
				Vector3 vec1 = monster1.getPosition() - mTower.getPosition();
				return (int)sign(vec0.getSquaredLength() - vec1.getSquaredLength());
			});
		}
		// 发射子弹
		for (int i = 0; i < bulletCount; ++i)
		{
			CMD_DELAY(out CmdCharacterFireBullet cmd);
			cmd.mFirePosMap = mFirePointLocalPosition;
			cmd.mBulletData = mBulletDataList[i];
			// 第 i 发锁定第 i 近的怪；子弹数多于怪数时,多余的都打最远那个(clamp 防越界)
			cmd.mTarget = targetList[i.clampMax(targetList.Count - 1)];
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