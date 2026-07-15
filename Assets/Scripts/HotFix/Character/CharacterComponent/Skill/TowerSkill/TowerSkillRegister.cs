using System;
using System.Collections.Generic;
using static UnityUtility;
using static GBR;

// 防御塔技能注册
public class TowerSkillRegister
{
	protected static ParamParseCollection mParamCollection = new();		// 参数解析对象
	protected static Dictionary<int, Type> mSkillTypeList = new();      // 技能类型列表
	public static void registeSkill()
	{
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_1_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_1_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_1_RED_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_1_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_1_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_2_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_2_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_2_RED_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_2_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_2_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_3_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_3_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_3_RED_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_3_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.SHI_ZI_GONG_3_BLUE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_1_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_1_PURPLE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_1_RED_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_1_GREEN_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_1_BLUE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_2_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_2_PURPLE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_2_RED_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_2_GREEN_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_2_BLUE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_3_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_3_PURPLE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_3_RED_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_3_GREEN_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.FANG_KONG_FEI_DAN_3_BLUE_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_1_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_1_PURPLE_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_1_RED_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_1_GREEN_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_1_BLUE_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_2_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_2_PURPLE_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_2_RED_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_2_GREEN_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_2_BLUE_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_3_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_3_PURPLE_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_3_RED_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_3_GREEN_ID);
		registe<TowerSkill_TouShiJi, SkillCustomParam_TouShiJi>(EDTowerSkill.TOU_SHI_JI_3_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_1_PURPLE_1_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_1_RED_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_1_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_1_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_1_PURPLE_2_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_1_PURPLE_3_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_1_PURPLE_4_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_1_PURPLE_5_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_2_PURPLE_1_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_2_RED_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_2_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_2_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_2_PURPLE_2_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_2_PURPLE_3_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_2_PURPLE_4_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_2_PURPLE_5_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_3_PURPLE_1_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_3_RED_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_3_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_3_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_3_PURPLE_2_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_3_PURPLE_3_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_3_PURPLE_4_ID);
		registe<TowerSkill>(EDTowerSkill.XIANG_QIAN_3_PURPLE_5_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_1_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_1_PURPLE_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_1_RED_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_1_GREEN_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_1_BLUE_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_2_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_2_PURPLE_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_2_RED_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_2_GREEN_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_2_BLUE_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_3_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_3_PURPLE_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_3_RED_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_3_GREEN_ID);
		registe<TowerSkill_HuoPao, SkillCustomParam_HuoPao>(EDTowerSkill.HUO_PAO_3_BLUE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_1_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_1_PURPLE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_1_RED_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_1_GREEN_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_1_BLUE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_2_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_2_PURPLE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_2_RED_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_2_GREEN_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_2_BLUE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_3_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_3_PURPLE_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_3_RED_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_3_GREEN_ID);
		registe<TowerSkill_FangKongFeiDan>(EDTowerSkill.QIU_XING_FEI_DAN_3_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_1_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_1_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_1_RED_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_1_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_1_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_2_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_2_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_2_RED_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_2_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_2_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_3_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_3_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_3_RED_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_3_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.XIAN_DAN_3_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_1_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_1_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_1_RED_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_1_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_1_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_2_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_2_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_2_RED_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_2_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_2_BLUE_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_3_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_3_PURPLE_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_3_RED_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_3_GREEN_ID);
		registe<TowerSkill>(EDTowerSkill.TIAN_KONG_ZHI_MAO_3_BLUE_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_1_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_1_PURPLE_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_1_RED_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_1_GREEN_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_1_BLUE_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_2_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_2_PURPLE_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_2_RED_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_2_GREEN_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_2_BLUE_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_3_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_3_PURPLE_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_3_RED_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_3_GREEN_ID);
		registe<TowerSkill_FeiBiaoFaSheQi>(EDTowerSkill.FEI_BIAO_FA_SHE_3_BLUE_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_1_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_1_PURPLE_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_1_RED_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_1_GREEN_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_1_BLUE_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_2_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_2_PURPLE_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_2_RED_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_2_GREEN_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_2_BLUE_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_3_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_3_PURPLE_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_3_RED_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_3_GREEN_ID);
		registe<TowerSkill_QiQiuZhaDan>(EDTowerSkill.QI_QIU_ZHA_DAN_3_BLUE_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_1_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_1_PURPLE_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_1_RED_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_1_GREEN_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_1_BLUE_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_2_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_2_PURPLE_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_2_RED_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_2_GREEN_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_2_BLUE_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_3_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_3_PURPLE_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_3_RED_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_3_GREEN_ID);
		registe<TowerSkill_ZhenDang, SkillCustomParam_ZhenDang>(EDTowerSkill.ZHEN_DANG_3_BLUE_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_1_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_1_PURPLE_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_1_RED_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_1_GREEN_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_1_BLUE_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_2_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_2_PURPLE_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_2_RED_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_2_GREEN_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_2_BLUE_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_3_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_3_PURPLE_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_3_RED_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_3_GREEN_ID);
		registe<TowerSkill_BoDong, SkillCustomParam_BoDong>(EDTowerSkill.BO_DONG_3_BLUE_ID);

        foreach (EDTowerSkill item in mExcelTowerSkill.queryAll())
        {
            mParamCollection.registeParamTemplate(item.mID, item.mID, item.mParam0, item.mParam1);
        }
    }
	public static Type getSkillType(int id)
	{
		Type type = mSkillTypeList.get(id);
		if (type == null)
		{
			logError("防御塔技能未注册:" + id);
		}
		return type;
	}
	public static ParamCopyable getSkillParam(EDTowerSkill skillData)
	{
		return mParamCollection.getParamTemplate(skillData.mID) as ParamCopyable;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void registe<T>(int id) where T : TowerSkill
	{
		mSkillTypeList.Add(id, typeof(T));
	}
	protected static void registe<Skill, Param>(int id) where Skill : TowerSkill where Param : ParamBase
	{
		mSkillTypeList.Add(id, typeof(Skill));
		mParamCollection.registe<Param>(id);
	}
}