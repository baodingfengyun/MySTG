using System;
using System.Text;
using static FileUtility;
using static GameDefine;
using static GB;

// 工具函数
public class GameUtility
{
	public static void dialogYesNoResources(string info, OnDialogYesNoCallback callback)
	{
		dialogYesNoResources(info, callback, null);
	}
	public static void dialogYesNoResources(string info, OnDialogOKCallback confirmCallback)
	{
		dialogYesNoResources(info, null, confirmCallback);
	}
	// 不传任何参数就是关闭对话框
	public static void dialogYesNoResources()
	{
		dialogYesNoResources(null, null, null);
	}
	// 显示一个对话框,有确认和取消按钮
	public static void dialogYesNoResources(string info, OnDialogYesNoCallback callback, OnDialogOKCallback confirmCallback)
	{
		if (info != null)
		{
			CmdLayoutManagerLoad.executeAsync<UIDialogYesNoResources>(10, () =>
			{
                mUIDialogYesNoResources.setInfo(mLocalizeResourcesManager.getLocalization(info));
                mUIDialogYesNoResources.setCallback(callback);
                mUIDialogYesNoResources.setConfirmCallback(confirmCallback);
            });
		}
		else
		{
			mUIDialogYesNoResources?.setCallback(callback);
			mUIDialogYesNoResources?.setConfirmCallback(confirmCallback);
			mUIDialogYesNoResources?.close();

        }
	}
	public static void dialogOKResources(string info, OnDialogOKCallback callback = null)
	{
		if (info != null)
		{
			CmdLayoutManagerLoad.executeAsync<UIDialogOKResources>(11, () =>
			{
                mUIDialogOKResources.setInfo(mLocalizeResourcesManager.getLocalization(info));
                mUIDialogOKResources.setOKCallback(callback);
            });
		}
		else
		{
			mUIDialogOKResources?.setOKCallback(callback);
			mUIDialogOKResources?.close();

        }
	}
	// 不传任何参数就是关闭对话框
	public static void dialogOKResources()
	{
		dialogOKResources(null, null);
	}
	public static void dialogTipResources(string info)
	{
		if (info != null)
		{
			CmdLayoutManagerLoad.executeAsync<UIDialogTipResources>(12, () =>
			{
				mUIDialogTipResources.setInfo(mLocalizeResourcesManager.getLocalization(info));
            });
		}
		else
		{
			mUIDialogTipResources?.close();

        }
	}
	public static void dialogTipResources()
	{
		dialogTipResources(null);
	}
    public static byte[] getAESKeyBytes()
    {
        // 将密钥再加一次密
        byte[] newBytes = new byte[16];
        Buffer.BlockCopy(Encoding.UTF8.GetBytes(generateFileMD5(AES_KEY)), 0, newBytes, 0, newBytes.Length);
        return newBytes;
    }
    public static byte[] getAESIVBytes()
    {
        byte[] newBytes = new byte[16];
        Buffer.BlockCopy(Encoding.UTF8.GetBytes(generateFileMD5(AES_IV)), 0, newBytes, 0, newBytes.Length);
        return newBytes;
    }
}