// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Chapter表格
public class EDChapter : ExcelDataT<EDChapter>
{
	public string mName;							// 名字
	public string mDesc;							// 描述
	public string mImage;							// 章节背景地图
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.readString(out mDesc);
		result = result && reader.readString(out mImage);
		return result;
	}
}
// auto generate end