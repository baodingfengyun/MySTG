
// 一个场景格子上的数据,不包含渲染数据
public class LevelGrid : ClassObject
{
	protected CharacterGame mMainCharacter;		// 该格子上放置的主要物品,如塔,英雄,可移除障碍
	protected CharacterPortal mCharacterPortal; // 该格子的传送门
	protected int mIndex;						// 格子下标
	protected GRID_STATE mState;				// 该格子的可行走属性
	public override void resetProperty()
	{
		base.resetProperty();
		mMainCharacter = null;
		mCharacterPortal = null;
		mIndex = -1;
		mState = GRID_STATE.NONE;
	}
	public CharacterGame getMainCharacater()					{ return mMainCharacter; }
	public CharacterTower getTower()							{ return mMainCharacter as CharacterTower; }
	public CharacterPortal getPortal()							{ return mCharacterPortal; }
	public bool hasItem()										{ return mMainCharacter != null || mCharacterPortal != null; }
	public bool hasPortal()										{ return getPortal() != null; }
	public GRID_STATE getState()								{ return mState; }
	public int getIndex()										{ return mIndex; }
	public void setMainCharacter(CharacterGame tower)			{ mMainCharacter = tower; }
	public void setPortal(CharacterPortal portal)				{ mCharacterPortal = portal; }
	public void setState(GRID_STATE state)						{ mState = state; }
	public void setIndex(int index)								{ mIndex = index; }
}