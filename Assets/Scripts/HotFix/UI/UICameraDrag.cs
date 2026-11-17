using Obfuz;
using UnityEngine;
using static GBR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UICameraDrag.prefab
// 摄像机操控界面(全屏透明拖拽层)：onDraging 拖动平移镜头，onTwoFingerScale 双指缩放；
//   setEnable 开关。战斗中查看地图用。
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UICameraDrag : LayoutScript
{
    protected myUGUIObject mDragArea;
    // auto generate member end
    protected COMWindowDrag mComDrag;
    protected COMWindowMultiTouch mComMultiTouch;
    public UICameraDrag()
    {
        // auto generate constructor start
        // auto generate constructor end
    }
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mDragArea, "DragArea");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		// auto generate init end
		mDragArea.registeCollider(true);
		mDragArea.setPassDragEvent(true);
		mComDrag = mDragArea.addComponent<COMWindowDrag>(true);
		mComDrag.setDragCallback(null, onDraging, null);
		mComDrag.setMovable(false);
		mComMultiTouch = mDragArea.addComponent<COMWindowMultiTouch>(true);
		mComMultiTouch.setTwoFingerScaleCallback(onTwoFingerScale);
		mComMultiTouch.setMoveFingerStartDistanceThreshold(4000.0f);
		mComMultiTouch.setScaleThreshold(20.0f);
	}
	public void setEnable(bool enable)
	{
		mComDrag.setActive(enable);
		mComMultiTouch.setActive(enable);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onDraging(ComponentOwner dragObj, Vector3 mousePos)
	{
		if(mTowerDefenceSystem.getSelectedTowerScene() != null || !mBattleScene.isCameraScaled())
		{
			return;
		}
		Vector3 moveDelta = mComDrag.getTouchPoint().getMoveDelta();
		mBattleScene.deltaMoveCamera(-moveDelta.x * 0.02f, -moveDelta.y * 0.02f);
	}
	protected void onTwoFingerScale(float scaleRelative, float scaleAbs, float scaleFrameDelta)
	{
		mBattleScene.deltaMoveCamera(scaleFrameDelta);
	}
}