using UnityEngine;
using static FrameDefine;

// auto generate classname start
// generate from:Assets/GameResources/UI/UIPrefab/UIHPBar.prefab
// 怪物血条显示
public class MonsterHPBar : WindowRecyclableUGUI
// auto generate classname end
{
	// auto generate member start
	protected myUGUIObject mVisibleNode;
	protected myUGUIImageSimple mBar;
	// auto generate member end
	public MonsterHPBar(IWindowObjectOwner script) : base(script) { }
    protected override void assignWindowInternal()
    {
		// auto generate assignWindowInternal start
		newObject(out mVisibleNode, "VisibleNode");
		newObject(out mBar, mVisibleNode, "Bar");
		// auto generate assignWindowInternal end
	}
	public override void reset()
	{
		base.reset();
		setPercent(1.0f);
	}
	public void setPercent(float percent)
	{
		mBar.setFillPercent(percent);
		mVisibleNode.setPosition(percent > 0.0f && percent < 1.0f ? Vector3.zero : FAR_POSITION);
	}
}