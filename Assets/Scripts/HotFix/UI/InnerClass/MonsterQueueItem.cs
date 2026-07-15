
// auto generate classname start
// generate from:Assets/GameResources/UI/UIPrefab/UIMonsterQueue.prefab
// 出场的小怪信息
public class MonsterQueueItem : WindowRecyclableUGUI
// auto generate classname end
{
	// auto generate member start
	protected myUGUIImage mSkillIcon;
	protected myUGUIImage mTypeIcon;
	protected myUGUIImage mStrength;
	protected myUGUIText mNum;
	// auto generate member end
	public MonsterQueueItem(IWindowObjectOwner script): base(script) {}
    protected override void assignWindowInternal()
    {
		// auto generate assignWindowInternal start
		newObject(out mSkillIcon, "SkillIcon");
		newObject(out mTypeIcon, "TypeIcon");
		newObject(out mStrength, "Strength");
		newObject(out mNum, "Num");
		// auto generate assignWindowInternal end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mRoot.registeCollider(onRootClick);
		// auto generate init end
	}
	public void setIcon(EDMonster monster, int count)
	{
		mSkillIcon.setSpriteName(monster.mIcon);
		mTypeIcon.setSpriteName(monster.mTypeIcon);
		mStrength.setActive(monster.mStrength == MONSTER_STRENGTH.ELITE);
		mTypeIcon.setActive(!monster.mTypeIcon.isEmpty());
		mNum.setText("x" + count.IToS());
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onRootClick()
	{
		;
	}
}