using static FrameBase;
using static FrameBaseUtility;

/// <summary>
/// 启动场景 - （5）退出流程
/// </summary>
public class StartSceneExit : SceneProcedure
{
	public override void exit()
	{
		base.exit();
		// 因为更新资源以后,所有的资源都会卸载然后重新加载一次,已经加载的资源就会出现丢失的情况,所以此处卸载之前加载的布局
		mLayoutManager.unloadAllLayout();
		logBase("退出启动场景，通过StartSceneExit.exit()");
	}
}