// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// Audio表格
public class EDAudio : ExcelDataT<EDAudio>
{
	public string mPath;							// 音效在GameResources下的相对路径,带后缀
	public string mDescription;						// 描述
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mPath);
		result = result && reader.readString(out mDescription);
		return result;
	}
}
// auto generate end