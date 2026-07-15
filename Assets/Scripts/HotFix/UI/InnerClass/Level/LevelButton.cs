using static GBR;

// auto generate classname start
// generate from:Assets/GameResources/UI/UIPrefab/UISelectLevel.prefab
// 选择关卡界面的关卡按钮
public class LevelButton : WindowRecyclableUGUI
// auto generate classname end
{
	// auto generate member start
	protected myUGUIObject mUnfinished;
	protected myUGUIObject mFinish;
	protected myUGUIObject mUnfinishedIcon;
	protected myUGUIObject mFinishIcon;
	protected myUGUIObject mPlaying;
	protected myUGUIObject mLocked;
	protected myUGUIObject mSelecting;
	protected myUGUIObject mRedPoint;
	protected myUGUIText mLevelName;
	// auto generate member end
	protected EDLevel mLevelData;
	protected LEVEL_STATE mLevelState;
	protected BATTLE_MODE mBattleMode;
	public LevelButton(IWindowObjectOwner script) : 
		base(script){}
	protected override void assignWindowInternal()
	{
		// auto generate assignWindowInternal start
		newObject(out mUnfinished, "Unfinished");
		newObject(out mFinish, "Finish");
		newObject(out mUnfinishedIcon, "UnfinishedIcon");
		newObject(out mFinishIcon, "FinishIcon");
		newObject(out mPlaying, "Playing");
		newObject(out mLocked, "Locked");
		newObject(out mSelecting, "Selecting");
		newObject(out mRedPoint, "RedPoint");
		newObject(out mLevelName, "LevelName");
		// auto generate assignWindowInternal end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mRoot.registeCollider(onRootClick);
		// auto generate init end
	}
	public override void destroy()
	{
		base.destroy();
		if (mLevelData != null)
		{
			mClientSystem.getCOMLevel().getLevelRedPoint(mLevelData.mID)?.removePointUI(mRedPoint);
		}
	}
	public override void reset()
	{
		base.reset();
		if (mLevelData != null)
		{
			mClientSystem.getCOMLevel().getLevelRedPoint(mLevelData.mID)?.removePointUI(mRedPoint);
		}
		mLevelData = null;
		mLevelState = LEVEL_STATE.NONE;
		mBattleMode = BATTLE_MODE.NONE;
	}
	public void setLevelState(LEVEL_STATE levelState)
	{
		mLevelState = levelState;
		mLocked.setActive(levelState == LEVEL_STATE.LOCK);
		mFinish.setActive(levelState == LEVEL_STATE.COMPLETED || levelState == LEVEL_STATE.PLAYING);
		mUnfinished.setActive(levelState == LEVEL_STATE.UNLOCKED);
		mFinishIcon.setActive(levelState == LEVEL_STATE.COMPLETED || levelState == LEVEL_STATE.PLAYING);
		mUnfinishedIcon.setActive(levelState == LEVEL_STATE.UNLOCKED);
		mPlaying.setActive(levelState == LEVEL_STATE.PLAYING);
	}
	public void setLevelData(EDLevel levelData)
	{
		if (mLevelData != levelData)
		{
			mLevelData = levelData;
			mBattleMode = mLevelData.mMode;
			mLevelName.setText(mLevelData.mIconNumberName, this);
		}
		RedPoint levelRedPoint = mClientSystem.getCOMLevel().getLevelRedPoint(mLevelData.mID);
		mRedPoint.setActive(levelRedPoint != null);
		levelRedPoint?.bindPointUI(mRedPoint);
	}
	public EDLevel getLevelData() { return mLevelData; }
	public LEVEL_STATE getLevelState() { return mLevelState; }
	public void setSelect(bool select) { mSelecting.setActive(select); }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onRootClick()
	{
		if (mUILevelInfo.safe() == null)
		{
			LT.LOAD<UILevelInfo>().setLevel(mLevelData);
			mUISelectLevel.setSelectLevelButton(this);
			mUISelectLevel.showSelectCircle(true);
		}
		else
		{
			mUILevelInfo.hide(() =>
			{
				LT.LOAD<UILevelInfo>().setLevel(mLevelData);
				mUISelectLevel.setSelectLevelButton(this);
				mUISelectLevel.showSelectCircle(true);
			});
		}
	}
}