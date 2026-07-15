// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Effect表格
public class EDEffect : ExcelDataT<EDEffect>
{
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