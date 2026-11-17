using UnityEngine;
using static GBR;
using static FrameUtility;

// 战斗镜头缩放命令:scale=true 拉近(聚焦到当前选中塔,否则到最近位置),false 拉回最远位置。
// 取消缩放时会顺带清除选中的场景塔;镜头移动统一用 EXPO_OUT 缓动,0.2 秒完成。
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