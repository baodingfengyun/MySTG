using static MathUtility;
using static FrameBaseHotFix;
using static GDR;

// 怪物的台词组件：监听自己的 EventMonsterHPChange 事件，血量跨过 50% 阈值时
// 按 mDyingTalkProbability 概率触发一次"濒死台词"。用 lastHP>阈值 && curHP<阈值
// 判断"跨线"，保证整场只触发一次。台词文本走 ExcelLocalization(表中存 LocID)。
// 注：当前随机命中后的分支体是空的，疑似触发效果尚未填充——仅标注，未改代码。
public class COMMonsterTalk : GameComponent
{
	protected CharacterMonster mMonster;	// 怪物对象
	public override void init(ComponentOwner owner)
	{
		base.init(owner);
		mMonster = mComponentOwner as CharacterMonster;
		mEventSystem.listenEvent<EventMonsterHPChange>(mMonster.getGUID(), onHPChanged, this);
	}
	public override void destroy()
	{
		base.destroy();
		mEventSystem?.unlistenEvent(this);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onHPChanged(EventMonsterHPChange eventParam)
	{
		// 当血量降低到一半时触发一次说话
		int hpThreshold = (int)(0.5f * mMonster.getMaxHP());
		if (eventParam.mLastHP > hpThreshold && eventParam.mCurHP < hpThreshold)
		{
			EDMonster monsterTableData = mMonster.getMonsterData().mTableData;
			if (randomHit(monsterTableData.mDyingTalkProbability, ODDS_SCALE))
			{
				// 疑似未完成：按概率命中后这里本应播放/显示濒死台词，当前为空实现。
				// 仅标注，未改代码。
			}
		}
	}
}