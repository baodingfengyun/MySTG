using System;
using static FrameBaseUtility;
using static GameUtility;
using static FrameBase;
using static GB;

/// <summary>
/// 启动场景 - （3）获取所有资源文件信息的流程
/// </summary>
public class StartSceneFileList : SceneProcedure
{
    public string mRemoteListMD5;       // 远端文件列表的MD5,用于对比本地和远端的文件列表是否一致
    public override void init()
	{
		base.init();
		mUIDownload.setDownloadInfo("正在获取最新的资源信息");
        getRemoteFileListMD5(md5 =>
        {
            mRemoteListMD5 = md5;
            mAssetVersionSystem.startCheckFileList(mRemoteListMD5, null, null, onSuccess, onFailed, checkNeedRequestRemoteFileList);
        });
    }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void getRemoteFileListMD5(Action<string> callback)
    {
        callback?.Invoke(null);
    }
    protected void onSuccess()
    {
        // 进入下一个流程：下载
        mGameSceneManager.getCurScene().changeProcedure<StartSceneDownload>();
    }
    protected void onFailed()
    {
        dialogYesNoResources("最新文件列表获取失败,是否重试?", (bool ok) =>
        {
            if (ok)
            {
                mAssetVersionSystem.startCheckFileList(mRemoteListMD5, null, null, onSuccess, onFailed, checkNeedRequestRemoteFileList);
            }
            else
            {
                stopApplication();
            }
        });
    }
    // 对比本地和远端的文件列表,如果不一致,则将远端的文件列表下载到本地
    protected void checkNeedRequestRemoteFileList(StringCallback callback)
    {
        callback?.Invoke("remote file list content");
    }
}