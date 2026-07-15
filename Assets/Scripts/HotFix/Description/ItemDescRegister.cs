
public class ItemDescRegister : DescRegisterBase
{
	protected static ItemDescRegister mInstance;
	public static void registeAll()
	{
		mInstance ??= new ItemDescRegister();
		mInstance.registeAllInternal();
	}
	// 获取到的描述,是已经经过了多语言转换以后的字符串
	public static string getDescLocalized(int itemID)
	{
		return mInstance.mRegisteCallbackList.get(itemID)?.Invoke(itemID);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected override void registeAllInternal()
	{
		;
	}
}