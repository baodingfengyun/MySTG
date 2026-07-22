// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// BuffDetail表格
public class EDBuffDetail : ExcelDataT<EDBuffDetail>
{
	public const int TOWER_BUILDING_BUFF_ID = 279;	// 建造塔的buff
	public const int CONVEYOR_SPEED_UP_ID = 5001;	// 传送带加速Buff
	public const int CONVEYOR_SPEED_DOWN_ID = 5002;	// 传送带减速Buff
	public const int CONVEYOR_PUSHMOVE_ID = 5003;	// 怪物眩晕时被传送带推动Buff
	public const int TUNNEL_ID = 5006;				// 进入隧道附加的buff
	public const int FOCUS_ATTACK_MONSTER_ID = 7205;// 集火某个怪物的buff

	private static EDBuffDetail _TOWER_BUILDING_BUFF;// 建造塔的buff
	private static EDBuffDetail _CONVEYOR_SPEED_UP;	// 传送带加速Buff
	private static EDBuffDetail _CONVEYOR_SPEED_DOWN;// 传送带减速Buff
	private static EDBuffDetail _CONVEYOR_PUSHMOVE;	// 怪物眩晕时被传送带推动Buff
	private static EDBuffDetail _TUNNEL;			// 进入隧道附加的buff
	private static EDBuffDetail _FOCUS_ATTACK_MONSTER;// 集火某个怪物的buff

	public static EDBuffDetail TOWER_BUILDING_BUFF { get { return _TOWER_BUILDING_BUFF ??= mTable.query(TOWER_BUILDING_BUFF_ID); } }// 建造塔的buff
	public static EDBuffDetail CONVEYOR_SPEED_UP { get { return _CONVEYOR_SPEED_UP ??= mTable.query(CONVEYOR_SPEED_UP_ID); } }// 传送带加速Buff
	public static EDBuffDetail CONVEYOR_SPEED_DOWN { get { return _CONVEYOR_SPEED_DOWN ??= mTable.query(CONVEYOR_SPEED_DOWN_ID); } }// 传送带减速Buff
	public static EDBuffDetail CONVEYOR_PUSHMOVE { get { return _CONVEYOR_PUSHMOVE ??= mTable.query(CONVEYOR_PUSHMOVE_ID); } }// 怪物眩晕时被传送带推动Buff
	public static EDBuffDetail TUNNEL { get { return _TUNNEL ??= mTable.query(TUNNEL_ID); } }// 进入隧道附加的buff
	public static EDBuffDetail FOCUS_ATTACK_MONSTER { get { return _FOCUS_ATTACK_MONSTER ??= mTable.query(FOCUS_ATTACK_MONSTER_ID); } }// 集火某个怪物的buff

	public int mBuffTypeID;							// buff类型ID,索引到Buff表
	public string mName;							// buff名字
	public string mDescription;						// buff描述
	public float mBuffTime;							// buff持续时间,-1表示无限,0表示只生效一帧,大于0表示持续时间,单位秒
	public string mParam0;							// 参数0
	public string mParamDesc0;						// 参数描述0
	public string mParam1;							// 参数1
	public string mParamDesc1;						// 参数描述1
	public string mParam2;							// 参数2
	public string mParamDesc2;						// 参数描述2
	public string mParam3;							// 参数3
	public string mParamDesc3;						// 参数描述3
	public string mParam4;							// 参数4
	public string mParamDesc4;						// 参数描述4
	public string mParam5;							// 参数5
	public string mParamDesc5;						// 参数描述5
	public string mParam6;							// 参数6
	public string mParamDesc6;						// 参数描述6
	public string mParam7;							// 参数7
	public string mParamDesc7;						// 参数描述7
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.read(out mBuffTypeID);
		result = result && reader.readString(out mName);
		result = result && reader.readString(out mDescription);
		result = result && reader.read(out mBuffTime);
		result = result && reader.readString(out mParam0);
		result = result && reader.readString(out mParamDesc0);
		result = result && reader.readString(out mParam1);
		result = result && reader.readString(out mParamDesc1);
		result = result && reader.readString(out mParam2);
		result = result && reader.readString(out mParamDesc2);
		result = result && reader.readString(out mParam3);
		result = result && reader.readString(out mParamDesc3);
		result = result && reader.readString(out mParam4);
		result = result && reader.readString(out mParamDesc4);
		result = result && reader.readString(out mParam5);
		result = result && reader.readString(out mParamDesc5);
		result = result && reader.readString(out mParam6);
		result = result && reader.readString(out mParamDesc6);
		result = result && reader.readString(out mParam7);
		result = result && reader.readString(out mParamDesc7);
		return result;
	}
}
// auto generate end