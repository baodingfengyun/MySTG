using System;
using System.Collections.Generic;
using static UnityUtility;
using static GBR;

public class GuideRegister
{
	protected static ParamParseCollection mParamCollection = new();			// 参数解析对象
	protected static Dictionary<int, Type> mTypeList = new();               // 引导对象类型列表,通过ID查询引导类型
	protected static Dictionary<int, KeyValuePair<Type, Type>> mGuideTypeList = new();	// key是引导步骤的类型,value的第一个是步骤类,第二个是步骤参数类
	public static void registeAllGuide()
    {
		addGuideType<GuideNoAction>(3);
		addGuideType<GuideClickStartFight>(5);
		addGuideType<GuideWaveEndTip>(9);
		addGuideType<GuideClickSceneTower, GuideClickSceneTowerParam>(11);
		addGuideType<GuideClickTowerUpgrade>(12);
		addGuideType<GuideClickLevelButton, GuideClickLevelButtonParam>(13);
		addGuideType<GuideClickLevelSelectExit>(16);
		addGuideType<GuideClickAdventure>(21);
		addGuideType<GuideClickEnterLevel>(24);
		addGuideType<GuideUpgradeTowerToLevel, GuideUpgradeTowerToLevelParam>(27);
		addGuideType<GuideAllFinish>(29);
		addGuideType<GuideClickTowerIcon, GuideClickTowerIconParam>(30);
		addGuideType<GuideClickCloseMonsterList>(36);
		foreach (EDGuide item in mExcelGuide.queryAll())
		{
			var typePiar = mGuideTypeList.get(item.mType);
			if (typePiar.Key == null)
			{
				logError("找不到引导类型,ID:" + item.mID.IToS());
			}
			registeGuide(item.mID, typePiar.Key, typePiar.Value);
            mParamCollection.registeParamTemplate(item.mID, item.mType, item.mParam0, item.mParam1, item.mParam2);
        }
	}
    public static Type getGuideType(int id)
    {
        return mTypeList.get(id, typeof(GuideStep));
	}
	public static ParamBase getParamTemplate(EDGuide data)
	{
		return mParamCollection.getParamTemplate(data.mID);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registeGuide(int id, Type guideType, Type paramType)
    {
        mTypeList.Add(id, guideType);
		if (paramType != null)
		{
			mParamCollection.registe(id, paramType);
		}
	}
	protected static void addGuideType<T0, T1>(int type) where T0 : GuideStep where T1 : ParamBase
	{
		mGuideTypeList.add(type, new(typeof(T0), typeof(T1)));
	}
	protected static void addGuideType<T0>(int type) where T0 : GuideStep
	{
		mGuideTypeList.add(type, new(typeof(T0), null));
	}
}