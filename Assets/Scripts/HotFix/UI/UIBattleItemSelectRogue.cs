using Obfuz;
using System.Collections.Generic;
using UnityEngine;
using static FrameBaseHotFix;
using static FrameUtility;
using static MathUtility;
using static GBR;
using static GDR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIBattleItemSelectRogue.prefab
// rogue模式中的抽卡列表界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIBattleItemSelectRogue : LayoutScript
{
	protected myUGUIObject mMask;
	protected myUGUIObject mBackground;
	protected myUGUIObject mRefresh;
	protected myUGUIText mPrice;
	protected myUGUIObject mCollapse;
	protected myUGUIObject mExpand;
	protected myUGUIObject mAnimMask;
	protected WindowStructPool<BattleItemSelectRogue> mBattleItemSelectRoguePool;
    // auto generate member end
    protected Animator mRootAnimator;
	protected Animator mBackgroundAnimator;
	public UIBattleItemSelectRogue()
	{
		// auto generate constructor start
		mBattleItemSelectRoguePool = new(this);
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out mBackground, "Background");
		newObject(out mRefresh, mBackground, "Refresh");
		newObject(out mPrice, mRefresh, "Price");
		newObject(out myUGUIObject buttonCard, "ButtonCard", false);
		newObject(out mCollapse, buttonCard, "Collapse");
		newObject(out mExpand, buttonCard, "Expand");
		newObject(out mAnimMask, "AnimMask");
		newObject(out myUGUIObject propListRoot, mBackground, "PropListRoot", false);
		mBattleItemSelectRoguePool.assignTemplate(propListRoot, "BattleItemSelectRogue");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider();
		mRefresh.registeCollider(onRefreshClick);
		mCollapse.registeCollider(onCollapseClick);
		mExpand.registeCollider(onExpandClick);
		mAnimMask.registeCollider();
		// auto generate init end
        mRootAnimator = mRoot.getGameObject().GetComponent<Animator>();
        mBackgroundAnimator = mBackground.getGameObject().GetComponent<Animator>();
	}
	public override void onGameState()
	{
		base.onGameState();
        setListVisible(true);
        mAnimMask.setActive(false);
        mPrice.setText(RANDOM_TOWER_COST_COIN);
		setPropList(mTowerDefenceSystem.getAllowSelectPropListRogue());
	}
	public void setPropList(List<AllowSelectProp> propList)
	{
        mBattleItemSelectRoguePool.unuseAll();
		if (!propList.isEmpty())
		{
			for (int i = 0; i < propList.Count; ++i)
			{
                mBattleItemSelectRoguePool.newItem().setPropData(propList[i].mPropData, i);
			}
            mBattleItemSelectRoguePool.autoGrid(new Vector2(100, 0), HORIZONTAL_DIRECTION.CENTER);
		}

        // 动画
        mBattleItemSelectRoguePool.For(item => item.playInitAnim());
		mRootAnimator.SetInteger("SelectAnim", 1);
		mBackgroundAnimator.SetInteger("SelectAnim", 1);
	}
	public void notifyStartFight()
	{
		setListVisible(false);
	}
	public void setListVisible(bool visible)
	{
		mCollapse.setActive(visible);
		mExpand.setActive(!visible);
		mBackground.setActive(visible);
	}
	public void setActiveOnlyPropItem(int index, out Vector3 pos)
	{
		if (index >= 0)
		{
			int curIndex = 0;
			foreach (BattleItemSelectRogue item in mBattleItemSelectRoguePool.getUsedList())
			{
				if (curIndex++ == index)
				{
					pos = item.getRoot().getWorldPosition();
					mGlobalTouchSystem.setActiveOnlyObject(item.getRoot());
					return;
				}
			}
		}
		else
		{
			mGlobalTouchSystem.setActiveOnlyObject(null);
			foreach (BattleItemSelectRogue item in mBattleItemSelectRoguePool.getUsedList())
			{
				mGlobalTouchSystem.addActiveOnlyObject(item.getRoot());
			}
		}
		pos = Vector3.zero;
	}
	public void hideItemList(int index)
	{
		mAnimMask.setActive(true);
		foreach (BattleItemSelectRogue each in mBattleItemSelectRoguePool.getUsedList().safe())
		{
			if (each.getIndex() == index)
			{
				each.playSelectAnim();
			}
			else
			{
				each.playNotSelectAnim();
			}
		}
		mRootAnimator.SetInteger("SelectAnim", 4);
		mBackgroundAnimator.SetInteger("SelectAnim", 4);
		delayCall(0.8f, close);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onCollapseClick()
	{
		setListVisible(false);
	}
	protected void onExpandClick()
	{
		setListVisible(true);
		// 卡池抽卡界面和手牌选中是互斥的,所以显示待选卡牌列表时需要取消选中手牌
		CmdGlobalSelectItemOwnedRogue.execute(null);
		// 动画
		mBattleItemSelectRoguePool.For(item => item.playInitAnim());
		mRootAnimator.SetInteger("SelectAnim", 1);
		mBackgroundAnimator.SetInteger("SelectAnim", 1);
	}
	protected void onRefreshClick()
	{
		CmdGlobalRandomPropListRogue.execute(RANDOM_TOWER_COST_COIN);
	}
}