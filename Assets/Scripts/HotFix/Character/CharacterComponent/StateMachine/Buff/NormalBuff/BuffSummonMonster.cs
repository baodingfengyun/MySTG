using System.Collections.Generic;
using UnityEngine;
using static UnityUtility;
using static GBR;

// 参数
public class BuffSummonMonsterParam : CharacterBuffParamT<BuffSummonMonsterParam>
{
	public List<int> mMonsterID = new();		// 召唤出来的怪物ID
	public List<int> mMonsterCount = new();		// 召唤出来的怪物数量
	public List<float> mHPPercent = new();      // 召唤出来的血量和防御力百分比
	public bool mOffsetPosition;				// 生成的怪物是否有一定的位置偏移
	public float mLifeTime;						// 生成的怪物的生存时间
	public override void registeAllParam()
	{
		registeParam((param) => { param.SToIs(mMonsterID); });
		registeParam((param) => { param.SToIs(mMonsterCount); });
		registeParam((param) => { param.SToFs(mHPPercent); });
		registeParam((param) => { mOffsetPosition = param.SToI() > 0; });
		registeParam((param) => { mLifeTime = param.SToF(); });
	}
	protected override void copyInternal(BuffSummonMonsterParam other)
	{
		mMonsterID.AddRange(other.mMonsterID);
		mMonsterCount.AddRange(other.mMonsterCount);
		mHPPercent.AddRange(other.mHPPercent);
		mOffsetPosition = other.mOffsetPosition;
		mLifeTime = other.mLifeTime;
	}
	public override void check()
	{
		checkDataRefByBuffDetail(mExcelMonster, mMonsterID);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMonsterID.Clear();
		mMonsterCount.Clear();
		mHPPercent.Clear();
		mOffsetPosition = false;
		mLifeTime = 0.0f;
	}
}

// 在当前的位置召唤一个怪物
public class BuffSummonMonster : CharacterBuffT<BuffSummonMonsterParam>
{
	public override void enter()
	{
		base.enter();
		if (mCharacter is not CharacterMonster monster)
		{
			return;
		}
		if (mCustomParam.mMonsterID.Count != mCustomParam.mMonsterCount.Count ||
			mCustomParam.mMonsterID.Count != mCustomParam.mHPPercent.Count)
		{
			logError("召唤怪物的ID列表和数量列表不对应,detailID:" + mBuffDetailData.mID);
			return;
		}
		int count = mCustomParam.mMonsterID.Count;
		for (int i = 0; i < count; ++i)
		{
			EDMonster newMonsterData = mExcelMonster.query(mCustomParam.mMonsterID[i]);
			int curGridIndex = monster.getComMovement().getGridIndex();
			if (curGridIndex < 0)
			{
				curGridIndex = monster.getComMovement().getTargetPointIndex();
			}
			float hpPercent = mCustomParam.mHPPercent[i];
			int summonCount = mCustomParam.mMonsterCount[i];
			bool offsetDir = true;
			for (int j = 0; j < summonCount; ++j)
			{
				CharacterMonster newMonster = CmdGlobalCreateMonster.execute(newMonsterData, curGridIndex);
				newMonster.setPosition(monster.getPosition().replaceY(newMonster.getPosition().y));
				COMMonsterMovement comMovement = newMonster.getComMovement();
				comMovement.checkRoadPointBetween();
				if (mCustomParam.mOffsetPosition)
				{
					if (offsetDir)
					{
						comMovement.moveForward(1.0f * ((j >> 1) + 1), false);
					}
					else
					{
						// 刚召唤出来的怪物没有从当前位置到起点的路线,所以向后偏移只能强制设置位置
						Vector3 curPos = monster.getPosition();
						Vector3 delta = (comMovement.getTargetPosition() - curPos).setLength(1.0f * ((j >> 1) + 1));
						newMonster.setPosition(curPos - delta);
					}
					offsetDir = !offsetDir;
				}
				newMonster.lookAtPoint(comMovement.getTargetPosition());
				newMonster.getComLifeTime().setLifeTime(mCustomParam.mLifeTime);

				// 计算出当前召唤出的怪物血量和防御力
				CharacterMonsterData monsterData = newMonster.getMonsterData();
				monsterData.mMaxHP = (int)(monster.getMaxHP() * hpPercent);
				monsterData.mDefence = (int)(monster.getMonsterData().mDefence * hpPercent);
				CmdMonsterSetHP.execute(newMonster, newMonster.getMaxHP());
			}
		}
	}
}