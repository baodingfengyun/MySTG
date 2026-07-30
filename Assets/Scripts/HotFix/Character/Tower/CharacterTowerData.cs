using static GBR;

// 防御塔数据
public class CharacterTowerData : CharacterGameData
{
	public EDTower mTableData;								// 塔的表格数据
	public int mLevelIncreasedAttack;						// Level增加的伤害
	public float mLevelUpdateTimer;							// 升级倒计时
	public float mOriginRange;                              // 攻击范围,由表格数据和格子大小共同计算出来
	public int mGridIndex;									// 在场景中放置的格子下标
	public int mBattleLevel = 1;							// 局内等级
	public int mGlobalLevel;								// 局外养成的等级
	public float mEliteMonsterDamageIncrease;				// 对精英怪物伤害增加
	public float mBossMonsterDamageIncrease;				// 对Boss怪物伤害增加
	public bool mFreeUpModeLevel;							// 等级免费提升
	public float mIncreaseGlobalLevel;						// 对局外等级带来的 攻击加成 进行加成 mGlobalLevel
	public float mIncreaseBattleLevel;						// 对局内等级带来的 攻击加成 进行加成 mNormalModeLevel/mRogueModeLevel/mCardModeLevel
	public int mUseCoin;									// 塔实际花了多少钱，比如手动升级，而天赋自动升级不花钱也不记录，这样卖出才能返还正确的钱
	public override void resetProperty()
	{
		base.resetProperty();
		mTableData = null;
		mLevelIncreasedAttack = 0;
		mLevelUpdateTimer = -1.0f;
		mOriginRange = 0.0f;
		mGridIndex = -1;
		mBattleLevel = 1;
		mGlobalLevel = 0;
		mEliteMonsterDamageIncrease = 0.0f;
		mBossMonsterDamageIncrease = 0.0f;
		mFreeUpModeLevel = false;
		mIncreaseGlobalLevel = 0.0f;
		mIncreaseBattleLevel = 0.0f;
		mUseCoin = 0;
	}
	public void setDefenceLevel(int level)
	{
		setBattleLevel(level);
	}
	public int getBattleLevel() { return mBattleLevel; }
	public void setBattleLevel(int level)
	{
		mBattleLevel = level;
		int lastLevelAttack = mLevelIncreasedAttack;
		mLevelIncreasedAttack = mExcelTower.getTowerLevelAttack(mTableData, level);
		mAttackIncrease += ((mIncreaseBattleLevel + 1.0f) * (mLevelIncreasedAttack - lastLevelAttack)).round();
	}
	public int getGlobalLevel() { return mGlobalLevel; }
	public void setGlobalLevel(int level) { mGlobalLevel = level; }
	public float getEliteMonsterDamageIncrease() { return mEliteMonsterDamageIncrease; }
	public void setEliteMonsterDamageIncrease(float value) { mEliteMonsterDamageIncrease = value; }
	public float getBossMonsterDamageIncrease() { return mBossMonsterDamageIncrease; }
	public void setBossMonsterDamageIncrease(float value) { mBossMonsterDamageIncrease = value; }
	public bool getFreeUpModeLevel() { return mFreeUpModeLevel; }
	public void setFreeUpModeLevel(bool value) { mFreeUpModeLevel = value; }
	public void addIncreaseGlobalLevel(float percent)
	{
		mIncreaseGlobalLevel += percent;
	}
	public void removeIncreaseGlobalLevel(float percent)
	{
		mIncreaseGlobalLevel -= percent;
	}
	public void addIncreaseBattleLevel(float percent)
	{
		mIncreaseBattleLevel += percent;
		mAttackIncrease += (mLevelIncreasedAttack * percent).round();
	}
	public void removeIncreaseBattleLevel(float percent)
	{
		mIncreaseBattleLevel -= percent;
		mAttackIncrease -= (mLevelIncreasedAttack * percent).round();
	}
	public void setUseCoin(int count) { mUseCoin = count; }
	public void addUseCoin(int count) { mUseCoin += count; }
}