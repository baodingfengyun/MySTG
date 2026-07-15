using static FrameBaseUtility;
using static GameUtility;
using static GameDefine;
using static FileUtility;
using static GB;

public class StartSceneDownload : SceneProcedure
{
	protected GameDownload mInstance;
	public StartSceneDownload()
	{
        mInstance = new GameDownload();
        mInstance.setDynamicDownloadList(DYNAMIC_DOWNLOAD_LIST);
		mInstance.setTipCallback((DOWNLOAD_TIP tip) =>
		{
			if (tip == DOWNLOAD_TIP.NONE)
			{
				dialogTipResources();
			}
			else if (tip == DOWNLOAD_TIP.CHECKING_UPDATE)
			{
				dialogTipResources("正在检查更新...");
			}
			else if (tip == DOWNLOAD_TIP.DOWNLOAD_FAILED)
			{
				dialogYesNoResources("文件下载失败,是否重试?", retry);
			}
			else if (tip == DOWNLOAD_TIP.NOT_IN_REMOTE_FILE_LIST)
			{
				dialogYesNoResources("已下载的文件不存在于远端文件列表,是否重新开始更新?", retry);
			}
			else if (tip == DOWNLOAD_TIP.VERIFY_FAILED)
			{
				dialogYesNoResources("下载文件错误,是否重试?", retry);
			}
		});
	}
	public override void init()
	{
		base.init();
        mInstance.setProgressCallback(onDownloadProgress);
        if (isEditor() || !isEnableHotFix() || isWebGL())
        {
            mInstance.skipDownload();
        }
        else
        {
            mInstance.startCheckVersion();
        }
    }
    public override void exit()
    {
        base.exit();
        mUIDownload?.close();
    }
    public override void willDestroy()
	{
		base.willDestroy();
		mInstance.willDestroy();
	}
    //------------------------------------------------------------------------------------------------------------------------------
    protected void retry(bool yes)
    {
        if (yes)
        {
            mInstance.startCheckVersion();
        }
        else
        {
            stopApplication();
        }
    }
    protected void onDownloadProgress(float progress, PROGRESS_TYPE type, string info, int bytesPerSecond, int downloadRemainSeconds)
	{
		mUIDownload.setProgress(progress);
		if (type == PROGRESS_TYPE.CHECKING_UPDATE)
		{
			mUIDownload.setDownloadInfo("正在巡视村庄...");
		}
		else if (type == PROGRESS_TYPE.DELETE_FILE)
		{
			mUIDownload.setDownloadInfo("正在打扫羊毛，准备干净利落...");
		}
		else if (type == PROGRESS_TYPE.DOWNLOAD_RESOURCE)
		{
			mUIDownload.setDownloadInfo("正在运送物资...");
		}
		else if (type == PROGRESS_TYPE.FINISH)
		{
			mUIDownload.setDownloadInfo("准备完毕, 即将启程...");
            launch();
        }
	}
    protected void onLaunchError()
    {
        dialogYesNoResources("资源加载失败,是否重试?", (bool yes) =>
        {
            if (yes)
            {
                launch();
            }
            else
            {
                stopApplication();
            }
        });
    }
    protected void launch()
    {
        // 下载或者加载程序集
        HybridCLRSystem.launchHotFix(getAESKeyBytes(), getAESIVBytes(), (string fileName, BytesIntCallback callback) =>
        {
            openFileAsync(availableReadPath(fileName), true, bytes => callback?.Invoke(bytes, bytes.Length));
        }, onLaunchError);
    }
}