using System.Collections.Generic;
using UnityEngine;
using static FrameBaseHotFix;
using static FrameUtility;
using static UnityUtility;
using static GBR;
using static GDR;

// 参数
public class TriggerBuffWithAreaColliderParam : CharacterTriggerParamT<TriggerBuffWithAreaColliderParam>
{
	public int mEffectID;						// 模型Effect表ID
	public bool mCancelWhenLeave;				// 离开范围是否取消
	public TARGET_BEHAVIOUR_TYPE mTargetType;   // 指定怪物类型TARGET_BEHAVIOUR_TYPE
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mEffectID = param.SToI(); });
		registeParam((param) => { mCancelWhenLeave = param.SToI() > 0; });
		registeParam((param) => { mTargetType = (TARGET_BEHAVIOUR_TYPE)param.SToI(); });
	}
	public override void check()
	{
		base.check();
		checkEnum(mTargetType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mEffectID = 0;
		mCancelWhenLeave = false;
		mTargetType = TARGET_BEHAVIOUR_TYPE.NONE;
	}
	protected override void copyInternal(TriggerBuffWithAreaColliderParam other)
	{
		base.copyInternal(other);
		mEffectID = other.mEffectID;
		mCancelWhenLeave = other.mCancelWhenLeave;
		mTargetType = other.mTargetType;
	}
}

// 生成一个区域模型，根据他的碰撞箱，对其中的怪物附加buff
public class TriggerBuffWithAreaCollider : CharacterTriggerT<TriggerBuffWithAreaColliderParam>
{
	public int mEffectID;						// 模型Effect表ID
	public bool mCancelWhenLeave;				// 离开范围是否取消
	public TARGET_BEHAVIOUR_TYPE mTargetType;	// 指定怪物类型TARGET_BEHAVIOUR_TYPE
	public HashSet<long> mAddedMonsters = new();// 已经添加了buff的怪物
	public float mTickTimer;					// 计时器
	public const float INTERVAL = 0.1f;			// 每0.1秒检测一次碰撞
	protected Collider[] mTempResult;			// 临时对象
	protected Collider mEffectCollider;			// 模型的碰撞组件
	protected GameEffect mEffect;				// 模型
	public TriggerBuffWithAreaCollider()
	{
		mTempResult = new Collider[16];
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mEffectID = 0;
		mCancelWhenLeave = false;
		mTargetType = TARGET_BEHAVIOUR_TYPE.NONE;
		mAddedMonsters.Clear();
		mTickTimer = 0.0f;
        mTempResult.setAllValue(null);
		mEffectCollider = null;
		mEffect = null;
	}
	public override void enter()
	{
		base.enter();
		mEffectID = mCustomParam.mEffectID;
		mCancelWhenLeave = mCustomParam.mCancelWhenLeave;
		mTargetType = mCustomParam.mTargetType;
		mEventSystem.listenEvent<EventWaveChange>(onWaveChanged, this);
		EDEffect effectData = mExcelEffect.query(mEffectID);
		Vector3 pos = mCustomParam.mBullet.getWorldPosition();
		mEffectManager.createEffectAsync(null, null, effectData.mPath, effectData.mSupportMoveToHide, (GameEffect effect) =>
		{
			mEffect = effect;
			mEffect.setWorldPosition(pos);
			mEffectCollider = mEffect.getUnityComponentInChild<Collider>();
			if (mEffectCollider == null)
			{
				logError("Effect[" + mEffectID + "]没有碰撞体组件");
			}
		}, true, -1.0f);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mEffectManager.destroyEffect(ref mEffect);
		removeAllAdded();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if(mEffectCollider == null)
		{
			return;
		}
		List<CharacterMonster> allMonster = mTowerDefenceSystem.getMonsterMainList();
		if (allMonster.Count > 0 && tickTimerLoop(ref mTickTimer, elapsedTime, INTERVAL))
		{
			using var a = new HashSetScope<long>(out var curInside);
			int hitCount = overlapCollider(mEffectCollider, mTempResult, MASK_MONSTER);
			for (int i = 0; i < hitCount; ++i)
			{
				CharacterMonster monster = mTowerDefenceSystem.getMonsterByCollider(mTempResult[i]);
				if (monster != null && checkMonsterCanEffective(monster) && monster.getHP() > 0)
				{
					if (mAddedMonsters.Add(monster.getGUID()))
					{
						addBuff(monster);
					}
					curInside.Add(monster.getGUID());
				}
			}
			if (mCancelWhenLeave)
			{
				foreach (CharacterMonster monster in allMonster)
				{
					if (mAddedMonsters.Contains(monster.getGUID()) && !curInside.Contains(monster.getGUID()))
					{
						removeCharacterAddedBuff(monster);
					}
				}
			}
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected bool checkMonsterCanEffective(CharacterMonster monster)
	{
		if (monster == null)
		{
			return false;
		}
		if (mTargetType == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
		{
			return !monster.getMonsterData().mFlyable;
		}
		if (mTargetType == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
		{
			return monster.getMonsterData().mFlyable;
		}
		return true;
	}
	protected void onWaveChanged(EventWaveChange param)
	{
		mAddedMonsters.Clear();
	}
}