using static GBR;

// 塔天赋信息条目(可回收列表项):展示"某塔的某天赋 + 已拥有数量"。
// setData 用塔类型取1级塔图标、天赋图标,并在数量>1时显示"X{数量}"。
public class TowerTalentInfoItem : WindowRecyclableUGUI
{
	protected myUGUIImage mTowerIcon;
	protected myUGUIImage mTalentIcon;
	protected myUGUIText mTalentInfoText;
	protected myUGUIText mCountText;
	public TowerTalentInfoItem(IWindowObjectOwner script) : base(script) { }
    protected override void assignWindowInternal()
    {
        newObject(out mTowerIcon, "TowerIcon");
		newObject(out mTalentIcon, "TalentIcon");
		newObject(out mTalentInfoText, "TalentInfoText");
		newObject(out mCountText, "CountText");
	}
	public void setData(EDTowerTalent towerTalentData, int count)
	{
		mTowerIcon.setSpriteName(mExcelTower.getTowerData(towerTalentData.mTowerType, 1).mIcon);
		mTalentIcon.setSpriteName(towerTalentData.mIcon);
		mTalentInfoText.setText("");
		mCountText.setText(count > 1 ? "X" + count.IToS() : null);
	}
}