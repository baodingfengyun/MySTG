using System;
using static FrameBaseHotFix;
using static FrameDefine;
using static StringUtility;
using static GBR;
using static GDR;

// 场景注册表:把"业务场景脚本类型 + Unity 场景资源路径"登记到框架的 SceneSystem。
// 只负责登记(registeScene),不负责加载;真正的加载/显示/隐藏/卸载由框架 SceneSystem 驱动。
// 登记时会附带一个回调,场景实例被创建出来后框架会回调它,这里借机把实例保存到全局静态变量
// (mMainScene / mBattleScene,定义于 GBR),之后业务代码用 mBattleScene 等直接访问当前场景。
public class SceneRegister
{
	// 战斗场景注册守卫:registerBattleScene 会被多个流程重复调用(如登录加载、进入大厅),
	// 但所有地图共用同一个 BattleScene 脚本,只需批量登记一次。用自减计数保证只有首次调用真正执行。
	protected static int mRegisted = 1;

	// 登记大厅主场景:路径 = 场景资源根路径 + 主场景名 + .unity 后缀。
	// 场景创建后回调把实例赋给全局 mMainScene。
	public static void registerAll()
	{
		register<MainScene>(R_SCENE_PATH + SCENE_MAIN + ".unity", (scene) => { mMainScene = scene; });
	}

	// 批量登记所有战斗场景:遍历地图配置表,每张地图的 mSceneName 对应一个 Unity 场景文件,
	// 但它们都复用同一个 BattleScene 脚本类(框架的 mScriptMappingList 支持"多场景→同一脚本")。
	public static void registerBattleScene()
	{
		// 守卫:非首次调用直接返回,避免重复登记同一批场景。
		if (mRegisted-- <= 0)
		{
			return;
		}
		// 用 HashSet 按场景名去重:不同地图可能配置了同一个场景文件,重复的名字只登记一次。
		using var a = new HashSetScope<string>(out var sceneNames);
		foreach (EDMapConfig item in mExcelMapConfig.queryAll())
		{
			// 取不带目录和 .unity 后缀的纯场景名,作为去重键。
			string name = getFileNameNoSuffixNoDir(item.mSceneName);
			if (!sceneNames.Add(name))
			{
				continue;
			}
			// 登记这张地图的场景;创建后回调把实例赋给全局 mBattleScene。
			// 注意:传的是完整路径 item.mSceneName(去重只用 name),因为框架要求路径以 .unity 结尾。
			register<BattleScene>(item.mSceneName, (scene) => { mBattleScene = scene; });
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	// 登记场景的通用封装。path 必须是 .unity 结尾的资源路径;callback 在场景实例创建后被框架回调。
	// 框架委托给的是基类 SceneInstance,这里再转型为具体的 T 交给业务回调。
	protected static void register<T>(string path, Action<T> callback) where T : SceneInstance
	{
		mSceneSystem.registeScene(typeof(T), path, (scene) => { callback?.Invoke(scene as T); });
	}
}