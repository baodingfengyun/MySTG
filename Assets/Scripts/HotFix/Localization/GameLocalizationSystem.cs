using System.Collections.Generic;
using UnityEngine;
using static UnityUtility;
using static FrameBaseUtility;
using static StringUtility;
using static FrameBaseDefine;
using static GameDefine;
using static FrameBaseHotFix;
using static GBR;

// 辅助Frame中的LocalizationManager的类
public class GameLocalizationSystem : FrameSystem
{
	public override void init()
	{
		base.init();
		mLocalizationManager.setReloadLanguageCallback(reloadLanguage);
		mLocalizationManager.setCheckLanguageCallback(checkLaguage);
		// 因为上面设置了回调,所以需要再次设置一下当前语言,真正去读取多语言表
		string defaultLanguage;
		switch (Application.systemLanguage)
		{
			case SystemLanguage.ChineseTraditional: defaultLanguage = LANGUAGE_CHINESE_TRADITIONAL; break;
			case SystemLanguage.ChineseSimplified:	defaultLanguage = LANGUAGE_CHINESE;break;
			case SystemLanguage.Chinese:			defaultLanguage = LANGUAGE_CHINESE;break;
			case SystemLanguage.English:			defaultLanguage = LANGUAGE_ENGLISH; break;
			default:								defaultLanguage = LANGUAGE_ENGLISH; break;
		}
		mLocalizationManager.setCurrentLanguage(PlayerPrefs.GetString(PREF_LOCALIZATION, defaultLanguage));
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void checkLaguage(string text, int id)
	{
		if (!isEditor())
		{
			return;
		}
		if (text.isEmpty() && id == 0)
		{
			return;
		}
		// 如果不是中文的就不检测
		if (!text.isEmpty() && !hasChinese(text))
		{
			return;
		}
		EDLocalization data = null;
		if (!text.isEmpty())
		{
			data = mExcelLocalization.getData(text);
		}
		else if (id > 0)
		{
			data = mExcelLocalization.query(id);
		}
		if (data == null)
		{
			logError("找不到对应的语言配置,ID:" + id + ", 中文:" + text);
			return;
		}
		if (data.mEnglish.isEmpty())
		{
			logError("找不到对应的英文语言配置,ID:" + id + ", 中文:" + text);
			return;
		}
	}
	protected void reloadLanguage(string language, Dictionary<string, string> zhKeyList, Dictionary<int, string> idKeyList)
	{
		zhKeyList.Clear();
		idKeyList.Clear();
		switch (language)
		{
			case LANGUAGE_CHINESE:
				foreach (EDLocalization item in mExcelLocalization.queryAll())
				{
					zhKeyList.Add(item.mChinese, item.mChinese);
					idKeyList.Add(item.mID, item.mChinese);
				}
				break;
			case LANGUAGE_CHINESE_TRADITIONAL:
				foreach (EDLocalization item in mExcelLocalization.queryAll())
				{
					zhKeyList.Add(item.mChinese, item.mChineseTraditional);
					idKeyList.Add(item.mID, item.mChineseTraditional);
				}
				break;
			case LANGUAGE_ENGLISH:
				foreach (EDLocalization item in mExcelLocalization.queryAll())
				{
					zhKeyList.Add(item.mChinese, item.mEnglish);
					idKeyList.Add(item.mID, item.mEnglish);
				}
				break;
		}
	}
}