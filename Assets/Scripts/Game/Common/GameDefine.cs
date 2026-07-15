using System.Collections.Generic;
using static FrameBaseUtility;

// 游戏常量定义
public class GameDefine
{
	// 路径定义
	// 常量定义
	public const string ANDROID_PLUGIN_BUNDLE_NAME = "com.blockfish.android";		// 安卓插件的包名
#if UNITY_ANDROID
	public static string REMOTE_FOLDER = isTestClient() ? "Assets_Android_Test" : "Assets_Android";
#elif UNITY_IOS
	public static string REMOTE_FOLDER = isTestClient() ? "Assets_iOS_Test" : "Assets_iOS";
#endif

    // 这里请使用自己的地址
    public static string BUILD_SERVER = "";
	// 允许动态下载的目录列表,此列表中的文件不会打包到apk中,也不会在游戏启动时从服务器下载,而是在加载资源时才会进行下载
	// 这里不要写成一行,需要换行写,才能正确解析
	public static List<string> DYNAMIC_DOWNLOAD_LIST = new()
	{
		"DynamicDownloading/",
	};
	public const string PREF_LOCALIZATION = "Localization";                     // 本地化的PlayerPrefs的名字
	// 这里最好改成自己的密钥
    public static byte[] AES_KEY = new byte[16] { 0xFF, 0x1F, 0xAA, 0x24, 0x55, 0x61, 0x91, 0x22, 0xFF, 0x1F, 0xAA, 0x24, 0x55, 0x61, 0x91, 0x22 };
    public static byte[] AES_IV = new byte[16] { 0xCF, 0xAB, 0x24, 0x5D, 0x6D, 0xFC, 0xC3, 0x9A, 0xCF, 0xAB, 0x24, 0x5D, 0x6D, 0xFC, 0xC3, 0x9A };
}