using static FrameBaseUtility;
using static GameUtility;
using static GB;

/// <summary>
/// 启动场景 - （4）下载流程
/// </summary>
public class StartSceneDownload : SceneProcedure
{
	// 游戏下载（框架提供）
	protected GameDownload mInstance;
	public StartSceneDownload()
	{
        mInstance = new GameDownload();
		mInstance.setErrorCallback(onDownloadError);
		mInstance.setProgressCallback(onDownloadProgress);
	}
	public override void init()
	{
		base.init();
		mInstance.start();
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
    // 是否重新下载
	protected void retry(bool yes)
    {
        if (yes)
        {
            mInstance.start();
        }
        else
        {
            stopApplication();
        }
    }
	// 根据下载错误类型，设置不同的弹窗
    protected void onDownloadError(DOWNLOAD_ERROR tip)
    {
		if (tip == DOWNLOAD_ERROR.NONE)
		{
			dialogTipResources();
		}
		else if (tip == DOWNLOAD_ERROR.DOWNLOAD_FAILED)
		{
			dialogYesNoResources("文件下载失败,是否重试?", retry);
		}
		else if (tip == DOWNLOAD_ERROR.NOT_IN_REMOTE_FILE_LIST)
		{
			dialogYesNoResources("已下载的文件不存在于远端文件列表,是否重新开始更新?", retry);
		}
		else if (tip == DOWNLOAD_ERROR.VERIFY_FAILED)
		{
			dialogYesNoResources("下载文件错误,是否重试?", retry);
		}
	}
	// 下载过程中的提示信息（根据不同类型，设置不同的提示信息）
    protected void onDownloadProgress(float progress, PROGRESS_TYPE type, string info, int bytesPerSecond, int downloadRemainSeconds)
	{
		mUIDownload.setProgress(progress);
		if (type == PROGRESS_TYPE.CHECKING_UPDATE)
		{
			mUIDownload.setDownloadInfo("CHECKING_UPDATE 正在巡视村庄...");
		}
		else if (type == PROGRESS_TYPE.DELETE_FILE)
		{
			mUIDownload.setDownloadInfo("DELETE_FILE 正在打扫羊毛，准备干净利落...");
		}
		else if (type == PROGRESS_TYPE.DOWNLOAD_RESOURCE)
		{
			mUIDownload.setDownloadInfo("DOWNLOAD_RESOURCE 正在运送物资...");
		}
		else if (type == PROGRESS_TYPE.FINISH)
		{
			mUIDownload.setDownloadInfo("FINISH 准备完毕, 即将启程...");
			// 资源加载完成后，加载程序集
			logBase("资源加载完成后，加载程序集...");
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
        HybridCLRSystem.launchHotFix(onLaunchError);
    }
}