// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// TowerSkill表格
public class EDTowerSkill : ExcelDataT<EDTowerSkill>
{
	public const int SHI_ZI_GONG_1_ID = 1001;		// 一星十字弓
	public const int SHI_ZI_GONG_1_PURPLE_ID = 1002;// 一星十字弓紫宝石
	public const int SHI_ZI_GONG_1_RED_ID = 1003;	// 一星十字弓红宝石
	public const int SHI_ZI_GONG_1_GREEN_ID = 1004;	// 一星十字弓绿宝石
	public const int SHI_ZI_GONG_1_BLUE_ID = 1005;	// 一星十字弓蓝宝石
	public const int SHI_ZI_GONG_2_ID = 1101;		// 二星十字弓
	public const int SHI_ZI_GONG_2_PURPLE_ID = 1102;// 二星十字弓紫宝石
	public const int SHI_ZI_GONG_2_RED_ID = 1103;	// 二星十字弓红宝石
	public const int SHI_ZI_GONG_2_GREEN_ID = 1104;	// 二星十字弓绿宝石
	public const int SHI_ZI_GONG_2_BLUE_ID = 1105;	// 二星十字弓蓝宝石
	public const int SHI_ZI_GONG_3_ID = 1201;		// 三星十字弓
	public const int SHI_ZI_GONG_3_PURPLE_ID = 1202;// 三星十字弓紫宝石
	public const int SHI_ZI_GONG_3_RED_ID = 1203;	// 三星十字弓红宝石
	public const int SHI_ZI_GONG_3_GREEN_ID = 1204;	// 三星十字弓绿宝石
	public const int SHI_ZI_GONG_3_BLUE_ID = 1205;	// 三星十字弓蓝宝石
	public const int FANG_KONG_FEI_DAN_1_ID = 2001;	// 一星防空飞弹塔
	public const int FANG_KONG_FEI_DAN_1_PURPLE_ID = 2002;// 一星防空飞弹塔紫宝石
	public const int FANG_KONG_FEI_DAN_1_RED_ID = 2003;// 一星防空飞弹塔红宝石
	public const int FANG_KONG_FEI_DAN_1_GREEN_ID = 2004;// 一星防空飞弹塔绿宝石
	public const int FANG_KONG_FEI_DAN_1_BLUE_ID = 2005;// 一星防空飞弹塔蓝宝石
	public const int FANG_KONG_FEI_DAN_2_ID = 2101;	// 二星防空飞弹塔
	public const int FANG_KONG_FEI_DAN_2_PURPLE_ID = 2102;// 二星防空飞弹塔紫宝石
	public const int FANG_KONG_FEI_DAN_2_RED_ID = 2103;// 二星防空飞弹塔红宝石
	public const int FANG_KONG_FEI_DAN_2_GREEN_ID = 2104;// 二星防空飞弹塔绿宝石
	public const int FANG_KONG_FEI_DAN_2_BLUE_ID = 2105;// 二星防空飞弹塔蓝宝石
	public const int FANG_KONG_FEI_DAN_3_ID = 2201;	// 三星防空飞弹塔
	public const int FANG_KONG_FEI_DAN_3_PURPLE_ID = 2202;// 三星防空飞弹塔紫宝石
	public const int FANG_KONG_FEI_DAN_3_RED_ID = 2203;// 三星防空飞弹塔红宝石
	public const int FANG_KONG_FEI_DAN_3_GREEN_ID = 2204;// 三星防空飞弹塔绿宝石
	public const int FANG_KONG_FEI_DAN_3_BLUE_ID = 2205;// 三星防空飞弹塔蓝宝石
	public const int TOU_SHI_JI_1_ID = 3001;		// 一星投石机
	public const int TOU_SHI_JI_1_PURPLE_ID = 3002;	// 一星投石机紫宝石
	public const int TOU_SHI_JI_1_RED_ID = 3003;	// 一星投石机红宝石
	public const int TOU_SHI_JI_1_GREEN_ID = 3004;	// 一星投石机绿宝石
	public const int TOU_SHI_JI_1_BLUE_ID = 3005;	// 一星投石机蓝宝石
	public const int TOU_SHI_JI_2_ID = 3101;		// 二星投石机
	public const int TOU_SHI_JI_2_PURPLE_ID = 3102;	// 二星投石机紫宝石
	public const int TOU_SHI_JI_2_RED_ID = 3103;	// 二星投石机红宝石
	public const int TOU_SHI_JI_2_GREEN_ID = 3104;	// 二星投石机绿宝石
	public const int TOU_SHI_JI_2_BLUE_ID = 3105;	// 二星投石机蓝宝石
	public const int TOU_SHI_JI_3_ID = 3201;		// 三星投石机
	public const int TOU_SHI_JI_3_PURPLE_ID = 3202;	// 三星投石机紫宝石
	public const int TOU_SHI_JI_3_RED_ID = 3203;	// 三星投石机红宝石
	public const int TOU_SHI_JI_3_GREEN_ID = 3204;	// 三星投石机绿宝石
	public const int TOU_SHI_JI_3_BLUE_ID = 3205;	// 三星投石机蓝宝石
	public const int XIANG_QIAN_1_PURPLE_1_ID = 4002;// 一星镶嵌塔一星紫宝石
	public const int XIANG_QIAN_1_RED_ID = 4003;	// 一星镶嵌塔红宝石
	public const int XIANG_QIAN_1_GREEN_ID = 4004;	// 一星镶嵌塔绿宝石
	public const int XIANG_QIAN_1_BLUE_ID = 4005;	// 一星镶嵌塔蓝宝石
	public const int XIANG_QIAN_1_PURPLE_2_ID = 4012;// 一星镶嵌塔二星紫宝石
	public const int XIANG_QIAN_1_PURPLE_3_ID = 4022;// 一星镶嵌塔三星紫宝石
	public const int XIANG_QIAN_1_PURPLE_4_ID = 4032;// 一星镶嵌塔四星紫宝石
	public const int XIANG_QIAN_1_PURPLE_5_ID = 4042;// 一星镶嵌塔五星紫宝石
	public const int XIANG_QIAN_2_PURPLE_1_ID = 4102;// 二星镶嵌塔一星紫宝石
	public const int XIANG_QIAN_2_RED_ID = 4103;	// 二星镶嵌塔红宝石
	public const int XIANG_QIAN_2_GREEN_ID = 4104;	// 二星镶嵌塔绿宝石
	public const int XIANG_QIAN_2_BLUE_ID = 4105;	// 二星镶嵌塔蓝宝石
	public const int XIANG_QIAN_2_PURPLE_2_ID = 4112;// 二星镶嵌塔二星紫宝石
	public const int XIANG_QIAN_2_PURPLE_3_ID = 4122;// 二星镶嵌塔三星紫宝石
	public const int XIANG_QIAN_2_PURPLE_4_ID = 4132;// 二星镶嵌塔四星紫宝石
	public const int XIANG_QIAN_2_PURPLE_5_ID = 4142;// 二星镶嵌塔五星紫宝石
	public const int XIANG_QIAN_3_PURPLE_1_ID = 4202;// 三星镶嵌塔一星紫宝石
	public const int XIANG_QIAN_3_RED_ID = 4203;	// 三星镶嵌塔红宝石
	public const int XIANG_QIAN_3_GREEN_ID = 4204;	// 三星镶嵌塔绿宝石
	public const int XIANG_QIAN_3_BLUE_ID = 4205;	// 三星镶嵌塔蓝宝石
	public const int XIANG_QIAN_3_PURPLE_2_ID = 4212;// 三星镶嵌塔二星紫宝石
	public const int XIANG_QIAN_3_PURPLE_3_ID = 4222;// 三星镶嵌塔三星紫宝石
	public const int XIANG_QIAN_3_PURPLE_4_ID = 4232;// 三星镶嵌塔四星紫宝石
	public const int XIANG_QIAN_3_PURPLE_5_ID = 4242;// 三星镶嵌塔五星紫宝石
	public const int HUO_PAO_1_ID = 8001;			// 一星火炮塔
	public const int HUO_PAO_1_PURPLE_ID = 8002;	// 一星火炮塔紫宝石
	public const int HUO_PAO_1_RED_ID = 8003;		// 一星火炮塔红宝石
	public const int HUO_PAO_1_GREEN_ID = 8004;		// 一星火炮塔绿宝石
	public const int HUO_PAO_1_BLUE_ID = 8005;		// 一星火炮塔蓝宝石
	public const int HUO_PAO_2_ID = 8101;			// 二星火炮塔
	public const int HUO_PAO_2_PURPLE_ID = 8102;	// 二星火炮塔紫宝石
	public const int HUO_PAO_2_RED_ID = 8103;		// 二星火炮塔红宝石
	public const int HUO_PAO_2_GREEN_ID = 8104;		// 二星火炮塔绿宝石
	public const int HUO_PAO_2_BLUE_ID = 8105;		// 二星火炮塔蓝宝石
	public const int HUO_PAO_3_ID = 8201;			// 三星火炮塔
	public const int HUO_PAO_3_PURPLE_ID = 8202;	// 三星火炮塔紫宝石
	public const int HUO_PAO_3_RED_ID = 8203;		// 三星火炮塔红宝石
	public const int HUO_PAO_3_GREEN_ID = 8204;		// 三星火炮塔绿宝石
	public const int HUO_PAO_3_BLUE_ID = 8205;		// 三星火炮塔蓝宝石
	public const int QIU_XING_FEI_DAN_1_ID = 9001;	// 一星回旋飞镖塔
	public const int QIU_XING_FEI_DAN_1_PURPLE_ID = 9002;// 一星回旋飞镖塔紫宝石
	public const int QIU_XING_FEI_DAN_1_RED_ID = 9003;// 一星回旋飞镖塔红宝石
	public const int QIU_XING_FEI_DAN_1_GREEN_ID = 9004;// 一星回旋飞镖塔绿宝石
	public const int QIU_XING_FEI_DAN_1_BLUE_ID = 9005;// 一星回旋飞镖塔蓝宝石
	public const int QIU_XING_FEI_DAN_2_ID = 9101;	// 二星回旋飞镖塔
	public const int QIU_XING_FEI_DAN_2_PURPLE_ID = 9102;// 二星回旋飞镖塔紫宝石
	public const int QIU_XING_FEI_DAN_2_RED_ID = 9103;// 二星回旋飞镖塔红宝石
	public const int QIU_XING_FEI_DAN_2_GREEN_ID = 9104;// 二星回旋飞镖塔绿宝石
	public const int QIU_XING_FEI_DAN_2_BLUE_ID = 9105;// 二星回旋飞镖塔蓝宝石
	public const int QIU_XING_FEI_DAN_3_ID = 9201;	// 三星回旋飞镖塔
	public const int QIU_XING_FEI_DAN_3_PURPLE_ID = 9202;// 三星回旋飞镖塔紫宝石
	public const int QIU_XING_FEI_DAN_3_RED_ID = 9203;// 三星回旋飞镖塔红宝石
	public const int QIU_XING_FEI_DAN_3_GREEN_ID = 9204;// 三星回旋飞镖塔绿宝石
	public const int QIU_XING_FEI_DAN_3_BLUE_ID = 9205;// 三星回旋飞镖塔蓝宝石
	public const int XIAN_DAN_1_ID = 10001;			// 一星霰弹塔
	public const int XIAN_DAN_1_PURPLE_ID = 10002;	// 一星霰弹塔紫宝石
	public const int XIAN_DAN_1_RED_ID = 10003;		// 一星霰弹塔红宝石
	public const int XIAN_DAN_1_GREEN_ID = 10004;	// 一星霰弹塔绿宝石
	public const int XIAN_DAN_1_BLUE_ID = 10005;	// 一星霰弹塔蓝宝石
	public const int XIAN_DAN_2_ID = 10101;			// 二星霰弹塔
	public const int XIAN_DAN_2_PURPLE_ID = 10102;	// 二星霰弹塔紫宝石
	public const int XIAN_DAN_2_RED_ID = 10103;		// 二星霰弹塔红宝石
	public const int XIAN_DAN_2_GREEN_ID = 10104;	// 二星霰弹塔绿宝石
	public const int XIAN_DAN_2_BLUE_ID = 10105;	// 二星霰弹塔蓝宝石
	public const int XIAN_DAN_3_ID = 10201;			// 三星霰弹塔
	public const int XIAN_DAN_3_PURPLE_ID = 10202;	// 三星霰弹塔紫宝石
	public const int XIAN_DAN_3_RED_ID = 10203;		// 三星霰弹塔红宝石
	public const int XIAN_DAN_3_GREEN_ID = 10204;	// 三星霰弹塔绿宝石
	public const int XIAN_DAN_3_BLUE_ID = 10205;	// 三星霰弹塔蓝宝石
	public const int TIAN_KONG_ZHI_MAO_1_ID = 11001;// 一星天空之矛
	public const int TIAN_KONG_ZHI_MAO_1_PURPLE_ID = 11002;// 一星天空之矛紫宝石
	public const int TIAN_KONG_ZHI_MAO_1_RED_ID = 11003;// 一星天空之矛红宝石
	public const int TIAN_KONG_ZHI_MAO_1_GREEN_ID = 11004;// 一星天空之矛绿宝石
	public const int TIAN_KONG_ZHI_MAO_1_BLUE_ID = 11005;// 一星天空之矛蓝宝石
	public const int TIAN_KONG_ZHI_MAO_2_ID = 11101;// 二星天空之矛
	public const int TIAN_KONG_ZHI_MAO_2_PURPLE_ID = 11102;// 二星天空之矛紫宝石
	public const int TIAN_KONG_ZHI_MAO_2_RED_ID = 11103;// 二星天空之矛红宝石
	public const int TIAN_KONG_ZHI_MAO_2_GREEN_ID = 11104;// 二星天空之矛绿宝石
	public const int TIAN_KONG_ZHI_MAO_2_BLUE_ID = 11105;// 二星天空之矛蓝宝石
	public const int TIAN_KONG_ZHI_MAO_3_ID = 11201;// 三星天空之矛
	public const int TIAN_KONG_ZHI_MAO_3_PURPLE_ID = 11202;// 三星天空之矛紫宝石
	public const int TIAN_KONG_ZHI_MAO_3_RED_ID = 11203;// 三星天空之矛红宝石
	public const int TIAN_KONG_ZHI_MAO_3_GREEN_ID = 11204;// 三星天空之矛绿宝石
	public const int TIAN_KONG_ZHI_MAO_3_BLUE_ID = 11205;// 三星天空之矛蓝宝石
	public const int FEI_BIAO_FA_SHE_1_ID = 12001;	// 一星飞镖发射器
	public const int FEI_BIAO_FA_SHE_1_PURPLE_ID = 12002;// 一星飞镖发射器紫宝石
	public const int FEI_BIAO_FA_SHE_1_RED_ID = 12003;// 一星飞镖发射器红宝石
	public const int FEI_BIAO_FA_SHE_1_GREEN_ID = 12004;// 一星飞镖发射器绿宝石
	public const int FEI_BIAO_FA_SHE_1_BLUE_ID = 12005;// 一星飞镖发射器蓝宝石
	public const int FEI_BIAO_FA_SHE_2_ID = 12101;	// 二星飞镖发射器
	public const int FEI_BIAO_FA_SHE_2_PURPLE_ID = 12102;// 二星飞镖发射器紫宝石
	public const int FEI_BIAO_FA_SHE_2_RED_ID = 12103;// 二星飞镖发射器红宝石
	public const int FEI_BIAO_FA_SHE_2_GREEN_ID = 12104;// 二星飞镖发射器绿宝石
	public const int FEI_BIAO_FA_SHE_2_BLUE_ID = 12105;// 二星飞镖发射器蓝宝石
	public const int FEI_BIAO_FA_SHE_3_ID = 12201;	// 三星飞镖发射器
	public const int FEI_BIAO_FA_SHE_3_PURPLE_ID = 12202;// 三星飞镖发射器紫宝石
	public const int FEI_BIAO_FA_SHE_3_RED_ID = 12203;// 三星飞镖发射器红宝石
	public const int FEI_BIAO_FA_SHE_3_GREEN_ID = 12204;// 三星飞镖发射器绿宝石
	public const int FEI_BIAO_FA_SHE_3_BLUE_ID = 12205;// 三星飞镖发射器蓝宝石
	public const int QI_QIU_ZHA_DAN_1_ID = 13001;	// 一星气球炸弹塔
	public const int QI_QIU_ZHA_DAN_1_PURPLE_ID = 13002;// 一星气球炸弹塔紫宝石
	public const int QI_QIU_ZHA_DAN_1_RED_ID = 13003;// 一星气球炸弹塔红宝石
	public const int QI_QIU_ZHA_DAN_1_GREEN_ID = 13004;// 一星气球炸弹塔绿宝石
	public const int QI_QIU_ZHA_DAN_1_BLUE_ID = 13005;// 一星气球炸弹塔蓝宝石
	public const int QI_QIU_ZHA_DAN_2_ID = 13101;	// 二星气球炸弹塔
	public const int QI_QIU_ZHA_DAN_2_PURPLE_ID = 13102;// 二星气球炸弹塔紫宝石
	public const int QI_QIU_ZHA_DAN_2_RED_ID = 13103;// 二星气球炸弹塔红宝石
	public const int QI_QIU_ZHA_DAN_2_GREEN_ID = 13104;// 二星气球炸弹塔绿宝石
	public const int QI_QIU_ZHA_DAN_2_BLUE_ID = 13105;// 二星气球炸弹塔蓝宝石
	public const int QI_QIU_ZHA_DAN_3_ID = 13201;	// 三星气球炸弹塔
	public const int QI_QIU_ZHA_DAN_3_PURPLE_ID = 13202;// 三星气球炸弹塔紫宝石
	public const int QI_QIU_ZHA_DAN_3_RED_ID = 13203;// 三星气球炸弹塔红宝石
	public const int QI_QIU_ZHA_DAN_3_GREEN_ID = 13204;// 三星气球炸弹塔绿宝石
	public const int QI_QIU_ZHA_DAN_3_BLUE_ID = 13205;// 三星气球炸弹塔蓝宝石
	public const int ZHEN_DANG_1_ID = 14001;		// 一星电磁震荡塔
	public const int ZHEN_DANG_1_PURPLE_ID = 14002;	// 一星电磁震荡塔紫宝石
	public const int ZHEN_DANG_1_RED_ID = 14003;	// 一星电磁震荡塔红宝石
	public const int ZHEN_DANG_1_GREEN_ID = 14004;	// 一星电磁震荡塔绿宝石
	public const int ZHEN_DANG_1_BLUE_ID = 14005;	// 一星电磁震荡塔蓝宝石
	public const int ZHEN_DANG_2_ID = 14101;		// 二星电磁震荡塔
	public const int ZHEN_DANG_2_PURPLE_ID = 14102;	// 二星电磁震荡塔紫宝石
	public const int ZHEN_DANG_2_RED_ID = 14103;	// 二星电磁震荡塔红宝石
	public const int ZHEN_DANG_2_GREEN_ID = 14104;	// 二星电磁震荡塔绿宝石
	public const int ZHEN_DANG_2_BLUE_ID = 14105;	// 二星电磁震荡塔蓝宝石
	public const int ZHEN_DANG_3_ID = 14201;		// 三星电磁震荡塔
	public const int ZHEN_DANG_3_PURPLE_ID = 14202;	// 三星电磁震荡塔紫宝石
	public const int ZHEN_DANG_3_RED_ID = 14203;	// 三星电磁震荡塔红宝石
	public const int ZHEN_DANG_3_GREEN_ID = 14204;	// 三星电磁震荡塔绿宝石
	public const int ZHEN_DANG_3_BLUE_ID = 14205;	// 三星电磁震荡塔蓝宝石
	public const int BO_DONG_1_ID = 15001;			// 一星风刃发射器
	public const int BO_DONG_1_PURPLE_ID = 15002;	// 一星风刃发射器紫宝石
	public const int BO_DONG_1_RED_ID = 15003;		// 一星风刃发射器红宝石
	public const int BO_DONG_1_GREEN_ID = 15004;	// 一星风刃发射器绿宝石
	public const int BO_DONG_1_BLUE_ID = 15005;		// 一星风刃发射器蓝宝石
	public const int BO_DONG_2_ID = 15101;			// 二星风刃发射器
	public const int BO_DONG_2_PURPLE_ID = 15102;	// 二星风刃发射器紫宝石
	public const int BO_DONG_2_RED_ID = 15103;		// 二星风刃发射器红宝石
	public const int BO_DONG_2_GREEN_ID = 15104;	// 二星风刃发射器绿宝石
	public const int BO_DONG_2_BLUE_ID = 15105;		// 二星风刃发射器蓝宝石
	public const int BO_DONG_3_ID = 15201;			// 三星风刃发射器
	public const int BO_DONG_3_PURPLE_ID = 15202;	// 三星风刃发射器紫宝石
	public const int BO_DONG_3_RED_ID = 15203;		// 三星风刃发射器红宝石
	public const int BO_DONG_3_GREEN_ID = 15204;	// 三星风刃发射器绿宝石
	public const int BO_DONG_3_BLUE_ID = 15205;		// 三星风刃发射器蓝宝石

