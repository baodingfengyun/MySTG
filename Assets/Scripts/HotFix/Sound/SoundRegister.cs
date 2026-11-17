using static FrameBaseHotFix;
using static GBR;

// 音效注册表:遍历 EDAudio 表把"音效ID→文件名"登记到 AudioManager,供 AT.SOUND 按 ID 播放。
// mRegisted 自减守卫保证 registerAll 只执行一次,避免重复注册。
public class SoundRegister
{
	protected static int mRegisted = 1;
	public static void registerAll()
	{
		if (mRegisted-- <= 0)
		{
			return;
		}
		foreach (EDAudio item in mExcelAudio.queryAll())
		{
			register(item.mID, item.mPath);
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected static void register(int soundID, string fileName)
	{
		mAudioManager.registeSoundDefine(soundID, fileName);
	}
}