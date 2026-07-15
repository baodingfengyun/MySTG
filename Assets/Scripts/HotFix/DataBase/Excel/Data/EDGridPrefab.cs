// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// GridPrefab表格
public class EDGridPrefab : ExcelDataT<EDGridPrefab>
{
	public GRID_STATE mGridState;					// 格子类型
	public int mTheme;								// 风格主题，对应MapConfig里同名参数
	public string mPrefab;							// Prefab路径,相对于GameResources
	public string mMaterial;						// Material, 相对于GameResources
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readEnumByte(out mGridState);
		result = result && reader.read(out mTheme);
		result = result && reader.readString(out mPrefab);
		result = result && reader.readString(out mMaterial);
		return result;
	}
}
// auto generate end