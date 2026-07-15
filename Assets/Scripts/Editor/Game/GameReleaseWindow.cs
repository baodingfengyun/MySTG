using System;
using UnityEditor;
using UnityEngine;
using static FrameBaseDefine;
using static FrameBaseUtility;
using static EditorCommonUtility;
using static FrameDefine;
using static GameDefine;

public class GameReleaseWindow : GameEditorWindow
{
	protected PlatformInfo mPlatform;		// 平台逻辑实例
	protected string mOutputPath;			// 输出路径
	protected string mPreName;				// 输出文件的名字
	protected bool mEnableHotFix;           // 打包的客户端是否启用热更
	protected string[] mVersionNumber;      // 用于修改本次打包的版本号
	protected string mCurVersion;           // 当前游戏的版本号
	protected CLIENT_TYPE mClientType;		// 客户端类型
	public void start()
	{
		Show();
		minSize = new(670, 650);
		// 打iOS时只能输出到英文路径,否则Xcode打开会报错
		mOutputPath = F_PROJECT_PATH + "GameOutput/";
		mClientType = CLIENT_TYPE.TEST;
		mEnableHotFix = true;
		mPreName = generatePreName(mEnableHotFix, mClientType, BUILD_SERVER);
		createPlatform();
		// 每打开一次都使bundleVersionCode自增
		if (isAndroid())
		{
			++PlayerSettings.Android.bundleVersionCode;
		}
	}
	//------------------------------------------------------------------------------------------------------------------------
	protected override void onGUI()
	{
		if (mPlatform == null)
		{
			createPlatform();
		}
		using (new GUILayout.VerticalScope())
		{
			label("当前平台:" + mPlatform.mName, 25);
			space(30);

			textField(ref mOutputPath, "输出路径:", 400);
			label("输出文件夹前缀: " + mPreName);
		}

		// 因为BUILD_SERVER不确定什么时候修改,所以一直都重新生成
		mPreName = generatePreName(mEnableHotFix, mClientType, BUILD_SERVER);
		space(30);
		using (new GUILayout.VerticalScope())
		{
			label("本地版本号:" + mCurVersion);
			using (new GUILayout.HorizontalScope())
			{
				label("新版本号:");
				for (int i = 0; i < mVersionNumber.Length; ++i)
				{
					textField(ref mVersionNumber[i], 100);
				}
				if (button("更新版本号", 100))
				{
					mVersionNumber = generateVersion(mVersionNumber.stringsToString('.')).split('.');
				}
			}
			if (isAndroid())
			{
				using (new GUILayout.HorizontalScope(GUILayout.Width(200)))
				{
					label("BundleVersionCode:" + PlayerSettings.Android.bundleVersionCode);
					if (button("更新BundleVersion", 150))
					{
						++PlayerSettings.Android.bundleVersionCode;
					}
				}
			}

			displayEnum("客户端类型:", "选择客户端的类型", ref mClientType);

			// 客户端选项
			if (button("ProjectSettings", 130))
			{
				GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.ProjectSettingsWindow")).Show();
			}
			if (button("还原宏定义", 120))
			{
                PlayerSettings.SetScriptingDefineSymbols(getNameBuildTarget(), mPlatform.getDefaultPlatformDefine());
            }
			label("当前宏定义:" + PlayerSettings.GetScriptingDefineSymbols(getNameBuildTarget()));
		}

		space(30);
		using (new GUILayout.VerticalScope())
		{
			toggle(ref mEnableHotFix, "启用热更");
			// 一键打包操作,分为小版本的热更打包和大版本的更新打包
			string packPreTip = "打包前需要确认以下状态:\n1.已经将项目更新到最新.\n2.已经更新了表格文件.";
			if (!isIOS())
			{
				packPreTip += "\n3.已经关闭了VisualStudio";
			}
			string version = getCurVersion();
			if (mEnableHotFix)
			{
				// 小版本的热更打包
				if (button("热更打包,打资源+打包", "小版本的热更打包,会执行打包AB,更新热更dll,更新版本号,更新文件列表", 220, 30))
				{
					// 需要大版本号与远端一致,小版本号大于远端小版本号
					messageOK("热更新打包时,需要大版本号与远端一致,且小版本号大于远端小版本号");
					bool execute = messageYesNo(packPreTip) &&
									MenuAssetBundle.packAssetBundle(mPlatform.mTarget, mPlatform.mAssetBundleFullPath, false) &&
									mPlatform.writeVersion() &&
									mPlatform.writeFileList(mPlatform.mAssetBundleFullPath) &&
									mPlatform.showNeedUploadFile(version);
				}
				if (button("热更打包,打包", "小版本的热更打包,会执行更新热更dll,更新版本号,更新文件列表", 220, 30))
				{
					// 需要大版本号与远端一致,小版本号大于远端小版本号
					messageOK("热更新打包时,需要大版本号与远端一致,且小版本号大于远端小版本号");
					bool execute = messageYesNo(packPreTip) &&
									mPlatform.writeVersion() &&
									mPlatform.writeFileList(mPlatform.mAssetBundleFullPath) &&
									mPlatform.showNeedUploadFile(version);
				}

				// 大版本更新打包
				if (button("大版本打包,打资源+打包", "大版本更新打包,会执行打包AB,构建xcode工程或生成apk", 220, 30))
				{
					// 需要大版本号大于远端
					messageOK("大版本更新打包时,需要大版本号大于远端");
					bool execute = messageYesNo(packPreTip) &&
									   MenuAssetBundle.packAssetBundle(mPlatform.mTarget, mPlatform.mAssetBundleFullPath, false) &&
									   build(mPlatform, version, mEnableHotFix, false) &&
									   mPlatform.showNeedUploadFile(version);
				}
				if (button("大版本打包,打包", "大版本更新打包,会执行构建xcode工程或生成apk", 220, 30))
				{
					// 需要大版本号大于远端
					messageOK("大版本更新打包时,需要大版本号大于远端");
					bool execute = messageYesNo(packPreTip) &&
									   build(mPlatform, version, mEnableHotFix, false) &&
									   mPlatform.showNeedUploadFile(version);
				}
			}
			else
			{
				// 不热更的版本更新
				if (button("打包,打资源+打包", "不热更的版本更新,打包AB,构建xcode工程或生成apk", 220, 30))
				{
					bool execute = messageYesNo(packPreTip) &&
								   MenuAssetBundle.packAssetBundle(mPlatform.mTarget, mPlatform.mAssetBundleFullPath, false) &&
								   build(mPlatform, version, mEnableHotFix, false);
				}
				if (button("只打包", "不热更的版本更新,构建xcode工程或生成apk", 220, 30))
				{
					bool execute = messageYesNo(packPreTip) &&
								   build(mPlatform, version, mEnableHotFix, false);
				}
			}

			space(30);

			// 分步打包
			if (button("打包AssetBundle", 120, 30))
			{
				bool execute = MenuAssetBundle.packAssetBundle(mPlatform.mTarget, mPlatform.mAssetBundleFullPath, false);
			}
			if (button("单独更新表格", "单独将表格打包AssetBundle,并且更新FileList", 120, 30))
			{
				bool execute = MenuAssetBundle.packAssetBundle(mPlatform.mTarget, mPlatform.mAssetBundleFullPath, false) &&
							   mPlatform.writeFileList(mPlatform.mAssetBundleFullPath);
			}

			space(30);

			// 测试打包是否有错误
			if (button("测试打包过程", 120, 30))
			{
				bool execute = messageYesNo(packPreTip) && 
							   build(mPlatform, version, mEnableHotFix, false);
			}
			if (isAndroid() && button("导出安卓工程", 120, 30))
			{
				build(mPlatform, version, false, true);
			}
		}
	}
	protected static bool build(PlatformInfo platform, string version, bool buildHybridCLR, bool exportAndroidProject)
	{
		try
		{
			PlayerSettings.bundleVersion = version;
			DateTime buildStartTime = DateTime.Now;
			bool result = platform.build(buildHybridCLR, exportAndroidProject);
			Debug.Log("打包完成:" + result + ", 耗时:" + (DateTime.Now - buildStartTime));
			++PlayerSettings.Android.bundleVersionCode;
			return result;
		}
		catch (Exception e)
		{
			Debug.LogError("打包错误:" + e.Message + ", stack:" + e.StackTrace);
			return false;
		}
	}
	protected static string generateVersion(string oldVersion)
	{
		oldVersion ??= "0.0.0";
		string[] newVersionNumbers = oldVersion.split('.');
		// 需要确保版本号只有3个部分
		if (newVersionNumbers.count() != 3)
		{
			newVersionNumbers = new string[3] { "0", "0", "0"};
		}
		DateTime time = DateTime.Now;
		string versionSuffix = (time.Year - 2000).IToS() + time.Month.IToS(2) + time.Day.IToS(2) + time.Hour.IToS(2) + time.Minute.IToS(2);
		return newVersionNumbers[0] + "." + newVersionNumbers[1] + "." + versionSuffix;
	}
	protected static string generatePreName(bool enableHotFix, CLIENT_TYPE type, string server)
	{
		string preName = "SheepVillage";
		if (!enableHotFix)
		{
			preName += "_NoHotFix";
		}
		if (type == CLIENT_TYPE.TEST)
        {
			preName += "_Test";
        }
		// 移除开头的http:或者https:,如果还有:,则认为是IP+端口号,虽然可以用正则表达式,但是正则阅读起来比较麻烦
		server = server.removeStartString("http:");
		server = server.removeStartString("https:");
		if (server.Contains(':'))
		{
			preName += "_" + server[(server.IndexOf(':') + 1)..];
		}
		return preName;
	}
	protected void createPlatform(BuildTarget target = BuildTarget.NoTarget)
	{
        Debug.Log("create platform:" + getBuildTarget());
        mPlatform = PlatformInfo.create();
        mPlatform.mIgnoreFile = new() { VERSION, FILE_LIST, mPlatform.mName, mPlatform.mName + ".manifest" };
		mPlatform.mTestClient = true;
		mPlatform.mEnableHotFix = true;
        mPlatform.generateFolderPreName();
        mPlatform.updateLocalVersion();
    }
	protected string getCurVersion()
	{
		return mVersionNumber.stringsToString('.');
	}
}