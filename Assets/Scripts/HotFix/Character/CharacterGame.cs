using UnityEngine;
using static GBR;
using static GDR;
using static MathUtility;
using static FrameUtility;

// 战斗角色基类
public class CharacterGame : Character
{
	protected CharacterGameData mGameData;								// 需要在子类中赋值
	protected TriggerProbabilityCallback mTriggerProbabilityCallback;   // 用于修改触发器的触发几率
	protected CharacterGame mForceTarget;                               // 强制目标,技能会将此角色强制作为攻击目标,技能目标为自身时除外
	protected long mForceTargetAssignID;								// 用于校验强制目标是否还有效
	public override void resetProperty()
	{
		base.resetProperty();
		// 不重置,在子类中赋值的
		// mGameData = null;
		mTriggerProbabilityCallback = null;
		mForceTarget = null;
		mForceTargetAssignID = 0;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mGameData != null)
		{
			tickTimerOnce(ref mGameData.mParalysisCD, elapsedTime);
			tickTimerOnce(ref mGameData.mFreezeCD, elapsedTime);
		}
	}
	public CharacterGameData getGameData() { return mGameData; }
	public virtual Transform getFootPoint() { return null; }
	public virtual Transform getBodyPoint() { return null; }
	public virtual Transform getHeadPoint() { return null; }
	public virtual float getRange() { return 0.0f; }
	public virtual float getOriginRange() { return 0.0f; }
	public virtual void setIncreaseRange(float value) { mGameData.mRangeIncreaseValue = value; }
	public virtual float getIncreaseRange() { return 0.0f; }
	public virtual Vector3 getFacingDirection() { return getForward(); }
	public CharacterGame getForceTarget()
	{
		if (mForceTarget != null && (mForceTarget.getHP() <= 0 || mForceTarget.getAssignID() != mForceTargetAssignID))
		{
			setForceTarget(null);
		}
		return mForceTarget;
	}
	public int getAttack() { return mGameData.getAttack(); }
	public int getMP() { return mGameData.mMP; }
	public float getAttackSpeed() { return mGameData.getAttackSpeed(); }
	public float getIncreaseAttackPercent() { return mGameData.mIncreaseAttackPercent; }
	public TriggerProbabilityCallback getTriggerProbabilityCallback() { return mTriggerProbabilityCallback; }
	public virtual int getHP() { return 0; }
	public virtual int getMaxHP() { return 0; }
	public float getHPPercent() { return divide(getHP(), getMaxHP()); }
	public virtual int getGridIndex() { return -1; }
	public virtual int getTableID() { return -1; }
	public void setForceTarget(CharacterGame target) { mForceTarget = target; mForceTargetAssignID = mForceTarget?.getAssignID() ?? 0; }
	public void setMP(int mp) { mGameData.mMP = mp; }
	public virtual void setGridIndex(int index) { }
	public void addTriggerProbabilityCallback(TriggerProbabilityCallback callback) { mTriggerProbabilityCallback += callback; }
	public void removeTriggerProbabilityCallback(TriggerProbabilityCallback callback) { mTriggerProbabilityCallback -= callback; }
	public float getBulletExploRangeIncreasePercent(float flyDis)
	{
		return mGameData.mExplosionRangeIncrease + mGameData.mExplosionRangeIncreaseByFlyDis * (flyDis / GRID_SIZE);
	}
	public virtual void setGridIndexAndPosition(int index)
	{
		setGridIndex(index);
		setPosition(mBattleScene.getGridPosition(index));
	}
	// 是否允许塔的移动，替换，卖出操作
	public virtual bool canOperate() { return true; }
	public void showSelect(bool show)
	{
		int gridIndex = getGridIndex();
		if (gridIndex < 0)
		{
			return;
		}
		Vector3 pos = mBattleScene.getGridPosition(gridIndex);
		// 默认都还原到1的缩放
		Vector3 scale = Vector3.one;
		if (show)
		{
			pos += TOWER_SELECT_OFFSET;
			scale = TOWER_SELECT_SCALE;
		}
		setPosition(pos);
		setScale(scale);
	}
}