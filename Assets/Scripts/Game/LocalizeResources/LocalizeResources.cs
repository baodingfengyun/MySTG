using static FrameBaseDefine;
using static GB;

/// <summary>
/// 本地化资源
/// </summary>
public class LocalizeResources
{
	// 简体中文
	public string mChinese;
	// 英文
	public string mEnglish;
	// 繁体中文
	public string mChineseTraditional;

	public LocalizeResources(string chinese, string english, string chineseTraditional)
	{
		mChinese = chinese;
		mEnglish = english;
		mChineseTraditional = chineseTraditional;
	}

	// 获取本地化语言，不匹配的话就默认简体中文
	public string getCurLocalization()
	{
		string curLanguage = mLocalizeResourcesManager.getCurLanguage();
		if (curLanguage == LANGUAGE_CHINESE)
		{
			return mChinese;
		}
		else if (curLanguage == LANGUAGE_ENGLISH)
		{
			return mEnglish;
		}
		else if (curLanguage == LANGUAGE_CHINESE_TRADITIONAL)
		{
			return mChineseTraditional;
		}
		return mChinese;
	}
}