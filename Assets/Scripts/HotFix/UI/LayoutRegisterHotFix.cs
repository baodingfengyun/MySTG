using System;
using static GBR;
using static FrameBaseHotFix;
using static FrameDefine;

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