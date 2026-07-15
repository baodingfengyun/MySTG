using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using static EditorCommonUtility;
using static PlatformUtility;

public class PlatformIOS : PlatformInfo
{
    protected override BuildResult buildInternal(out string outputFullPath)
    {
        PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.iOS, "");
        PlayerSettings.iOS.appleDeveloperTeamID = "";
        PlayerSettings.iOS.iOSManualProvisioningProfileID = "";
        PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Distribution;

        // 打包,此处只是到处一个xcode工程
        outputFullPath = mOutputPath + mFolderPreName + "_" + mBuildVersion;
        // 删除打包目录中的文件
        return buildIOS(outputFullPath, generateBuildOption(mTestClient));
    }
}