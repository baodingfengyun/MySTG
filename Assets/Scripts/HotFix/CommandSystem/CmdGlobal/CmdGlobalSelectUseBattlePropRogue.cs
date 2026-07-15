using static GBR;
using static FrameBaseHotFix;
using static GameUtilityHotFix;

// 选择了一个卡列表中的天赋,Rogue模式
public class CmdGlobalSelectUseBattlePropRogue
{
	public static void execute(int index)
	{
		if (mTowerDefenceSystem.getAllowSelectPropListRogue().Count <= index)
		{
			tip("没有词条可以选择");
			return;
		}
		ExcelData propData = mTowerDefenceSystem.getAllowSelectPropListRogue()[index].mPropData;
		if (propData is not EDTowerTalent talentData)
		{
			return;
		}

		// 使天赋生效
		CmdGlobalOwnedPropAddRogue.execute(talentData);

		// 发送事件
		using var a = new ClassScope<EventSelectRogueProp>(out var eventParam);
		eventParam.mData = talentData;
		mEventSystem.pushEvent(eventParam);

		mTowerDefenceSystem.getBattleModeRogue().setRogueSelected(true);
		mTowerDefenceSystem.clearAllowSelectPropListRogue();
		mUIBattleItemSelectRogue.safe()?.hideItemList(index);
	}
}