using System;
using System.Collections.Generic;
using static FrameUtility;
using static GBR;

// Rogue战斗模式的逻辑：在 BattleModeBase 的公共规则上，补充肉鸽特有的数据与行为
// 相比基类多出来的四样东西，正对应"局内 Rogue 构筑"这条成长轴：
//   mGoldCoin        —— 抽卡/买塔用的局内货币，每波结束按 WaveConfig.mRewardCurrency 发放
//   mAllowUseTowerList —— 当前可用的塔（初始只有 Lv1*Star1），通过天赋卡解锁新塔
//   mAllowSelectPropList —— 本波三选一卡池的候选条目（AllowSelectProp = 表格数据 + 是否已用）
//   mSelectedTalentDic —— 玩家已选中的天赋词条 → 叠层次数（Buff 强度靠次数堆叠）
// 覆写的三个 cmd* 把"放塔/卖塔/选卡"派发给 Rogue 版命令；cmdWaveFinish 先发奖励再走基类流程。
public class BattleModeRogue : BattleModeBase
{
	protected Dictionary<EDTowerTalent, int> mSelectedTalentDic = new();	// 从卡池中选择的随机词条列表
	protected List<AllowSelectProp> mAllowSelectPropList = new();			// 随机待选卡池
	protected HashSet<EDTower> mAllowUseTowerList = new();					// 可以使用的塔
	protected int mGoldCoin;												// 金币数量,选择防御塔时需要消耗,每次通关会获得
	protected bool mIsRogueSelected;										// 这波是否选过了卡
	public BattleModeRogue()
	{
		mMode = BATTLE_MODE.ROGUE_LIKE;
	}
	public override void clear()
	{
		base.clear();
		mSelectedTalentDic.Clear();
		UN_CLASS_LIST(mAllowSelectPropList);
		mAllowUseTowerList.Clear();
		mGoldCoin = 0;
		mIsRogueSelected = false;
	}
    public override void initLevel()
    {
        base.initLevel();
		foreach (EDTower item in mExcelTower.queryAll())
		{
			mAllowUseTowerList.addIf(item, item.mLevel == 1 && item.mStar == 1);
		}
    }
    public override void setLevelData(EDLevel levelData)
	{
		base.setLevelData(levelData);
		setGoldCoin(levelData.mInitCurrency);
	}
	public void addTaltent(EDTowerTalent talentData) { mSelectedTalentDic.addOrIncreaseValue(talentData, 1); }
	public Dictionary<EDTowerTalent, int> getTalentDic() { return mSelectedTalentDic; }
	public int getGoldCoin() { return mGoldCoin; }
	public void setGoldCoin(int gold) { mGoldCoin = gold; }
	public bool isAllPropUsed()
	{
		return !mAllowSelectPropList.contains(item => !item.mUsed);
	}
	public List<AllowSelectProp> getAllowSelectPropList() { return mAllowSelectPropList; }
	public void clearAllowSelectPropList() { UN_CLASS_LIST(mAllowSelectPropList); }
	public int getAllowSelectPropListCount() { return mAllowSelectPropList.count(); }
	public void setAllowSelectPropList(List<ExcelData> allowList)
	{
		UN_CLASS_LIST(mAllowSelectPropList);
		foreach (ExcelData data in allowList.safe())
		{
			var prop = mAllowSelectPropList.add(CLASS<AllowSelectProp>());
			prop.mPropData = data;
			prop.mUsed = false;
		}
	}
	public HashSet<EDTower> getAllowUseTowerList() { return mAllowUseTowerList; }
	public override Type getSetupTowerProcedure()
	{
		return typeof(GameSceneBattleGamingTowerSetupRogue);
	}
	public override void cmdSellTower(CharacterTower tower) { CmdGlobalSellTowerRogue.execute(tower); }
	public override void cmdSelectItemOwned(WindowRecyclableUGUI item) { CmdGlobalSelectItemOwnedRogue.execute(item as ClientPackRogueItemTower); }
	public override void cmdPutTower(CharacterTower tower, int gridIndex, int propIndex) { CmdGlobalPutTowerRogue.execute(tower, gridIndex, propIndex); }
	public override void cmdWaveFinish()
	{
		CmdGlobalWaveFinishRogue.execute();
		base.cmdWaveFinish();
	}
	public bool isRogueSelected() { return mIsRogueSelected; }
	public void setRogueSelected(bool value) { mIsRogueSelected = value; }
}