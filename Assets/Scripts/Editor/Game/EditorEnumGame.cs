using UnityEngine;

public enum CLIENT_TYPE : byte
{
	[EnumLabel("正式版"), Tooltip("正式客户端,关闭调试,启用热更,ios为正式项目")]
	OFFICIAL,
	[EnumLabel("测试版"), Tooltip("测试客户端,启用调试,显示服务器地址,ios为测试项目")]
	TEST,
}