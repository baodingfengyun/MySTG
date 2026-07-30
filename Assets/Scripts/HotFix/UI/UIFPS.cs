using Obfuz;
using static FrameBaseHotFix;
using static FrameBaseUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIFPS.prefab
// 显示版本号,帧率,电量
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIFPS : LayoutScript
{
	protected myUGUIText mVersion;
	protected myUGUIText mFPS;
	protected myUGUIObject mBatteryRoot;
	protected myUGUIText mEnergy;
    // auto generate member end
    public UIFPS()
    {
        // auto generate constructor start
        // auto generate constructor end
    }
    public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out myUGUIObject versionRoot, "VersionRoot", false);
		newObject(out mVersion, versionRoot, "Version");
		newObject(out myUGUIObject fPSRoot, "FPSRoot", false);
		newObject(out mFPS, fPSRoot, "FPS");
		newObject(out mBatteryRoot, "BatteryRoot");
		newObject(out mEnergy, mBatteryRoot, "Energy");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		// auto generate init end
		mBatteryRoot.setActive(isEditor() || isTestClient());
	}
	public override void onGameState()
	{
		base.onGameState();
		mVersion.setText(mAssetVersionSystem.getLocalVersion());
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		mFPS.setText(mGameFrameworkHotFix.getFPS());
		mEnergy.setText((AndroidMainClass.getBatteryEnergy() * 0.001f).round());
	}
	public void setVersionVisible(bool visible)
	{
		mVersion.setActive(visible);
	}
}