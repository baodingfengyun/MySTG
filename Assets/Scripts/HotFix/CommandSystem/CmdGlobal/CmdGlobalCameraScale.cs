using UnityEngine;
using static GBR;
using static FrameUtility;

public class CmdGlobalCameraScale
{
	public static void execute(bool scale)
	{
		if (!scale)
		{
			CmdGlobalSelectTowerScene.execute(null);
		}
		Vector3 newPos;
		if (scale && mTowerDefenceSystem.getSelectedTowerScene() != null)
		{
			newPos = mBattleScene.focusCamera(mTowerDefenceSystem.getSelectedTowerScene().getPosition());
		}
		else
		{
			newPos = scale ? mBattleScene.getMinCameraPos() : mBattleScene.getMaxCameraPos();
		}
		GameCamera mainCamera = getMainCamera();
		mainCamera.MOVE(KEY_CURVE.EXPO_OUT, mainCamera.getPosition(), newPos, 0.2f);
	}
}