	public static EDTowerSkill _SHI_ZI_GONG_1;		// 一星十字弓
	public static EDTowerSkill _SHI_ZI_GONG_1_PURPLE;// 一星十字弓紫宝石
	public static EDTowerSkill _SHI_ZI_GONG_1_RED;	// 一星十字弓红宝石
	public static EDTowerSkill _SHI_ZI_GONG_1_GREEN;// 一星十字弓绿宝石
	public static EDTowerSkill _SHI_ZI_GONG_1_BLUE;	// 一星十字弓蓝宝石
	public static EDTowerSkill _SHI_ZI_GONG_2;		// 二星十字弓
	public static EDTowerSkill _SHI_ZI_GONG_2_PURPLE;// 二星十字弓紫宝石
	public static EDTowerSkill _SHI_ZI_GONG_2_RED;	// 二星十字弓红宝石
	public static EDTowerSkill _SHI_ZI_GONG_2_GREEN;// 二星十字弓绿宝石
	public static EDTowerSkill _SHI_ZI_GONG_2_BLUE;	// 二星十字弓蓝宝石
	public static EDTowerSkill _SHI_ZI_GONG_3;		// 三星十字弓
	public static EDTowerSkill _SHI_ZI_GONG_3_PURPLE;// 三星十字弓紫宝石
	public static EDTowerSkill _SHI_ZI_GONG_3_RED;	// 三星十字弓红宝石
	public static EDTowerSkill _SHI_ZI_GONG_3_GREEN;// 三星十字弓绿宝石
	public static EDTowerSkill _SHI_ZI_GONG_3_BLUE;	// 三星十字弓蓝宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_1;// 一星防空飞弹塔
	public static EDTowerSkill _FANG_KONG_FEI_DAN_1_PURPLE;// 一星防空飞弹塔紫宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_1_RED;// 一星防空飞弹塔红宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_1_GREEN;// 一星防空飞弹塔绿宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_1_BLUE;// 一星防空飞弹塔蓝宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_2;// 二星防空飞弹塔
	public static EDTowerSkill _FANG_KONG_FEI_DAN_2_PURPLE;// 二星防空飞弹塔紫宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_2_RED;// 二星防空飞弹塔红宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_2_GREEN;// 二星防空飞弹塔绿宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_2_BLUE;// 二星防空飞弹塔蓝宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_3;// 三星防空飞弹塔
	public static EDTowerSkill _FANG_KONG_FEI_DAN_3_PURPLE;// 三星防空飞弹塔紫宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_3_RED;// 三星防空飞弹塔红宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_3_GREEN;// 三星防空飞弹塔绿宝石
	public static EDTowerSkill _FANG_KONG_FEI_DAN_3_BLUE;// 三星防空飞弹塔蓝宝石
	public static EDTowerSkill _TOU_SHI_JI_1;		// 一星投石机
	public static EDTowerSkill _TOU_SHI_JI_1_PURPLE;// 一星投石机紫宝石
	public static EDTowerSkill _TOU_SHI_JI_1_RED;	// 一星投石机红宝石
	public static EDTowerSkill _TOU_SHI_JI_1_GREEN;	// 一星投石机绿宝石
	public static EDTowerSkill _TOU_SHI_JI_1_BLUE;	// 一星投石机蓝宝石
	public static EDTowerSkill _TOU_SHI_JI_2;		// 二星投石机
	public static EDTowerSkill _TOU_SHI_JI_2_PURPLE;// 二星投石机紫宝石
	public static EDTowerSkill _TOU_SHI_JI_2_RED;	// 二星投石机红宝石
	public static EDTowerSkill _TOU_SHI_JI_2_GREEN;	// 二星投石机绿宝石
	public static EDTowerSkill _TOU_SHI_JI_2_BLUE;	// 二星投石机蓝宝石
	public static EDTowerSkill _TOU_SHI_JI_3;		// 三星投石机
	public static EDTowerSkill _TOU_SHI_JI_3_PURPLE;// 三星投石机紫宝石
	public static EDTowerSkill _TOU_SHI_JI_3_RED;	// 三星投石机红宝石
	public static EDTowerSkill _TOU_SHI_JI_3_GREEN;	// 三星投石机绿宝石
	public static EDTowerSkill _TOU_SHI_JI_3_BLUE;	// 三星投石机蓝宝石
	public static EDTowerSkill _XIANG_QIAN_1_PURPLE_1;// 一星镶嵌塔一星紫宝石
	public static EDTowerSkill _XIANG_QIAN_1_RED;	// 一星镶嵌塔红宝石
	public static EDTowerSkill _XIANG_QIAN_1_GREEN;	// 一星镶嵌塔绿宝石
	public static EDTowerSkill _XIANG_QIAN_1_BLUE;	// 一星镶嵌塔蓝宝石
	public static EDTowerSkill _XIANG_QIAN_1_PURPLE_2;// 一星镶嵌塔二星紫宝石
	public static EDTowerSkill _XIANG_QIAN_1_PURPLE_3;// 一星镶嵌塔三星紫宝石
	public static EDTowerSkill _XIANG_QIAN_1_PURPLE_4;// 一星镶嵌塔四星紫宝石
	public static EDTowerSkill _XIANG_QIAN_1_PURPLE_5;// 一星镶嵌塔五星紫宝石
	public static EDTowerSkill _XIANG_QIAN_2_PURPLE_1;// 二星镶嵌塔一星紫宝石
	public static EDTowerSkill _XIANG_QIAN_2_RED;	// 二星镶嵌塔红宝石
	public static EDTowerSkill _XIANG_QIAN_2_GREEN;	// 二星镶嵌塔绿宝石
	public static EDTowerSkill _XIANG_QIAN_2_BLUE;	// 二星镶嵌塔蓝宝石
	public static EDTowerSkill _XIANG_QIAN_2_PURPLE_2;// 二星镶嵌塔二星紫宝石
	public static EDTowerSkill _XIANG_QIAN_2_PURPLE_3;// 二星镶嵌塔三星紫宝石
	public static EDTowerSkill _XIANG_QIAN_2_PURPLE_4;// 二星镶嵌塔四星紫宝石
	public static EDTowerSkill _XIANG_QIAN_2_PURPLE_5;// 二星镶嵌塔五星紫宝石
	public static EDTowerSkill _XIANG_QIAN_3_PURPLE_1;// 三星镶嵌塔一星紫宝石
	public static EDTowerSkill _XIANG_QIAN_3_RED;	// 三星镶嵌塔红宝石
	public static EDTowerSkill _XIANG_QIAN_3_GREEN;	// 三星镶嵌塔绿宝石
	public static EDTowerSkill _XIANG_QIAN_3_BLUE;	// 三星镶嵌塔蓝宝石
	public static EDTowerSkill _XIANG_QIAN_3_PURPLE_2;// 三星镶嵌塔二星紫宝石
	public static EDTowerSkill _XIANG_QIAN_3_PURPLE_3;// 三星镶嵌塔三星紫宝石
	public static EDTowerSkill _XIANG_QIAN_3_PURPLE_4;// 三星镶嵌塔四星紫宝石
	public static EDTowerSkill _XIANG_QIAN_3_PURPLE_5;// 三星镶嵌塔五星紫宝石
	public static EDTowerSkill _HUO_PAO_1;			// 一星火炮塔
	public static EDTowerSkill _HUO_PAO_1_PURPLE;	// 一星火炮塔紫宝石
	public static EDTowerSkill _HUO_PAO_1_RED;		// 一星火炮塔红宝石
	public static EDTowerSkill _HUO_PAO_1_GREEN;	// 一星火炮塔绿宝石
	public static EDTowerSkill _HUO_PAO_1_BLUE;		// 一星火炮塔蓝宝石
	public static EDTowerSkill _HUO_PAO_2;			// 二星火炮塔
	public static EDTowerSkill _HUO_PAO_2_PURPLE;	// 二星火炮塔紫宝石
	public static EDTowerSkill _HUO_PAO_2_RED;		// 二星火炮塔红宝石
	public static EDTowerSkill _HUO_PAO_2_GREEN;	// 二星火炮塔绿宝石
	public static EDTowerSkill _HUO_PAO_2_BLUE;		// 二星火炮塔蓝宝石
	public static EDTowerSkill _HUO_PAO_3;			// 三星火炮塔
	public static EDTowerSkill _HUO_PAO_3_PURPLE;	// 三星火炮塔紫宝石
	public static EDTowerSkill _HUO_PAO_3_RED;		// 三星火炮塔红宝石
	public static EDTowerSkill _HUO_PAO_3_GREEN;	// 三星火炮塔绿宝石
	public static EDTowerSkill _HUO_PAO_3_BLUE;		// 三星火炮塔蓝宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_1;	// 一星回旋飞镖塔
	public static EDTowerSkill _QIU_XING_FEI_DAN_1_PURPLE;// 一星回旋飞镖塔紫宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_1_RED;// 一星回旋飞镖塔红宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_1_GREEN;// 一星回旋飞镖塔绿宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_1_BLUE;// 一星回旋飞镖塔蓝宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_2;	// 二星回旋飞镖塔
	public static EDTowerSkill _QIU_XING_FEI_DAN_2_PURPLE;// 二星回旋飞镖塔紫宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_2_RED;// 二星回旋飞镖塔红宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_2_GREEN;// 二星回旋飞镖塔绿宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_2_BLUE;// 二星回旋飞镖塔蓝宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_3;	// 三星回旋飞镖塔
	public static EDTowerSkill _QIU_XING_FEI_DAN_3_PURPLE;// 三星回旋飞镖塔紫宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_3_RED;// 三星回旋飞镖塔红宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_3_GREEN;// 三星回旋飞镖塔绿宝石
	public static EDTowerSkill _QIU_XING_FEI_DAN_3_BLUE;// 三星回旋飞镖塔蓝宝石
	public static EDTowerSkill _XIAN_DAN_1;			// 一星霰弹塔
	public static EDTowerSkill _XIAN_DAN_1_PURPLE;	// 一星霰弹塔紫宝石
	public static EDTowerSkill _XIAN_DAN_1_RED;		// 一星霰弹塔红宝石
	public static EDTowerSkill _XIAN_DAN_1_GREEN;	// 一星霰弹塔绿宝石
	public static EDTowerSkill _XIAN_DAN_1_BLUE;	// 一星霰弹塔蓝宝石
	public static EDTowerSkill _XIAN_DAN_2;			// 二星霰弹塔
	public static EDTowerSkill _XIAN_DAN_2_PURPLE;	// 二星霰弹塔紫宝石
	public static EDTowerSkill _XIAN_DAN_2_RED;		// 二星霰弹塔红宝石
	public static EDTowerSkill _XIAN_DAN_2_GREEN;	// 二星霰弹塔绿宝石
	public static EDTowerSkill _XIAN_DAN_2_BLUE;	// 二星霰弹塔蓝宝石
	public static EDTowerSkill _XIAN_DAN_3;			// 三星霰弹塔
	public static EDTowerSkill _XIAN_DAN_3_PURPLE;	// 三星霰弹塔紫宝石
	public static EDTowerSkill _XIAN_DAN_3_RED;		// 三星霰弹塔红宝石
	public static EDTowerSkill _XIAN_DAN_3_GREEN;	// 三星霰弹塔绿宝石
	public static EDTowerSkill _XIAN_DAN_3_BLUE;	// 三星霰弹塔蓝宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_1;// 一星天空之矛
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_1_PURPLE;// 一星天空之矛紫宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_1_RED;// 一星天空之矛红宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_1_GREEN;// 一星天空之矛绿宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_1_BLUE;// 一星天空之矛蓝宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_2;// 二星天空之矛
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_2_PURPLE;// 二星天空之矛紫宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_2_RED;// 二星天空之矛红宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_2_GREEN;// 二星天空之矛绿宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_2_BLUE;// 二星天空之矛蓝宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_3;// 三星天空之矛
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_3_PURPLE;// 三星天空之矛紫宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_3_RED;// 三星天空之矛红宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_3_GREEN;// 三星天空之矛绿宝石
	public static EDTowerSkill _TIAN_KONG_ZHI_MAO_3_BLUE;// 三星天空之矛蓝宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_1;	// 一星飞镖发射器
	public static EDTowerSkill _FEI_BIAO_FA_SHE_1_PURPLE;// 一星飞镖发射器紫宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_1_RED;// 一星飞镖发射器红宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_1_GREEN;// 一星飞镖发射器绿宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_1_BLUE;// 一星飞镖发射器蓝宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_2;	// 二星飞镖发射器
	public static EDTowerSkill _FEI_BIAO_FA_SHE_2_PURPLE;// 二星飞镖发射器紫宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_2_RED;// 二星飞镖发射器红宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_2_GREEN;// 二星飞镖发射器绿宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_2_BLUE;// 二星飞镖发射器蓝宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_3;	// 三星飞镖发射器
	public static EDTowerSkill _FEI_BIAO_FA_SHE_3_PURPLE;// 三星飞镖发射器紫宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_3_RED;// 三星飞镖发射器红宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_3_GREEN;// 三星飞镖发射器绿宝石
	public static EDTowerSkill _FEI_BIAO_FA_SHE_3_BLUE;// 三星飞镖发射器蓝宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_1;	// 一星气球炸弹塔
	public static EDTowerSkill _QI_QIU_ZHA_DAN_1_PURPLE;// 一星气球炸弹塔紫宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_1_RED;// 一星气球炸弹塔红宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_1_GREEN;// 一星气球炸弹塔绿宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_1_BLUE;// 一星气球炸弹塔蓝宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_2;	// 二星气球炸弹塔
	public static EDTowerSkill _QI_QIU_ZHA_DAN_2_PURPLE;// 二星气球炸弹塔紫宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_2_RED;// 二星气球炸弹塔红宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_2_GREEN;// 二星气球炸弹塔绿宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_2_BLUE;// 二星气球炸弹塔蓝宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_3;	// 三星气球炸弹塔
	public static EDTowerSkill _QI_QIU_ZHA_DAN_3_PURPLE;// 三星气球炸弹塔紫宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_3_RED;// 三星气球炸弹塔红宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_3_GREEN;// 三星气球炸弹塔绿宝石
	public static EDTowerSkill _QI_QIU_ZHA_DAN_3_BLUE;// 三星气球炸弹塔蓝宝石
	public static EDTowerSkill _ZHEN_DANG_1;		// 一星电磁震荡塔
	public static EDTowerSkill _ZHEN_DANG_1_PURPLE;	// 一星电磁震荡塔紫宝石
	public static EDTowerSkill _ZHEN_DANG_1_RED;	// 一星电磁震荡塔红宝石
	public static EDTowerSkill _ZHEN_DANG_1_GREEN;	// 一星电磁震荡塔绿宝石
	public static EDTowerSkill _ZHEN_DANG_1_BLUE;	// 一星电磁震荡塔蓝宝石
	public static EDTowerSkill _ZHEN_DANG_2;		// 二星电磁震荡塔
	public static EDTowerSkill _ZHEN_DANG_2_PURPLE;	// 二星电磁震荡塔紫宝石
	public static EDTowerSkill _ZHEN_DANG_2_RED;	// 二星电磁震荡塔红宝石
	public static EDTowerSkill _ZHEN_DANG_2_GREEN;	// 二星电磁震荡塔绿宝石
	public static EDTowerSkill _ZHEN_DANG_2_BLUE;	// 二星电磁震荡塔蓝宝石
	public static EDTowerSkill _ZHEN_DANG_3;		// 三星电磁震荡塔
	public static EDTowerSkill _ZHEN_DANG_3_PURPLE;	// 三星电磁震荡塔紫宝石
	public static EDTowerSkill _ZHEN_DANG_3_RED;	// 三星电磁震荡塔红宝石
	public static EDTowerSkill _ZHEN_DANG_3_GREEN;	// 三星电磁震荡塔绿宝石
	public static EDTowerSkill _ZHEN_DANG_3_BLUE;	// 三星电磁震荡塔蓝宝石
	public static EDTowerSkill _BO_DONG_1;			// 一星风刃发射器
	public static EDTowerSkill _BO_DONG_1_PURPLE;	// 一星风刃发射器紫宝石
	public static EDTowerSkill _BO_DONG_1_RED;		// 一星风刃发射器红宝石
	public static EDTowerSkill _BO_DONG_1_GREEN;	// 一星风刃发射器绿宝石
	public static EDTowerSkill _BO_DONG_1_BLUE;		// 一星风刃发射器蓝宝石
	public static EDTowerSkill _BO_DONG_2;			// 二星风刃发射器
	public static EDTowerSkill _BO_DONG_2_PURPLE;	// 二星风刃发射器紫宝石
	public static EDTowerSkill _BO_DONG_2_RED;		// 二星风刃发射器红宝石
	public static EDTowerSkill _BO_DONG_2_GREEN;	// 二星风刃发射器绿宝石
	public static EDTowerSkill _BO_DONG_2_BLUE;		// 二星风刃发射器蓝宝石
	public static EDTowerSkill _BO_DONG_3;			// 三星风刃发射器
	public static EDTowerSkill _BO_DONG_3_PURPLE;	// 三星风刃发射器紫宝石
	public static EDTowerSkill _BO_DONG_3_RED;		// 三星风刃发射器红宝石
	public static EDTowerSkill _BO_DONG_3_GREEN;	// 三星风刃发射器绿宝石
	public static EDTowerSkill _BO_DONG_3_BLUE;		// 三星风刃发射器蓝宝石

