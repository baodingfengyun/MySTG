// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// MapPortal表格
public class EDMapPortal : ExcelDataT<EDMapPortal>
{
	public int mMap;								// 所属地图Excel MapConfig
	public int mStart;								// 起点
	public PORTAL_RULE mEndRule;					// 终点选择方式
	public List<int> mEndList = new();				// 终点列表
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.read(out mMap);
		result = result && reader.read(out mStart);
		result = result && reader.readEnumByte(out mEndRule);
		result = result && reader.readList(mEndList);
		return result;
	}
}
// auto generate end