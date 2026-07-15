using Obfuz;
using System.Collections.Generic;
using UnityEngine;
using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static MathUtility;
using static UnityUtility;
using static WidgetUtility;
using static GBR;
using static GDR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIClientPackRogue.prefab
// rogue模式中底部显示当前背包中的所有道具的界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIClientPackRogue : LayoutScript
{
	protected myUGUIObject mDeleteDisplay;
	protected myUGUIObject mDeleteDisplayImage;
	protected myUGUIObject mPanel;
	protected myUGUIObject mTowerTag;
	protected myUGUIObject mGemTag;
	protected myUGUIObject mConsumableTag;
	protected myUGUIObject mTowerTagOn;
	protected myUGUIObject mGemTagOn;
	protected myUGUIObject mConsumableTagOn;
	protected myUGUIObject mTagSelecter;
	protected myUGUIDragView mDragView;
	protected myUGUIObject mPanelHidePoint;
	protected myUGUIObject mReceiveDragArea;
	protected myUGUIObject mHidePanel;
	protected myUGUIObject mShowPanel;
	protected myUGUIObject mStoneTowerOff;
	protected myUGUIObject mStoneTowerOn;
	protected WindowStructPool<ClientPackRogueItemTower> mClientPackRogueItemTowerPool;
    // auto generate member end
    protected ClientPackItem mReadyToSetupItem;
	protected ClientPackRogueItemTower mReadyToSetupTower;
	protected Vector3 mPanelStartPosition;
	protected Vector3 mDeleteDisplayStartPosition;
	protected CLIENT_PACK_VIEW mCurViewType;
	protected bool mClientPackDirty;
	protected Dictionary<CLIENT_PACK_VIEW, float> mListLeftPos;			// 滑动列表左边界相对于父节点左边界的位置
	protected Dictionary<CLIENT_PACK_VIEW, bool> mListLeftPosValid;		// 保存的滑动列表位置是否有效
	protected Vector3 mTagSelecterOriginPos;	// 按钮初始位置
	protected CharacterTower mHoverTower;		// 在交换塔时，鼠标所在的格子的塔
	protected CharacterTower mDragingTower;
	protected int mCurDragingIndex;
	protected bool mDragValid;
	public UIClientPackRogue()
	{
		// auto generate constructor start
		mClientPackRogueItemTowerPool = new(this);
		// auto generate constructor end
		mNeedUpdate = true;
		mListLeftPos = new()
		{
			{ CLIENT_PACK_VIEW.TOWER, 0.0f},
		};
		mListLeftPosValid = new()
		{
			{ CLIENT_PACK_VIEW.TOWER, false},
		};
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mDeleteDisplay, "DeleteDisplay");
		newObject(out mDeleteDisplayImage, mDeleteDisplay, "DeleteDisplayImage");
		newObject(out mPanel, "Panel");
		newObject(out mTowerTag, mPanel, "TowerTag");
		newObject(out mGemTag, mPanel, "GemTag");
		newObject(out mConsumableTag, mPanel, "ConsumableTag");
		newObject(out mTowerTagOn, mPanel, "TowerTagOn");
		newObject(out mGemTagOn, mPanel, "GemTagOn");
		newObject(out mConsumableTagOn, mPanel, "ConsumableTagOn");
		newObject(out mTagSelecter, mPanel, "TagSelecter");
		newObject(out myUGUIObject viewport, mPanel, "Viewport", false);
		newObject(out mDragView, viewport, "DragView");
		newObject(out mPanelHidePoint, "PanelHidePoint");
		newObject(out mReceiveDragArea, "ReceiveDragArea");
		newObject(out mHidePanel, "HidePanel");
		newObject(out mShowPanel, "ShowPanel");
		newObject(out mStoneTowerOff, "StoneTowerOff");
		newObject(out mStoneTowerOn, "StoneTowerOn");
		mClientPackRogueItemTowerPool.assignTemplate(mDragView, "ClientPackRogueItemTower");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mTowerTag.registeCollider(onTowerTagClick);
		mHidePanel.registeCollider(onHidePanelClick);
		mShowPanel.registeCollider(onShowPanelClick);
		mStoneTowerOn.registeCollider();
		// auto generate init end
		mDragView.setDragDirection(DRAG_DIRECTION.HORIZONTAL);
        mDragView.setDragAngleThreshold(toRadian(15.0f));
		mStoneTowerOn.registeCollider();
		mReceiveDragArea.registeCollider(true);
		mReceiveDragArea.setOnReceiveDrag(onReceiveDrag);
		mReceiveDragArea.setOnDragHover(onDragHover);
		mPanelStartPosition = mPanel.getPosition();
		mDeleteDisplayStartPosition = mDeleteDisplay.getPosition();
		mTagSelecterOriginPos = mTagSelecter.getPosition();
		var dragCom = mStoneTowerOff.getOrAddComponent<COMWindowDrag>();
		dragCom.initDrag(Vector2.up, toRadian(180.0f), true, false);
		dragCom.setDragCallback(onDragStart, onDraging, onDragEnd);
	}
	public override void onGameState()
	{
		base.onGameState();
        mClientPackRogueItemTowerPool.unuseAll();
        mStoneTowerOn.setActive(false);
        mReadyToSetupItem = null;
        mReadyToSetupTower = null;
        mCurViewType = CLIENT_PACK_VIEW.TOWER;
        mClientPackDirty = false;
        mListLeftPosValid.setAllValue(false);
        mListLeftPos.setAllValue(0.0f);
        mStoneTowerOff.activeComponent<COMWindowDrag>(false);
        mDeleteDisplay.setPosition(mPanelHidePoint.getPosition());
        mDeleteDisplay.setActive(false);
        showTypeItems(mCurViewType, true);
		setPanelVisible(true);
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mClientPackDirty)
		{
			mClientPackDirty = false;
			showTypeItems(mCurViewType, false);
		}
		foreach (ClientPackRogueItemTower item in mClientPackRogueItemTowerPool.getUsedList())
		{
			item.update();
		}
	}
	public void setClientPackDirty() { mClientPackDirty = true; }
	public ClientPackItem getSelectedOwnedItem() { return mReadyToSetupItem; }
	public void setSelectedOwnedItem(ClientPackItem battleItem)
	{
		mReadyToSetupItem = battleItem;
		foreach (ClientPackRogueItemTower item in mClientPackRogueItemTowerPool.getUsedList())
		{
			item.setSelected(item == battleItem);
		}
	}
	public ClientPackRogueItemTower getReadyToSetupTower() { return mReadyToSetupTower; }
	public void stopDrag()
	{
		mStoneTowerOff.getComponent<COMWindowDrag>()?.cancelDrag();
		foreach (ClientPackRogueItemTower item in mClientPackRogueItemTowerPool.getUsedList())
		{
			item.stopDrag();
		}
	}
	public void refreshCoinColor()
	{
		foreach (ClientPackRogueItemTower item in mClientPackRogueItemTowerPool.getUsedList())
		{
			item.refreshCoinColor();
		}
	}
	public void setPanelVisible(bool visible, bool showDeleteOnHide = true)
	{
		mShowPanel.setActive(false);
		mHidePanel.setActive(false);
		if (visible)
		{
			mPanel.setActive(true);
			mPanel.MOVE(KEY_CURVE.EXPO_OUT, mPanel.getPosition(), replaceY(mPanel.getPosition(), mPanelStartPosition.y), 0.2f);
			if(mDeleteDisplay.isActive())
			{
                mDeleteDisplay.MOVE_EX(KEY_CURVE.EXPO_OUT, mDeleteDisplayStartPosition, replaceY(mDeleteDisplayStartPosition, mPanelHidePoint.getPosition().y), 0.2f, (_, _)=>
				{
					mDeleteDisplay.setActive(false);
				});
			}
		}
		else
		{
			mPanel.MOVE(KEY_CURVE.EXPO_IN, mPanel.getPosition(), replaceY(mPanel.getPosition(), mPanelHidePoint.getPosition().y), 0.2f);
			if(showDeleteOnHide)
			{
				mDeleteDisplay.setActive(true);
				mDeleteDisplay.MOVE(KEY_CURVE.EXPO_OUT, replaceY(mDeleteDisplayStartPosition, mPanelHidePoint.getPosition().y), mDeleteDisplayStartPosition, 0.2f);
			}
		}
	}
	public void setActiveOnlyAllTowerDrag()
	{
		// 先清空
		mGlobalTouchSystem.setActiveOnlyObject(null);
		foreach (ClientPackRogueItemTower item in mClientPackRogueItemTowerPool.getUsedList())
		{
				// 因为节点处于一个滑动列表中,需要将父节点也加入激活列表才能正常响应
			mGlobalTouchSystem.addActiveOnlyObjectWithAllParent(item.getDragArea());
		}
	}
	public void setActiveOnlyTowerDrag(TOWER_TYPE type)
	{
		foreach (ClientPackRogueItemTower item in mClientPackRogueItemTowerPool.getUsedList())
		{
			if (item.getTowerData().mType == type)
			{
				// 因为节点处于一个滑动列表中,需要将父节点也加入激活列表才能正常响应
				mGlobalTouchSystem.setActiveOnlyObjectWithAllParent(item.getDragArea());
				break;
			}
		}
	}
	public void setActiveOnlyTowerClick(TOWER_TYPE type)
	{
		foreach (ClientPackRogueItemTower item in mClientPackRogueItemTowerPool.getUsedList())
		{
			if (item.getTowerData().mType == type)
			{
				// 因为节点处于一个滑动列表中,需要将父节点也加入激活列表才能正常响应
				mGlobalTouchSystem.setActiveOnlyObjectWithAllParent(item.getClickArea());
				break;
			}
		}
	}
	public Vector3 getTowerPropPosition(TOWER_TYPE type)
	{
		foreach (ClientPackRogueItemTower item in mClientPackRogueItemTowerPool.getUsedList())
		{
			if (item.getTowerData().mType == type)
			{
				return item.getRoot().getWorldPosition();
			}
		}
		return Vector3.zero;
	}
	public bool isMoveDone() { return !mPanel.getComponent<COMTransformableMove>().isActive(); }
	public void refresh()
	{
		// 暂时不显示
		mTowerTag.setActive(false);
		mTowerTagOn.setActive(false);
		mGemTag.setActive(false);
		mConsumableTag.setActive(false);
		mGemTagOn.setActive(false);
		mConsumableTagOn.setActive(false);

		mClientPackRogueItemTowerPool.unuseAll();
		if (mCurViewType == CLIENT_PACK_VIEW.TOWER)
		{
			using var a = new ListScope<EDTower>(out var tempList);
			tempList.addRange(mTowerDefenceSystem.getBattleModeRogue().getAllowUseTowerList());
			tempList.Sort((x, y) => sign(x.mDisplayInOrder - y.mDisplayInOrder));
			int count = mExcelGlobalConfig.getRogueTowerSlotCount();
			for (int i = 0; i < count; i++)
			{
				mClientPackRogueItemTowerPool.newItem().setTowerData(i < tempList.Count ? tempList[i] : null);
			}
		}
		autoGridHorizontal(mDragView);
		if (mListLeftPosValid.get(mCurViewType))
		{
			mDragView.setLeftInParent(mListLeftPos.get(mCurViewType) + mDragView.getLeft().x);
		}
		else
		{
			mDragView.setLeftCenterToParentLeftCenter();
		}
		refreshCoinColor();
		mStoneTowerOff.getOrAddComponent<COMWindowDrag>().setActive(true);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerTagClick()
	{
		showTypeItems(CLIENT_PACK_VIEW.TOWER, false);
	}
	protected void showTypeItems(CLIENT_PACK_VIEW type, bool isInit)
	{
		if (!isInit)
		{
			mListLeftPosValid.set(mCurViewType, true);
			mListLeftPos.set(mCurViewType, mDragView.getLeftInParent() - mDragView.getLeft().x);
		}
		mCurViewType = type;
		refresh();
	}
	protected void onShowPanelClick()
	{
		setPanelVisible(true);
	}
	protected void onHidePanelClick()
	{
		setPanelVisible(false);
	}
	protected void onDragHover(IMouseEventCollect dragObj, Vector3 mousePos, bool hover)
	{
		mDeleteDisplayImage.SCALE(KEY_CURVE.BACK_OUT, hover ? 1.0f : 1.2f, hover ? 1.2f : 1.0f, 0.3f);
	}
	protected void onReceiveDrag(IMouseEventCollect dragObj, Vector3 mousePos, ref bool continueEvent)
	{
		if (dragObj is myUGUIObject dragUI && dragUI.getLayout().getScript() == mUIDraging)
		{
			CmdGlobalSellTowerRogue.execute(mUIDraging.getDragingTower());
		}
	}
	protected void onDragStart(ComponentOwner dragObj, TouchPoint touchPoint, ref bool allowDrag)
	{
		// 开始拖拽就只是拖拽放置,闪烁表示点击放置,所以不再闪烁
		mTowerDefenceSystem.cmdSelectItemOwned(null);
		CmdGlobalSelectTowerScene.execute(null);
		mBattleScene.getMouseGridIndexAndPoint(touchPoint.getCurPosition(), out _, out Vector3 point);
		mDragingTower = CmdGlobalCreateTower.execute(mExcelTower.query(STONE_TOWER_ID), point);
		mDragingTower.setPosition(generateOffset(point));
		mDragValid = false;
		LT.HIDE<UITowerInfo>();
		mStoneTowerOn.setActive(true);
		setPanelVisible(false);
	}
	protected void onDraging(ComponentOwner dragObj, Vector3 mousePos)
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
			mDragValid = mBattleScene.canReplaceTower() && mHoverTower.canOperate();
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
	protected void onDragEnd(ComponentOwner dragObj, Vector3 mousePos, bool cancel)
	{
		setPanelVisible(true);
		mHoverTower?.showSelect(false);
		mHoverTower = null;
		mBattleScene.setGridMaterial(-1, null);
		mBattleScene.showTowerRange(null);
		mBattleScene.hideAllPreviewPath();
		mStoneTowerOn.setActive(false);
		if (cancel)
		{
			if (mDragingTower != null)
			{
				CmdGlobalDestroyTower.execute(mDragingTower);
				mDragingTower = null;
			}
			return;
		}
		// 检查货币是否足够
		bool allowPut = mDragValid;
		int buildCost = mExcelTower.getRogueNextLevelCost(mExcelTower.query(STONE_TOWER_ID), 0);
		if (allowPut && mTowerDefenceSystem.getGoldCoinRogue() < buildCost)
		{
			tip("道具不足，需要{0}", buildCost.IToS());
			allowPut = false;
		}
		if (allowPut)
		{
			CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() - buildCost);
			mTowerDefenceSystem.cmdPutTower(mDragingTower, mCurDragingIndex, 0);
		}
		else
		{
			CmdGlobalDestroyTower.execute(mDragingTower);
		}
		mDragingTower = null;
	}
}