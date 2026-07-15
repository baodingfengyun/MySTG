using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EditorCommonUtility;
using static GameDefine;
using static UnityUtility;
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
        if (info != null)
        {
            info.mTarget = target;
            info.mAssetBundleFullPath = getAssetBundlePath(true);
        }
        return info;
    }
    public override string getDefaultPlatformDefineInternal() 
    {
        return USE_HYBRID_CLR + ";" + USE_OBFUZ + ";" + USE_URP; 
    }
    protected override void configureScriptingDefine()
    {
        string platformDefine = getDefaultPlatformDefine();
        log("设置宏:" + platformDefine);
        PlayerSettings.SetScriptingDefineSymbols(getNameBuildTarget(), platformDefine);
    }
    protected override List<string> getDynamicDownloadList() { return DYNAMIC_DOWNLOAD_LIST; }
}