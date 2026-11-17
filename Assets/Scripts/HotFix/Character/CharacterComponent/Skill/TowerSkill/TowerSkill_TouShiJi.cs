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

// 投石机的技能：带"最小射程"的火炮，只能打中远距离，是典型的攻城/越顶单位
// 【独特机制 —— 覆写索敌】标准塔在 [0, range] 内找最近目标，投石机改成 [mMinRange, range] 环形区间：
//   · targetAvailable()：判定目标距离平方必须 ≥ minRange² 且 ≤ range²，近身敌人会被判定为"不可用"
//   · searchNewTarget()：调 getNearest*InRange(pos, minRange, range) 的带最小半径重载
// 战术意义：近身贴脸的敌人投石机打不到，需要其他塔补防 —— 这就是"最小射程"带来的取舍。
// 伤害/子弹仍是通用的抛物线形态（SkillBulletParabola），所以只需覆写索敌两个方法，无需碰开火。
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