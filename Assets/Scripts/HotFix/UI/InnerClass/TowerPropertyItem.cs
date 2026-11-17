
// 塔属性条目: UITowerInfo 面板里每一行"属性名-数值"的显示单元,只显示一个数值文本(属性名由外部标签提供)。
// setValue 提供 float(保留1位)与 int 两个重载。类体/成员/assignWindowInternal 均为 prefab 自动生成区,勿手改。
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