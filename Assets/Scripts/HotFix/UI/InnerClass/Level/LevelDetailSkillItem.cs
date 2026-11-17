using static GBR;

// 关卡详情里的怪物技能条目(可回收):按技能ID查表,填充技能图标、描述(带本地化参数)与名称。
public class LevelDetailSkillItem : WindowRecyclableUGUI
{
	protected myUGUIObject mMask;
	protected myUGUIImage mIcon;
	protected myUGUIText mDesc;
	protected myUGUIText mName;
	public LevelDetailSkillItem(IWindowObjectOwner script)
		: base(script) { }
    protected override void assignWindowInternal()
    {
        newObject(out mMask, "Mask");
		newObject(out mIcon, mMask, "Icon");
		newObject(out mDesc, "Desc");
		newObject(out mName, "Name");
	}
	public void setData(int skillID)
	{
		EDMonsterSkill tableData = mExcelMonsterSkill.query(skillID);
		mIcon.setSpriteName(tableData.mIcon);
		mDesc.setText(tableData.mDescriptionID, this);
		mName.setText(tableData.mName, this);
	}
}