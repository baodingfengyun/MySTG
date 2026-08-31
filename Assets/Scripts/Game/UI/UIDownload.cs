using UnityEngine.UI;
using UnityEngine;
using static StringUtility;
using static GB;

/// <summary>
/// UI：下载
/// </summary>
public class UIDownload : GameLayout
{
	protected Image mProgressForeground;
	protected Text mProgressLabel;
	protected Text mDownloadLabel;
	public override void assignWindow()
	{
		base.assignWindow();
		getUIComponent(out mProgressForeground, "ProgressForeground");
		getUIComponent(out mProgressLabel, "ProgressLabel");
        getUIComponent(out mDownloadLabel, "DownloadLabel");
	}
    public override void onGameState()
    {
		base.onGameState();
		setProgress(0);
		mDownloadLabel.text = null;
    }
	public void setProgress(float progress)
	{
		mProgressLabel.text = toPercent(progress) + "%";
        mProgressForeground.fillAmount = progress;
	}
	public void setDownloadInfo(string info)
	{
		mDownloadLabel.text = mLocalizeResourcesManager.getLocalization(info);
	}
}