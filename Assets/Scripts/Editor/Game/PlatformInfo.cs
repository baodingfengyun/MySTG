using System.Collections.Generic;
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
    public override string getDefaultPlatformDefineInternal() 
    {
        return USE_HYBRID_CLR + ";" + USE_OBFUZ + ";" + USE_URP; 
    }
}