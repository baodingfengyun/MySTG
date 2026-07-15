using static FrameBaseDefine;
using static GB;

public class LocalizeResources
{
	public string mChinese;
	public string mEnglish;
	public string mChineseTraditional;
	public LocalizeResources(string chinese, string english, string chineseTraditional)
	{
		mChinese = chinese;
		mEnglish = english;
		mChineseTraditional = chineseTraditional;
	}
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