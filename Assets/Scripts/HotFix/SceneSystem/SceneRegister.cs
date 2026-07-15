using System;
using static FrameBaseHotFix;
using static FrameDefine;
using static StringUtility;
using static GBR;
using static GDR;

public class SceneRegister
{
	protected static int mRegisted = 1;
	public static void registerAll()
	{
		register<MainScene>(R_SCENE_PATH + SCENE_MAIN + ".unity", (scene) => { mMainScene = scene; });
	}
	public static void registerBattleScene()
	{
		if (mRegisted-- <= 0)
		{
			return;
		}
		using var a = new HashSetScope<string>(out var sceneNames);
		foreach (EDMapConfig item in mExcelMapConfig.queryAll())
		{
			string name = getFileNameNoSuffixNoDir(item.mSceneName);
			if (!sceneNames.Add(name))
			{
				continue;
			}
			register<BattleScene>(item.mSceneName, (scene) => { mBattleScene = scene; });
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void register<T>(string path, Action<T> callback) where T : SceneInstance
	{
		mSceneSystem.registeScene(typeof(T), path, (scene) => { callback?.Invoke(scene as T); });
	}
}