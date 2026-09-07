using Obfuz;
using UnityEngine;
using static FrameBaseHotFix;
using static FrameUtility;
using static GameUtilityHotFix;
using static GBR;
using static FrameBaseUtility;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIGaming.prefab
// 战斗主界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIGaming : LayoutScript
{
	protected myUGUIText mMapName;
	protected myUGUIText mProgressText;
	protected myUGUIText mWaveText;
	protected myUGUIObject mMapDetail;
	protected myUGUIText mHeartCountText;
	protected myUGUIImage mSilverCoinIcon;
	protected myUGUIText mSilverCoinCount;
	protected myUGUIObject mSpeedChangeOn;
	protected myUGUIObject mSpeedChangeOff;
	protected myUGUIObject mTalentInfo;
	protected myUGUIObject mTalentInfoSelect;
	protected myUGUIImageSimple mSetting;
	protected myUGUIObject mStartFight;
	protected myUGUIText mAutoStartTime;
    // auto generate member end
    protected Vector3 mOriginPosititon;
	protected float mOriginLength;
	protected int mAutoStartTotalTimer;
	protected float mAutoStartTimer;
	protected const int AUTO_START_TIME = 20;
	public UIGaming()
	{
		// auto generate constructor start
		// auto generate constructor end
		;
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out myUGUIObject leftTopRoot, "LeftTopRoot", false);
		newObject(out mMapName, leftTopRoot, "MapName");
		newObject(out myUGUIObject rightTopRoot, "RightTopRoot", false);
		newObject(out mProgressText, rightTopRoot, "ProgressText");
		newObject(out mWaveText, rightTopRoot, "WaveText");
		newObject(out mMapDetail, rightTopRoot, "MapDetail");
		newObject(out mHeartCountText, rightTopRoot, "HeartCountText");
		newObject(out myUGUIObject silverCoinBackground, rightTopRoot, "SilverCoinBackground", false);
		newObject(out mSilverCoinIcon, silverCoinBackground, "SilverCoinIcon");
		newObject(out mSilverCoinCount, silverCoinBackground, "SilverCoinCount");
		newObject(out mSpeedChangeOn, rightTopRoot, "SpeedChangeOn");
		newObject(out mSpeedChangeOff, rightTopRoot, "SpeedChangeOff");
		newObject(out mTalentInfo, rightTopRoot, "TalentInfo");
		newObject(out mTalentInfoSelect, rightTopRoot, "TalentInfoSelect");
		newObject(out mSetting, rightTopRoot, "Setting");
		newObject(out myUGUIObject rightBottomRoot, "RightBottomRoot", false);
		newObject(out mStartFight, rightBottomRoot, "StartFight");
		newObject(out mAutoStartTime, rightBottomRoot, "AutoStartTime");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		mMapDetail.registeCollider(onMapDetailClick);
		mSpeedChangeOn.registeCollider(onSpeedChangeOnClick);
		mSpeedChangeOff.registeCollider(onSpeedChangeOffClick);
		mTalentInfo.registeCollider(onTalentInfoClick);
		mSetting.registeColliderImage(onSettingClick);
		mStartFight.registeCollider(onStartFightClick);
		// auto generate init end
		mSetting.setClickSound(SOUND_HOTFIX.CLOSE_BUTTON);
		mStartFight.setClickSound(SOUND_HOTFIX.START_FIGHT_BUTTON);
	}
	public override void onGameState()
	{
		base.onGameState();
        setProgress(0);
        setWaveValue(0);
        setTimeScaled(false);
        mTalentInfoSelect.setActive(false);
        mAutoStartTime.setActive(false);
        mAutoStartTotalTimer = 0;
        mAutoStartTimer = 0.0f;
        BATTLE_MODE mode = mTowerDefenceSystem.getBattleMode();
		mTalentInfo.setActive(mode == BATTLE_MODE.ROGUE_LIKE);
		refresh();
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if(mTowerDefenceSystem.getLevelData().mAutoStart && mAutoStartTotalTimer > 0 && mUIBattleItemSelectRogue.safe() == null && tickTimerLoop(ref mAutoStartTimer, elapsedTime, 1))
		{
			mAutoStartTotalTimer -= 1;
			mAutoStartTime.setText(mAutoStartTotalTimer);
			mAutoStartTime.setColor(mAutoStartTotalTimer <= 5 ? Color.red : Color.white);
			if (mAutoStartTotalTimer == 0)
			{
				onStartFightClick();
			}
		}
	}
	public void refresh()
	{
		setTimeScaled(!Time.timeScale.isEqual(1.0f));
		setHeartCount(mTowerDefenceSystem.getHp());
		mMapName.setText(mTowerDefenceSystem.getLevelName(), this);
		refreshCoin();
		setProgress(mTowerDefenceSystem.getCurExp());
		setWaveValue(mTowerDefenceSystem.getWaveIndex());
	}
	public void refreshCoin()
	{
		BATTLE_MODE mode = mTowerDefenceSystem.getBattleMode();
		if (mode == BATTLE_MODE.ROGUE_LIKE)
		{
			mSilverCoinIcon.setSpriteName("BuildingCoin");
			mSilverCoinCount.setText(mTowerDefenceSystem.getGoldCoinRogue());
		}
	}
	public void setProgress(int value)
	{
		mProgressText.setText(value.IToS() + "/" + mTowerDefenceSystem.getLevelNeedExp().IToS());
	}
	public void setWaveValue(int value)
	{
		mWaveText.setText(value + 1); // 显示值比实际下标多1
	}
	public void setHeartCount(int count)
	{
		mHeartCountText.setText(count);
	}
	public void notifyStartFight(bool fighting)
	{
		mStartFight.setActive(!fighting);
		mAutoStartTime.setActive(!fighting && mTowerDefenceSystem.getLevelData().mAutoStart);
		if(!fighting)
		{
			mAutoStartTotalTimer = AUTO_START_TIME;
			mAutoStartTime.setColor(Color.white);
			mAutoStartTime.setText(mAutoStartTotalTimer);
		}
		else
		{
			mAutoStartTotalTimer = 0;
		}
	}
	public void setTimeScaled(bool scaled)
	{
		mSpeedChangeOff.setActive(!scaled);
		mSpeedChangeOn.setActive(scaled);
	}
	public void displayTalentButton(bool select)
	{
		mTalentInfo.setActive(!select);
		mTalentInfoSelect.setActive(select);
	}
	public void setActiveOnlyStartFight(out Vector3 pos)
	{
		mGlobalTouchSystem.setActiveOnlyObject(mStartFight);
		pos = mStartFight.getWorldPosition();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onSettingClick()
	{
		LT.LOAD<UIQuitBattle>().setConfirm(() => { exitToLobbyOrMapEditor(); });
	}
	protected void onTalentInfoClick()
	{
		displayTalentButton(true);
	}
	protected void onMapDetailClick()
	{
		;
	}
	protected void onSpeedChangeOffClick()
	{
		CmdGlobalTimeScale.execute(true);
	}
	protected void onSpeedChangeOnClick()
	{
		CmdGlobalTimeScale.execute(false);
	}
	protected void onStartFightClick()
	{
		// 如果已经获得了足够的通关经验,就不能再继续开始战斗了
		if (mTowerDefenceSystem.isEnded())
		{
			tip("已经通关了,不能再开始战斗了");
			return;
		}
		var roadList = mTowerDefenceSystem.getMonsterRoadList();
		int roadListCount = roadList.Count;
		for (int i = 0; i < roadListCount; ++i)
		{
			if(roadList[i].mMonsterWalkRoadPoint.Count == 0)
			{
				tip("出怪口{0}没有怪物寻路路线", i.IToS());
				return;
			}
		}
		changeProcedure<GameSceneBattleGamingFight>();
		logBase("点击开始战斗按钮，进入战斗流程");
	}
	protected void onCameraScaleClick()
	{
		CmdGlobalCameraScale.execute(true);
	}
	protected void onCameraResetClick()
	{
		CmdGlobalCameraScale.execute(false);
	}
}