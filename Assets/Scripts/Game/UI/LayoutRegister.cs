using System;
using static GB;
using static FrameBase;
using static FrameBaseUtility;

// 非热更层的界面注册
public class LayoutRegister
{
	public static void registeAllLayout()
	{
		registeLayout<UIDialogOKResources>(script =>	mUIDialogOKResources = script);
		registeLayout<UIDialogYesNoResources>(script => mUIDialogYesNoResources = script);
		registeLayout<UIDialogTipResources>(script =>	mUIDialogTipResources = script);
		registeLayout<UIDownload>(script =>				mUIDownload = script);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registeLayout<T>(Action<T> callback = null) where T : GameLayout
	{
		String fileName = typeof(T).ToString();
		logBase("注册UI: Assets/Resources/UI/UIPrefab/" + fileName + ".prefab");
        mLayoutManager.registeLayout(typeof(T), fileName, (script) => { callback?.Invoke(script as T); });
    }
}