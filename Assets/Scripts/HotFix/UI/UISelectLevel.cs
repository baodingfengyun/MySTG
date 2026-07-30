using Obfuz;
using System.Collections.Generic;
using UnityEngine;
using static FrameBaseHotFix;
using static FrameUtility;
using static GameUtilityHotFix;
using static MathUtility;
using static UnityUtility;
using static GBR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UISelectLevel.prefab
// 关卡选择界面
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UISelectLevel : LayoutScript
{
    protected myUGUIObject mCenterRoot;
    protected myUGUIObject mNormalRoot;
    protected myUGUIObject mLevelListRoot;
    protected myUGUIObject mNormalBack;
    protected myUGUIText mNormalChapterName;
    protected myUGUIText mNormalChapterNameID;
    protected myUGUIObject mLastLevel;
    protected myUGUIObject mNextLevel;
    protected myUGUIObject mLeftChapter;
    protected myUGUIObject mRightChapter;
    protected WindowStructPool<LevelButton> mLevelButtonPool;
    // auto generate member end
    protected List<int> mCurWorldChapters = new();
    protected EDLevel mSelectLevel;
    protected LevelButton mDragStartButton;
    protected int mDragStartButtonEdge;
    protected float mDragStartMousePosX;
    protected int mCurChapter;
    protected LEVEL_TYPE mCurLevelType;
    protected const float DRAG_CHANGE_SCREEN_PERCENT = 0.1f;
    public UISelectLevel()
    {
        // auto generate constructor start
        mLevelButtonPool = new(this);
        // auto generate constructor end
        mNeedUpdate = false;
    }
    public override void assignWindow()
    {
        // auto generate assignWindow start
        newObject(out mCenterRoot, "CenterRoot");
        newObject(out mNormalRoot, mCenterRoot, "NormalRoot");
        newObject(out mLevelListRoot, mNormalRoot, "LevelListRoot");
        newObject(out mNormalBack, mNormalRoot, "NormalBack");
        newObject(out myUGUIObject normalTopRoot, mNormalRoot, "NormalTopRoot", false);
        newObject(out mNormalChapterName, normalTopRoot, "NormalChapterName");
        newObject(out mNormalChapterNameID, normalTopRoot, "NormalChapterNameID");
        newObject(out mLastLevel, mNormalRoot, "LastLevel");
        newObject(out mNextLevel, mNormalRoot, "NextLevel");
        newObject(out mLeftChapter, "LeftChapter");
        newObject(out mRightChapter, "RightChapter");
        mLevelButtonPool.assignTemplate(mLevelListRoot, "LevelButton");
        // auto generate assignWindow end
    }
    public override void init()
    {
        base.init();
        // auto generate init start
        mCenterRoot.registeCollider();
        mNormalBack.registeCollider(onNormalBackClick);
        mLastLevel.registeCollider(onLastLevelClick);
        mNextLevel.registeCollider(onNextLevelClick);
        mLeftChapter.registeCollider(onLeftChapterClick);
        mRightChapter.registeCollider(onRightChapterClick);
        // auto generate init end
        var dragCom = mLevelListRoot.addComponent<COMWindowDrag>(true);
        dragCom.setDragCallback(onLevelListRootDragStart, onLevelListRootDraging, onLevelListRootDragEnd);
        dragCom.initDrag(Vector2.zero, 0.0f, false, true);
        mNormalBack.setClickSound(SOUND_HOTFIX.CLOSE_BUTTON);
    }
    public override void onGameState()
    {
        base.onGameState();
        mLevelButtonPool.unuseAll();
        mCurWorldChapters.Clear();
        foreach (EDChapter item in mExcelChapter.queryAll())
        {
            mCurWorldChapters.add(item.mID);
        }
        mSelectLevel = null;
        mDragStartButton = null;
        mDragStartButtonEdge = 0;
        mDragStartMousePosX = 0.0f;
        mCurChapter = -1;
        mLastLevel.setActive(false);
        mNextLevel.setActive(false);
        mLeftChapter.setActive(false);
        mRightChapter.setActive(false);
        mLevelListRoot.MOVE();
        mLevelListRoot.setLeftCenterToParentLeftCenter();
        mCurLevelType = LEVEL_TYPE.MAIN_LEVEL;
        setChapter(mCurWorldChapters[0]);
    }
    public void refreshAllLevelButtonState()
    {
        int maxAbsX = 0;
        LevelButton findButton = null;
        LevelButton selectButton = null;
        int buttonPosX = 0;
        foreach (LevelButton levelButton in mLevelButtonPool.getUsedList())
        {
            EDLevel levelData = levelButton.getLevelData();
            levelButton.setLevelData(levelData);
            levelButton.setPosition(levelButton.getRoot().getPosition().replaceX(buttonPosX));
            buttonPosX += (int)(mNormalRoot.getSize().x * 0.7f); // 间距稍微近一点
            levelButton.setSelect(mSelectLevel == levelButton.getLevelData());
            if (mSelectLevel == levelButton.getLevelData())
            {
                selectButton = levelButton;
            }
			maxAbsX = maxAbsX.clampMin(((int)levelButton.getRoot().getPosition().x).abs());
            if (mClientSystem.getCOMLevel().isLevelComplete(levelData.mID))
            {
                levelButton.setLevelState(LEVEL_STATE.COMPLETED);
            }
            else if (mClientSystem.getCOMLevel().isLevelComplete(levelData.mUnLockByCompleteLevel))
            {
                levelButton.setLevelState(LEVEL_STATE.UNLOCKED);
            }
            else
            {
                levelButton.setLevelState(LEVEL_STATE.LOCK);
            }
            // 如果是战斗中，优先选中
            if (levelButton.getLevelState() != LEVEL_STATE.LOCK && (findButton == null || findButton.getLevelState() != LEVEL_STATE.PLAYING))
            {
                findButton = levelButton;
            }
        }
        if (selectButton != null)
        {
            findButton = selectButton;
        }
        findButton ??= mLevelButtonPool.getUsedList().get(0);
        mLayout.refreshUIDepth(mLevelListRoot, true);
        // 当图标所占区域的宽度没有超出屏幕时,就设置为屏幕大小。拖拽后所有图标需要居中，再加一个屏幕长度。
        mLevelListRoot.setWidth(((maxAbsX + 300.0f) * 2.0f).clampMin(getScreenSize().x) + getScreenSize().x);
        setSelectLevelButton(findButton);
        if (findButton != null)
        {
            float limit = (mLevelListRoot.getSize().x - getScreenSize().x) * 0.5f;
            mLevelListRoot.setPositionX(getMin(limit, getMax(-limit, -findButton.getRoot().getPosition().x)));
        }
    }
    public void showSelectCircle(bool newShow)
    {
        foreach (LevelButton each in mLevelButtonPool.getUsedList())
        {
            each.setSelect(newShow && mSelectLevel == each.getLevelData());
        }
    }
    public void setSelectLevelButton(LevelButton levelButton)
    {
        mSelectLevel = levelButton?.getLevelData();
        if (mSelectLevel != null)
        {
            mNormalChapterNameID.setText(mSelectLevel.mIconNumberName, this);
            refreshLeftRightButton();
            mLastLevel.setActive(levelButton != mLevelButtonPool.getUsedList()[0]);
            mNextLevel.setActive(levelButton != mLevelButtonPool.getUsedList()[^1]);
        }
    }
    public void refreshLeftRightButton()
    {
        BATTLE_MODE curMode = BATTLE_MODE.ROGUE_LIKE;
        int maxChapter = -1;
        EDLevel maxLevelData = mExcelLevel.query(mClientSystem.getCOMLevel().getMaxCompleteLevel(curMode), false);
        if (maxLevelData != null)
        {
            maxChapter = maxLevelData.mChapter;
            if (!maxLevelData.mNextLevel.isEmpty())
            {
                EDLevel nextLevelData = mExcelLevel.query(maxLevelData.mNextLevel[0], false);
                if (nextLevelData != null && nextLevelData.mMode == curMode)
                {
                    maxChapter = nextLevelData.mChapter;
                }
            }
        }
        mLeftChapter.setActive(mCurChapter != mCurWorldChapters[0]);
        mRightChapter.setActive(maxChapter >= 0 && mCurChapter != maxChapter);
    }
    public void setSelectModePage(LEVEL_TYPE curModePage)
    {
        mCurLevelType = curModePage;
        setChapter(mCurChapter);
    }
    public void setLevel(EDLevel level)
    {
        mSelectLevel = level;
        setChapter(level.mChapter);
    }
    public void selectLevel(EDLevel level)
    {
        setLevel(level);
        foreach (LevelButton each in mLevelButtonPool.getUsedList())
        {
            if (each.getLevelData() == mSelectLevel && each.getLevelState() == LEVEL_STATE.LOCK)
            {
                tip("当前关卡未解锁");
                mSelectLevel = null;
                break;
            }
        }
    }
    public bool setActiveOnlyLevel(int levelID, out Vector3 pos)
    {
        EDLevel data = mExcelLevel.query(levelID);
        if (mCurChapter != data.mChapter)
        {
            setChapter(data.mChapter);
        }
        foreach (LevelButton item in mLevelButtonPool.getUsedList())
        {
            if (item.getLevelData() == data)
            {
                myUGUIObject root = item.getRoot();
                mGlobalTouchSystem.setActiveOnlyObject(root);
                pos = root.getWorldPosition();
                return true;
            }
        }
        pos = Vector3.zero;
        return false;
    }
    public void setActiveOnlyNormalBack(out Vector3 pos)
    {
        mGlobalTouchSystem.setActiveOnlyObject(mNormalBack);
        pos = mNormalBack.getWorldPosition();
    }
    //------------------------------------------------------------------------------------------------------------------------------
    protected void onLevelListRootDragStart(ComponentOwner dragObj, TouchPoint touchPoint, ref bool allowDrag)
    {
        mLevelListRoot.MOVE();
        mUILevelInfo.safe()?.hide(null);
        int index = mLevelButtonPool.getUsedList().FindIndex((item) => item.getLevelData() == mSelectLevel);
        mDragStartButton = index < 0 ? null : mLevelButtonPool.getUsedList()[index];
        mDragStartButtonEdge = index == 0 ? -1 : (index == mLevelButtonPool.getUsedList().Count - 1) ? 1 : 0;
        mDragStartMousePosX = touchPoint.getCurPosition().x;
    }
    protected void onLevelListRootDraging(ComponentOwner dragObj, Vector3 mousePos)
    {
        float maxX = mLevelListRoot.getSize().x * 0.5f - mCenterRoot.getSize().x * 0.5f;
        mLevelListRoot.setPosition(new(mLevelListRoot.getPosition().x.clamp(-maxX, maxX), 0.0f));
        float nearX = float.MaxValue;
        LevelButton select = null;
        foreach (LevelButton each in mLevelButtonPool.getUsedList().safe())
        {
            float xOffset = mDragStartMousePosX - mousePos.x;
            // 如果拖拽的距离大于了屏幕的0.2，就不比较最初的按钮位置
            if (mDragStartButton == each && xOffset.abs() > mRoot.getSize().x * DRAG_CHANGE_SCREEN_PERCENT)
            {
                if (mDragStartButtonEdge == 0 || (mDragStartButtonEdge == -1 && xOffset > 0) || (mDragStartButtonEdge == 1 && xOffset < 0))
                {
                    continue;
                }
            }
            float eachX = mNormalRoot.worldToLocal(each.getRoot().getWorldPosition()).x;
            if (eachX.abs() < nearX.abs())
            {
                nearX = eachX;
                select = each;
            }
        }
        setSelectLevelButton(select);
    }
    protected void onLevelListRootDragEnd(ComponentOwner dragObj, Vector3 mousePos, bool cancel)
    {
        float nearX = float.MaxValue;
        LevelButton select = null;
        foreach (LevelButton each in mLevelButtonPool.getUsedList().safe())
        {
            float xOffset = mDragStartMousePosX - mousePos.x;
            // 如果拖拽的距离大于了屏幕的0.2，就不比较最初的按钮位置
            if (mDragStartButton == each && xOffset.abs() > mRoot.getSize().x * DRAG_CHANGE_SCREEN_PERCENT)
            {
                if (mDragStartButtonEdge == 0 || (mDragStartButtonEdge == -1 && xOffset > 0) || (mDragStartButtonEdge == 1 && xOffset < 0))
                {
                    continue;
                }
            }
            float eachX = mNormalRoot.worldToLocal(each.getRoot().getWorldPosition()).x;
            if (eachX.abs() < nearX.abs())
            {
                nearX = eachX;
                select = each;
            }
        }
        float maxX = mLevelListRoot.getSize().x * 0.5f - mCenterRoot.getSize().x * 0.5f;
        Vector3 targetPos = new((mLevelListRoot.getPosition().x - nearX).clamp(-maxX, maxX), 0.0f);
        mLevelListRoot.MOVE(KEY_CURVE.EXPO_OUT, mLevelListRoot.getPosition(), targetPos, 0.3f);
        setSelectLevelButton(select);
    }
    protected void onNormalBackClick()
    {
        changeProcedure<GameSceneLobbyMain>();
    }
    protected int findLevelButtonIndex(EDLevel levelData)
    {
        var list = mLevelButtonPool.getUsedList();
        int count = list.Count;
        for (int i = 0; i < count; ++i)
        {
            if (list[i].getLevelData() == levelData)
            {
                return i;
            }
        }
        return -1;
    }
    protected void onLastLevelClick()
    {
        if (mSelectLevel == null)
        {
            return;
        }
        int index = findLevelButtonIndex(mSelectLevel);
        if (index <= 0 || index - 1 >= mLevelButtonPool.getUsedList().Count)
        {
            return;
        }
        LevelButton lastButton = mLevelButtonPool.getUsedList()[index - 1];
        float nearX = mNormalRoot.worldToLocal(lastButton.getRoot().getWorldPosition()).x;
        float maxX = mLevelListRoot.getSize().x * 0.5f - mCenterRoot.getSize().x * 0.5f;
        Vector3 targetPos = new((mLevelListRoot.getPosition().x - nearX).clamp(-maxX, maxX), 0.0f);
        mLevelListRoot.MOVE(KEY_CURVE.EXPO_OUT, mLevelListRoot.getPosition(), targetPos, 0.3f);
        setSelectLevelButton(lastButton);
    }
    protected void onNextLevelClick()
    {
        if (mSelectLevel == null)
        {
            return;
        }
        int index = findLevelButtonIndex(mSelectLevel);
        if (index < 0 || index + 1 >= mLevelButtonPool.getUsedList().Count)
        {
            return;
        }
        LevelButton nextButton = mLevelButtonPool.getUsedList()[index + 1];
        float nearX = mNormalRoot.worldToLocal(nextButton.getRoot().getWorldPosition()).x;
        float maxX = mLevelListRoot.getSize().x * 0.5f - mCenterRoot.getSize().x * 0.5f;
        Vector3 targetPos = new((mLevelListRoot.getPosition().x - nearX).clamp(-maxX, maxX), 0.0f);
        mLevelListRoot.MOVE(KEY_CURVE.EXPO_OUT, mLevelListRoot.getPosition(), targetPos, 0.3f);
        setSelectLevelButton(nextButton);
    }
    protected void onLeftChapterClick()
    {
        int index = mCurWorldChapters.IndexOf(mCurChapter);
        if (index <= 0)
        {
            return;
        }
        setChapter(mCurWorldChapters[index - 1]);
    }
    protected void onRightChapterClick()
    {
        int index = mCurWorldChapters.IndexOf(mCurChapter);
        if (index < 0 || index + 1 >= mCurWorldChapters.Count)
        {
            return;
        }
        setChapter(mCurWorldChapters[index + 1]);
    }
    protected void setChapter(int chapter)
    {
        if (chapter <= 0)
        {
            return;
        }
        if (mCurChapter == chapter)
        {
            refreshAllLevelButtonState();
            return;
        }

        // 设置选中章节
        mCurChapter = chapter;
        PlayerPrefs.Save();
        mNormalChapterName.setText(mExcelChapter.query(mCurChapter).mName, this);
        // 生成level按钮
        mLevelButtonPool.unuseAll();
        int showIndex = 0; // 设计上多显示两个未解锁的关卡
        foreach (EDLevel levelData in mExcelLevel.getChapterLevels(BATTLE_MODE.ROGUE_LIKE, mCurChapter).safe())
        {
            if (mClientSystem.getCOMLevel().isLevelComplete(levelData.mUnLockByCompleteLevel))
            {
                mLevelButtonPool.newItem().setLevelData(levelData);
            }
            else if (showIndex < 2)
            {
                mLevelButtonPool.newItem().setLevelData(levelData);
                showIndex++;
            }
        }
        refreshAllLevelButtonState();
    }
}