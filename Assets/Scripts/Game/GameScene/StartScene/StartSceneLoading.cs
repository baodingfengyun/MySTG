using UnityEngine;
using static FrameBaseDefine;
using static FrameBase;
using static GameDefine;
using static GB;

/// <summary>
/// 启动场景 - （1）加载流程
/// </summary>
public class StartSceneLoading : SceneProcedure
{
	public override void init()
	{
		base.init();
		// 先根据手机系统语言设置初始的语言类型,这里只是设置当前语言,并不会读取多语言表
		string defaultLanguage;
		switch (Application.systemLanguage)
		{
			case SystemLanguage.ChineseTraditional: defaultLanguage = LANGUAGE_CHINESE_TRADITIONAL; break;
			case SystemLanguage.ChineseSimplified:	defaultLanguage = LANGUAGE_CHINESE; break;
			case SystemLanguage.Chinese:			defaultLanguage = LANGUAGE_CHINESE; break;
			case SystemLanguage.English:			defaultLanguage = LANGUAGE_ENGLISH; break;
			default:								defaultLanguage = LANGUAGE_ENGLISH; break;
		}
        mLocalizeResourcesManager.setCurLanguage(PlayerPrefs.GetString(PREF_LOCALIZATION, defaultLanguage));
		// 进入下一个流程：版本比较
        mGameSceneManager.getCurScene().changeProcedure<StartSceneVersion>();
	}
}