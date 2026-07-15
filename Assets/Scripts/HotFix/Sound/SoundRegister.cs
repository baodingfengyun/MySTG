using static FrameBaseHotFix;
using static GBR;

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