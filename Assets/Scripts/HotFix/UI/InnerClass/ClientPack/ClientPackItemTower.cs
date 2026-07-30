using UnityEngine;
using static UnityUtility;
using static GameUtilityHotFix;
using static GBR;
using static GDR;

// 界面上显示的塔的图标的基类,可拖拽出去创建塔
public class ClientPackItemTower : ClientPackItem
{
	protected myUGUIImage mTowerIcon;
	protected EDTower mTowerData;
	protected CharacterTower mHoverTower;           // 在交换塔时，鼠标所在的格子的塔
	protected CharacterTower mDragingTower;
	protected int mCurDragingIndex;
	protected bool mDragValid;
	public ClientPackItemTower(IWindowObjectOwner script) : base(script) { }
	public override void reset()
	{
		base.reset();
		mTowerData = null;
		mDragingTower = null;
		mHoverTower = null;
		mCurDragingIndex = -1;
		mDragValid = false;
		mTowerIcon.setActive(false);
		mDragArea.activeComponent<COMWindowDrag>(false);
		mTargetTypes.setActive(true);
	}
	public override void update()
	{
		base.update();
		mCD.setFillPercent(mTowerDefenceSystem.getBuildingCD().divide(BUILDING_CD));
	}
	public CharacterTower getDragingTower() { return mDragingTower; }
	public EDTower getTowerData() { return mTowerData; }
	public void setReadyToSetup(bool ready) { mDisableMask.setActive(ready); }
	public void stopDrag() { mDragArea.getComponent<COMWindowDrag>()?.cancelDrag(); }
	public virtual void setTowerData(EDTower towerData)
	{
		mTowerData = towerData;
		setCount(-1);
		// 暂时根据星级显示品质
		setQuality(towerData.mStar - 1);
		setName(towerData.mName);
		mTowerIcon.setActive(true);
		mTowerIcon.setSpriteName(towerData.mIcon);
		mCD.setActive(true);
		mDragArea.getOrAddComponent<COMWindowDrag>().setActive(true);
		EDTowerSkill skill = mExcelTowerSkill.query(towerData.mSkill, false);
		if (mTargetTypes.setActive(skill != null))
		{
			setTargetType(skill.mEnemyType);
		}
	}
	public void setTargetType(TARGET_BEHAVIOUR_TYPE type)
	{
		mTargetAll.setActive(type == TARGET_BEHAVIOUR_TYPE.ALL_MONSTER);
		mTargetFly.setActive(type == TARGET_BEHAVIOUR_TYPE.FLY_MONSTER);
		mTargetGround.setActive(type == TARGET_BEHAVIOUR_TYPE.WALK_MONSTER);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onDragStart(ComponentOwner dragObj, TouchPoint touchPoint, ref bool allowDrag)
	{
		if (mTowerDefenceSystem.isBuildingCDing())
		{
			allowDrag = false;
			return;
		}
		// 开始拖拽就只是拖拽放置,闪烁表示点击放置,所以不再闪烁
		mDisableMask.setActive(true);
		mTowerDefenceSystem.cmdSelectItemOwned(null);
		CmdGlobalSelectTowerScene.execute(null);
		mBattleScene.getMouseGridIndexAndPoint(touchPoint.getCurPosition(), out _, out Vector3 point);
		mDragingTower = CmdGlobalCreateTower.execute(mTowerData, point);
		mDragingTower.setPosition(generateOffset(point));
		mDragValid = false;
		LT.HIDE<UITowerInfo>();
	}
	protected override void onDraging(ComponentOwner dragObj, Vector3 mousePos)
	{
		// 计算当前在哪个格子
		mBattleScene.getMouseGridIndexAndPoint(mousePos, out _, out Vector3 point);
		point += new Vector3(0.0f, 0.0f, 1.0f);
		int index = mBattleScene.worldPointToGridIndex(point, mCurDragingIndex);
		// 设置实时位置
		mDragingTower.setPosition(generateOffset(point));
		// 改变了拖拽的格子,重新计算一下怪物的行走路线显示
		if (index == mCurDragingIndex)
		{
			return;
		}
		mHoverTower?.showSelect(false);
		mHoverTower = null;
		mCurDragingIndex = index;

		mHoverTower = mTowerDefenceSystem.getTowerAtGrid(mCurDragingIndex);
		if (mHoverTower == null)
		{
			mDragValid = true;
			int roadListCount = mTowerDefenceSystem.getMonsterRoadList().Count;
			using var a = new ListScope<int>(out var tempList);
			for (int i = 0; i < roadListCount; ++i)
			{
				tempList.Clear();
				mDragValid &= checkCanPutTower(i, mCurDragingIndex, tempList);
				mBattleScene.showPreviewPath(i, tempList);
			}
		}
		else
		{
			mDragValid = mHoverTower.canOperate() && mBattleScene.canReplaceTower();
			if (mDragValid)
			{
				mHoverTower.showSelect(true);
			}
			mBattleScene.hideAllPreviewPath();
		}

		if (mBattleScene.getDragOnlyGrid() >= 0 && mBattleScene.getDragOnlyGrid() != mCurDragingIndex)
		{
			mDragValid = false;
		}
		if (mDragValid && mCurDragingIndex < 0)
		{
			logError("拖拽有效但是下标无效");
		}
		Material gridMaterial = mCurDragingIndex >= 0 && mDragValid ? mBattleScene.getGreenMaterial() : mBattleScene.getRedMaterial();
		mBattleScene.setGridMaterial(mCurDragingIndex, gridMaterial);
		mBattleScene.showTowerRange(mDragValid ? mDragingTower : null, mCurDragingIndex);
		if (!mDragValid)
		{
			mBattleScene.hideAllPreviewPath();
		}
	}
	protected override void onDragEnd(ComponentOwner dragObj, Vector3 mousePos, bool cancel)
	{
		mHoverTower?.showSelect(false);
		mHoverTower = null;
		mDisableMask.setActive(false);
		mBattleScene.setGridMaterial(-1, null);
		mBattleScene.showTowerRange(null);
		mBattleScene.hideAllPreviewPath();
		if (cancel)
		{
			if (mDragingTower != null)
			{
				CmdGlobalDestroyTower.execute(mDragingTower);
				mDragingTower = null;
			}
			return;
		}

		// 放下的时候再检测一次是否在CD中
		if (mTowerDefenceSystem.isBuildingCDing())
		{
			mDragValid = false;
		}
	}
	protected override void onAreaClick()
	{
		LT.LOAD<UITowerInfo>().setTower(mTowerData);
	}
}