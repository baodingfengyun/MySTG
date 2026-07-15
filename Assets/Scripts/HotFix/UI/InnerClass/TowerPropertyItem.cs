
// auto generate classname start
// generate from:Assets/GameResources/UI/UIPrefab/UITowerInfo.prefab
// 
public class TowerPropertyItem : WindowObjectUGUI
// auto generate classname end
{
	// auto generate member start
	protected myUGUIText mValue;
	// auto generate member end
	public TowerPropertyItem(IWindowObjectOwner script) : base(script) { }
    protected override void assignWindowInternal()
    {
		// auto generate assignWindowInternal start
		newObject(out mValue, "Value");
		// auto generate assignWindowInternal end
	}
	public void setValue(float value)
	{
		mValue.setText(value.FToS(1));
	}
	public void setValue(int value)
	{
		mValue.setText(value);
	}
}