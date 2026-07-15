// auto generate start
using System;
using System.Collections.Generic;
using UnityEngine;

// BulletDamageModifier表格
public class EDBulletDamageModifier : ExcelDataT<EDBulletDamageModifier>
{
	public string mName;							// 名字
	public BULLET_DAMAGE_MODIFIER mType;			// 类型
	public string mParam0;							// 参数0
	public string mParamDesc0;						// 参数描述0
	public string mParam1;							// 参数1
	public string mParamDesc1;						// 参数描述1
	public string mParam2;							// 参数2
	public string mParamDesc2;						// 参数描述2
	public override bool read(SerializerRead reader)
	{
		bool result = base.read(reader);
		result = result && reader.readString(out mName);
		result = result && reader.readEnumByte(out mType);
		result = result && reader.readString(out mParam0);
		result = result && reader.readString(out mParamDesc0);
		result = result && reader.readString(out mParam1);
		result = result && reader.readString(out mParamDesc1);
		result = result && reader.readString(out mParam2);
		result = result && reader.readString(out mParamDesc2);
		return result;
	}
}
// auto generate end