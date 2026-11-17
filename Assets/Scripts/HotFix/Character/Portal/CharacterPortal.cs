using System.Collections.Generic;
using static FrameBaseHotFix;
using static MathUtility;
using static GBR;

// 传送门实体:地图上的特殊格子,怪物走到此格时会被"传送"到配置的出口点并重新寻路。
// 出口规则由 EDMapPortal 配置:RANDOM=从出口列表随机选一个,SEQUENCE=按序轮换(循环取模)。
// 通过监听 EventMonsterGridChange(怪物跨格事件)触发,只在怪物恰好停在传送门所在格时生效。
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
	// 怪物跨格事件回调:若怪物当前格不是本传送门格则忽略;否则按配置的出口规则把它送到出口格,
	// 并调用 regenerateRoadList 从新位置重新计算行进路线(SEQUENCE 模式发送后会推进序号)。
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