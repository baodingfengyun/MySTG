using System.Collections.Generic;
using UnityEngine;
using static FrameUtility;
using static GameUtilityHotFix;
using static UnityUtility;
using static MathUtility;
using static GBR;
using static GDR;

// 防御塔角色,处理防御塔的所有逻辑
public class CharacterTower : CharacterGame
{
	protected List<CharacterState> mBuffStates = new(); // 添加的buff state列表
	protected COMMovableObjectDrag mComDrag;			// 拖拽组件
	protected CharacterTowerData mTowerData = new();    // 塔的数据
	protected COMTowerAvatar mComAvatar;				// 显示组件
	protected COMTowerSkill mComSkill;					// 技能组件
	protected CharacterGame mHoverCharacter;			// 在交换塔时，鼠标所在的格子的塔或者指挥官
	protected int mCurDragingIndex = -1;				// 当前拖拽到的格子下标
	protected bool mDragValid;							// 当前拖拽是否有效
	public CharacterTower()
	{
		mGameData = mTowerData;
		// 不需要基类自动添加Avatar组件,手动添加一个继承后的Avatar组件
		addDontAutoCreate<COMCharacterAvatar>();
	}
	public virtual void initData(EDTower towerData)
	{
		// 初始化数据,技能数据,控制器,以及加载模型
		mTowerData.mTableData = towerData;
		mTowerData.mCritical = towerData.mCritical;
		mTowerData.mAttack = 1;
		mTowerData.mOriginRange = towerData.mRange * GRID_SIZE;
		mTowerData.mCriticalDamage = divide(mExcelGlobalConfig.getInitCriticalDamage(), ODDS_SCALE);
		mTowerData.setGlobalLevel(1);
		mComAvatar.setModelLoaded(false);
		if (towerData.mSkill > 0)
		{
			mComSkill.addSkill(towerData.mSkill);
			mComSkill.setCurSkill(towerData.mSkill);
		}
		mComSkill.setActive(false);
		mComAvatar.setModelInitedCallback((Character character) =>
		{
			mComAvatar.syncTransform();
			mComSkill.onModelLoaded();
		});
		mComAvatar.loadModelAsync(mTowerData.mTableData.mPrefab);
		mComDrag.setDragCallback(onStartDrag, onDraging, onEndDrag);
		setName(mTowerData.mTableData.mName);

		// 添加初始buff
		foreach (int buffID in towerData.mDefaultBuff)
		{
			mBuffStates.Add(characterAddBuff(buffID, this, null));
		}
#if UNITY_EDITOR
		getOrAddUnityComponent<TowerDebug>().setTower(this);
#endif
	}
	public override void destroy()
	{
		base.destroy();
	}
	public void updateData(EDTower towerData)
	{
		// 销毁之前的技能
		mComSkill.clearSkills();
		// 清空默认buff
		foreach (CharacterState state in mBuffStates)
		{
			mStateMachine.removeState(state, true);
		}
		mBuffStates.Clear();
		// 重新初始化数据
		initData(towerData);
		notifyStartFight();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffStates.Clear();
		mComDrag = null;
		mTowerData.resetProperty();
		mComAvatar = null;
		mComSkill = null;
		mHoverCharacter = null;
		mCurDragingIndex = -1;
		mDragValid = false;
	}
	public void notifyStartFight()
	{
		mComSkill.setActive(true);
	}
	public CharacterTowerData getTowerData() { return mTowerData; }
	public TOWER_TYPE getTowerType() { return mTowerData.mTableData.mType; }
	public COMTowerAvatar getComAvatar() { return mComAvatar; }
	public bool isModelLoaded() { return mComAvatar.isModelLoaded(); }
	public COMTowerSkill getComSkill() { return mComSkill; }
	public override Collider getCollider(bool addIfNotExist = false) { return mComAvatar.getCollider(); }
	public override Vector3 getFacingDirection() 
	{
		if (mComAvatar.getTowerRotateRoot() == null)
		{
			return Vector3.forward;
		}
		return mComAvatar.getTowerRotateRoot().forward; 
	}
	public void setGridPosition(int index)
	{
		setPosition(mBattleScene.getGridPosition(index));
	}
	public EDTower getNextStarData()
	{
		return mExcelTower.getTowerData(mTowerData.mTableData.mType, mTowerData.mTableData.mStar + 1);
	}
	public float generatPower()
	{
		// 攻击力*攻击频率*攻击半径 + 宝石配置数值
		return mTowerData.getAttack() * (1 + mTowerData.getAttackSpeed()) * getRange();
	}
	public override float getIncreaseRange() { return mTowerData.mRangeIncreaseValue; }
	public override float getRange() { return mTowerData.mOriginRange * (1 + mTowerData.mRangeIncreasePercent) + mTowerData.mRangeIncreaseValue; }
	public override float getOriginRange() { return mTowerData.mOriginRange; }
	public override int getHP() { return 100; }
	public override int getGridIndex() { return mTowerData.mGridIndex; }
	public override void setGridIndex(int index) { mTowerData.mGridIndex = index; }
	public override int getTableID() { return mTowerData.mTableData.mID; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void initComponents()
	{
		base.initComponents();
		// 控制器是默认不启用,要开始战斗时才会启用
		addComponent(out mComSkill, false);      // 技能组件需要在模型组件之前添加,这样可以先销毁技能,以及销毁挂在角色上的技能相关特效
		addComponent(out mComAvatar, true);
		addComponent(out mComDrag, true);
		mAvatar = mComAvatar;
	}
	protected void onStartDrag(ComponentOwner dragObj, TouchPoint touchPoint, ref bool allowDrag)
	{
		// 不知道什么原因导致下面拖拽过程中会出现拖拽有效但是下标无效的错误,可能原因之一就是塔拖拽时还没设置下标,所以没有下标时不允许拖拽
		bool isAllowDrag = canOperate() && !mTowerDefenceSystem.getBattleModeInstance().isFighting() && mTowerData.mGridIndex >= 0;
		isAllowDrag &= !mBattleScene.isCameraScaled() || (mBattleScene.isCameraScaled() && mTowerDefenceSystem.getSelectedTowerScene() == this);
		allowDrag = isAllowDrag;
		if (!isAllowDrag)
		{
			return;
		}
		mUIClientPackRogue.safe()?.setPanelVisible(false);
		setPosition(generateOffset(getPosition()) + TOWER_SELECT_OFFSET);
		mUIDraging.setDragingItem(this, touchPoint);
		mComSkill.setActive(false);
		// 拖拽时放大，提高塔
		showSelect(true);
		mDragValid = false;
		mCurDragingIndex = -1;
	}
	protected void onDraging(ComponentOwner dragObj, Vector3 pos)
	{
		// 计算当前在哪个格子
		mBattleScene.getMouseGridIndexAndPoint(pos, out int index, out Vector3 point);
		// 设置实时位置，拖拽时放大，提高塔
		setPosition(generateOffset(point) + TOWER_SELECT_OFFSET);
		// 拖拽过程中摄像机跟随
		if(index >= 0)
		{
			Vector3 curpos = lerp(getMainCamera().getPosition(), mBattleScene.focusCamera(mBattleScene.getGridPosition(index)), 0.02f);
			getMainCamera().setPosition(curpos);
		}
		// 改变了拖拽的格子,重新计算一下怪物的行走路线显示
		if (index != mCurDragingIndex)
		{
			if (mHoverCharacter != null && mHoverCharacter.getGridIndex() != mTowerData.mGridIndex)
			{
				mHoverCharacter?.showSelect(false);
				mHoverCharacter = null;
			}
			mCurDragingIndex = index;
			using var a = new ListScope<int>(out var tempList);
			// 没有改变拖拽格子,或者拖拽到有塔的格子上,都不需要改变寻路路线
			mHoverCharacter = mTowerDefenceSystem.getTowerAtGrid(mCurDragingIndex);
			if (mCurDragingIndex == mTowerData.mGridIndex || mHoverCharacter != null)
			{
				mDragValid = mBattleScene.canReplaceTower() && mHoverCharacter == null || mHoverCharacter.canOperate();
				if (mDragValid && mCurDragingIndex != mTowerData.mGridIndex)
				{
					mHoverCharacter?.showSelect(true);
				}
				mBattleScene.hideAllPreviewPath();
			}
			else
			{
				mDragValid = true;
				int roadListCount = mTowerDefenceSystem.getMonsterRoadList().Count;
				for (int i = 0; i < roadListCount; ++i)
				{
					tempList.Clear();
					mDragValid &= checkCanMoveTowerTo(i, mCurDragingIndex, mTowerData.mGridIndex, tempList);
					mBattleScene.showPreviewPath(i, tempList);
				}
			}
			if (mBattleScene.getDragOnlyGrid() >= 0 && mBattleScene.getDragOnlyGrid() != mCurDragingIndex)
			{
				mDragValid = false;
			}
			if (mDragValid && mCurDragingIndex < 0)
			{
				logError("拖拽有效但是下标无效");
			}
			Material gridMaterial = mDragValid ? mBattleScene.getGreenMaterial() : mBattleScene.getRedMaterial();
			mBattleScene.setGridMaterial(mCurDragingIndex, gridMaterial);
			mBattleScene.showTowerRange(mDragValid ? this : null, mCurDragingIndex);
			if (!mDragValid)
			{
				mBattleScene.hideAllPreviewPath();
			}
		}
	}
	protected void onEndDrag(ComponentOwner dragObj, Vector3 pos, bool cancel)
	{
		// 还原缩放
		mUIClientPackRogue.safe()?.setPanelVisible(true);
		CmdGlobalSelectTowerScene.execute(null);
		showSelect(false);
		mComSkill.setActive(true);
		mHoverCharacter?.showSelect(false);
		mHoverCharacter = null;
		mBattleScene.setGridMaterial(-1, null);
		mBattleScene.showTowerRange(null);
		mBattleScene.hideAllPreviewPath();
		if (cancel)
		{
			return;
		}
		if (mDragValid)
		{
			mTowerDefenceSystem.cmdPutTower(this, mCurDragingIndex, -1);
		}
		else
		{
			setPosition(mBattleScene.getGridPosition(mTowerData.mGridIndex));
		}
		if (mTowerDefenceSystem.getSelectedTowerScene() == this)
		{
			Vector3 targetPos = mBattleScene.focusCamera(mBattleScene.getGridPosition(mTowerData.mGridIndex));
            getMainCamera().MOVE(KEY_CURVE.EXPO_OUT, getMainCamera().getPosition(), targetPos, 0.2f);
		}
	}
}