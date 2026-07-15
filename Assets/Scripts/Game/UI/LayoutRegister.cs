using System;
using static GB;
using static FrameBase;

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
        mLayoutManager.registeLayout(typeof(T), typeof(T).ToString(), (script) => { callback?.Invoke(script as T); });
    }
}