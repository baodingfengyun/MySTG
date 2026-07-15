using UnityEngine;
using static UnityUtility;

// 伤害数字基类
public class DamageNumber : WindowRecyclableUGUI
{
	protected myUGUINumber mNumber;
	protected myUGUINumber mCriticalNumber;
	protected myUGUIImageSimple mCritical;
	protected bool mIsCritical;
	public DamageNumber(IWindowObjectOwner parent):base(parent)
	{
		mChangePositionAsInvisible = true;
	}
    protected override void assignWindowInternal()
    {
        newObject(out mNumber, "Number", false);
		if (mNumber != null)
		{
			newObject(out mCriticalNumber, "CriticalNumber");
			newObject(out mCritical, "Critical");
		}
	}
	public override void init()
	{
		base.init();
		if (mNumber != null)
		{
			mNumber.setInterval(-24);
			mNumber.setDockingPosition(DOCKING_POSITION.CENTER);
			mCriticalNumber.setInterval(-24);
			mCriticalNumber.setDockingPosition(DOCKING_POSITION.CENTER);
		}
		mScript.notifyUIObjectNeedUpdate(mRoot, true);
	}
	public override bool setActive(bool visible)
	{
		base.setActive(visible);
		if (visible)
		{
			// 让当前节点成为该父节点下的最顶层节点
			mRoot.setAsLastSibling(false);
		}
		return visible;
	}
	public override void reset()
	{
		base.reset();
		mIsCritical = false;
	}
	public override void recycle()
	{
		base.recycle();
		mNumber?.setNumber(null);
		mCriticalNumber?.setNumber(null);
        if (mCritical != null)
		{
			mCritical.getImage().enabled = false;
		}
	}
	public void setCriticalHitEnabled(bool critical)
	{
		if (mNumber != null)
		{
			mIsCritical = critical;
			mCritical.getImage().enabled = critical;
		}
	}
	public virtual void setNumber(int number)
	{
		if (mNumber != null && !mIsCritical)
		{
			mNumber.setNumber(number);
		}
		if (mCriticalNumber != null && mIsCritical)
		{
			mCriticalNumber.setNumber(number);
			mCritical.setPositionX(-(mCriticalNumber.getContentWidth() + mCritical.getSize().x) * 0.5f);
		}
	}
}