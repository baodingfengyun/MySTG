using static GBR;

// 带技能的怪物条目:在基础怪物条目上叠加一个"技能标记"图标,
// 若怪物配置了技能则显示其首个技能图标,否则隐藏。
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
