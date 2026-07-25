using UnityEditor;
using UnityEngine;
using static EditorCommonUtility;
using static FrameMacro;

public abstract class PlatformInfo : PlatformBase
{
    public static PlatformInfo create()
    {
        BuildTarget target = getBuildTarget();
        PlatformInfo info = null;
        if (target == BuildTarget.Android)
        {
            info = new PlatformAndroid();
        }
        else if (target == BuildTarget.iOS)
        {
            info = new PlatformIOS();
        }
        else
        {
            Debug.LogError("不支持的平台");
        }
        return info;
    }
    // 此处只做示例,并非真的只用版本号作为文件夹名字
	public override string getRemotePathInEditor(string version)
	{
        return version;
	}
    public override string getDefaultPlatformDefineInternal() 
    {
        return USE_HYBRID_CLR + ";" + USE_OBFUZ + ";" + USE_URP; 
    }
}