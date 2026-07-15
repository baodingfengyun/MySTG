using UnityEditor;

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