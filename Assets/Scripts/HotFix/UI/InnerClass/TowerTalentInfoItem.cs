using static GBR;

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