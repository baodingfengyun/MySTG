using UnityEngine;

// 客户端构建类型:正式版关闭调试/启用热更(iOS用正式项目),测试版开启调试/显示服务器地址(iOS用测试项目)。
// 用于编辑器打包菜单按类型切换配置。
public enum CLIENT_TYPE : byte
{
	[EnumLabel("正式版"), Tooltip("正式客户端,关闭调试,启用热更,ios为正式项目")]
	OFFICIAL,
	[EnumLabel("测试版"), Tooltip("测试客户端,启用调试,显示服务器地址,ios为测试项目")]
	TEST,
}