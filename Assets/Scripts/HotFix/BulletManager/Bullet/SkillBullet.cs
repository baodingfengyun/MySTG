using UnityEngine;
using System.Collections.Generic;
using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static FrameBaseUtility;
using static UnityUtility;
using static MathUtility;
using static GBR;
using static GDR;

// 技能的子弹基类
public class SkillBullet : MovableObject
{
	protected BulletDamageModifier mDamageModifier;		// 子弹伤害修改器
	protected CharacterGame mTarget;					// 攻击的目标
	protected CharacterGame mCharacterGame;				// 发射此子弹的角色
	protected EDSkillBullet mBulletData;				// 子弹的表格数据
	protected GameEffect mFlyEffect;					// 子弹飞行特效,也是子弹本身的节点
	protected CharacterSkill mSkill;					// 所属的技能,允许为空,也就是子弹可以不属于任何技能,单独存在
	protected DamageCallback mDamageCallback;			// 计算伤害的函数,如果没有设置则使用默认的公式进行计算
	protected HitCallback mHitCallback;					// 命中时的回调,用于通知其他地方击中了敌人,不适合用于附加buff之类的简单判断,而是用于一些其他逻辑的触发
	protected BulletCallback mExplosionCallback;		// 子弹销毁时的回调
	protected Vector3 mLastPosition;					// 上一帧的位置
	protected Vector3 mHitPointOffset;					// 子弹目标点相对于怪物坐标的偏移
	protected Vector3 mStartPosition;					// 子弹的起点
	protected INT mDamage = new();                      // 缓存的伤害值对象
	protected long mTargetAssignID;						// 用于检测目标是否已经被销毁
	protected long mFireID;								// 技能释放的唯一ID，每次技能释放发射的所有子弹都有同一个释放唯一ID
	protected bool mHasLastPosition;					// 上一帧的位置是否有效
	protected bool mWillDestroy;						// 是否已经加入到销毁列表,防止重复销毁
	protected bool mLoaded;                             // 子弹是否加载完毕
	protected bool mFaceForward = true;					// 是否在移动过程中始终朝向移动方向
	protected TARGET_BEHAVIOUR_TYPE mEffectiveTarget;	// 生效的目标类型
	protected float mAttackPercent;						// 攻击力提高百分比
	public override void resetProperty()
	{
		base.resetProperty();
		mDamageModifier = null;
		mTarget = null;
		mCharacterGame = null;
		mBulletData = null;
		mFlyEffect = null;
		mSkill = null;
		mDamageCallback = null;
		mHitCallback = null;
		mExplosionCallback = null;
		mLastPosition = Vector3.zero;
		mHitPointOffset = Vector3.zero;
		mStartPosition = Vector3.zero;
		mDamage.mValue = 0;
		mTargetAssignID = 0;
		mFireID = 0L;
		mHasLastPosition = false;
		mWillDestroy = false;
		mLoaded = false;
		mFaceForward = true;
		mEffectiveTarget = TARGET_BEHAVIOUR_TYPE.NONE;
		mAttackPercent = 0.0f;
	}
	public virtual void initData(EDSkillBullet data, ParamCopyable paramTemplate)
	{
		mBulletData = data;
		mEffectiveTarget = (TARGET_BEHAVIOUR_TYPE)mBulletData.mEffectiveTarget;
		mAttackPercent = mBulletData.mAttackPercent;
		setName(mBulletData.mName);
		if (mBulletData.mDamageModifier > 0)
		{
			mDamageModifier = BulletDamageModifierRegister.getModifier(mBulletData.mDamageModifier);
		}
	}
	public override void destroy()
	{
		// 销毁子弹特效时,也要将mObject重置一下,一般都是只回收特效,而不会销毁特效对象
		if (mFlyEffect != null)
		{
			mEffectManager.destroyEffect(ref mFlyEffect);
			setObject(null);
		}
		mDamageModifier = null;
		base.destroy();
	}
	// 需要外部显式来真正销毁子弹特效
	public void destroyFireEffectReally()
	{
		if (mFlyEffect != null)
		{
			mEffectManager.destroyEffect(ref mFlyEffect, true);
			setObject(null);
		}
	}
	public override void lateUpdate(float elapsedTime)
	{
		base.lateUpdate(elapsedTime);
		if (mFaceForward && mObject != null)
		{
			if (mHasLastPosition)
			{
				lookAt(getPosition() - mLastPosition);
			}
			mLastPosition = getPosition();
			mHasLastPosition = true;
		}
	}
	public void fire()
	{
		// 加载子弹特效,加载完再移动
		if (mBulletData.mFlyEffect > 0)
		{
			EDEffect flyEffectData = mExcelEffect.query(mBulletData.mFlyEffect);
			mEffectManager.createEffectAsyncSafe(flyEffectData.mPath, this, null, flyEffectData.mSupportMoveToHide, (GameEffect effect) =>
			{
				mFlyEffect = effect;
				setObject(mFlyEffect.getUnityObject());
				onBulletLoaded(mStartPosition);
			}, 0, false);
		}
		else
		{
			onBulletLoaded(mStartPosition);
		}
		if (mBulletData.mMuzzleEffect > 0)
		{
			EDEffect effect = mExcelEffect.query(mBulletData.mMuzzleEffect);
			float yaw = mCharacterGame.getFacingDirection().getAngle(ANGLE.DEGREE);
			mEffectManager.playEffectAsyncAtPosition(effect.mPath, mStartPosition, new(0.0f, yaw, 0.0f), 1.0f, effect.mSupportMoveToHide, 0);
		}
	}
	public CharacterGame getCharacter()							{ return mCharacterGame; }
	public void setDamageCallback(DamageCallback callback)		{ mDamageCallback = callback; }
	public void setHitCallback(HitCallback callback)			{ mHitCallback = callback; }
	public void setStartPosition(Vector3 firePoint)				{ mStartPosition = firePoint; }
	public void setSkill(CharacterSkill skill)					{ mSkill = skill; }
	public void setCharacter(CharacterGame character)			{ mCharacterGame = character; }
	public void setExplosionCallback(BulletCallback callback)	{ mExplosionCallback = callback; }
	public void setFaceForward(bool faceForward)				{ mFaceForward = faceForward; }
	public void setTarget(CharacterGame target)					
	{
		mTarget = target;
		mTargetAssignID = mTarget?.getAssignID() ?? 0;
		// 获取子弹该命中的怪物部位
		if (mTarget != null)
		{
			GameObject hitPoint = findGameObject(mBulletData.mHitPoint, mTarget.getGameObject());
			if (hitPoint != null)
			{
				mHitPointOffset = hitPoint.transform.localPosition;
			}
		}
	}
	public void setAttackPercent(float value)					{ mAttackPercent = value; }
	public void setWillDestroy(bool willDestroy)				{ mWillDestroy = willDestroy; }
	public EDSkillBullet getBulletData()						{ return mBulletData; }
	public Vector3 getStartPosition()							{ return mStartPosition; }
	public Vector3 getHitPointOffset()							{ return mHitPointOffset; }
	public CharacterGame getTarget()							{ return mTarget; }
	public GameEffect getFlyEffect()							{ return mFlyEffect; }
	public CharacterSkill getSkill()							{ return mSkill; }
	public float getFlyDistance()								{ return (getPosition() - mStartPosition).resetY().getLength(); }
	public virtual float getRealtimeRange()						{ return 0.0f; }
	public float getAttackPercent()								{ return mAttackPercent; }
	public bool isWillDestroy()									{ return mWillDestroy; }
	public long getFireID() { return mFireID; }
	public void setFireID(long value) { mFireID = value; }
	public Vector3 generateStartPos(CharacterGame target, Dictionary<string, Transform> pointMap)
	{
		Vector3 pos = Vector3.zero;
		if (mBulletData.mStartPosition == BULLET_FIRE_POINT.SELF_FOOT)
		{
			if (mCharacterGame.getFootPoint() != null)
			{
				pos = mCharacterGame.getFootPoint().position;
			}
		}
		else if (mBulletData.mStartPosition == BULLET_FIRE_POINT.SELF_BODY)
		{
			if (mCharacterGame.getBodyPoint() != null)
			{
				pos = mCharacterGame.getBodyPoint().position;
			}
		}
		else if (mBulletData.mStartPosition == BULLET_FIRE_POINT.SELF_HEAD)
		{
			if (mCharacterGame.getHeadPoint() != null)
			{
				pos = mCharacterGame.getHeadPoint().position;
			}
		}
		else if (mBulletData.mStartPosition == BULLET_FIRE_POINT.SELF_POINT)
		{
			Transform trans = null;
			if (pointMap == null || !pointMap.TryGetValue(mBulletData.mStartPointName, out trans))
			{
				logError("找不到发射起点:" + mBulletData.mStartPointName + ", BulletID:" + mBulletData.mID);
			}
			pos = trans != null ? trans.position : Vector3.zero;
		}
		else if (mBulletData.mStartPosition == BULLET_FIRE_POINT.TARGET_FOOT)
		{
			if (target?.getFootPoint() != null)
			{
				pos = target.getFootPoint().position;
			}
		}
		else if (mBulletData.mStartPosition == BULLET_FIRE_POINT.TARGET_BODY)
		{
			if (target?.getBodyPoint() != null)
			{
				pos = target.getBodyPoint().position;
			}
		}
		else if (mBulletData.mStartPosition == BULLET_FIRE_POINT.TARGET_HEAD)
		{
			if (target?.getHeadPoint() != null)
			{
				pos = target.getHeadPoint().position;
			}
		}
		return pos;
	}
	public void getRangeEffectiveMonster(float range, List<CharacterMonster> monsterList)
	{
		if (mEffectiveTarget == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
		{
			mTowerDefenceSystem.getWalkMonstersInRange(getPosition(), range, monsterList);
		}
		else if (mEffectiveTarget == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
		{
			mTowerDefenceSystem.getFlyMonstersInRange(getPosition(), range, monsterList);
		}
		else if (mEffectiveTarget == TARGET_BEHAVIOUR_TYPE.ALL_MONSTER)
		{
			mTowerDefenceSystem.getMonstersInRange(getPosition(), range, monsterList);
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected virtual void onBulletLoaded(Vector3 firePoint)
	{
		mLoaded = true;
		setPosition(firePoint);
		// 等待设置子弹位置以后才会将子弹显示出来,避免某些带拖尾的特效由于先显示再设置位置而导致的拖尾错误
		mFlyEffect?.setActive(true);
		mLastPosition = getPosition();
		setScale(Vector3.one + mCharacterGame.getGameData().mBulletScale);
	}
	protected void hit(CharacterGame target)
	{
		if (target == null)
		{
			return;
		}
		var monster = target as CharacterMonster;
		if (monster != null)
		{
			// 广播即将命中怪物的事件,事件中可以修改伤害
			using var a = new ClassScope<EventWillGenerateDamage>(out var param);
			param.mAttacker = mCharacterGame;
			param.mTarget = monster;
			param.mBullet = this;
			mEventSystem.pushEvent(param, mCharacterGame.getGUID());
		}

		// 给被击中目标附加击中时的buff
		foreach (int buffID in mBulletData.mWillHitBuffToTarget)
		{
			characterAddBuff(buffID, target, mCharacterGame, this, mSkill, null);
		}

		HP_DELTA deltaType = HP_DELTA.NONE;
		bool critical = false;
		bool isHit = false;
		// 如果是伤害型子弹,则计算伤害
		if (mBulletData.mIsDamage && monster != null)
		{
			if (mDamageCallback != null)
			{
				mDamage.mValue = mDamageCallback(monster, mCharacterGame, this, out isHit, out critical, out deltaType);
			}
			else
			{
				mDamage.mValue = generateDamage(monster, mCharacterGame, this, out isHit, out critical, out deltaType);
				// 子弹飞行距离对伤害的增幅
				mDamage.mValue = (int)(mDamage.mValue * (mCharacterGame.getGameData().mDamageIncreaseByFlyDis * (getFlyDistance() / GRID_SIZE) + 1.0f));
			}
			deltaType = isHit ? deltaType : HP_DELTA.MISS;
			// 对伤害进行一定规则的修改
			mDamageModifier?.modify(monster, ref mDamage.mValue);
		}
		else
		{
			mDamage.mValue = 0;
		}

		if (monster != null)
		{
			// 广播即将命中怪物的事件,事件中可以修改伤害
			using var a = new ClassScope<EventWillHitCharacter>(out var param);
			param.mAttacker = mCharacterGame;
			param.mTarget = monster;
			param.mBullet = this;
			param.mDamage = mDamage;
			param.mDeltaType = deltaType;
			mEventSystem.pushEvent(param, mCharacterGame.getGUID());
			// 广播完即将命中的事件后就扣血
			if (mBulletData.mIsDamage && (mDamage.mValue > 0 || deltaType == HP_DELTA.MISS) && deltaType != HP_DELTA.NONE)
			{
				CmdMonsterSetHP.execute(monster, mCharacterGame, monster.getMonsterData().mHP - mDamage.mValue, -mDamage.mValue, true, deltaType, critical);
			}
		}

		// 已经被销毁的怪物不能再使用,不能对其做任何操作
		if (target.isDestroy())
		{
			target = null;
			monster = null;
		}

		// 播放击中特效
		if (mBulletData.mHitEffect > 0)
		{
			Vector3 pos = Vector3.zero;
			if (mBulletData.mHitEffectPosition == HIT_EFFECT_POSITION.BULLET_POSITION)
			{
				pos = getPosition();
			}
			else if (mBulletData.mHitEffectPosition == HIT_EFFECT_POSITION.TARGET_POSITION)
			{
				GameObject hitPoint = findGameObject(mBulletData.mHitPoint, target.getGameObject());
				pos = hitPoint != null ? hitPoint.transform.position : target.getPosition();
			}
			EDEffect effectData = mExcelEffect.query(mBulletData.mHitEffect);
			mEffectManager.playEffectAsyncAtPosition(effectData.mPath, pos, 0.5f, effectData.mSupportMoveToHide, 0);
		}

		// 播放击中音效
		if (mBulletData.mHitSound0 > 0 || mBulletData.mHitSound1 > 0)
		{
			int sound;
			if (mBulletData.mHitSound0 > 0 && mBulletData.mHitSound1 > 0)
			{
				sound = randomHit(0.5f) ? mBulletData.mHitSound0 : mBulletData.mHitSound1;
			}
			else
			{
				sound = mBulletData.mHitSound0 > 0 ? mBulletData.mHitSound0 : mBulletData.mHitSound1;
			}
			if (sound > 0)
			{
				AT.SOUND_2D(sound);
			}
		}

		// 给被击中目标附加击中时的buff
		foreach (int buffID in mBulletData.mHitBuffToTarget)
		{
			characterAddBuff(buffID, target, mCharacterGame, this, mSkill, mDamage);
		}

		// 给自己附加击中时的buff
		foreach (int buffID in mBulletData.mHitBuffToSelf)
		{
			characterAddBuff(buffID, mCharacterGame, mCharacterGame, this, mSkill, mDamage);
		}

		// 击中的回调
		mHitCallback?.Invoke(target, mCharacterGame, this);

		// 广播怪物被击中的事件
		if (monster != null)
		{
			using var a = new ClassScope<EventHitCharacter>(out var param0);
			param0.mAttacker = mCharacterGame;
			param0.mTarget = monster;
			param0.mDamage = mDamage.mValue;
			param0.mBullet = this;
			param0.mCritical = critical;
			param0.mMiss = !isHit;
			mEventSystem.pushEvent(param0, mCharacterGame.getGUID());

			using var b = new ClassScope<EventCharacterBeenHit>(out var param1);
			param1.mAttacker = mCharacterGame;
			param1.mTarget = monster;
			param1.mDamage = mDamage.mValue;
			param1.mBullet = this;
			param1.mCritical = critical;
			param1.mMiss = !isHit;
			mEventSystem.pushEvent(param1, monster.getGUID());
		}
	}
	// 播放爆炸特效,需要子类调用
	protected void explosion()
	{
		using var a = new ClassScope<EventBulletExplosion>(out var param);
		param.mBullet = this;
		mEventSystem.pushEvent(param, mCharacterGame.getGUID());

		mExplosionCallback?.Invoke(this);
		if (mBulletData.mExplosionEffect == 0)
		{
			return;
		}
		EDEffect effectData = mExcelEffect.query(mBulletData.mExplosionEffect);
		mEffectManager.playEffectAsyncAtPosition(effectData.mPath, getPosition(), 1.0f, effectData.mSupportMoveToHide, 0);
	}
	protected CharacterMonster getNearestEffectiveMonster(float range)
	{
		if (mEffectiveTarget == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
		{
			return mTowerDefenceSystem.getNearestWalkMonsterInRange(getPosition(), range);
		}
		if (mEffectiveTarget == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
		{
			return mTowerDefenceSystem.getNearestFlyMonsterInRange(getPosition(), range);
		}
		if (mEffectiveTarget == TARGET_BEHAVIOUR_TYPE.ALL_MONSTER)
		{
			return mTowerDefenceSystem.getNearestMonsterInRange(getPosition(), range);
		}
		return null;
	}
	protected bool checkMonsterCanEffective(CharacterMonster monster)
	{
		if (monster == null)
		{
			return false;
		}
		if (mEffectiveTarget == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER)
		{
			return !monster.getMonsterData().mFlyable;
		}
		if (mEffectiveTarget == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER)
		{
			return monster.getMonsterData().mFlyable;
		}
		return true;
	}
}