using static GBR;

// auto generate classname start
// generate from:Assets/GameResources/UI/UIPrefab/UIMonsterQueue.prefab
// 出场的boss信息
public class MonsterBossQueueItem : WindowRecyclableUGUI
// auto generate classname end
{
	// auto generate member start
	protected myUGUIImageSimple mHpBar;
	protected myUGUIImage mBossIcon;
	protected myUGUIImage mTypeIcon;
	// auto generate member end
	protected EDMonster mMonsterData;
	protected CharacterMonster mMonster;
	public MonsterBossQueueItem(IWindowObjectOwner script) :base(script){ }
	protected override void assignWindowInternal()
	{
		// auto generate assignWindowInternal start
		newObject(out mHpBar, "HpBar");
		newObject(out mBossIcon, "BossIcon");
		newObject(out mTypeIcon, "TypeIcon");
		// auto generate assignWindowInternal end
	}
	public override void reset()
	{
		base.reset();
		mMonster = null;
		mMonsterData = null;
		updateHpBar(1.0f);
	}
	public void setData(EDMonster tableData)
	{
		mMonsterData = tableData;
		mBossIcon.setSpriteName(mMonsterData.mIcon);
		mTypeIcon.setSpriteName(mMonsterData.mTypeIcon);
	}
	public bool trySetCharacter(CharacterMonster monster)
	{
		if(monster == null || mMonsterData == null || monster.getMonsterData().mTableData.mID != mMonsterData.mID || mMonster != null)
		{
			return false;
		}
		mMonster = monster;
		return true;
	}
	public void updateHpBar(float hp)
	{
		mHpBar.setFillPercent(hp);
	}
	public long getMonsterGUID()
	{
		return mMonster?.getGUID() ?? -1;
	}
}