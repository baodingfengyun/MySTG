using UnityEngine;
using static FrameUtility;
using static FrameBaseUtility;
using static UnityUtility;
using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 加载资源
public class GameSceneLoginLoading : SceneProcedure
{
	protected override void onInit(SceneProcedure lastProcedure)
	{
        myUGUIObject.setDefaultClickSound(SOUND_HOTFIX.BUTTON);
        mClientSystem.clear();
		mGameFrameworkHotFix.setFrameRate(30);
		LT.LOAD_TOP<UITip>(1100);
		LT.LOAD_TOP<UIFPS>(1101);
		LT.LOAD_TOP<UIClickEffect>(2351);
		SceneRegister.registerBattleScene();
		StateGroupRegister.registeDebuff();
		SoundRegister.registerAll();
		if (!isEditor() && !isTestClient())
		{
			mUIFPS.setVersionVisible(true);
		}
		changeProcedureDelay<GameSceneLoginGaming>();
		setRenderScale(PlayerPrefs.GetFloat(PREF_RENDER_SCALE, 1.0f));
		mAudioManager.setMaxAudioCount(10);
	}
	protected override void onExit(SceneProcedure nextProcedure) { }
}