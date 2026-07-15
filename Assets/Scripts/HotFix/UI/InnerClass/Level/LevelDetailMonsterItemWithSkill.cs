using static GBR;

public class LevelDetailMonsterItemWithSkill : LevelDetailMonsterItem
{
	protected myUGUIObject mMask;
	protected myUGUIImage mMaskSkillIcon;
	public LevelDetailMonsterItemWithSkill(IWindowObjectOwner script) : base(script){}
    protected override void assignWindowInternal()
    {
		base.assignWindowInternal();
        newObject(out mMask, "Mask");
		newObject(out mMaskSkillIcon, mMask, "MaskSkillIcon");
	}
	public override void setData(int monsterID)
	{
		base.setData(monsterID);
		if (mMaskSkillIcon.setActive(mTableData.mSkill.Count > 0))
		{
			mMaskSkillIcon.setSpriteName(mExcelMonsterSkill.query(mTableData.mSkill[0]).mIcon);
		}
	}
}
