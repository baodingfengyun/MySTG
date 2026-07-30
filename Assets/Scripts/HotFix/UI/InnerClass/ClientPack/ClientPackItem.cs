using UnityEngine;
using static StringUtility;

// 界面上显示的塔的图标,可拖拽出去创建塔
public class ClientPackItem : WindowRecyclableUGUI
{
	protected myUGUIText mCost;
	protected myUGUIObject mCostIcon;
	protected myUGUIText mCount;
	protected myUGUIObject mDisableMask;
	protected myUGUIImageSimple mCD;
	protected myUGUIText mItemNameText;
	protected myUGUIObject mTargetTypes;
	protected myUGUIObject mTargetFly;
	protected myUGUIObject mTargetGround;
	protected myUGUIObject mTargetAll;
	protected myUGUIObject mClickArea;			// 因为新手引导中有单独的点击而不拖拽的操作,所以将点击响应和拖拽响应放到不同的节点中
	protected myUGUIObject mDragArea;			// 因为新手引导中有单独的点击而不拖拽的操作,所以将点击响应和拖拽响应放到不同的节点中
	protected Vector3 mOriginPosition;
	protected Color mOriginCostColor;
	protected bool mIsInfinite;
	public ClientPackItem(IWindowObjectOwner script) : base(script) { }
    protected override void assignWindowInternal()
    {
        newObject(out mCost, "Cost");
		newObject(out mCostIcon, "CostIcon");
		newObject(out mCount, "Count");
		newObject(out mDisableMask, "DisableMask");
		newObject(out mCD, "CD");
		newObject(out mItemNameText, "ItemNameText");
		newObject(out mTargetTypes, "TargetTypes");
		newObject(out mTargetFly, mTargetTypes, "TargetFly");
		newObject(out mTargetGround, mTargetTypes, "TargetGround");
		newObject(out mTargetAll, mTargetTypes, "TargetAll");
		newObject(out mClickArea, "ClickArea");
		newObject(out mDragArea, "DragArea");
	}
	public override void init()
	{
		base.init();
		mDisableMask.registeCollider();
		mClickArea.registeCollider(onAreaClick);
		var dragCom = mDragArea.addComponent<COMWindowDrag>(false);
		dragCom.initDrag(Vector2.up, 82.0f.toRadian(), true, false);
		dragCom.setDragCallback(onDragStart, onDraging, onDragEnd);
		mOriginCostColor = mCost.getColor();
	}
	public override void reset()
	{
		base.reset();
		setQuality(0);
		mDisableMask.setActive(false);
		mCount.setActive(false);
		mCD.setActive(false);
		mItemNameText.setText(EMPTY);
		mIsInfinite = false;
		mOriginPosition = Vector3.zero;
	}
	public void setName(string name) { mItemNameText.setText(name, this); }
	public void setQuality(int quality)
	{
		;
	}
	public void positionAvailable()
	{
		mOriginPosition = mRoot.getPosition();
	}
	public void setSelected(bool selected)
	{
		if (selected)
		{
			mRoot.MOVE(KEY_CURVE.QUAD_IN, mRoot.getPosition(), mOriginPosition + new Vector3(0.0f, 30.0f, 0.0f), 0.1f);
		}
		else
		{
			mRoot.MOVE(mOriginPosition);
		}
	}
	public void setCount(int count)
	{
		mIsInfinite = count < 0;
		mCostIcon.setActive(mIsInfinite);
		mCost.setActive(mIsInfinite);
		mCount.setActive(!mIsInfinite);
		mCount.setText(count);
	}
	public void refreshCoinColor(bool enough)
	{
		if (!mCost.isActive())
		{
			return;
		}
		mCost.setColor(enough ? mOriginCostColor : Color.red);
	}
	public myUGUIObject getClickArea() { return mClickArea; }
	public myUGUIObject getDragArea() { return mDragArea; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected virtual void onAreaClick() {}
	protected virtual void onDragStart(ComponentOwner dragObj, TouchPoint touchPoint, ref bool allowDrag) {}
	protected virtual void onDraging(ComponentOwner dragObj, Vector3 mousePos) {}
	protected virtual void onDragEnd(ComponentOwner dragObj, Vector3 mousePos, bool cancel) {}
}