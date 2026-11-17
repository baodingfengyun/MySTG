using Obfuz;
using static FrameUtility;
using static GBR;
using static GDR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UITowerInfo.prefab
// ===== 防御塔信息面板：展示一座塔的图标/名称/星级/攻击/攻速/射程/描述 =====
// 【两种数据来源(两个重载)】
//   ① setTower(CharacterTower)：场景中已放置的塔——数值取实时实例(攻击、最终CD、射程按格数)；
//      并根据塔在屏幕左/右半边，把面板定位到另一侧(mPosLeft/mPosRight)避免遮挡。
//   ② setTower(EDTower, battleLevel)：配置表里的塔(选卡/图鉴预览)——数值取表+等级加成，固定放左侧。
// 【攻速显示】mRate 显示的是"攻击间隔(CD)"而非频率：有技能取技能表 CD，无技能显示 0；
//   实例版用 getFinalCD 叠 Buff 后的最终 CD。
// 【刷新 refreshForData】通用部分：图标、名字、等级、本地化描述、按 mStar 点亮星星。
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UITowerInfo : LayoutScript
{
	protected myUGUIObject mCloseMask;
	protected myUGUIObject mPanel;
	protected myUGUIText mTowerName;
	protected myUGUIObject[] mStar = new myUGUIObject[3];
	protected myUGUIImage mTowerIcon;
	protected myUGUIText mTowerDesc;
	protected myUGUIText mTowerLevel;
	protected TowerPropertyItem mDamageProperty;
	protected TowerPropertyItem mRangeProperty;
	protected TowerPropertyItem mRateProperty;
	protected myUGUIObject mPosLeft;
	protected myUGUIObject mPosRight;
    // auto generate member end
    public UITowerInfo()
	{
		// auto generate constructor start
		mDamageProperty = new(this);
		mRangeProperty = new(this);
		mRateProperty = new(this);
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mCloseMask, "CloseMask");
		newObject(out mPanel, "Panel");
		newObject(out myUGUIObject avatar, mPanel, "Avatar", false);
		newObject(out mTowerName, avatar, "TowerName");
		newObject(out myUGUIObject starRoot, avatar, "StarRoot", false);
		for (int i = 0; i < mStar.Length; ++i)
		{
			newObject(out mStar[i], starRoot, "Star" + i.IToS());
		}
		newObject(out mTowerIcon, avatar, "TowerIcon");
		newObject(out mTowerDesc, avatar, "TowerDesc");
		newObject(out mTowerLevel, avatar, "TowerLevel");
		newObject(out myUGUIObject property, mPanel, "Property", false);
		mDamageProperty.assignWindow(property, "DamageProperty");
		mRangeProperty.assignWindow(property, "RangeProperty");
		mRateProperty.assignWindow(property, "RateProperty");
		newObject(out mPosLeft, "PosLeft");
		newObject(out mPosRight, "PosRight");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mCloseMask.registeCollider(onCloseMaskClick);
		// auto generate init end
	}
	public void setTower(CharacterTower tower)
	{
		if (tower == null)
		{
			return;
		}
		bool atLeft = getMainCamera().getCamera().WorldToScreenPoint(tower.getWorldPosition()).x < mRoot.getSize().x * 0.5f;
		mPanel.setPosition(atLeft ? mPosRight.getPosition() : mPosLeft.getPosition());
		CharacterTowerData towerData = tower.getTowerData();
		EDTower tableData = towerData.mTableData;
		mDamageProperty.setValue(tower.getAttack());
		if (tableData.mSkill > 0)
		{
			mRateProperty.setValue(towerData.getFinalCD(mExcelTowerSkill.query(tableData.mSkill).mCD));
		}
		else
		{
			mRateProperty.setValue(0);
		}
		mRangeProperty.setValue(tower.getRange() / GRID_SIZE);
		refreshForData(tableData, towerData.getBattleLevel());
	}
	public void setTower(EDTower towerData, int battleLevel = 1)
	{
		mPanel.setPosition(mPosLeft.getPosition());
		int levelIncreaseAttack = mExcelTower.getTowerLevelAttack(towerData, battleLevel);
		if (towerData.mSkill > 0)
		{
			mDamageProperty.setValue(levelIncreaseAttack);
			mRateProperty.setValue(mExcelTowerSkill.query(towerData.mSkill).mCD);
		}
		else
		{
			mDamageProperty.setValue(0);
			mRateProperty.setValue(0);
		}
		mRangeProperty.setValue(towerData.mRange);
		refreshForData(towerData, battleLevel);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void refreshForData(EDTower towerData, int level)
	{
		mTowerIcon.setSpriteName(towerData.mIcon);
		mTowerName.setText(towerData.mName, this);
		mTowerLevel.setText("Lv" + level.IToS());
		mTowerDesc.setText(towerData.mLocalLang, this);
		for (int i = 0; i < TOWER_STAR_COUNT; ++i)
		{
			mStar[i].setActive(i < towerData.mStar);
		}
	}
	protected void onCloseMaskClick()
	{
        close();
    }
}