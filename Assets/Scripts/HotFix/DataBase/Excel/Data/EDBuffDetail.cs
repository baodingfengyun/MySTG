// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// BuffDetail表格
public class EDBuffDetail : ExcelDataT<EDBuffDetail>
{
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