	public static EDTowerSkill SHI_ZI_GONG_1 { get { return _SHI_ZI_GONG_1 ??= mTable.query(SHI_ZI_GONG_1_ID); } }// 一星十字弓
	public static EDTowerSkill SHI_ZI_GONG_1_PURPLE { get { return _SHI_ZI_GONG_1_PURPLE ??= mTable.query(SHI_ZI_GONG_1_PURPLE_ID); } }// 一星十字弓紫宝石
	public static EDTowerSkill SHI_ZI_GONG_1_RED { get { return _SHI_ZI_GONG_1_RED ??= mTable.query(SHI_ZI_GONG_1_RED_ID); } }// 一星十字弓红宝石
	public static EDTowerSkill SHI_ZI_GONG_1_GREEN { get { return _SHI_ZI_GONG_1_GREEN ??= mTable.query(SHI_ZI_GONG_1_GREEN_ID); } }// 一星十字弓绿宝石
	public static EDTowerSkill SHI_ZI_GONG_1_BLUE { get { return _SHI_ZI_GONG_1_BLUE ??= mTable.query(SHI_ZI_GONG_1_BLUE_ID); } }// 一星十字弓蓝宝石
	public static EDTowerSkill SHI_ZI_GONG_2 { get { return _SHI_ZI_GONG_2 ??= mTable.query(SHI_ZI_GONG_2_ID); } }// 二星十字弓
	public static EDTowerSkill SHI_ZI_GONG_2_PURPLE { get { return _SHI_ZI_GONG_2_PURPLE ??= mTable.query(SHI_ZI_GONG_2_PURPLE_ID); } }// 二星十字弓紫宝石
	public static EDTowerSkill SHI_ZI_GONG_2_RED { get { return _SHI_ZI_GONG_2_RED ??= mTable.query(SHI_ZI_GONG_2_RED_ID); } }// 二星十字弓红宝石
	public static EDTowerSkill SHI_ZI_GONG_2_GREEN { get { return _SHI_ZI_GONG_2_GREEN ??= mTable.query(SHI_ZI_GONG_2_GREEN_ID); } }// 二星十字弓绿宝石
	public static EDTowerSkill SHI_ZI_GONG_2_BLUE { get { return _SHI_ZI_GONG_2_BLUE ??= mTable.query(SHI_ZI_GONG_2_BLUE_ID); } }// 二星十字弓蓝宝石
	public static EDTowerSkill SHI_ZI_GONG_3 { get { return _SHI_ZI_GONG_3 ??= mTable.query(SHI_ZI_GONG_3_ID); } }// 三星十字弓
	public static EDTowerSkill SHI_ZI_GONG_3_PURPLE { get { return _SHI_ZI_GONG_3_PURPLE ??= mTable.query(SHI_ZI_GONG_3_PURPLE_ID); } }// 三星十字弓紫宝石
	public static EDTowerSkill SHI_ZI_GONG_3_RED { get { return _SHI_ZI_GONG_3_RED ??= mTable.query(SHI_ZI_GONG_3_RED_ID); } }// 三星十字弓红宝石
	public static EDTowerSkill SHI_ZI_GONG_3_GREEN { get { return _SHI_ZI_GONG_3_GREEN ??= mTable.query(SHI_ZI_GONG_3_GREEN_ID); } }// 三星十字弓绿宝石
	public static EDTowerSkill SHI_ZI_GONG_3_BLUE { get { return _SHI_ZI_GONG_3_BLUE ??= mTable.query(SHI_ZI_GONG_3_BLUE_ID); } }// 三星十字弓蓝宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_1 { get { return _FANG_KONG_FEI_DAN_1 ??= mTable.query(FANG_KONG_FEI_DAN_1_ID); } }// 一星防空飞弹塔
	public static EDTowerSkill FANG_KONG_FEI_DAN_1_PURPLE { get { return _FANG_KONG_FEI_DAN_1_PURPLE ??= mTable.query(FANG_KONG_FEI_DAN_1_PURPLE_ID); } }// 一星防空飞弹塔紫宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_1_RED { get { return _FANG_KONG_FEI_DAN_1_RED ??= mTable.query(FANG_KONG_FEI_DAN_1_RED_ID); } }// 一星防空飞弹塔红宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_1_GREEN { get { return _FANG_KONG_FEI_DAN_1_GREEN ??= mTable.query(FANG_KONG_FEI_DAN_1_GREEN_ID); } }// 一星防空飞弹塔绿宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_1_BLUE { get { return _FANG_KONG_FEI_DAN_1_BLUE ??= mTable.query(FANG_KONG_FEI_DAN_1_BLUE_ID); } }// 一星防空飞弹塔蓝宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_2 { get { return _FANG_KONG_FEI_DAN_2 ??= mTable.query(FANG_KONG_FEI_DAN_2_ID); } }// 二星防空飞弹塔
	public static EDTowerSkill FANG_KONG_FEI_DAN_2_PURPLE { get { return _FANG_KONG_FEI_DAN_2_PURPLE ??= mTable.query(FANG_KONG_FEI_DAN_2_PURPLE_ID); } }// 二星防空飞弹塔紫宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_2_RED { get { return _FANG_KONG_FEI_DAN_2_RED ??= mTable.query(FANG_KONG_FEI_DAN_2_RED_ID); } }// 二星防空飞弹塔红宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_2_GREEN { get { return _FANG_KONG_FEI_DAN_2_GREEN ??= mTable.query(FANG_KONG_FEI_DAN_2_GREEN_ID); } }// 二星防空飞弹塔绿宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_2_BLUE { get { return _FANG_KONG_FEI_DAN_2_BLUE ??= mTable.query(FANG_KONG_FEI_DAN_2_BLUE_ID); } }// 二星防空飞弹塔蓝宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_3 { get { return _FANG_KONG_FEI_DAN_3 ??= mTable.query(FANG_KONG_FEI_DAN_3_ID); } }// 三星防空飞弹塔
	public static EDTowerSkill FANG_KONG_FEI_DAN_3_PURPLE { get { return _FANG_KONG_FEI_DAN_3_PURPLE ??= mTable.query(FANG_KONG_FEI_DAN_3_PURPLE_ID); } }// 三星防空飞弹塔紫宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_3_RED { get { return _FANG_KONG_FEI_DAN_3_RED ??= mTable.query(FANG_KONG_FEI_DAN_3_RED_ID); } }// 三星防空飞弹塔红宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_3_GREEN { get { return _FANG_KONG_FEI_DAN_3_GREEN ??= mTable.query(FANG_KONG_FEI_DAN_3_GREEN_ID); } }// 三星防空飞弹塔绿宝石
	public static EDTowerSkill FANG_KONG_FEI_DAN_3_BLUE { get { return _FANG_KONG_FEI_DAN_3_BLUE ??= mTable.query(FANG_KONG_FEI_DAN_3_BLUE_ID); } }// 三星防空飞弹塔蓝宝石
	public static EDTowerSkill TOU_SHI_JI_1 { get { return _TOU_SHI_JI_1 ??= mTable.query(TOU_SHI_JI_1_ID); } }// 一星投石机
	public static EDTowerSkill TOU_SHI_JI_1_PURPLE { get { return _TOU_SHI_JI_1_PURPLE ??= mTable.query(TOU_SHI_JI_1_PURPLE_ID); } }// 一星投石机紫宝石
	public static EDTowerSkill TOU_SHI_JI_1_RED { get { return _TOU_SHI_JI_1_RED ??= mTable.query(TOU_SHI_JI_1_RED_ID); } }// 一星投石机红宝石
	public static EDTowerSkill TOU_SHI_JI_1_GREEN { get { return _TOU_SHI_JI_1_GREEN ??= mTable.query(TOU_SHI_JI_1_GREEN_ID); } }// 一星投石机绿宝石
	public static EDTowerSkill TOU_SHI_JI_1_BLUE { get { return _TOU_SHI_JI_1_BLUE ??= mTable.query(TOU_SHI_JI_1_BLUE_ID); } }// 一星投石机蓝宝石
	public static EDTowerSkill TOU_SHI_JI_2 { get { return _TOU_SHI_JI_2 ??= mTable.query(TOU_SHI_JI_2_ID); } }// 二星投石机
	public static EDTowerSkill TOU_SHI_JI_2_PURPLE { get { return _TOU_SHI_JI_2_PURPLE ??= mTable.query(TOU_SHI_JI_2_PURPLE_ID); } }// 二星投石机紫宝石
	public static EDTowerSkill TOU_SHI_JI_2_RED { get { return _TOU_SHI_JI_2_RED ??= mTable.query(TOU_SHI_JI_2_RED_ID); } }// 二星投石机红宝石
	public static EDTowerSkill TOU_SHI_JI_2_GREEN { get { return _TOU_SHI_JI_2_GREEN ??= mTable.query(TOU_SHI_JI_2_GREEN_ID); } }// 二星投石机绿宝石
	public static EDTowerSkill TOU_SHI_JI_2_BLUE { get { return _TOU_SHI_JI_2_BLUE ??= mTable.query(TOU_SHI_JI_2_BLUE_ID); } }// 二星投石机蓝宝石
	public static EDTowerSkill TOU_SHI_JI_3 { get { return _TOU_SHI_JI_3 ??= mTable.query(TOU_SHI_JI_3_ID); } }// 三星投石机
	public static EDTowerSkill TOU_SHI_JI_3_PURPLE { get { return _TOU_SHI_JI_3_PURPLE ??= mTable.query(TOU_SHI_JI_3_PURPLE_ID); } }// 三星投石机紫宝石
	public static EDTowerSkill TOU_SHI_JI_3_RED { get { return _TOU_SHI_JI_3_RED ??= mTable.query(TOU_SHI_JI_3_RED_ID); } }// 三星投石机红宝石
	public static EDTowerSkill TOU_SHI_JI_3_GREEN { get { return _TOU_SHI_JI_3_GREEN ??= mTable.query(TOU_SHI_JI_3_GREEN_ID); } }// 三星投石机绿宝石
	public static EDTowerSkill TOU_SHI_JI_3_BLUE { get { return _TOU_SHI_JI_3_BLUE ??= mTable.query(TOU_SHI_JI_3_BLUE_ID); } }// 三星投石机蓝宝石
	public static EDTowerSkill XIANG_QIAN_1_PURPLE_1 { get { return _XIANG_QIAN_1_PURPLE_1 ??= mTable.query(XIANG_QIAN_1_PURPLE_1_ID); } }// 一星镶嵌塔一星紫宝石
	public static EDTowerSkill XIANG_QIAN_1_RED { get { return _XIANG_QIAN_1_RED ??= mTable.query(XIANG_QIAN_1_RED_ID); } }// 一星镶嵌塔红宝石
	public static EDTowerSkill XIANG_QIAN_1_GREEN { get { return _XIANG_QIAN_1_GREEN ??= mTable.query(XIANG_QIAN_1_GREEN_ID); } }// 一星镶嵌塔绿宝石
	public static EDTowerSkill XIANG_QIAN_1_BLUE { get { return _XIANG_QIAN_1_BLUE ??= mTable.query(XIANG_QIAN_1_BLUE_ID); } }// 一星镶嵌塔蓝宝石
	public static EDTowerSkill XIANG_QIAN_1_PURPLE_2 { get { return _XIANG_QIAN_1_PURPLE_2 ??= mTable.query(XIANG_QIAN_1_PURPLE_2_ID); } }// 一星镶嵌塔二星紫宝石
	public static EDTowerSkill XIANG_QIAN_1_PURPLE_3 { get { return _XIANG_QIAN_1_PURPLE_3 ??= mTable.query(XIANG_QIAN_1_PURPLE_3_ID); } }// 一星镶嵌塔三星紫宝石
	public static EDTowerSkill XIANG_QIAN_1_PURPLE_4 { get { return _XIANG_QIAN_1_PURPLE_4 ??= mTable.query(XIANG_QIAN_1_PURPLE_4_ID); } }// 一星镶嵌塔四星紫宝石
	public static EDTowerSkill XIANG_QIAN_1_PURPLE_5 { get { return _XIANG_QIAN_1_PURPLE_5 ??= mTable.query(XIANG_QIAN_1_PURPLE_5_ID); } }// 一星镶嵌塔五星紫宝石
	public static EDTowerSkill XIANG_QIAN_2_PURPLE_1 { get { return _XIANG_QIAN_2_PURPLE_1 ??= mTable.query(XIANG_QIAN_2_PURPLE_1_ID); } }// 二星镶嵌塔一星紫宝石
	public static EDTowerSkill XIANG_QIAN_2_RED { get { return _XIANG_QIAN_2_RED ??= mTable.query(XIANG_QIAN_2_RED_ID); } }// 二星镶嵌塔红宝石
	public static EDTowerSkill XIANG_QIAN_2_GREEN { get { return _XIANG_QIAN_2_GREEN ??= mTable.query(XIANG_QIAN_2_GREEN_ID); } }// 二星镶嵌塔绿宝石
	public static EDTowerSkill XIANG_QIAN_2_BLUE { get { return _XIANG_QIAN_2_BLUE ??= mTable.query(XIANG_QIAN_2_BLUE_ID); } }// 二星镶嵌塔蓝宝石
	public static EDTowerSkill XIANG_QIAN_2_PURPLE_2 { get { return _XIANG_QIAN_2_PURPLE_2 ??= mTable.query(XIANG_QIAN_2_PURPLE_2_ID); } }// 二星镶嵌塔二星紫宝石
	public static EDTowerSkill XIANG_QIAN_2_PURPLE_3 { get { return _XIANG_QIAN_2_PURPLE_3 ??= mTable.query(XIANG_QIAN_2_PURPLE_3_ID); } }// 二星镶嵌塔三星紫宝石
	public static EDTowerSkill XIANG_QIAN_2_PURPLE_4 { get { return _XIANG_QIAN_2_PURPLE_4 ??= mTable.query(XIANG_QIAN_2_PURPLE_4_ID); } }// 二星镶嵌塔四星紫宝石
	public static EDTowerSkill XIANG_QIAN_2_PURPLE_5 { get { return _XIANG_QIAN_2_PURPLE_5 ??= mTable.query(XIANG_QIAN_2_PURPLE_5_ID); } }// 二星镶嵌塔五星紫宝石
	public static EDTowerSkill XIANG_QIAN_3_PURPLE_1 { get { return _XIANG_QIAN_3_PURPLE_1 ??= mTable.query(XIANG_QIAN_3_PURPLE_1_ID); } }// 三星镶嵌塔一星紫宝石
	public static EDTowerSkill XIANG_QIAN_3_RED { get { return _XIANG_QIAN_3_RED ??= mTable.query(XIANG_QIAN_3_RED_ID); } }// 三星镶嵌塔红宝石
	public static EDTowerSkill XIANG_QIAN_3_GREEN { get { return _XIANG_QIAN_3_GREEN ??= mTable.query(XIANG_QIAN_3_GREEN_ID); } }// 三星镶嵌塔绿宝石
	public static EDTowerSkill XIANG_QIAN_3_BLUE { get { return _XIANG_QIAN_3_BLUE ??= mTable.query(XIANG_QIAN_3_BLUE_ID); } }// 三星镶嵌塔蓝宝石
	public static EDTowerSkill XIANG_QIAN_3_PURPLE_2 { get { return _XIANG_QIAN_3_PURPLE_2 ??= mTable.query(XIANG_QIAN_3_PURPLE_2_ID); } }// 三星镶嵌塔二星紫宝石
	public static EDTowerSkill XIANG_QIAN_3_PURPLE_3 { get { return _XIANG_QIAN_3_PURPLE_3 ??= mTable.query(XIANG_QIAN_3_PURPLE_3_ID); } }// 三星镶嵌塔三星紫宝石
	public static EDTowerSkill XIANG_QIAN_3_PURPLE_4 { get { return _XIANG_QIAN_3_PURPLE_4 ??= mTable.query(XIANG_QIAN_3_PURPLE_4_ID); } }// 三星镶嵌塔四星紫宝石
	public static EDTowerSkill XIANG_QIAN_3_PURPLE_5 { get { return _XIANG_QIAN_3_PURPLE_5 ??= mTable.query(XIANG_QIAN_3_PURPLE_5_ID); } }// 三星镶嵌塔五星紫宝石
	public static EDTowerSkill HUO_PAO_1 { get { return _HUO_PAO_1 ??= mTable.query(HUO_PAO_1_ID); } }// 一星火炮塔
	public static EDTowerSkill HUO_PAO_1_PURPLE { get { return _HUO_PAO_1_PURPLE ??= mTable.query(HUO_PAO_1_PURPLE_ID); } }// 一星火炮塔紫宝石
	public static EDTowerSkill HUO_PAO_1_RED { get { return _HUO_PAO_1_RED ??= mTable.query(HUO_PAO_1_RED_ID); } }// 一星火炮塔红宝石
	public static EDTowerSkill HUO_PAO_1_GREEN { get { return _HUO_PAO_1_GREEN ??= mTable.query(HUO_PAO_1_GREEN_ID); } }// 一星火炮塔绿宝石
	public static EDTowerSkill HUO_PAO_1_BLUE { get { return _HUO_PAO_1_BLUE ??= mTable.query(HUO_PAO_1_BLUE_ID); } }// 一星火炮塔蓝宝石
	public static EDTowerSkill HUO_PAO_2 { get { return _HUO_PAO_2 ??= mTable.query(HUO_PAO_2_ID); } }// 二星火炮塔
	public static EDTowerSkill HUO_PAO_2_PURPLE { get { return _HUO_PAO_2_PURPLE ??= mTable.query(HUO_PAO_2_PURPLE_ID); } }// 二星火炮塔紫宝石
	public static EDTowerSkill HUO_PAO_2_RED { get { return _HUO_PAO_2_RED ??= mTable.query(HUO_PAO_2_RED_ID); } }// 二星火炮塔红宝石
	public static EDTowerSkill HUO_PAO_2_GREEN { get { return _HUO_PAO_2_GREEN ??= mTable.query(HUO_PAO_2_GREEN_ID); } }// 二星火炮塔绿宝石
	public static EDTowerSkill HUO_PAO_2_BLUE { get { return _HUO_PAO_2_BLUE ??= mTable.query(HUO_PAO_2_BLUE_ID); } }// 二星火炮塔蓝宝石
	public static EDTowerSkill HUO_PAO_3 { get { return _HUO_PAO_3 ??= mTable.query(HUO_PAO_3_ID); } }// 三星火炮塔
	public static EDTowerSkill HUO_PAO_3_PURPLE { get { return _HUO_PAO_3_PURPLE ??= mTable.query(HUO_PAO_3_PURPLE_ID); } }// 三星火炮塔紫宝石
	public static EDTowerSkill HUO_PAO_3_RED { get { return _HUO_PAO_3_RED ??= mTable.query(HUO_PAO_3_RED_ID); } }// 三星火炮塔红宝石
	public static EDTowerSkill HUO_PAO_3_GREEN { get { return _HUO_PAO_3_GREEN ??= mTable.query(HUO_PAO_3_GREEN_ID); } }// 三星火炮塔绿宝石
	public static EDTowerSkill HUO_PAO_3_BLUE { get { return _HUO_PAO_3_BLUE ??= mTable.query(HUO_PAO_3_BLUE_ID); } }// 三星火炮塔蓝宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_1 { get { return _QIU_XING_FEI_DAN_1 ??= mTable.query(QIU_XING_FEI_DAN_1_ID); } }// 一星回旋飞镖塔
	public static EDTowerSkill QIU_XING_FEI_DAN_1_PURPLE { get { return _QIU_XING_FEI_DAN_1_PURPLE ??= mTable.query(QIU_XING_FEI_DAN_1_PURPLE_ID); } }// 一星回旋飞镖塔紫宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_1_RED { get { return _QIU_XING_FEI_DAN_1_RED ??= mTable.query(QIU_XING_FEI_DAN_1_RED_ID); } }// 一星回旋飞镖塔红宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_1_GREEN { get { return _QIU_XING_FEI_DAN_1_GREEN ??= mTable.query(QIU_XING_FEI_DAN_1_GREEN_ID); } }// 一星回旋飞镖塔绿宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_1_BLUE { get { return _QIU_XING_FEI_DAN_1_BLUE ??= mTable.query(QIU_XING_FEI_DAN_1_BLUE_ID); } }// 一星回旋飞镖塔蓝宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_2 { get { return _QIU_XING_FEI_DAN_2 ??= mTable.query(QIU_XING_FEI_DAN_2_ID); } }// 二星回旋飞镖塔
	public static EDTowerSkill QIU_XING_FEI_DAN_2_PURPLE { get { return _QIU_XING_FEI_DAN_2_PURPLE ??= mTable.query(QIU_XING_FEI_DAN_2_PURPLE_ID); } }// 二星回旋飞镖塔紫宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_2_RED { get { return _QIU_XING_FEI_DAN_2_RED ??= mTable.query(QIU_XING_FEI_DAN_2_RED_ID); } }// 二星回旋飞镖塔红宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_2_GREEN { get { return _QIU_XING_FEI_DAN_2_GREEN ??= mTable.query(QIU_XING_FEI_DAN_2_GREEN_ID); } }// 二星回旋飞镖塔绿宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_2_BLUE { get { return _QIU_XING_FEI_DAN_2_BLUE ??= mTable.query(QIU_XING_FEI_DAN_2_BLUE_ID); } }// 二星回旋飞镖塔蓝宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_3 { get { return _QIU_XING_FEI_DAN_3 ??= mTable.query(QIU_XING_FEI_DAN_3_ID); } }// 三星回旋飞镖塔
	public static EDTowerSkill QIU_XING_FEI_DAN_3_PURPLE { get { return _QIU_XING_FEI_DAN_3_PURPLE ??= mTable.query(QIU_XING_FEI_DAN_3_PURPLE_ID); } }// 三星回旋飞镖塔紫宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_3_RED { get { return _QIU_XING_FEI_DAN_3_RED ??= mTable.query(QIU_XING_FEI_DAN_3_RED_ID); } }// 三星回旋飞镖塔红宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_3_GREEN { get { return _QIU_XING_FEI_DAN_3_GREEN ??= mTable.query(QIU_XING_FEI_DAN_3_GREEN_ID); } }// 三星回旋飞镖塔绿宝石
	public static EDTowerSkill QIU_XING_FEI_DAN_3_BLUE { get { return _QIU_XING_FEI_DAN_3_BLUE ??= mTable.query(QIU_XING_FEI_DAN_3_BLUE_ID); } }// 三星回旋飞镖塔蓝宝石
	public static EDTowerSkill XIAN_DAN_1 { get { return _XIAN_DAN_1 ??= mTable.query(XIAN_DAN_1_ID); } }// 一星霰弹塔
	public static EDTowerSkill XIAN_DAN_1_PURPLE { get { return _XIAN_DAN_1_PURPLE ??= mTable.query(XIAN_DAN_1_PURPLE_ID); } }// 一星霰弹塔紫宝石
	public static EDTowerSkill XIAN_DAN_1_RED { get { return _XIAN_DAN_1_RED ??= mTable.query(XIAN_DAN_1_RED_ID); } }// 一星霰弹塔红宝石
	public static EDTowerSkill XIAN_DAN_1_GREEN { get { return _XIAN_DAN_1_GREEN ??= mTable.query(XIAN_DAN_1_GREEN_ID); } }// 一星霰弹塔绿宝石
	public static EDTowerSkill XIAN_DAN_1_BLUE { get { return _XIAN_DAN_1_BLUE ??= mTable.query(XIAN_DAN_1_BLUE_ID); } }// 一星霰弹塔蓝宝石
	public static EDTowerSkill XIAN_DAN_2 { get { return _XIAN_DAN_2 ??= mTable.query(XIAN_DAN_2_ID); } }// 二星霰弹塔
	public static EDTowerSkill XIAN_DAN_2_PURPLE { get { return _XIAN_DAN_2_PURPLE ??= mTable.query(XIAN_DAN_2_PURPLE_ID); } }// 二星霰弹塔紫宝石
	public static EDTowerSkill XIAN_DAN_2_RED { get { return _XIAN_DAN_2_RED ??= mTable.query(XIAN_DAN_2_RED_ID); } }// 二星霰弹塔红宝石
	public static EDTowerSkill XIAN_DAN_2_GREEN { get { return _XIAN_DAN_2_GREEN ??= mTable.query(XIAN_DAN_2_GREEN_ID); } }// 二星霰弹塔绿宝石
	public static EDTowerSkill XIAN_DAN_2_BLUE { get { return _XIAN_DAN_2_BLUE ??= mTable.query(XIAN_DAN_2_BLUE_ID); } }// 二星霰弹塔蓝宝石
	public static EDTowerSkill XIAN_DAN_3 { get { return _XIAN_DAN_3 ??= mTable.query(XIAN_DAN_3_ID); } }// 三星霰弹塔
	public static EDTowerSkill XIAN_DAN_3_PURPLE { get { return _XIAN_DAN_3_PURPLE ??= mTable.query(XIAN_DAN_3_PURPLE_ID); } }// 三星霰弹塔紫宝石
	public static EDTowerSkill XIAN_DAN_3_RED { get { return _XIAN_DAN_3_RED ??= mTable.query(XIAN_DAN_3_RED_ID); } }// 三星霰弹塔红宝石
	public static EDTowerSkill XIAN_DAN_3_GREEN { get { return _XIAN_DAN_3_GREEN ??= mTable.query(XIAN_DAN_3_GREEN_ID); } }// 三星霰弹塔绿宝石
	public static EDTowerSkill XIAN_DAN_3_BLUE { get { return _XIAN_DAN_3_BLUE ??= mTable.query(XIAN_DAN_3_BLUE_ID); } }// 三星霰弹塔蓝宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_1 { get { return _TIAN_KONG_ZHI_MAO_1 ??= mTable.query(TIAN_KONG_ZHI_MAO_1_ID); } }// 一星天空之矛
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_1_PURPLE { get { return _TIAN_KONG_ZHI_MAO_1_PURPLE ??= mTable.query(TIAN_KONG_ZHI_MAO_1_PURPLE_ID); } }// 一星天空之矛紫宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_1_RED { get { return _TIAN_KONG_ZHI_MAO_1_RED ??= mTable.query(TIAN_KONG_ZHI_MAO_1_RED_ID); } }// 一星天空之矛红宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_1_GREEN { get { return _TIAN_KONG_ZHI_MAO_1_GREEN ??= mTable.query(TIAN_KONG_ZHI_MAO_1_GREEN_ID); } }// 一星天空之矛绿宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_1_BLUE { get { return _TIAN_KONG_ZHI_MAO_1_BLUE ??= mTable.query(TIAN_KONG_ZHI_MAO_1_BLUE_ID); } }// 一星天空之矛蓝宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_2 { get { return _TIAN_KONG_ZHI_MAO_2 ??= mTable.query(TIAN_KONG_ZHI_MAO_2_ID); } }// 二星天空之矛
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_2_PURPLE { get { return _TIAN_KONG_ZHI_MAO_2_PURPLE ??= mTable.query(TIAN_KONG_ZHI_MAO_2_PURPLE_ID); } }// 二星天空之矛紫宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_2_RED { get { return _TIAN_KONG_ZHI_MAO_2_RED ??= mTable.query(TIAN_KONG_ZHI_MAO_2_RED_ID); } }// 二星天空之矛红宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_2_GREEN { get { return _TIAN_KONG_ZHI_MAO_2_GREEN ??= mTable.query(TIAN_KONG_ZHI_MAO_2_GREEN_ID); } }// 二星天空之矛绿宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_2_BLUE { get { return _TIAN_KONG_ZHI_MAO_2_BLUE ??= mTable.query(TIAN_KONG_ZHI_MAO_2_BLUE_ID); } }// 二星天空之矛蓝宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_3 { get { return _TIAN_KONG_ZHI_MAO_3 ??= mTable.query(TIAN_KONG_ZHI_MAO_3_ID); } }// 三星天空之矛
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_3_PURPLE { get { return _TIAN_KONG_ZHI_MAO_3_PURPLE ??= mTable.query(TIAN_KONG_ZHI_MAO_3_PURPLE_ID); } }// 三星天空之矛紫宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_3_RED { get { return _TIAN_KONG_ZHI_MAO_3_RED ??= mTable.query(TIAN_KONG_ZHI_MAO_3_RED_ID); } }// 三星天空之矛红宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_3_GREEN { get { return _TIAN_KONG_ZHI_MAO_3_GREEN ??= mTable.query(TIAN_KONG_ZHI_MAO_3_GREEN_ID); } }// 三星天空之矛绿宝石
	public static EDTowerSkill TIAN_KONG_ZHI_MAO_3_BLUE { get { return _TIAN_KONG_ZHI_MAO_3_BLUE ??= mTable.query(TIAN_KONG_ZHI_MAO_3_BLUE_ID); } }// 三星天空之矛蓝宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_1 { get { return _FEI_BIAO_FA_SHE_1 ??= mTable.query(FEI_BIAO_FA_SHE_1_ID); } }// 一星飞镖发射器
	public static EDTowerSkill FEI_BIAO_FA_SHE_1_PURPLE { get { return _FEI_BIAO_FA_SHE_1_PURPLE ??= mTable.query(FEI_BIAO_FA_SHE_1_PURPLE_ID); } }// 一星飞镖发射器紫宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_1_RED { get { return _FEI_BIAO_FA_SHE_1_RED ??= mTable.query(FEI_BIAO_FA_SHE_1_RED_ID); } }// 一星飞镖发射器红宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_1_GREEN { get { return _FEI_BIAO_FA_SHE_1_GREEN ??= mTable.query(FEI_BIAO_FA_SHE_1_GREEN_ID); } }// 一星飞镖发射器绿宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_1_BLUE { get { return _FEI_BIAO_FA_SHE_1_BLUE ??= mTable.query(FEI_BIAO_FA_SHE_1_BLUE_ID); } }// 一星飞镖发射器蓝宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_2 { get { return _FEI_BIAO_FA_SHE_2 ??= mTable.query(FEI_BIAO_FA_SHE_2_ID); } }// 二星飞镖发射器
	public static EDTowerSkill FEI_BIAO_FA_SHE_2_PURPLE { get { return _FEI_BIAO_FA_SHE_2_PURPLE ??= mTable.query(FEI_BIAO_FA_SHE_2_PURPLE_ID); } }// 二星飞镖发射器紫宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_2_RED { get { return _FEI_BIAO_FA_SHE_2_RED ??= mTable.query(FEI_BIAO_FA_SHE_2_RED_ID); } }// 二星飞镖发射器红宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_2_GREEN { get { return _FEI_BIAO_FA_SHE_2_GREEN ??= mTable.query(FEI_BIAO_FA_SHE_2_GREEN_ID); } }// 二星飞镖发射器绿宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_2_BLUE { get { return _FEI_BIAO_FA_SHE_2_BLUE ??= mTable.query(FEI_BIAO_FA_SHE_2_BLUE_ID); } }// 二星飞镖发射器蓝宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_3 { get { return _FEI_BIAO_FA_SHE_3 ??= mTable.query(FEI_BIAO_FA_SHE_3_ID); } }// 三星飞镖发射器
	public static EDTowerSkill FEI_BIAO_FA_SHE_3_PURPLE { get { return _FEI_BIAO_FA_SHE_3_PURPLE ??= mTable.query(FEI_BIAO_FA_SHE_3_PURPLE_ID); } }// 三星飞镖发射器紫宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_3_RED { get { return _FEI_BIAO_FA_SHE_3_RED ??= mTable.query(FEI_BIAO_FA_SHE_3_RED_ID); } }// 三星飞镖发射器红宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_3_GREEN { get { return _FEI_BIAO_FA_SHE_3_GREEN ??= mTable.query(FEI_BIAO_FA_SHE_3_GREEN_ID); } }// 三星飞镖发射器绿宝石
	public static EDTowerSkill FEI_BIAO_FA_SHE_3_BLUE { get { return _FEI_BIAO_FA_SHE_3_BLUE ??= mTable.query(FEI_BIAO_FA_SHE_3_BLUE_ID); } }// 三星飞镖发射器蓝宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_1 { get { return _QI_QIU_ZHA_DAN_1 ??= mTable.query(QI_QIU_ZHA_DAN_1_ID); } }// 一星气球炸弹塔
	public static EDTowerSkill QI_QIU_ZHA_DAN_1_PURPLE { get { return _QI_QIU_ZHA_DAN_1_PURPLE ??= mTable.query(QI_QIU_ZHA_DAN_1_PURPLE_ID); } }// 一星气球炸弹塔紫宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_1_RED { get { return _QI_QIU_ZHA_DAN_1_RED ??= mTable.query(QI_QIU_ZHA_DAN_1_RED_ID); } }// 一星气球炸弹塔红宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_1_GREEN { get { return _QI_QIU_ZHA_DAN_1_GREEN ??= mTable.query(QI_QIU_ZHA_DAN_1_GREEN_ID); } }// 一星气球炸弹塔绿宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_1_BLUE { get { return _QI_QIU_ZHA_DAN_1_BLUE ??= mTable.query(QI_QIU_ZHA_DAN_1_BLUE_ID); } }// 一星气球炸弹塔蓝宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_2 { get { return _QI_QIU_ZHA_DAN_2 ??= mTable.query(QI_QIU_ZHA_DAN_2_ID); } }// 二星气球炸弹塔
	public static EDTowerSkill QI_QIU_ZHA_DAN_2_PURPLE { get { return _QI_QIU_ZHA_DAN_2_PURPLE ??= mTable.query(QI_QIU_ZHA_DAN_2_PURPLE_ID); } }// 二星气球炸弹塔紫宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_2_RED { get { return _QI_QIU_ZHA_DAN_2_RED ??= mTable.query(QI_QIU_ZHA_DAN_2_RED_ID); } }// 二星气球炸弹塔红宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_2_GREEN { get { return _QI_QIU_ZHA_DAN_2_GREEN ??= mTable.query(QI_QIU_ZHA_DAN_2_GREEN_ID); } }// 二星气球炸弹塔绿宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_2_BLUE { get { return _QI_QIU_ZHA_DAN_2_BLUE ??= mTable.query(QI_QIU_ZHA_DAN_2_BLUE_ID); } }// 二星气球炸弹塔蓝宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_3 { get { return _QI_QIU_ZHA_DAN_3 ??= mTable.query(QI_QIU_ZHA_DAN_3_ID); } }// 三星气球炸弹塔
	public static EDTowerSkill QI_QIU_ZHA_DAN_3_PURPLE { get { return _QI_QIU_ZHA_DAN_3_PURPLE ??= mTable.query(QI_QIU_ZHA_DAN_3_PURPLE_ID); } }// 三星气球炸弹塔紫宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_3_RED { get { return _QI_QIU_ZHA_DAN_3_RED ??= mTable.query(QI_QIU_ZHA_DAN_3_RED_ID); } }// 三星气球炸弹塔红宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_3_GREEN { get { return _QI_QIU_ZHA_DAN_3_GREEN ??= mTable.query(QI_QIU_ZHA_DAN_3_GREEN_ID); } }// 三星气球炸弹塔绿宝石
	public static EDTowerSkill QI_QIU_ZHA_DAN_3_BLUE { get { return _QI_QIU_ZHA_DAN_3_BLUE ??= mTable.query(QI_QIU_ZHA_DAN_3_BLUE_ID); } }// 三星气球炸弹塔蓝宝石
	public static EDTowerSkill ZHEN_DANG_1 { get { return _ZHEN_DANG_1 ??= mTable.query(ZHEN_DANG_1_ID); } }// 一星电磁震荡塔
	public static EDTowerSkill ZHEN_DANG_1_PURPLE { get { return _ZHEN_DANG_1_PURPLE ??= mTable.query(ZHEN_DANG_1_PURPLE_ID); } }// 一星电磁震荡塔紫宝石
	public static EDTowerSkill ZHEN_DANG_1_RED { get { return _ZHEN_DANG_1_RED ??= mTable.query(ZHEN_DANG_1_RED_ID); } }// 一星电磁震荡塔红宝石
	public static EDTowerSkill ZHEN_DANG_1_GREEN { get { return _ZHEN_DANG_1_GREEN ??= mTable.query(ZHEN_DANG_1_GREEN_ID); } }// 一星电磁震荡塔绿宝石
	public static EDTowerSkill ZHEN_DANG_1_BLUE { get { return _ZHEN_DANG_1_BLUE ??= mTable.query(ZHEN_DANG_1_BLUE_ID); } }// 一星电磁震荡塔蓝宝石
	public static EDTowerSkill ZHEN_DANG_2 { get { return _ZHEN_DANG_2 ??= mTable.query(ZHEN_DANG_2_ID); } }// 二星电磁震荡塔
	public static EDTowerSkill ZHEN_DANG_2_PURPLE { get { return _ZHEN_DANG_2_PURPLE ??= mTable.query(ZHEN_DANG_2_PURPLE_ID); } }// 二星电磁震荡塔紫宝石
	public static EDTowerSkill ZHEN_DANG_2_RED { get { return _ZHEN_DANG_2_RED ??= mTable.query(ZHEN_DANG_2_RED_ID); } }// 二星电磁震荡塔红宝石
	public static EDTowerSkill ZHEN_DANG_2_GREEN { get { return _ZHEN_DANG_2_GREEN ??= mTable.query(ZHEN_DANG_2_GREEN_ID); } }// 二星电磁震荡塔绿宝石
	public static EDTowerSkill ZHEN_DANG_2_BLUE { get { return _ZHEN_DANG_2_BLUE ??= mTable.query(ZHEN_DANG_2_BLUE_ID); } }// 二星电磁震荡塔蓝宝石
	public static EDTowerSkill ZHEN_DANG_3 { get { return _ZHEN_DANG_3 ??= mTable.query(ZHEN_DANG_3_ID); } }// 三星电磁震荡塔
	public static EDTowerSkill ZHEN_DANG_3_PURPLE { get { return _ZHEN_DANG_3_PURPLE ??= mTable.query(ZHEN_DANG_3_PURPLE_ID); } }// 三星电磁震荡塔紫宝石
	public static EDTowerSkill ZHEN_DANG_3_RED { get { return _ZHEN_DANG_3_RED ??= mTable.query(ZHEN_DANG_3_RED_ID); } }// 三星电磁震荡塔红宝石
	public static EDTowerSkill ZHEN_DANG_3_GREEN { get { return _ZHEN_DANG_3_GREEN ??= mTable.query(ZHEN_DANG_3_GREEN_ID); } }// 三星电磁震荡塔绿宝石
	public static EDTowerSkill ZHEN_DANG_3_BLUE { get { return _ZHEN_DANG_3_BLUE ??= mTable.query(ZHEN_DANG_3_BLUE_ID); } }// 三星电磁震荡塔蓝宝石
	public static EDTowerSkill BO_DONG_1 { get { return _BO_DONG_1 ??= mTable.query(BO_DONG_1_ID); } }// 一星风刃发射器
	public static EDTowerSkill BO_DONG_1_PURPLE { get { return _BO_DONG_1_PURPLE ??= mTable.query(BO_DONG_1_PURPLE_ID); } }// 一星风刃发射器紫宝石
	public static EDTowerSkill BO_DONG_1_RED { get { return _BO_DONG_1_RED ??= mTable.query(BO_DONG_1_RED_ID); } }// 一星风刃发射器红宝石
	public static EDTowerSkill BO_DONG_1_GREEN { get { return _BO_DONG_1_GREEN ??= mTable.query(BO_DONG_1_GREEN_ID); } }// 一星风刃发射器绿宝石
	public static EDTowerSkill BO_DONG_1_BLUE { get { return _BO_DONG_1_BLUE ??= mTable.query(BO_DONG_1_BLUE_ID); } }// 一星风刃发射器蓝宝石
	public static EDTowerSkill BO_DONG_2 { get { return _BO_DONG_2 ??= mTable.query(BO_DONG_2_ID); } }// 二星风刃发射器
	public static EDTowerSkill BO_DONG_2_PURPLE { get { return _BO_DONG_2_PURPLE ??= mTable.query(BO_DONG_2_PURPLE_ID); } }// 二星风刃发射器紫宝石
	public static EDTowerSkill BO_DONG_2_RED { get { return _BO_DONG_2_RED ??= mTable.query(BO_DONG_2_RED_ID); } }// 二星风刃发射器红宝石
	public static EDTowerSkill BO_DONG_2_GREEN { get { return _BO_DONG_2_GREEN ??= mTable.query(BO_DONG_2_GREEN_ID); } }// 二星风刃发射器绿宝石
	public static EDTowerSkill BO_DONG_2_BLUE { get { return _BO_DONG_2_BLUE ??= mTable.query(BO_DONG_2_BLUE_ID); } }// 二星风刃发射器蓝宝石
	public static EDTowerSkill BO_DONG_3 { get { return _BO_DONG_3 ??= mTable.query(BO_DONG_3_ID); } }// 三星风刃发射器
	public static EDTowerSkill BO_DONG_3_PURPLE { get { return _BO_DONG_3_PURPLE ??= mTable.query(BO_DONG_3_PURPLE_ID); } }// 三星风刃发射器紫宝石
	public static EDTowerSkill BO_DONG_3_RED { get { return _BO_DONG_3_RED ??= mTable.query(BO_DONG_3_RED_ID); } }// 三星风刃发射器红宝石
	public static EDTowerSkill BO_DONG_3_GREEN { get { return _BO_DONG_3_GREEN ??= mTable.query(BO_DONG_3_GREEN_ID); } }// 三星风刃发射器绿宝石
	public static EDTowerSkill BO_DONG_3_BLUE { get { return _BO_DONG_3_BLUE ??= mTable.query(BO_DONG_3_BLUE_ID); } }// 三星风刃发射器蓝宝石

