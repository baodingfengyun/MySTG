using System.Collections.Generic;
using static FrameBaseHotFix;
using static MathUtility;
using static GBR;

public class CharacterPortal : Character
{
	protected EDMapPortal mTableData;
	protected int mGridIndex;
	protected int mSequenceIndex;
	public override void resetProperty()
	{
		base.resetProperty();
		mTableData = null;
		mGridIndex = 0;
		mSequenceIndex = 0;
	}
	public void setModel(string prefabPath)
	{
		mAvatar.loadModelAsync(prefabPath);
	}
	public virtual void setGridIndexAndPosition(int index)
	{
		mGridIndex = index;
		setPosition(mBattleScene.getGridPosition(index));
	}
	public virtual void initData(EDMapPortal mapPortal)
	{
		mEventSystem?.unlistenEvent(this);
		mEventSystem.listenEvent<EventMonsterGridChange>(onMonsterGridChange, this);
		mTableData = mapPortal;
	}
	public void setSequenceIndex(int value) { mSequenceIndex = value; }
	private void onMonsterGridChange(EventMonsterGridChange param)
	{
		if (param.mMonster.getComMovement().getGridIndex() != mGridIndex)
		{
			return;
		}
		List<int> endList = mTableData.mEndList;
		if (mTableData.mEndRule == PORTAL_RULE.RANDOM)
		{
			int end = endList[randomInt(0, endList.Count - 1)];
			param.mMonster.setPosition(mBattleScene.getGridPosition(end));
			param.mMonster.getComMovement().regenerateRoadList();
		}
		else if (mTableData.mEndRule == PORTAL_RULE.SEQUENCE)
		{
			param.mMonster.setPosition(mBattleScene.getGridPosition(endList[mSequenceIndex]));
			param.mMonster.getComMovement().regenerateRoadList();
			mSequenceIndex = (mSequenceIndex + 1) % endList.Count;
		}
	}
	public override void destroy()
	{
		base.destroy();
		mEventSystem?.unlistenEvent(this);
	}
}