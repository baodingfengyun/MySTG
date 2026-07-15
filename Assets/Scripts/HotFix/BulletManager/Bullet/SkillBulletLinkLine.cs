using System;
using System.Collections.Generic;
using UnityEngine;
using static FrameBaseUtility;
using static MathUtility;
using static FrameUtility;
using static UnityUtility;
using static GameUtilityHotFix;
using static GBR;
using static GDR;

// 子弹参数
public class BulletCustomParam_LinkLine : ParamCopyableT<BulletCustomParam_LinkLine>
{
	public int mTargetCount;		// 目标数量
	public float mAttackDecrease;   // 攻击递减百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mTargetCount = param.SToI(); });
		registeParam((param) => { mAttackDecrease = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mTargetCount = 0;
		mAttackDecrease = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(BulletCustomParam_LinkLine other)
	{
		mTargetCount = other.mTargetCount;
		mAttackDecrease = other.mAttackDecrease;
	}
}

// 技能的子弹,在各个目标中连线
public class SkillBulletLinkLine : SkillBulletT<BulletCustomParam_LinkLine>
{
	protected myLineRenderer mLine = new();						// 显示的连线
	protected float mLifeTime = -1.0f;							// 剩余显示持续时间
	protected const float MAX_LIFE_TIME = 0.5f;					// 最大显示持续时间
	public override void resetProperty()
	{
		base.resetProperty();
		mLine.setLineRenderer(null);
		mLifeTime = -1.0f;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (tickTimerOnce(ref mLifeTime, elapsedTime))
		{
			explosion();
			mBulletManager.destroyBullet(this, mCharacterGame.getGUID());
		}
	}
	public void setTargetCount(int count) { mCustomParam.mTargetCount = count; }
	public int getTargetCount() { return mCustomParam.mTargetCount; }
	public BulletCustomParam_LinkLine getCustomParamLinkLine() { return mCustomParam; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected CharacterGame findNearestMonster(List<CharacterMonster> allMonsterList, List<CharacterGame> selectedList, CharacterGame curMonster)
	{
		CharacterGame nearestMonster = null;
		int flag = 0;
		try
		{
			// 找到当前怪物周围最近的怪物
			float nearestDis = 0.0f;
			foreach (CharacterMonster thisMonster in allMonsterList)
			{
				flag = 2;
				if (thisMonster == null)
				{
					flag = 3;
					continue;
				}
				flag = 4;
				if (!checkMonsterCanEffective(thisMonster))
				{
					flag = 5;
					continue;
				}
				flag = 6;
				if (selectedList.Contains(thisMonster))
				{
					flag = 7;
					continue;
				}
				flag = 8;
				float curDis = getSquaredLength(thisMonster.getPosition() - curMonster.getPosition());
				flag = 9;
				float maxRange = GRID_SIZE * 2.5f;
				if (curDis < maxRange * maxRange && (nearestMonster == null || curDis < nearestDis))
				{
					flag = 11;
					nearestMonster = thisMonster;
					flag = 12;
					nearestDis = curDis;
					flag = 13;
				}
				flag = 14;
			}
		}
		catch (Exception e)
		{
			logException(e, "flag:" + flag);
		}
		// 找到当前怪物周围最近的怪物以后,加入列表,并且以找到的这个怪物开始再找最近的怪物
		return nearestMonster;
	}
	protected override void onBulletLoaded(Vector3 firePoint)
	{
		base.onBulletLoaded(firePoint);
		mLifeTime = MAX_LIFE_TIME;
		if (mLine.getRenderer() == null)
		{
			mLine.setLineRenderer(getOrAddUnityComponent<LineRenderer>());
		}
		if (mTarget == null)
		{
			return;
		}
		using var a = new ListScope<CharacterGame>(out var tempList);
		// 寻找指定数量的目标
		tempList.Add(mTarget);
		if (tempList.Count < mCustomParam.mTargetCount)
		{
			CharacterGame monster = mTarget;
			var allMonsterList = mTowerDefenceSystem.getMonsterMainList();
			// 这里的allMonsterList可能为空指针,原因未知
			if (allMonsterList != null)
			{
				for (int i = 0; i < mCustomParam.mTargetCount - 1; ++i)
				{
					// 找到当前怪物周围最近的怪物以后,加入列表,并且以找到的这个怪物开始再找最近的怪物
					CharacterGame nearestMonster = findNearestMonster(allMonsterList, tempList, monster);
					if (nearestMonster == null)
					{
						break;
					}
					monster = nearestMonster;
					tempList.Add(nearestMonster);
					if (tempList.Count >= mCustomParam.mTargetCount)
					{
						break;
					}
				}
			}
		}

		if (tempList.Count == 0)
		{
			return;
		}
		int curTargetCount = tempList.Count;
		Span<Vector3> points = stackalloc Vector3[curTargetCount + 1];
		points[0] = mCharacterGame.getPosition();
		for (int i = 0; i < curTargetCount; ++i)
		{
			GameObject hitPoint = findGameObject(mBulletData.mHitPoint, tempList[i].getGameObject());
			points[i + 1] = hitPoint != null ? hitPoint.transform.position : tempList[i].getPosition();
			int index = i;
			setDamageCallback((CharacterGame target, CharacterGame attacker, SkillBullet bullet, out bool isHit, out bool isCritical, out HP_DELTA deltaType) =>
			{
				int damage = generateDamage(target, attacker, bullet, out isHit, out isCritical, out deltaType);
				return clampMin(round(damage * (1 - mCustomParam.mAttackDecrease * index)), 1);
			});
			hit(tempList[i]);
		}
		mLine.setPointList(points);
	}
}