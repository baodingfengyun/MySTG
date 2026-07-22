// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Effect表格
public class EDEffect : ExcelDataT<EDEffect>
{
	public const int FOCUS_BUFF_ID = 2099;			// 集火Buff特效
	public const int HERO_PLACE_ID = 5006;			// 放置英雄时播放的特效
	public const int ACTIVITY_CHANGE_ID = 5007;		// 活动界面切页特效
	public const int TOWER_PLACE_ID = 5008;			// 放置塔时播放的特效
	public const int TOWER_SELECT_ID = 5009;		// 塔选中的特效
	public const int MONSTER_DEAD_ID = 5010;		// 怪物死亡时的特效
	public const int TOWER_RANGE_ID = 5011;			// 显示防御塔攻击范围的特效
	public const int TOWER_MIN_RANGE_ID = 5012;		// 显示防御塔攻击范围的特效
	public const int SKILL_RANGE_ID = 5013;			// 显示技能攻击范围的特效
	public const int END_POINT_ID = 5014;			// 终点特效
	public const int BATTLE_PATH_ID = 5015;			// 战斗路线显示
	public const int BATTLE_PATH_PREVIEW_ID = 5016;	// 战斗路线预览显示
	public const int BATTLE_MOVE_PATH_ID = 5017;	// 战斗路线特效显示
	public const int MAP_PORTAL_ID = 5018;			// 传送门特效

	private static EDEffect _FOCUS_BUFF;			// 集火Buff特效
	private static EDEffect _HERO_PLACE;			// 放置英雄时播放的特效
	private static EDEffect _ACTIVITY_CHANGE;		// 活动界面切页特效
	private static EDEffect _TOWER_PLACE;			// 放置塔时播放的特效
	private static EDEffect _TOWER_SELECT;			// 塔选中的特效
	private static EDEffect _MONSTER_DEAD;			// 怪物死亡时的特效
	private static EDEffect _TOWER_RANGE;			// 显示防御塔攻击范围的特效
	private static EDEffect _TOWER_MIN_RANGE;		// 显示防御塔攻击范围的特效
	private static EDEffect _SKILL_RANGE;			// 显示技能攻击范围的特效
	private static EDEffect _END_POINT;				// 终点特效
	private static EDEffect _BATTLE_PATH;			// 战斗路线显示
	private static EDEffect _BATTLE_PATH_PREVIEW;	// 战斗路线预览显示
	private static EDEffect _BATTLE_MOVE_PATH;		// 战斗路线特效显示
	private static EDEffect _MAP_PORTAL;			// 传送门特效

	public static EDEffect FOCUS_BUFF { get { return _FOCUS_BUFF ??= mTable.query(FOCUS_BUFF_ID); } }// 集火Buff特效
	public static EDEffect HERO_PLACE { get { return _HERO_PLACE ??= mTable.query(HERO_PLACE_ID); } }// 放置英雄时播放的特效
	public static EDEffect ACTIVITY_CHANGE { get { return _ACTIVITY_CHANGE ??= mTable.query(ACTIVITY_CHANGE_ID); } }// 活动界面切页特效
	public static EDEffect TOWER_PLACE { get { return _TOWER_PLACE ??= mTable.query(TOWER_PLACE_ID); } }// 放置塔时播放的特效
	public static EDEffect TOWER_SELECT { get { return _TOWER_SELECT ??= mTable.query(TOWER_SELECT_ID); } }// 塔选中的特效
	public static EDEffect MONSTER_DEAD { get { return _MONSTER_DEAD ??= mTable.query(MONSTER_DEAD_ID); } }// 怪物死亡时的特效
	public static EDEffect TOWER_RANGE { get { return _TOWER_RANGE ??= mTable.query(TOWER_RANGE_ID); } }// 显示防御塔攻击范围的特效
	public static EDEffect TOWER_MIN_RANGE { get { return _TOWER_MIN_RANGE ??= mTable.query(TOWER_MIN_RANGE_ID); } }// 显示防御塔攻击范围的特效
	public static EDEffect SKILL_RANGE { get { return _SKILL_RANGE ??= mTable.query(SKILL_RANGE_ID); } }// 显示技能攻击范围的特效
	public static EDEffect END_POINT { get { return _END_POINT ??= mTable.query(END_POINT_ID); } }// 终点特效
	public static EDEffect BATTLE_PATH { get { return _BATTLE_PATH ??= mTable.query(BATTLE_PATH_ID); } }// 战斗路线显示
	public static EDEffect BATTLE_PATH_PREVIEW { get { return _BATTLE_PATH_PREVIEW ??= mTable.query(BATTLE_PATH_PREVIEW_ID); } }// 战斗路线预览显示
	public static EDEffect BATTLE_MOVE_PATH { get { return _BATTLE_MOVE_PATH ??= mTable.query(BATTLE_MOVE_PATH_ID); } }// 战斗路线特效显示
	public static EDEffect MAP_PORTAL { get { return _MAP_PORTAL ??= mTable.query(MAP_PORTAL_ID); } }// 传送门特效

	public string mPath;							// 特效在GameResources下的相对路径,带后缀
	public bool mSupportMoveToHide;					// 是否支持将特效移动到远处来实现隐藏
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mPath);
		result = result && reader.read(out mSupportMoveToHide);
		return result;
	}
}
// auto generate end