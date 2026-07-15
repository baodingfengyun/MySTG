
// 游戏枚举定义-----------------------------------------------------------------------------------------------

// 音效定义,对应表格中的ID
public class SOUND_HOTFIX
{
	public static int NONE = 0;
	public static int BUTTON = 1000;
	public static int CLOSE_BUTTON = 1001;
	public static int MAIN_MISSION_BUTTON = 1000;
	public static int MAIN_PACK_BUTTON = 1000;
	public static int MAIN_SHOP_BUTTON = 1000;
	public static int MAIN_FIGHT_BUTTON = 1000;
	public static int START_FIGHT_BUTTON = 1006;
	public static int LOSE_HP = 1007;
	public static int NEXT_WAVE_BOSS = 1008;
	//public static int MAIN_WATER_BGM = 1009;
	public static int MAIN_BIRDSONG_BGM = 1010;
	public static int LOGIN_BGM = 2000;
	public static int MAIN_BGM = 2001;
	public static int LEVEL_VICTORY_BGM = 2004;
	public static int LEVEL_DEFEAT_BGM = 2005;
	public static int BUILD_TOWER = 3000;
	public static int REMOVE_TOWER = 3001;
	public static int PLACE_HERO = 4000;
}

// 战斗状态
public enum BATTLE_STATE : byte
{
	NONE,           // 无效值
	SETUP_TOWER,    // 正在布置塔
	FIGHTING,       // 战斗中
	FINISH,         // 战斗已经结束
	WAIT_FINISH,    // 等待结束消息返回
}

// 战斗模式
public enum BATTLE_MODE : byte
{
	NONE = 0,           // 无效值
	ROGUE_LIKE = 5,     // RogueLike模式
	MAX,                // 最大值
}

// 格子的状态
public enum GRID_STATE : byte
{
	NONE = 0,                       // 无效值
	BLOCK = 1,                      // 阻挡
	WALKABLE = 2,                   // 允许行走,同时也可以放置塔
	EMPTY = 5,                      // 空格子,不允许行走,不显示在场景中,用于给地图配置占位
	WALK_FLY_TRAP_UNTOWER = 6,      // 允许行走,允许飞行,允许放陷阱,不允许放塔
	UNWALK_FLY_UNTRAP_TOWER = 7,    // 不允许行走,允许飞行,不允许放陷阱,允许放塔
	WALK_FLY_UNTRAP_UNTOWER = 8,    // 允许行走,允许飞行,不允许放陷阱,不允许放塔(一般起点和终点使用)
	FLY_ONLY_HIDE = 9,              // 只允许飞行,不能做其他操作,也不显示
}

// 血量变化类型
public enum HP_DELTA : byte
{
	NONE,           // 无效值
	NORMAL_DAMAGE,  // 普通子弹伤害
	DEBUFF,         // 普通debuff伤害
	MISS,           // 被闪避
}

// 伤害元素属性
public enum DAMAGE_ELEMENT : byte
{
	NONE,           // 无效值
	DARK,           // 暗属性
	FIRE,           // 火属性
	ICE,            // 冰属性
	LIGHTNING,      // 电属性
	POISION,        // 毒属性
	LIGHT,          // 光属性
}

// 怪物动作枚举,也是动画状态机中的参数值
public enum MONSTER_ANIMATION : byte
{
	NONE,               // 无效值
	WALK,               // 走路
	STAND,              // 站立
	SKILL,              // 释放技能
	DEAD,               // 死亡
	VERTIGO,            // 眩晕
}

// 道具攻击的目标类型
public enum TARGET_BEHAVIOUR_TYPE : byte
{
	NONE,               // 无效值
	WALK_MONSTER,       // 地面行走怪物
	FLY_MONSTER,        // 空中飞行怪物
	ALL_MONSTER,        // 所有怪物
}

// 关卡的状态
public enum LEVEL_STATE
{
	NONE,           // 无效值
	LOCK,           // 未解锁
	UNLOCKED,       // 当前可玩的
	COMPLETED,      // 已通过的
	PLAYING,        // 正在玩的
}

// 子弹类型
public enum BULLET_TYPE : byte
{
	NONE,                           // 无效值
	TRACK,                          // 追踪子弹
	PARABOLA_TRACK,                 // 抛物线,带追踪
	LINK_LINE,                      // 连线,可以瞬间串联多个目标
	NO_MOVE,                        // 原地范围伤害子弹
	STRAIGHT_LINE,                  // 直线飞行的子弹
	STRAIGHT_LINE_ALWAYS_COLLIDE,   // 直线飞行的子弹,飞行过程中会一直检测是否碰到物体
	CURVE_MULTI_DAMAGE,             // 按折线移动,每隔一定时间产生一次
	NO_MOVE_FAN,                    // 扇形子弹
	CURVE,                          // 按折线移动
	PARABOLA,                       // 抛物线,不带追踪
	BALLOON,                        // 气球子弹
	ROTATE_AROUND,                  // 绕某个点旋转的子弹
	ZHEN_DANG,                      // 震荡塔子弹
	GOU_ZHUA,                       // 钩爪
	TRACK_BOUNCE,                   // 追踪并且弹射周围目标
	BOOMERANG,                      // 回旋镖
}

// 战斗中物品的类型
public enum BATTLE_ITEM_TYPE : byte
{
	NONE,               // 无效值
	TOWER,              // 防御塔
	BATTLE_PROP,        // 战斗道具
	TOWER_TALENT,       // 防御塔天赋
	MAX,                // 最大值
}

