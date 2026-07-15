using Obfuz;
using UnityEngine;
using static FrameBaseHotFix;
using static FrameUtility;
using static MathUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIGuide.prefab
// 新手引导界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIGuide : LayoutScript
{
	protected myUGUIObject mTip;
	protected myUGUIText mTipText;
	protected myUGUIObject mTipTopPoint;
	protected myUGUIObject mTipBottomPoint;
	protected myUGUIObject mRogueBuffTip;
	protected myUGUIObject mGridTip;
	protected myUGUIObject mHandBigButton;
	protected myUGUIObject mHandNoEffect;
	protected myUGUIObject mHandStatic;
	protected myUGUIObject mHandSmallButton;
	protected myUGUIObject mNPCRoot;
	protected myUGUIObject mMask;
	protected myUGUIObject mNPCTalkLong;
	protected myUGUIText mNPCTalkLongText;
	protected myUGUIObject mClickContinueLong;
	protected myUGUIObject mNPCTalkShort;
	protected myUGUIText mNPCTalkShortText;
	protected myUGUIObject mClickContinueShort;
	protected myUGUIObject mNPCTalkShortLeft;
	protected myUGUIObject mNPCTalkShortRight;
	protected myUGUIObject mNPCTalkShortCenter;
    // auto generate member end
    protected long mDelayActiveAssignID;
    public UIGuide()
    {
        // auto generate constructor start
        // auto generate constructor end
    }
	public override void assignWindow()
    {
		// auto generate assignWindow start
		newObject(out mTip, "Tip");
		newObject(out mTipText, mTip, "TipText");
		newObject(out mTipTopPoint, "TipTopPoint");
		newObject(out mTipBottomPoint, "TipBottomPoint");
		newObject(out mRogueBuffTip, "RogueBuffTip");
		newObject(out mGridTip, "GridTip");
		newObject(out mHandBigButton, "HandBigButton");
		newObject(out mHandNoEffect, "HandNoEffect");
		newObject(out mHandStatic, "HandStatic");
		newObject(out mHandSmallButton, "HandSmallButton");
		newObject(out mNPCRoot, "NPCRoot");
		newObject(out mMask, mNPCRoot, "Mask");
		newObject(out mNPCTalkLong, mNPCRoot, "NPCTalkLong");
		newObject(out mNPCTalkLongText, mNPCTalkLong, "NPCTalkLongText");
		newObject(out mClickContinueLong, mNPCTalkLong, "ClickContinueLong");
		newObject(out mNPCTalkShort, mNPCRoot, "NPCTalkShort");
		newObject(out mNPCTalkShortText, mNPCTalkShort, "NPCTalkShortText");
		newObject(out mClickContinueShort, mNPCTalkShort, "ClickContinueShort");
		newObject(out mNPCTalkShortLeft, mNPCRoot, "NPCTalkShortLeft");
		newObject(out mNPCTalkShortRight, mNPCRoot, "NPCTalkShortRight");
		newObject(out mNPCTalkShortCenter, mNPCRoot, "NPCTalkShortCenter");
		// auto generate assignWindow end
    }
	public override void init()
	{
		base.init();
		// auto generate init start
		mNPCRoot.registeCollider(onNPCRootClick);
		// auto generate init end
		mHandBigButton.setIgnoreTimeScale(true);
		mHandBigButton.tryGetUnityComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
		mHandNoEffect.setIgnoreTimeScale(true);
		mHandNoEffect.tryGetUnityComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
		mHandSmallButton.setIgnoreTimeScale(true);
		mHandSmallButton.tryGetUnityComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
	}
	public override void onGameState()
	{
		base.onGameState();
		mNPCRoot.setActive(false);
		mTip.setActive(false);
		deactiveAllTip();
	}
	public void setTip(int tipID, int pos)
    {
		mTip.setActive(tipID != 0);
        if (mTip.isActive())
        {
            if (pos == 1)
            {
				mTip.setPosition(mTipTopPoint.getPosition());
            }
            else if (pos == 2)
            {
				mTip.setPosition(mTipBottomPoint.getPosition());
            }
			mTipText.setText(tipID, this);
		}
    }
    public void setNPCTalk(int textID, int pos, int talkBackground, int dialogPos, bool needClick)
    {
        mNPCRoot.setActive(textID != 0 || pos != 0);
		if (!mNPCRoot.isActive())
		{
			return;
		}
		if (needClick)
		{
			mGlobalTouchSystem.addActiveOnlyObject(mNPCRoot);
		}
		mNPCRoot.setHandleInput(needClick);
		mMask.setActive(needClick);
		mNPCTalkShort.setActive(textID != 0 && talkBackground == 1);
        mNPCTalkLong.setActive(textID != 0 && talkBackground == 2);
		mClickContinueShort.setActive(false);
        mClickContinueLong.setActive(false);
		if (mNPCTalkShort.isActive())
        {
            if (dialogPos == 1)
            {
				mNPCTalkShort.setPosition(mNPCTalkShortLeft.getPosition());
            }
            else if (dialogPos == 2)
			{
				mNPCTalkShort.setPosition(mNPCTalkShortRight.getPosition());
			}
			else if (dialogPos == 2)
			{
				mNPCTalkShort.setPosition(mNPCTalkShortCenter.getPosition());
			}
			mNPCTalkShortText.setText(textID, this);
		}
		else if (mNPCTalkLong.isActive())
		{
			mNPCTalkLongText.setText(textID, this);
			mClickContinueLong.setActive(needClick);
		}
		mClickContinueShort.setActive(needClick);
	}
    public void setDragTip(Vector3 startPos, Vector3 endPos, int handType)
    {
		deactiveAllTip();
        activeHand(handType).MOVE(startPos, endPos, divide(getLength(startPos - endPos), 800.0f), true);
    }
    public void deactiveAllTip()
    {
		if (mDelayActiveAssignID > 0)
		{
			mCommandSystem.interruptCommand(mDelayActiveAssignID);
			mDelayActiveAssignID = 0;
		}
		mTip.setActive(false);
		mRogueBuffTip.setActive(false);
        mGridTip.setActive(false);
		mHandBigButton.setActive(false);
		mHandNoEffect.setActive(false);
		mHandStatic.setActive(false);
		mHandSmallButton.setActive(false);
        mHandNoEffect.MOVE();
	}
    public void setRogueBuffTip(Vector3 pos)
    {
        deactiveAllTip();
		mDelayActiveAssignID = delayCall(1.0f, () => 
		{
			mRogueBuffTip.setActive(true);
			mRogueBuffTip.setPosition(pos);
			mDelayActiveAssignID = 0;
		});
    }
	public void setHandPosition(int type, Vector3 pos)
    {
		deactiveAllTip();
		activeHand(type)?.setPosition(pos);
    }
    public bool isNPCActive() { return mNPCRoot.isActive(); }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onNPCRootClick()
    {
        mNPCRoot.setActive(false);
    }
	protected myUGUIObject activeHand(int type)
	{
		mHandNoEffect.setActive(type == 1);
		mHandSmallButton.setActive(type == 2);
		mHandBigButton.setActive(type == 3);
		mHandStatic.setActive(type == 4);
        if (type == 1)
        {
            return mHandNoEffect;
		}
		else if (type == 2)
		{
			return mHandSmallButton;
		}
		else if (type == 3)
		{
			return mHandBigButton;
		}
		else if (type == 4)
		{
			return mHandStatic;
		}
		return null;
	}
}