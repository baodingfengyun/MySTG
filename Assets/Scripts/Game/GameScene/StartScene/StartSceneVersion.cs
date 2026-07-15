using static FrameBaseUtility;
using static GameUtility;
using static FrameBase;
using static GB;

public class StartSceneVersion : SceneProcedure
{
    protected bool mRemoteDone;
    protected bool mStreamingAndPersistDone;
    public override void init()
	{
        base.init();
		CmdLayoutManagerLoad.executeAsync<UIDownload>(0, () =>
		{
            mUIDownload.setDownloadInfo("正在获取版本号");
            doGetRemoteVersion();
            mAssetVersionSystem.loadStreamingAndPersistentVersion(() =>
            {
                mStreamingAndPersistDone = true;
                checkEnterNext();
            });
        });
	}
	protected void doGetRemoteVersion()
	{
		requestRemoteVersion(null, null, (version) =>
		{
            if (version.isEmpty())
            {
                dialogYesNoResources("找不到最新版本,是否重试?", (bool ok) =>
                {
                    if (ok)
                    {
                        doGetRemoteVersion();
                    }
                    else
                    {
                        stopApplication();
                    }
                });
                return;
            }
            mAssetVersionSystem.setRemoteVersion(version);
            mRemoteDone = true;
            checkEnterNext();
        });
	}
    protected void requestRemoteVersion(string url, string localVersion, StringCallback callback)
    {
		callback?.Invoke("1.0.0");
    }
    protected void checkEnterNext()
    {
        if (mRemoteDone && mStreamingAndPersistDone)
        {
            logBase("StreamingVersion:" + mAssetVersionSystem.getStreamingAssetsVersion() +
                    ", PersistVersion:" + mAssetVersionSystem.getPersistentDataVersion() +
                    ", RemoteVersion:" + mAssetVersionSystem.getRemoteVersion());
            // 这里按需设置自己的资源下载地址
            //mResourceManager.setDownloadURL();
            mGameSceneManager.getCurScene().changeProcedure<StartSceneFileList>();
        }
    }
}