using System;
using System.Collections.Generic;
using static UnityUtility;
using static GBR;

// 防御塔技能注册：把 TowerSkill 表里的技能 ID 反射映射到具体技能类 + 参数模板
//
// 【理解"13 座塔如何差异化"的总钥匙】
// 每座塔在 Tower.csv 的 Skill 列只填一个技能 ID，COMTowerSkill.addSkill() 拿这个 ID 调
// TowerSkillRegister.getSkillType() 得到真正的 C# 类。注册情况分两类：
//   · 专属技能类（约 7 种）：震荡/火炮/投石机/气球炸弹/波动/防空飞弹(含球形)/飞镖发射器
//     —— 这些塔有独特行为（绕圈卫星弹、两段抛射、最小射程、弹跳等），必须写子类。
//   · 通用 TowerSkill（十字弓/霰弹/天空之矛/回旋飞镖/镶嵌塔等）
//     —— 行为就是标准"索敌→按 FireTime 发弹"，差异完全靠 SkillBullet 表里不同的子弹形态
//        （直线/扇形/曲线/追踪）+ Buff 配置实现，一行 C# 都不用写。
// 这正是 KR1 复刻评估里"塔层约七成能落、靠配置就够"的由来：加塔优先配子弹形态和 Buff，
// 只有需要全新行为模式时才 register 一个新 TowerSkill 子类。
//
// registe<T>(id)          —— 只登记类，用于无专属参数的技能
// registe<Skill,Param>(id) —— 同时登记类和参数模板，TowerSkillT<Param> 会用它解析表里的自定义参数
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
	// 仅登记技能类：addSkill 时 CLASS(类型) 从对象池反射出实例
	protected static void registe<T>(int id) where T : TowerSkill
	{
		mSkillTypeList.Add(id, typeof(T));
	}
	// 登记技能类 + 参数模板：模板由 getSkillParam 取出，交给 TowerSkillT.initData 深拷贝解析
	protected static void registe<Skill, Param>(int id) where Skill : TowerSkill where Param : ParamBase
	{
		mSkillTypeList.Add(id, typeof(Skill));
		mParamCollection.registe<Param>(id);
	}
}