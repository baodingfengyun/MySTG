using UnityEditor;
using UnityEditor.Build.Reporting;
using static UnityUtility;
using static EditorCommonUtility;
using static PlatformUtility;

public class PlatformAndroid : PlatformInfo
{
    protected override BuildResult buildInternal(out string outputFullPath)
    {
        log("已自动填充Android密码");
        PlayerSettings.Android.keystorePass = "3462124zhourui@@";
        PlayerSettings.Android.keyaliasPass = "3462124zhourui@@";

        // 打包
        outputFullPath = mOutputPath + mFolderPreName + "_" + mBuildVersion;
        if (!mExportAndroidProject)
        {
            outputFullPath += ".apk";
        }
        return buildAndroid(outputFullPath, generateBuildOption(mTestClient));
    }
}