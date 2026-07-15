using Obfuz;
using System;
using UnityEngine;
using static FrameBaseHotFix;
using static GBR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UILevelInfo.prefab
// 关卡信息界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UILevelInfo : LayoutScript
{
	protected myUGUIObject mMask;
	protected myUGUIObject mCenterRoot;
	protected myUGUIText mLevelNameText;
	protected myUGUIObject mConfirm;
	protected myUGUIText mConfirmText;
	protected myUGUIText mConfirmCostText;
	protected myUGUIText mConfirmCostBlockedText;
	protected myUGUIObject mStartPosition;
    // auto generate member end
    protected EDLevel mLevelData;
	protected Vector3 mOriginalPosition;
	protected Vector3 mStartPos;
	protected bool mMovingDone;
	public UILevelInfo()
	{
		// auto generate constructor start
		// auto generate constructor end
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out mMask, "Mask");
		newObject(out mCenterRoot, "CenterRoot");
		newObject(out mLevelNameText, mCenterRoot, "LevelNameText");
		newObject(out myUGUIObject buttonRoot, mCenterRoot, "ButtonRoot", false);
		newObject(out mConfirm, buttonRoot, "Confirm");
		newObject(out mConfirmText, mConfirm, "ConfirmText");
		newObject(out mConfirmCostText, mConfirm, "ConfirmCostText");
		newObject(out mConfirmCostBlockedText, mConfirm, "ConfirmCostBlockedText");
		newObject(out mStartPosition, "StartPosition");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMask.registeCollider(onMaskClick);
		mCenterRoot.registeCollider();
		mConfirm.registeCollider(onConfirmClick);
		// auto generate init end
		mMask.registeCollider(onCloseClick, true);
		mOriginalPosition = mCenterRoot.getPosition();
		mStartPos = mStartPosition.getPosition();
	}
	public override void onGameState()
	{
		base.onGameState();
		mLevelData = null;
        mCenterRoot.MOVE(mStartPos);
		mMovingDone = false;
    }
	public void setLevel(EDLevel level)
	{
		mLevelData = level;
		mLevelNameText.setActive(!level.mEndless);
		mLevelNameText.setText(mLevelData.mName, this);
		// 根据服务器数据刷新状态
		refreshButtonState();
        mCenterRoot.MOVE_EX(KEY_CURVE.EXPO_OUT, mCenterRoot.getPosition(), mOriginalPosition, 0.2f, (_, _) => { mMovingDone = true; });
	}
	public void refreshButtonState()
	{
		int cost = mLevelData.mPowerUse;
		bool enoughPower = false;
		mConfirmCostText.setActive(enoughPower);
		mConfirmCostText.setText(cost);
		mConfirmCostBlockedText.setActive(!enoughPower);
		mConfirmCostBlockedText.setText(cost);
	}
	public void hide(Action callback)
	{
		mUISelectLevel?.safe().showSelectCircle(false);
        mCenterRoot.MOVE_EX(KEY_CURVE.EXPO_OUT, mCenterRoot.getPosition(), mStartPos, 0.2f, (_, isBreak)=>
		{
			// 确保break时不会执行回调
			if (isBreak)
			{
				return;
			}
			callback?.Invoke();
		});
	}
	public void setActiveOnlyConfirm(out Vector3 pos)
	{
		mGlobalTouchSystem.setActiveOnlyObject(mConfirm);
		pos = mConfirm.getWorldPosition();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onCloseClick()
	{
		hide(()=> { close(); });
	}
	protected void onConfirmClick()
	{
		if (mLevelData == null)
		{
			return;
		}
        CmdGlobalEnterLevel.execute(mLevelData.mID);
    }
	protected void onMaskClick()
	{
        hide(() => { close(); });
    }
}