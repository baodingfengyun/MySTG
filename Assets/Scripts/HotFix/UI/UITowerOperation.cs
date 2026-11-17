using Obfuz;
using UnityEngine;
using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static UnityUtility;
using static GBR;
using static GDR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UITowerOperation.prefab
// ===== 防御塔操作浮层：点选场景中一座塔后，在其头顶弹出的"升级 / 拆除"小面板 =====
// 【定位】它跟着塔走(setTowerPosition 把世界坐标转屏幕坐标)，不是常驻界面。
// 【三个操作】
//   ① 升级(onUpgradeClick)：Rogue 模式走 CmdGlobalUpgradeLevelTowerRogue，升完刷新按钮+塔信息；
//   ② 拆除(onSellClick)：二次确认弹窗后 cmdSellTower，按 mUseCoin × 比例退钱；
//   ③ 遮罩点击(onMaskClick)：CmdGlobalSelectTowerScene(null) 取消选中，面板消失。
// 【按钮状态 refreshButtonState】比较"升级花费"与"当前金币"决定显示可点升级还是上锁图标；
//   满级(无下一阶)或 Rogue 升级已达上限则都隐藏。开始战斗后(isFighting)禁止卖塔。
// 【金币体系】Rogue 模式读 getGoldCoinRogue；免费升级(getFreeUpModeLevel)时花费显示为 0。
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UITowerOperation : LayoutScript
{
	protected myUGUIObject mMask;
	protected myUGUIObject mPanel;
	protected myUGUIObject mUpgrade;
	protected myUGUIText mCoinCountText;
	protected myUGUIObject mUpgradeLock;
	protected myUGUIText mLockCoinCountText;
	protected myUGUIObject mSell;
	protected myUGUIText mSellCoinCountText;
	protected myUGUIObject mLevelTextBg;
	protected myUGUIText mLevelText;
    // auto generate member end
    public UITowerOperation()
	{
		// auto generate constructor start
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out mPanel, "Panel");
		newObject(out mUpgrade, mPanel, "Upgrade");
		newObject(out mCoinCountText, mUpgrade, "CoinCountText");
		newObject(out mUpgradeLock, mPanel, "UpgradeLock");
		newObject(out mLockCoinCountText, mUpgradeLock, "LockCoinCountText");
		newObject(out mSell, mPanel, "Sell");
		newObject(out mSellCoinCountText, mSell, "SellCoinCountText");
		newObject(out mLevelTextBg, mPanel, "LevelTextBg");
		newObject(out mLevelText, mLevelTextBg, "LevelText");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mUpgrade.registeCollider(onUpgradeClick);
		mUpgradeLock.registeCollider(onUpgradeLockClick);
		mSell.registeCollider(onSellClick);
		// auto generate init end
		mMask.registeCollider(onMaskClick, true);
	}
	public void setTowerPosition(Vector3 worldPos)
	{
		// 设置界面显示位置
		mPanel.setPosition(worldToScreen(worldPos));
	}
	public override void onGameState()
	{
		base.onGameState();
		// 开始游戏后,不能卖塔
		bool showSell = !mTowerDefenceSystem.getBattleModeInstance().isFighting();
		CharacterTower selectTower = mTowerDefenceSystem.getSelectedTowerScene();
		mSell.setActive(showSell && selectTower != null && selectTower.canOperate());
		refreshButtonState();
	}
	public void refreshButtonState()
	{
		CharacterTower tower = mTowerDefenceSystem.getSelectedTowerScene();
		if (tower == null)
		{
			return;
		}
		CharacterTowerData towerData = tower.getTowerData();
		EDTower towerTabelData = towerData.mTableData;
		BATTLE_MODE mode = mTowerDefenceSystem.getBattleMode();
		mLevelTextBg.setActive(mode == BATTLE_MODE.ROGUE_LIKE);
		int cost = 0;
		int owned = 0;
		int level = towerData.getBattleLevel();
		bool activeUpgrade = false;
		if (mode == BATTLE_MODE.ROGUE_LIKE)
		{
			var upgradeConfigCost = mExcelTower.getRogueNextLevelCost(towerTabelData, level);
			activeUpgrade = upgradeConfigCost != 0;
			cost = towerData.getFreeUpModeLevel() ? 0 : upgradeConfigCost;
			owned = mTowerDefenceSystem.getGoldCoinRogue();
		}
		bool levelMax = tower.getNextStarData() == null;
		mLevelText.setText("Lv" + level.IToS());
		if (!levelMax && activeUpgrade)
		{
			bool enoughCost = owned >= cost;
			mUpgrade.setActive(enoughCost);
			mUpgradeLock.setActive(!enoughCost);
			mCoinCountText.setText(cost);
			mLockCoinCountText.setText(cost);
		}
		else
		{
			// 如果mUpgrade没显示说明没法再升级了，所以lock图标也不显示
			mUpgrade.setActive(false);
			mUpgradeLock.setActive(false);
		}
		// 计算卖出价格
		int sellCount = 0;
		if (mode == BATTLE_MODE.ROGUE_LIKE)
		{
			sellCount = (int)(towerData.mUseCoin * ROGUE_MODE_SELL_TOWER_PERCENT);
		}
		mSellCoinCountText.setText(sellCount);
	}
	public void addActiveOnlyUpgrade()
	{
		mGlobalTouchSystem.addActiveOnlyObject(mUpgrade);
	}
	public void setActiveOnlyUpgrade(out Vector3 pos)
	{
		mGlobalTouchSystem.setActiveOnlyObject(mUpgrade); 
		pos = mUpgrade.getWorldPosition();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onMaskClick()
	{
		CmdGlobalSelectTowerScene.execute(null);
	}
	protected void onUpgradeLockClick()
	{
		CharacterTower tower = mTowerDefenceSystem.getSelectedTowerScene();
		if (tower == null)
		{
			return;
		}
		BATTLE_MODE mode = mTowerDefenceSystem.getBattleMode();
		if (mode == BATTLE_MODE.ROGUE_LIKE)
		{
			int cost = mExcelTower.getRogueNextLevelCost(tower.getTowerData().mTableData, tower.getTowerData().getBattleLevel());
			tip("道具不足，需要{0}", cost.IToS());
		}
	}
	protected void onUpgradeClick()
	{
		CharacterTower tower = mTowerDefenceSystem.getSelectedTowerScene();
		BATTLE_MODE mode = mTowerDefenceSystem.getBattleMode();
		if (mode == BATTLE_MODE.ROGUE_LIKE)
		{
			CmdGlobalUpgradeLevelTowerRogue.execute(tower);
		}
		refreshButtonState();
		mUITowerInfo.setTower(tower);
	}
	protected void onSellClick()
	{
		dialogYesNo("是否拆除此防御塔？", () =>
		{
			if (mTowerDefenceSystem.getSelectedTowerScene() == null)
			{
				return;
			}
			mTowerDefenceSystem.cmdSellTower(mTowerDefenceSystem.getSelectedTowerScene());
		});
	}
}