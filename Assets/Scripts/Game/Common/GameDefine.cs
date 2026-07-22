using static FrameBaseUtility;

// 游戏常量定义
public class GameDefine
{
	// 常量定义
#if UNITY_ANDROID
	public static string REMOTE_FOLDER = isTestClient() ? "Assets_Android_Test" : "Assets_Android";
#elif UNITY_IOS
	public static string REMOTE_FOLDER = isTestClient() ? "Assets_iOS_Test" : "Assets_iOS";
#endif

    // 这里请使用自己的地址
    public static string BUILD_SERVER = "";
	public const string PREF_LOCALIZATION = "Localization";                     // 本地化的PlayerPrefs的名字
}