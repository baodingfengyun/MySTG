// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Localization表格
public class EDLocalization : ExcelDataT<EDLocalization>
{
	public string mChinese;							// 简体中文
	public string mChineseTraditional;				// 繁体中文
	public string mEnglish;							// 英文
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mChinese);
		result = result && reader.readString(out mChineseTraditional);
		result = result && reader.readString(out mEnglish);
		return result;
	}
}
// auto generate end