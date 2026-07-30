using static GBR;

// 技能参数
public class SkillCustomParam_TouShiJi : ParamCopyableT<SkillCustomParam_TouShiJi>
{
	public float mMinRange;
	public override void registeAllParam()
	{
		registeParam((param) => { mMinRange = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMinRange = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initFromCopyInternal(SkillCustomParam_TouShiJi other)
	{
		mMinRange = other.mMinRange;
	}
}

// 投石机的技能
public class TowerSkill_TouShiJi : TowerSkillT<SkillCustomParam_TouShiJi>
{
	public float getMinRange() { return mCustomParam.mMinRange; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override bool targetAvailable()
	{
		if (mFacingTarget.getHP() <= 0)
		{
			return false;
		}
		if (mFacingTarget is CharacterMonster monster && monster.getMonsterData().mIsInvisible > 0)
		{
			return false;
		}
		float squaredLength = (mTower.getPosition() - mFacingTarget.getPosition()).resetY().getSquaredLength();
		return squaredLength >= mCustomParam.mMinRange * mCustomParam.mMinRange && squaredLength <= mTower.getRange() * mTower.getRange();
	}
	protected override void searchNewTarget()
	{
		// 寻找最小范围外最近的敌人
		if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.ALL_MONSTER)
		{
			mFacingTarget = mTowerDefenceSystem.getNearestMonsterInRange(mTower.getPosition(), mCustomParam.mMinRange, mTower.getRange());
		}
		else if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
		{
			mFacingTarget = mTowerDefenceSystem.getNearestWalkMonsterInRange(mTower.getPosition(), mCustomParam.mMinRange, mTower.getRange());
		}
		else if (mSkillData.mEnemyType == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
		{
			mFacingTarget = mTowerDefenceSystem.getNearestFlyMonsterInRange(mTower.getPosition(), mCustomParam.mMinRange, mTower.getRange());
		}
	}
}