// 寻路格子的类型
public enum GRID_TYPE : byte
{
	NONE,               // 无效值
	FOUR,               // 4个方向可移动
	SIX,                // 6个方向可移动
}

// 塔的类型
public enum TOWER_TYPE : byte
{
	NONE,                       // 无效值
	SHI_ZI_GONG = 1,            // 十字弓
	FANG_KONG_FEI_DAN_TA = 2,   // 防空飞弹塔
	TOU_SHI_JI = 3,             // 投石机
	XIANG_QIAN_TA = 4,          // 镶嵌塔
	SHI_DUN = 8,                // 石墩
	HUO_PAO_TA = 9,             // 火炮塔
	QIU_XING_FEI_DAN_TA = 10,   // 球形飞弹塔
	XIAN_DAN_TA = 11,           // 霰弹塔
	TIAN_KONG_ZHI_MAO = 12,     // 天空之矛
	FEI_BIAO_FA_SHE_QI = 13,    // 飞镖发射塔
	QI_QIU_ZHA_DAN_TA = 14,     // 气球炸弹塔
	ZHEN_DANG_TA = 15,          // 震荡塔
	BO_DONG_TA = 16,            // 波动塔
}

// 技能释放操作类型
public enum SKILL_FIRE_TYPE : byte
{
	NONE,                       // 无效值
	CLICK,                      // 点击直接原地释放
	DRAG_GRID,                  // 拖拽到任意格子点释放
	DRAG_POSITION,              // 拖拽到任意点释放
	CLICK_OR_DRAG_POSITION,     // 点击或者拖拽到任意位置都可以
}

// 寻敌方式
public enum SEARCH_TARGET : byte
{
	NONE,                       // 无效值
	NEAREST,                    // 距离自己最近的敌人
	SELF,                       // 角色自身
	RANGE_NEAREST,              // 最小和最大范围之间距离自己最近的敌人
	RANDOM,                     // 范围内随机敌人
}

// 子弹发射的起始位置类型
public enum BULLET_FIRE_POINT : byte
{
	NONE,               // 无效值
	SELF_FOOT,          // 从攻击者脚底处发出
	SELF_BODY,          // 从攻击者身体处发出
	SELF_HEAD,          // 从攻击者头部处发出
	SELF_POINT,         // 从攻击者指定节点发出
	TARGET_FOOT,        // 在目标脚底处发出
	TARGET_BODY,        // 在目标身体处发出
	TARGET_HEAD,        // 在目标头部处发出
}

// 子弹伤害修改器
public enum BULLET_DAMAGE_MODIFIER : byte
{
	NONE,                           // 无效值
	ANY_DEBUFF_INCREASE_DAMAGE,     // 对拥有任意异常状态的敌人增加伤害
	WALK_MONSTER_DECREASE_DAMAGE,   // 对地面敌人伤害减少
	FLY_MONSTER_DECREASE_DAMAGE,    // 对空中敌人伤害减少
}

// 怪物释放技能时的寻找目标的方式
public enum MONSTER_SEARCH_TARGET : byte
{
	NONE,                           // 无效值
	SELF,                           // 自己
	RANGE_MIN_HP_PERCENT_MONSTER,   // 范围内血量百分比最低的友方单位
	HIGHEST_ATTACK_TOWER,           // 场上攻击力最高的塔
}

// 任务分类
public enum QUEST_CATEGORY : byte
{
	NONE,                           // 无效值
	MAIN,                           // 主线
	DAILY,                          // 日常
	CHALLENGE,                      // 挑战
}

// 英雄技能的释放特效的位置计算方式
public enum FIRE_EFFECT_POSITION : byte
{
	NONE,                           // 不做任何操作
	ATTACH_POINT,                   // 挂到指定节点上
	USE_POINT_POSITION,             // 使用指定节点的位置
	USE_POINT_POSITION_TO_GROUND,   // 使用指定节点的位置垂直对应的地面位置
}

// 击中特效的播放位置计算方式
public enum HIT_EFFECT_POSITION : byte
{
	NONE,                           // 无效值
	BULLET_POSITION,                // 在子弹位置播放
	TARGET_POSITION,                // 在被命中目标位置播放
}

// 每次刷怪，出怪口刷怪规则
public enum SPAWN_POINT_RULE : byte
{
	NONE,                           // 无效值
	RANDOM,                         // 从权重中随机一个出口
	SYNC,                           // 所有出口同时出怪
	TIMES,                          // 每个口刷n个怪再切换下一个口
}

// 传送门传送规则
public enum PORTAL_RULE : byte
{
	NONE,                           // 无效值
	RANDOM,                         // 随机一个出口
	SEQUENCE,                       // 依次出口
}

// 怪物强度
public enum MONSTER_STRENGTH : byte
{
	NONE,
	COMMON,                         // 普通
	ELITE,                          // 精英
	BOSS,                           // BOSS
}

// 怪物偏移路线类型
public enum MONSTER_GRID_OFFSET : byte
{
	NONE,                   // 不偏移
	LEFT,                   // 行进方向左侧
	RIGHT,                  // 行进方向右侧
	MAX,                    // 最大值,用于随机
}

// 关卡的类型
public enum LEVEL_TYPE : byte
{
	NONE,           // 无效值
	ENDLESS,        // 无尽模式
	MAIN_LEVEL,     // 肉鸽模式
}

// 战斗内背包的查看类型
public enum CLIENT_PACK_VIEW : byte
{
	NONE,       // 无效值
	TOWER,      // 防御塔
}