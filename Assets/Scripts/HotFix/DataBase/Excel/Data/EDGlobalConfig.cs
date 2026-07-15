// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// GlobalConfig表格
public class EDGlobalConfig : ExcelDataT<EDGlobalConfig>
{
	public string mType;							// 说明
	public string mValue;							// 参数
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mType);
		result = result && reader.readString(out mValue);
		return result;
	}
}
// auto generate end