using static FrameBaseHotFix;

public class RedPointManager : FrameSystem
{
	private RedPoint mMainLevel;
    // 创建所有的静态红点,红点区分静态红点和动态红点,静态红点有固定的ID,且不会销毁,动态红点会动态创建,所以ID不固定
    public override void init()
    {
        base.init();
		mRedPointSystem.createRedPoint(out mMainLevel);
	}
}