using static GBR;

// 关卡详情里的怪物条目(可回收):显示怪物头像与类型标记,响应背景点击(当前空实现,留给子类/外部扩展)。
// setData 按怪物ID查表填充图标,类型图标为空时隐藏标记。
public class LevelDetailMonsterItem : WindowRecyclableUGUI
{
	protected myUGUIObject mBg;
	protected myUGUIImage mHead;
	protected myUGUIImage mSignType;
	protected myUGUIObject mSelect;
	protected EDMonster mTableData;
	public LevelDetailMonsterItem(IWindowObjectOwner script)
		: base(script) { }
	protected override void assignWindowInternal()
	{
		newObject(out mBg, "Bg");
		newObject(out mHead, "Head");
		newObject(out mSignType, "SignType");
		newObject(out mSelect, "Select");
	}
	public override void init()
	{
		base.init();
		mBg.registeCollider(onBgClick);
	}
	public virtual void setData(int monsterID)
	{
		mTableData = mExcelMonster.query(monsterID);
		mHead.setSpriteName(mTableData.mIcon);
		mSignType.setActive(!mTableData.mTypeIcon.isEmpty());
		mSignType.setSpriteName(mTableData.mTypeIcon);
	}
	public myUGUIObject getBg() { return mBg; }
	public int getMonsterID() { return mTableData.mID; }
	public void displaySelect(bool newSelect) { mSelect.setActive(newSelect); }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBgClick()
	{
	}
}
