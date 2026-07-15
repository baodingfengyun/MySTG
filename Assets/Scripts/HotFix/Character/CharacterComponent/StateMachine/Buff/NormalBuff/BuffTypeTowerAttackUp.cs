using System.Collections.Generic;
using static FrameBaseHotFix;
using static GBR;
using static FrameUtility;
using static StringUtility;

// 参数
public class BuffTypeTowerAttackUpParam : CharacterBuffParamT<BuffTypeTowerAttackUpParam>
{
	public int mIncrease;					// 增加的攻击力
	public float mIncreasePercent;			// 攻击力提升的百分比
	public TOWER_TYPE mTowerType;           // 塔的类型
	public override void registeAllParam()
	{
		registeParam((param) => { mIncrease = param.SToI(); });
		registeParam((param) => { mIncreasePercent = param.SToF(); });
		registeParam((param) => { mTowerType = (TOWER_TYPE)param.SToI(); });
	}
	protected override void copyInternal(BuffTypeTowerAttackUpParam other)
	{
		mIncrease = other.mIncrease;
		mIncreasePercent = other.mIncreasePercent;
		mTowerType = other.mTowerType;
	}
	public override void check()
	{
		checkEnum(mTowerType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncrease = 0;
		mIncreasePercent = 0.0f;
		mTowerType = TOWER_TYPE.NONE;
	}
}

// 所有指定类型的塔攻击力提升
public class BuffTypeTowerAttackUp : CharacterBuffT<BuffTypeTowerAttackUpParam>
{
	protected HashSet<CharacterTower> mLastCharacterList;   // 已经提升了攻击力的塔列表
	protected int mIncrease;								// 提升的攻击力
	protected float mIncreasePercent;						// 提升的百分比
	protected TOWER_TYPE mTowerType;                        // 塔的类型
	protected bool mTowerChanged;							// 场上的塔是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	public override void enter()
	{
		base.enter();
		mIncrease = mCustomParam.mIncrease;
		mIncreasePercent = mCustomParam.mIncreasePercent;
		mTowerType = mCustomParam.mTowerType;
		mEventSystem.listenEvent<EventGridTowerChange>(onTowerChanged, this);
		mEventSystem.listenEvent<EventTowerDestroy>(onTowerDestroy, this);
		mTowerChanged = true;
	}
	public override void update(float elapsedTime)
	{
		if (mTowerChanged)
		{
			mTowerChanged = false;
			var newList = mTowerDefenceSystem.getTowerList();
			foreach (CharacterTower tower in newList)
			{
				// 新增的塔
				if (mLastCharacterList == null || !mLastCharacterList.Contains(tower))
				{
					tower.getTowerData().mAttackIncrease += mIncrease;
					tower.getTowerData().mIncreaseAttackPercent += mIncreasePercent;
				}
			}
			foreach (CharacterTower item in mLastCharacterList.safe())
			{
				// 已经不在的塔
				if (!newList.Contains(item))
				{
					item.getTowerData().mAttackIncrease -= mIncrease;
					item.getTowerData().mIncreaseAttackPercent -= mIncreasePercent;
				}
			}
			// 只在必要时才会创建列表
			if (newList.Count > 0)
			{
				mLastCharacterList ??= new();
			}
			mLastCharacterList?.Clear();
            mLastCharacterList?.addRange(newList);
		}
		base.update(elapsedTime);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		// 移除所有塔的增幅
		foreach (CharacterTower item in mLastCharacterList.safe())
		{
			if (item.isDestroy())
			{
				continue;
			}
			item.getTowerData().mAttackIncrease -= mIncrease;
			item.getTowerData().mIncreaseAttackPercent -= mIncreasePercent;
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mLastCharacterList?.Clear();
		mIncrease = 0;
		mIncreasePercent = 0.0f;
		mTowerType = TOWER_TYPE.NONE;
		mTowerChanged = false;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
	protected void onTowerDestroy(EventTowerDestroy eventParam)
	{
		if (eventParam.mTower == null)
		{
			return;
		}
		mLastCharacterList?.Remove(eventParam.mTower);
	}
}