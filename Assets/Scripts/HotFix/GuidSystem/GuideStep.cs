using static UnityUtility;
using static FrameBaseHotFix;
using static GBR;

public class GuideStep : ClassObject
{
	protected EDGuide mData;
	protected bool mStarted;
	public virtual void init(EDGuide data, ParamBase paramTemplate)
	{
		mData = data;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mData = null;
		mStarted = false;
	}
	public virtual void update(float elapsedTime) { }
	public virtual void start()
	{
		log("开始步骤:" + ToString() + ", ID:" + getID());
		mStarted = false;
	}
	public int getID() { return mData.mID; }
	public virtual void clear()
	{
		mUIGuide.setTip(0, 0);
		mUIGuide.setNPCTalk(0, 0, 0, 0, false);
		mUIGuide.deactiveAllTip();
		mGlobalTouchSystem.setActiveOnlyObject(null);
		mEventSystem?.unlistenEvent(this);
	}
	//------------------------------------------------------------------------------------------------------------------------------
	// 由子节点主动调用
	protected void finish(int customNextStepID = 0)
	{
		// 任意步骤在结束以后都会禁用输入检测
		mGlobalTouchSystem.setUseGlobalTouch(!mData.mDeactiveInputAtFinish);
		mInputSystem.setActiveInput(!mData.mDeactiveInputAtFinish);
		clear();
		if (mData != null)
		{
			mGuideSystem.notifyStepFinish(mData.mID, customNextStepID);
		}
	}
	// 需要在子类中设置完仅激活的响应对象以后才能调用,否则引导界面无法响应事件
	protected void startInternal()
	{
		mStarted = true;
		mUIGuide.setTip(mData.mTipLocID, mData.mTipPosition);
		mUIGuide.setNPCTalk(mData.mNPCTalkLocID, mData.mNPCPosition, mData.mTalkBackground, mData.mTalkPosition, mData.mNeedClickTalk);
		// 如果有任意需要点击的对象,则启用输入检测,否则不检测任何输入
		mGlobalTouchSystem.setUseGlobalTouch(mGlobalTouchSystem.hasActiveOnlyObject());
		mInputSystem.setActiveInput(mGlobalTouchSystem.hasActiveOnlyObject());
	}
}