	public string mName;							// 技能名字
	public List<int> mBullet = new();				// 包含的子弹列表
	public List<float> mFireTime = new();			// 子弹发射的时间点列表,秒
	public int mFireEffect;							// 释放时的特效
	public string mFireAnimation;					// 释放时的动作
	public int mFireSound;							// 释放时播放的音效
	public float mCD;								// 技能CD
	public bool mClearTarget;						// 每次释放技能时是否会先清除目标,如果不清除,则只有上一次的目标不满足攻击条件时才会重新选择,当同时选择多个目标时仍然会每次重新选择
	public SEARCH_TARGET mSearchTarget;				// 寻敌方式
	public TARGET_BEHAVIOUR_TYPE mEnemyType;		// 敌人类型
	public string mParam0;							// 参数0
	public string mParam1;							// 参数1
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.readList(mBullet);
		result = result && reader.readList(mFireTime);
		result = result && reader.read(out mFireEffect);
		result = result && reader.readString(out mFireAnimation);
		result = result && reader.read(out mFireSound);
		result = result && reader.read(out mCD);
		result = result && reader.read(out mClearTarget);
		result = result && reader.readEnumByte(out mSearchTarget);
		result = result && reader.readEnumByte(out mEnemyType);
		result = result && reader.readString(out mParam0);
		result = result && reader.readString(out mParam1);
		return result;
	}
}
// auto generate end