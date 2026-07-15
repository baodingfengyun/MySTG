using UnityEngine;
using System.Collections.Generic;
using static FrameUtility;
using static FrameBaseHotFix;
using static GBR;

public class GuideSystem : FrameSystem
{
    protected Dictionary<int, GuideStep> mGuideMap = new();     // 存储所有引导进度信息,用于根据ID查询
    protected List<GuideStep> mGuideList = new();               // 存储所有引导进度信息,按顺序存储的列表
    protected GuideStep mCurStep;                               // 当前引导序列
    protected bool mMuteGuide;
	public override void update(float elapsedTime)
	{
        base.update(elapsedTime);
        mCurStep?.update(elapsedTime);
        if (isKeyCurrentDown(KeyCode.F))
        {
            mCurStep?.clear();
			mGlobalTouchSystem.setUseGlobalTouch(true);
            mInputSystem.setActiveInput(true);
            clear();
		}
	}
    public void clear()
    {
		mGuideMap.Clear();
		UN_CLASS_LIST(mGuideList);
        mCurStep = null;
        mMuteGuide = false;
    }
	// stepID是上一次完成的最后一个步骤的ID
	public void initCurStep(int stepID)
    {
		if (mMuteGuide)
        {
            return;
        }
        clear();
		// 需要找到此ID的起始步骤ID
		int nextGuide = stepID == 0 ? mExcelGuide.getFirstGuide() : mExcelGuide.getNextGuide(stepID);
		if (nextGuide > 0)
        {
			initAllGuide();
			startGuide(mExcelGuide.getFallbackStep(nextGuide));
		}
	}
    public void setCurStep(int stepID) { mCurStep = mGuideMap.get(stepID); }
    public GuideStep getStep(int stepID) { return mGuideMap.get(stepID); }
    // 通知当前系统一个引导步骤完成,进入下一个步骤
    public void notifyStepFinish(int stepID, int customNextStepID = 0)
    {
		if (customNextStepID <= 0)
        {
            customNextStepID = mExcelGuide.getNextGuide(stepID);
		}
		startGuide(customNextStepID);
        if (mCurStep == null)
        {
			// 如果已经没有引导步骤了,开启输入检测系统
			mGlobalTouchSystem.setUseGlobalTouch(true);
			mInputSystem.setActiveInput(true);
		}
    }
    // 手动指定开始哪个引导步骤
    public void startGuide(int stepID)
    {
		mCurStep = mGuideMap.get(stepID);
        mCurStep?.start();
	}
    public void setMuteGuide()
    {
        mMuteGuide = true;
        mCurStep = null;
        mGlobalTouchSystem.setUseGlobalTouch(true);
        mInputSystem.setActiveInput(true);
    }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void initAllGuide()
    {
        foreach (EDGuide data in mExcelGuide.queryAll())
        {
			var step = mGuideMap.add(data.mID, CLASS(GuideRegister.getGuideType(data.mID)) as GuideStep);
            step.init(data, GuideRegister.getParamTemplate(data));
            mGuideList.Add(step);
		}
    }
}