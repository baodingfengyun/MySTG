using System.Collections.Generic;

// 用于在不能读取多语言表格时的多语言切换
public class LocalizeResourcesManager : FrameSystem
{
	protected Dictionary<string, LocalizeResources> mLocalizationList = new();
	protected string mCurLanguage;
	public override void init()
	{
		base.init();
		addLocalization("正在获取版本号", "Getting version number", "正在獲取版本號");
		addLocalization("找不到最新版本,是否重试?", "Can't find the latest version, do you want to try again?", "找不到最新版本,是否重試?");
		addLocalization("正在获取最新的资源信息", "Getting the latest resource information", "正在獲取最新的資源信息");
		addLocalization("最新文件列表获取失败,是否重试?", "Do I want to try again if I fail to get the latest file list?", "最新文件列表獲取失敗,是否重試?");
		addLocalization("正在检查更新...", "Checking for updates...", "正在檢查更新...");
		addLocalization("文件下载失败,是否重试?", "File download fails, do you want to try again?", "文件下載失敗,是否重試?");
		addLocalization("已下载的文件不存在于远端文件列表,是否重新开始更新?", "If the downloaded file does not exist in the remote file list, do you want to start the update again?", "已下載的文件不存在于遠端文件列表,是否重新開始更新?");
		addLocalization("下载文件错误,是否重试?", "Error downloading file, do you want to try again?", "下載文件錯誤,是否重試?");
		addLocalization("正在巡视村庄...", "Patrolling the village...", "正在巡視村莊...");
		addLocalization("正在打扫羊毛，准备干净利落...", "Tidying up the wool, getting things neat and tidy...", "正在打掃羊毛,準備乾净利落...");
		addLocalization("正在运送物资...", "Transporting supplies...", "正在運送物資...");
		addLocalization("准备完毕, 即将启程...", "All set, ready to head out...", "準備完畢,即將啓程...");
	}
	public void setCurLanguage(string language) { mCurLanguage = language; }
	public string getCurLanguage() { return mCurLanguage; }
	public string getLocalization(string chinese) { return mLocalizationList.get(chinese)?.getCurLocalization() ?? chinese; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void addLocalization(string chinese, string english, string chineseTraditional)
	{
		mLocalizationList.Add(chinese, new(chinese, english, chineseTraditional));
	}
}