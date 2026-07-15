using static MathUtility;
using static FrameBaseHotFix;
using static GDR;

// 管理怪物的说话
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
				
			}
		}
	}
}