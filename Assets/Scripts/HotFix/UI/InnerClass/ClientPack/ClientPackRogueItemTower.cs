using UnityEngine;
using static GameUtilityHotFix;
using static GBR;

// 界面上显示的塔的图标,可拖拽出去创建塔,仅Rogue模式用
public class ClientPackRogueItemTower : ClientPackItemTower
{
	public ClientPackRogueItemTower(IWindowObjectOwner script) : base(script) { }
    protected override void assignWindowInternal()
    {
		base.assignWindowInternal();
        newObject(out mTowerIcon, "TowerIcon");
	}
	public override void setTowerData(EDTower towerData)
	{
		mCostIcon.setActive(towerData != null);
		mCost.setActive(towerData != null);
		if(towerData == null)
		{
			mCD.setActive(false);
			mDragArea.getOrAddComponent<COMWindowDrag>().setActive(false);
			mTowerIcon.setActive(false);
			setTargetType(TARGET_BEHAVIOUR_TYPE.NONE);
			return;
		}
		base.setTowerData(towerData);
		mCost.setText(mExcelTower.getRogueNextLevelCost(towerData, 0));
	}
	public void refreshCoinColor()
	{
		if(mTowerData == null)
		{
			return;
		}
		refreshCoinColor(mTowerDefenceSystem.getGoldCoinRogue() >= mExcelTower.getRogueNextLevelCost(mTowerData, 0));
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void onDragStart(ComponentOwner dragObj, TouchPoint touchPoint, ref bool allowDrag)
	{
		base.onDragStart(dragObj, touchPoint, ref allowDrag);
		if(allowDrag)
		{
			mUIClientPackRogue.safe()?.setPanelVisible(false);
		}
	}
	protected override void onDragEnd(ComponentOwner dragObj, Vector3 mousePos, bool cancel)
	{
		base.onDragEnd(dragObj, mousePos, cancel);
		mUIClientPackRogue.safe()?.setPanelVisible(true);
		if (cancel)
		{
			return;
		}
		// 检查货币是否足够
		bool allowPut = mDragValid;
		int buildCost = mExcelTower.getRogueNextLevelCost(mTowerData, 0);
		if (allowPut && mTowerDefenceSystem.getGoldCoinRogue() < buildCost)
		{
			tip("道具不足，需要{0}", buildCost.IToS());
			allowPut = false;
		}
		if (allowPut)
		{
			CmdGlobalSetGoldCoinRogue.execute(mTowerDefenceSystem.getGoldCoinRogue() - buildCost);
			mDragingTower.getTowerData().addUseCoin(buildCost);
			mTowerDefenceSystem.cmdPutTower(mDragingTower, mCurDragingIndex, 0);
		}
		else
		{
			CmdGlobalDestroyTower.execute(mDragingTower);
		}
		mDragingTower = null;
	}
}