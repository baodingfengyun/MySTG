using UnityEditor;

// 编辑器发布菜单:在"发布版本"菜单下提供入口,按当前平台(Android/iOS)打开 GameReleaseWindow 启动打包发布流程。
public class GameMenu
{
#if UNITY_ANDROID
	[MenuItem("发布版本/发布Android版本", false, 100)]
#elif UNITY_IOS
	[MenuItem("发布版本/发布iOS版本", false, 100)]
#endif
    public static void releaseVersion()
	{
		EditorWindow.GetWindow<GameReleaseWindow>(true, "发布版本", true).start();
	}
}