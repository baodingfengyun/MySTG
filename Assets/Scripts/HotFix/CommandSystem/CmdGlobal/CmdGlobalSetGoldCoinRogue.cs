using static GBR;
using static FrameBaseHotFix;

// 设置当前金币,Rogue模式
public class CmdGlobalSetGoldCoinRogue
{
	public static void execute(int coin, bool noEvent = false)
	{
		if(!noEvent)
		{
			using var a = new ClassScope<EventBuildCoinChange>(out var eventParam);
			eventParam.mOldCoin = mTowerDefenceSystem.getGoldCoinRogue();
			eventParam.mNewCoin = coin;
			mEventSystem.pushEvent(eventParam);
		}
		mTowerDefenceSystem.setGoldCoinRogue(coin);
		mUIGaming.safe()?.refreshCoin();
		mUIClientPackRogue.safe()?.refreshCoinColor();
	}
}