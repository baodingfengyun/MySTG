using Obfuz;
using UnityEngine;
using static UnityUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIDraging.prefab
// 显示拖拽物品的界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIDraging : LayoutScript
{
    protected myUGUIObject mDragRoot;
    // auto generate member end
    protected CharacterTower mDragingTower;
	public UIDraging()
	{
		// auto generate constructor start
		// auto generate constructor end
		mNeedUpdate = false;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mDragRoot, "DragRoot");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		// auto generate init end
		mDragRoot.registeCollider(true);
		var dragCom = mDragRoot.addComponent<COMWindowDrag>(false);
		dragCom.setDragEndCallback(onEndDragObjectItem);
		dragCom.setStartDragThreshold(1.0f);
	}
	public override void onGameState()
	{
		base.onGameState();
		mDragRoot.setActive(false);
		mDragingTower = null;
	}
	public void setDragingItem(CharacterTower tower, TouchPoint touchPoint)
	{
		mDragRoot.setActive(true);
		mDragRoot.setPosition((Vector2)touchPoint.getCurPosition() - getHalfScreenSize());
		mDragRoot.activeComponent<COMWindowDrag>();
		mDragRoot.getComponent<COMWindowDrag>().startDrag(touchPoint, Vector3.zero);
		mDragingTower = tower;
	}
	public CharacterTower getDragingTower() { return mDragingTower; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onEndDragObjectItem(ComponentOwner dragObj, Vector3 mousePos, bool cancel)
	{
		mDragRoot.setActive(false);
	}
}