using UnityEngine;
using static GBR;
using static FrameUtility;

// auto generate classname start
// generate from:Assets/GameResources/UI/UIPrefab/UIBattleItemSelectRogue.prefab
// 三选一抽卡界面里的一个"词条选项"条目：setPropData 填入道具数据与序号，
//   三种动画 playInitAnim(入场)/playSelectAnim(选中)/playNotSelectAnim(未选变暗)对应选择流程；
//   onRootClick 点击后由 UIBattleItemSelectRogue 统一处理应用与收尾。对象池复用。
public class BattleItemSelectRogue : WindowRecyclableUGUI
// auto generate classname end
{
	// auto generate member start
	protected myUGUIObject mAnim;
	protected myUGUIObject mLight;
	protected myUGUIImage mTowerIcon;
	protected myUGUIText mName;
	protected myUGUIText mDescription;
	protected myUGUIImage mTalentIcon;
	// auto generate member end
	protected Animator mAnimAnimator;
	protected ExcelData mData;
	protected int mIndex;
	public BattleItemSelectRogue(IWindowObjectOwner script)
		: base(script) { }
    protected override void assignWindowInternal()
    {
		// auto generate assignWindowInternal start
		newObject(out mAnim, "Anim");
		newObject(out mLight, mAnim, "Light");
		newObject(out mTowerIcon, mAnim, "TowerIcon");
		newObject(out mName, mAnim, "Name");
		newObject(out mDescription, mAnim, "Description");
		newObject(out mTalentIcon, mAnim, "TalentIcon");
		// auto generate assignWindowInternal end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mRoot.registeCollider(onRootClick);
		// auto generate init end
		mAnimAnimator = mRoot.tryGetUnityComponent<Animator>();
	}
	public override void reset()
	{
		base.reset();
		mData = null;
		mIndex = 0;
		mTowerIcon.setActive(false);
		mTalentIcon.setActive(false);
		mDescription.setActive(false);
		mLight.setActive(false);
	}
	public void setPropData(ExcelData data, int index)
	{
		mData = data;
		mIndex = index;
		if (mData is EDTowerTalent talentData)
		{
			mName.setText(talentData.mName, this);
			mDescription.setActive(true);
            mDescription.setText(TowerTalentDescRegister.getDescLocalized(talentData.mID));
            mTowerIcon.setActive(true);
			mTowerIcon.setSpriteName(mExcelTower.getTowerData(talentData.mTowerType, 1).mIcon);
			mTalentIcon.setActive(true);
			mTalentIcon.setSpriteName(talentData.mIcon);
		}
	}
	public void playInitAnim()
	{
		mAnim.setActive(false);
		long assignID = mAssignID;
		delayCall(0.1f * mIndex, () =>
		{
			if (assignID != mAssignID)
			{
				return;
			}
			mAnim.setActive(true);
			mAnimAnimator.SetInteger("SelectAnim", 1);
		});
	}
	public void playSelectAnim()
	{
		mAnimAnimator.SetInteger("SelectAnim", 2);
	}
	public void playNotSelectAnim()
	{
		mAnimAnimator.SetInteger("SelectAnim", 3);
	}
	public int getIndex() { return mIndex; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onRootClick()
	{
		mLight.setActive(true);
		CmdGlobalSelectUseBattlePropRogue.execute(mIndex);
	}
}