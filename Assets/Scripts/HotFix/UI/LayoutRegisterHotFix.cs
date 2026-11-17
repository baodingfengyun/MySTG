using System;
using static GBR;
using static FrameBaseHotFix;
using static FrameDefine;

// ===== UI 布局注册表：整个 UI 框架的入口，声明每个界面 prefab 的加载方式与生命周期 =====
// 【注册做什么】registeLayout 把"脚本类型 T"关联到 prefab 路径(R_UI_PREFAB_PATH + 类名 + .prefab)，
//   并传入一个回调，加载完成后把 LayoutScript 实例赋给全局访问器(mUIGaming 等)，
//   于是各系统能用 mUIGaming.xxx 直接访问界面。
// 【两种生命周期(LAYOUT_LIFE_CYCLE)】
//   ① registeLayout → PART_USE(按需)：用 LT.LOAD<SHOW/HIDE> 时才创建/显示，适合战斗HUD、弹窗、
//      关卡界面等大多数界面；
//   ② registeLayoutPersist → PERSIST(常驻)：全程不销毁，适合高频复用的对话框(OK/Tip/YesNo)、
//      伤害数字、飘字、FPS、点击特效——避免反复实例化的开销。
// 【自动生成】auto generate start/end 之间由工具从 prefab 生成；但两个 registeLayout 辅助方法是手写。
// 【命名约定】类名 = prefab 文件名 = 全局字段名，工具据此自动生成，改界面名需同步三处。
public class LayoutRegisterHotFix
{
	public static void registerAll()
	{
        // auto generate start
		registeLayout<UIBattleItemSelectRogue>(script =>		mUIBattleItemSelectRogue = script);
		registeLayout<UICameraDrag>(script =>					mUICameraDrag = script);
		registeLayoutPersist<UIClickEffect>(script =>			mUIClickEffect = script);
		registeLayout<UIClientPackRogue>(script =>				mUIClientPackRogue = script);
		registeLayout<UIDamageNumber>(script =>					mUIDamageNumber = script);
		registeLayoutPersist<UIDialogOK>(script =>				mUIDialogOK = script);
		registeLayoutPersist<UIDialogTip>(script =>				mUIDialogTip = script);
		registeLayoutPersist<UIDialogYesNo>(script =>			mUIDialogYesNo = script);
		registeLayout<UIDraging>(script =>						mUIDraging = script);
		registeLayoutPersist<UIFPS>(script =>					mUIFPS = script);
		registeLayout<UIGaming>(script =>						mUIGaming = script);
		registeLayout<UIGuide>(script =>						mUIGuide = script);
		registeLayout<UIHPBar>(script =>						mUIHPBar = script);
		registeLayout<UILevelComplete>(script =>				mUILevelComplete = script);
		registeLayout<UILevelFaild>(script =>					mUILevelFaild = script);
		registeLayout<UILevelInfo>(script =>					mUILevelInfo = script);
		registeLayout<UILevelReward>(script =>					mUILevelReward = script);
		registeLayout<UILoading>(script =>						mUILoading = script);
		registeLayout<UILobby>(script =>						mUILobby = script);
		registeLayout<UILogin>(script =>						mUILogin = script);
		registeLayout<UIMonsterQueue>(script =>					mUIMonsterQueue = script);
		registeLayout<UIQuitBattle>(script =>					mUIQuitBattle = script);
		registeLayout<UISelectLevel>(script =>					mUISelectLevel = script);
		registeLayoutPersist<UITip>(script =>					mUITip = script);
		registeLayout<UITowerInfo>(script =>					mUITowerInfo = script);
		registeLayout<UITowerOperation>(script =>				mUITowerOperation = script);
        // auto generate end
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registeLayout<T>(Action<T> callback) where T : LayoutScript
	{
		registeLayout(typeof(T).ToString(), LAYOUT_LIFE_CYCLE.PART_USE, callback);
	}
	protected static void registeLayoutPersist<T>(Action<T> callback) where T : LayoutScript
	{
		registeLayout(typeof(T).ToString(), LAYOUT_LIFE_CYCLE.PERSIST, callback);
	}
	public static void registeLayout<T>(string name, LAYOUT_LIFE_CYCLE lifeCycle, Action<T> callback) where T : LayoutScript
	{
		mLayoutManager.registeLayout(typeof(T), R_UI_PREFAB_PATH + name + ".prefab", lifeCycle, (script) => { callback?.Invoke(script as T); });
	}
}