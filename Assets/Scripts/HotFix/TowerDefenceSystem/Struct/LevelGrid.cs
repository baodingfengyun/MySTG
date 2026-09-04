
// 一个场景格子上的数据,不包含渲染数据
public class LevelGrid : ClassObject
{
	protected CharacterGame mMainCharacter;		// 该格子上放置的主要物品,如塔,英雄,可移除障碍
	protected CharacterPortal mCharacterPortal; // 该格子的传送门
	protected int mIndex;						// 格子下标
	protected GRID_STATE mState;				// 该格子的可行走属性
	// 重置格子数据（放回对象池之前）
	public override void resetProperty()
	{
		base.resetProperty();
		mMainCharacter = null;
		mCharacterPortal = null;
		mIndex = -1;
		mState = GRID_STATE.NONE;
	}
	// 格子上的角色
	public CharacterGame getMainCharacater()					{ return mMainCharacter; }
	// 格子上的塔（假设是）
	public CharacterTower getTower()							{ return mMainCharacter as CharacterTower; }
	// 格子上的传送门
	public CharacterPortal getPortal()							{ return mCharacterPortal; }
	// 判断格子上是否有东西（角色或传送门）
	public bool hasItem()										{ return mMainCharacter != null || mCharacterPortal != null; }
	// 判断格子上是否有传送门
	public bool hasPortal()										{ return getPortal() != null; }
	// 获取格子的状态
	public GRID_STATE getState()								{ return mState; }
	// 获取格子的索引下标
	public int getIndex()										{ return mIndex; }
	// 放角色在格子上
	public void setMainCharacter(CharacterGame tower)			{ mMainCharacter = tower; }
	// 放传送门在格子上
	public void setPortal(CharacterPortal portal)				{ mCharacterPortal = portal; }
	// 设置格子状态
	public void setState(GRID_STATE state)						{ mState = state; }
	// 设置格子索引下标
	public void setIndex(int index)								{ mIndex = index; }
}