using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityUtility;
using static FrameDefine;
using static FrameBaseDefine;

// 因为使用很频繁所以简写为GDR,全称为GameDefineILR
public class GDR
{
	// 路径
	public const string BATTLE_COMMON = "BattleCommon";
	public const string MAP = "Map";
	public const string R_BATTLE_COMMON_PATH = R_SCENE_PATH + BATTLE_COMMON + "/";
	// 层
	public const string LAYER_LEVEL_GRID = "LevelGrid";
	public const string LAYER_TERRAIN = "Terrain";
	public const string LAYER_TOWER = "Tower";
	public const string LAYER_MONSTER = "Monster";
	public const string LAYER_BLOCK = "Block";
	public static int MASK_LEVEL_GRID = nameToLayerPhysics(LAYER_LEVEL_GRID);
	public static int MASK_TERRAIN = nameToLayerPhysics(LAYER_TERRAIN);
	public static int MASK_TOWER = nameToLayerPhysics(LAYER_TOWER);
	public static int MASK_MONSTER = nameToLayerPhysics(LAYER_MONSTER);
	public static int MASK_BLOCK = nameToLayerPhysics(LAYER_BLOCK);

	// 字符串常量
	public const string R_BATTLE_COMMON_PREFAB_PATH = R_BATTLE_COMMON_PATH + "Prefab/";							// 路径
	public const string R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH = R_BATTLE_COMMON_PATH + "Material_Texture/";	// 路径
	public const string RECT_WALKABLE_MAT = R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH + "LevelGridWalkable.mat";	// 正方形格子可行走的格子材质
	public const string RECT_RED_MAT = R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH + "DecalRed.mat";					// 正方形格子红色的格子材质
	public const string RECT_GREEN_MAT = R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH + "DecalGreen.mat";				// 正方形格子绿色的格子材质
	public const string RECT_BLOCK_MAT = R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH + "DecalWhite.mat";				// 正方形格子不可行走也不可摆放的格子材质
	public const string HEX_WALKABLE_MAT = R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH + "GridHex_Grey.mat";			// 六边形格子可行走的格子材质
	public const string HEX_RED_MAT = R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH + "GridHex_Red.mat";				// 六边形格子红色的格子材质
	public const string HEX_GREEN_MAT = R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH + "GridHex_Green.mat";			// 六边形格子绿色的格子材质
	public const string HEX_BLOCK_MAT = R_BATTLE_COMMON_RESOURCE_MATERIAL_PATH + "GridHex_White.mat";			// 六边形格子不可行走也不可摆放的格子材质
	public const string GRID_PREFAB_4D = R_BATTLE_COMMON_PREFAB_PATH + "GridTemplate4D.prefab";					// 正方形格子的模型
	public const string GRID_PREFAB_6D = R_BATTLE_COMMON_PREFAB_PATH + "GridTemplate6D.prefab";					// 六边形格子的模型
	public const string DRAG_TIP_MAT = R_BATTLE_COMMON_PATH + "Guide/M_Grid_Guide.mat";							// 六边形格子提示拖拽的材质
	public const string GRID_DRAG_TIP_ARROW = R_BATTLE_COMMON_PATH + "Guide/P_Focus_2.prefab";					// 提示拖拽格子的箭头模型
	public const string INSTALL_POINT = "Install_point";                                        // 可安装的道具在塔的gameobject上的点位
	public const string SCENE_MAIN = "MainScene";                                               // 大厅的场景
	public const string CHARACTER_FOOT_POINT = "FootPoint";                                     // 脚底节点的名字
	public const string CHARACTER_BODY_POINT = "BodyPoint";                                     // 身体节点的名字
	public const string CHARACTER_HEAD_POINT = "HeadPoint";                                     // 头部节点的名字
	public const string PREF_RENDER_SCALE = "RenderScale";                                      // 渲染质量的PlayerPrefs的名字
	public const string SERVER_CLIENT_ID = "000000000000-o60cr21b61e1vp1qcp2o5bl2gajsm38p.apps.googleusercontent.com";  // 拉起谷歌登录所需要的ID,webClientID,使用AndroidClientID会报错activity is cancelled by user
	// 其他常量
	public const int BUFF_PARAM_COUNT = 8;                                                      // 表格中配置的buff参数的最大数量
	public const int ODDS_SCALE = 10000;                                                        // 几率的缩放值,表格中填写的都是万分比
	public const int OWNED_PROP_COUNT = 8;                                                      // 手牌里最多可以拥有8个塔或者宝石
	public const int RANDOM_TOWER_COST_COIN = 2;                                                // 刷新随机防御塔列表时需要消耗2个金币
	public const int ITEM_STAR_COUNT = 5;                                                       // 道具最大星级5星
	public const int TOWER_STAR_COUNT = 3;                                                      // 塔的最高星级
	public const int LEVEL_STAR_COUNT = 3;                                                      // 关卡最高星级
	public const int MONSTER_ATTRIBUTE_STAR_COUNT = 5;                                          // 怪物属性最高星级
	public const int PRESTIGE_NOTE_COUNT = 3;                                                   // 声望便签数量
	public const int ROGUE_RANDOM_PROP_COUNT = 3;                                               // 固定从卡池中选择3个卡牌,Rogue模式
	public const int ROGUE_RANDOM_ADD_PROBABILITY = 500;										// 肉鸽抽卡场上有此防御塔时相关词条增加的权重
	public const int LEVEL_INIT_HP = 5;                                                         // 关卡默认血量
	public const int MAX_RESEARCH_LEVEL = 20;                                                   // 防御塔养成最高等级
	public const int DEFENCE_FILL_TOWER_EXP = 500;                                              // 防线默认经验灌注
	public const int SERVER_SHOP_REFRESH_HOUR = 5;                                              // 服务器当天的刷新时间
	public const float GRID_SIZE = 3.0f;                                                        // 格子大小,也是六边形的对边的距离
	public const float HEX_CORNER_DISTANCE = GRID_SIZE * 1.1547f;                               // 六边形对角距离,GRID_SIZE / (2 / sqrt(3))
	public const float HEX_EDGE_LENGTH = HEX_CORNER_DISTANCE * 0.5f;                            // 六边形的边长
	public const float HEX_MONSTER_MOVE_OFFSET = 0.5f;                                          // 六边形行进方向左右的偏移值，用于表现怪物不走同一条线
	public const float HEX_INTERSECT_LENGTH = (HEX_CORNER_DISTANCE - HEX_EDGE_LENGTH) * 0.5f;   // 上下两派六边形相交的部分的长度
	public const float BUILDING_CD = 0.7f;                                                      // 建造塔的CD
	public const float FLY_MONSTER_HEIGHT = 2.0f;                                               // 飞行怪物的位置高度
	public const float ATTACK_SPEED_MIN = -0.9f;                                                // 攻速最低
	public const float ROGUE_MODE_SELL_TOWER_PERCENT = 1.0f;                                    // 肉鸽模式 卖出塔时返还成本的倍率（临时）
	public const float HEX_CONFIG_INDEX_START_ANGLE = -30.0f;                                   // 配置中六边形1号位角度
	public const float HEX_EACH_ANGLE = 60.0f;                                                  // 六边形每个角的角度
	public const float BATTLE_PATH_EFFECT_TIME = 4.0f;                                          // 路径特效总时间
	public const float BATTLE_PATH_EFFECT_INTERVAL_TIME = 3.0f;                                 // 路径特效间隔时间
	public const string MONEY_FORMAT_DEFAULT = "{0} {1}";                                       // 默认钱显示格式
	public static int THIRTY_DAYS = (int)TimeSpan.FromDays(30).TotalSeconds;                    // 30天的秒数
	public static Vector3 CHARACTER_WORDS_OFFSET = Vector3.up * 100;                            // 英雄说话显示位置的偏移
	public static Vector3 TOWER_SELECT_OFFSET = new(0, 0.5f, 0);                                // 塔选择时抬起高度
	public static Vector3 TOWER_SELECT_SCALE = new(1.3f, 1.3f, 1.3f);                           // 塔选择时缩放
	// 列表类
	// 六边形从左上角顺时针开始标号
	// 六边形格子六边延伸的点位 0号位, (y%2==0)偶数加0号位，奇数加1号位
	public static Vector2Int[] HEX_AROUND_GRID0 = new Vector2Int[6]
	{
		new(-1, -1),
		new(0, -1),
		new(1, 0),
		new(0, 1),
		new(-1, 1),
		new(-1, 0)
	};
	// 六边形格子六边延伸的点位 1号位, (y%2==1)偶数加0号位，奇数加1号位
	public static Vector2Int[] HEX_AROUND_GRID1 = new Vector2Int[6]
	{
		new(0, -1),
		new(1, -1),
		new(1, 0),
		new(1, 1),
		new(0, 1),
		new(-1, 0)
	};
	public static string[] QUALITY_SUFFIX = { "White", "Blue", "Green", "Purple", "Yellow" };
	// 镶嵌塔各星级的宝石对应的prefab
	public static readonly Dictionary<int, string> XIANG_QIAN_TA_GEM_PREFABS = new()
	{
		{ 1, "Tower/XiangQianTa/P_Crystal_L1.prefab" },
		{ 2, "Tower/XiangQianTa/P_Crystal_L2.prefab" },
		{ 3, "Tower/XiangQianTa/P_Crystal_L3.prefab" },
		{ 4, "Tower/XiangQianTa/P_Crystal_L4.prefab" },
		{ 5, "Tower/XiangQianTa/P_Crystal_L5.prefab" },
	};
	public static readonly Dictionary<BATTLE_MODE, string> BATTLE_MODE_NAMES = new()
	{
		{BATTLE_MODE.ROGUE_LIKE, "普通"},
	};
	public static readonly Dictionary<string, string> LOCALIZATION_NAMES = new()
	{
		{LANGUAGE_CHINESE, "中文"},
		{LANGUAGE_CHINESE_TRADITIONAL, "繁體中文"},
		{LANGUAGE_ENGLISH, "English"},
	};
	public static Dictionary<TOWER_TYPE, int> ADD_TOWER_TALENT = new()
	{
		{ TOWER_TYPE.SHI_ZI_GONG, 9001 },
		{ TOWER_TYPE.FEI_BIAO_FA_SHE_QI, 9002 },
		{ TOWER_TYPE.FANG_KONG_FEI_DAN_TA, 9003 },
		{ TOWER_TYPE.HUO_PAO_TA, 9004 },
		{ TOWER_TYPE.XIAN_DAN_TA, 9005 },
		{ TOWER_TYPE.QIU_XING_FEI_DAN_TA, 9006 },
		{ TOWER_TYPE.TIAN_KONG_ZHI_MAO, 9007 },
		{ TOWER_TYPE.BO_DONG_TA, 9008 },
		{ TOWER_TYPE.ZHEN_DANG_TA, 9009 },
		{ TOWER_TYPE.QI_QIU_ZHA_DAN_TA, 9010 },
	};
}