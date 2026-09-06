using System;
using System.Collections.Generic;
using UnityEngine;
using static FrameBaseHotFix;
using static FrameUtility;
using static StringUtility;
using static UnityUtility;
using static MathUtility;
using static GameUtilityHotFix;
using static FrameBaseUtility;
using static GBR;
using static GDR;

// 战斗模式基类，定义战斗时需要的数据容器。主要分为几类：
// 关卡数据
// 地图数据
// 单位集合：怪物，地面怪物，飞行怪物，防御塔和传送门
// 波次数据
// 局内状态
// 操作状态
public abstract class BattleModeBase : IEventListener
{
	protected SafeList<CharacterMonster> mMonsterList = new();      // 场景中怪物的列表,包含mWalkMonsterList和mFlyMonsterList
	protected HashSet<CharacterMonster> mWalkMonsterList = new();	// 场景中地面怪物的列表
	protected HashSet<CharacterMonster> mFlyMonsterList = new();	// 场景中飞行怪物的列表
	protected List<CharacterMonster> mDeadMonsterList = new();		// 已经死亡还未销毁的怪物列表
	protected List<CharacterTower> mTowerList = new();              // 已经放置的塔的列表
	protected List<CharacterPortal> mPortalList = new();			// 传送门列表
	protected List<LevelGrid> mGridList = new();                    // 地上的格子列表
	protected List<Vector2Int> mMonsterDisplay = new();				// 下一波要显示的怪物列表,由于需要有一定的顺序,所以不用字典
	protected List<MonsterRoad> mMonsterRoadList = new();			// 多个出怪口对应的怪物路线
	protected EDLevel mLevelData;									// 当前关卡的表格数据,当游玩的是编辑器的关卡时,此变量为空
	protected EDMapConfig mMapConfig;                               // 当前关卡的地图数据
	protected CharacterGame mGlobalCharacter;                       // 用于使抽到的天赋词条生效的角色对象,在战斗中负责全局buff的角色
	protected MonsterGenerator mMonsterGenerator = new();			// 负责刷新怪物
	protected CharacterTower mSelectedTowerScene;					// 当前选中正在操作的场景中的塔
	protected CharacterMonster mBeenFocusedMonster;					// 当前被集火的怪物
	protected Point mTargetPoint;									// 怪物移动目标的格子
	protected float mBuildingCD;									// 建塔的CD,大于0表示正在CD中,此时不允许建塔
	protected int mCurExp;											// 已经获得的经验值
	protected int mWaveIndex;										// 当前是第几波,只有成功打过一波才进入下一波
	protected int mStreakCount;										// 连胜/败次数
	protected int mHp;												// 当前关卡的血量
	protected bool mHasBossNextWave;								// 下波是否有Boss
	protected BATTLE_STATE mState;									// 当前状态
	protected BATTLE_MODE mMode;									// 战斗模式
	// 退出战斗时的清理
	public virtual void clear()
	{
		mCharacterManager?.destroyCharacterList(mMonsterList.getMainList());
		mMonsterList.clear();
		mCharacterManager?.destroyCharacterList(mDeadMonsterList);
		mCharacterManager?.destroyCharacterList(mTowerList);
		mCharacterManager?.destroyCharacterList(mPortalList);
		mCharacterManager?.destroyCharacter(mGlobalCharacter);
		mGlobalCharacter = null;
		mBeenFocusedMonster = null;
		UN_CLASS_LIST(mGridList);
		mMonsterDisplay.Clear();
		mMonsterRoadList.Clear();
		mLevelData = null;
		mMapConfig = null;
		mMonsterGenerator.clear();
		mSelectedTowerScene = null;
		mTargetPoint = new(0, 0);
		mBuildingCD = 0.0f;
		mCurExp = 0;
		mWaveIndex = 0;
		mStreakCount = 0;
		mHp = 0;
		mHasBossNextWave = false;
		mState = BATTLE_STATE.NONE;
		mEventSystem?.unlistenEvent(this);
	}
	// 清空波次数据
	public virtual void clearWave()
	{
		mCharacterManager.destroyCharacterList(mMonsterList.getMainList());
		mMonsterList.clear();
		mMonsterGenerator.clearWave();
		mMonsterDisplay.Clear();
		logBase("[波次]清空数据 怪物列表，出怪，下一波怪");
	}
	// 战斗基础规则
	public virtual void update(float elapsedTime)
	{
		// 编辑器模式下，按下S键：时间倍数为10 （快进）
		if (isEditor() && isKeyCurrentDown(KeyCode.S))
		{
			Time.timeScale = 10.0f;
		}
		// 在战斗状态更新生成器并检查活动怪物
		if (mState == BATTLE_STATE.FIGHTING)
		{
			// 调用出怪逻辑的更新
			mMonsterGenerator.update(elapsedTime);

			// 检查有没有已经移动完成的怪物
			foreach (CharacterMonster monster in mMonsterList)
			{
				// 如果怪物对象已经销毁，从怪物表中删除
				if (monster.isDestroy())
				{
					removeMonster(monster);
					continue;
				}
				CharacterMonsterData monsterData = monster.getMonsterData();
				// 怪物移动到终点,直接销毁
				if (monster.getComMovement().isMoveFinish())
				{
					// 怪物突破防线事件
					using var b = new ClassScope<EventMonsterBreak>(out var param);
					param.mMonster = monster;
					mEventSystem.pushEvent(param);
					// 扣对应的血，播放音效，删除怪物，发送事件
					CmdGlobalSetLevelHp.execute(mHp - monsterData.mTableData.mHurtHp);
					AT.SOUND_2D(SOUND_HOTFIX.LOSE_HP);
					removeMonster(monster);
					CmdGlobalDestroyMonster.execute(monster);
				}
				// 怪物的生成时间结束
				else if (monster.getComLifeTime().isLifeDone())
				{
					removeMonster(monster);
					CmdGlobalDestroyMonster.execute(monster);
				}
				// 怪物血量为0,死亡
				else if (monsterData.mHP <= 0)
				{
					var killer = mCharacterManager.getCharacter(monsterData.mKillerGUID) as CharacterGame;
					// 波次经验
					int exp = mExcelMonster.getWaveExp(monsterData.mTableData.mID, mWaveIndex);
					CmdGlobalSetCurExp.execute(mCurExp + exp);
					removeMonster(monster);
					// 怪物死亡事件
					using var b = new ClassScope<EventMonsterWillDie>(out var eventParam0);
					eventParam0.mMonster = monster;
					eventParam0.mKiller = killer;
					mEventSystem.pushEvent(eventParam0, monster.getGUID());

					// 移除此怪物身上的所有buff
					monster.getStateMachine().clearState();
					// 添加死亡状态,虽然现在死亡状态没有持续时间,可以直接销毁怪物,但是为了以后的扩展性,还是保留先添加死亡状态,死亡状态结束后再销毁
					monster.getStateMachine().addState<ActionDead>();
					mDeadMonsterList.Add(monster);
					using var c = new ClassScope<EventMonsterDie>(out var eventParam1);
					eventParam1.mMonster = monster;
					eventParam1.mKiller = killer;
					mEventSystem.pushEvent(eventParam1, monster.getGUID());
					if (killer != null)
					{
						using var d = new ClassScope<EventKillMonster>(out var eventParam2);
						eventParam2.mMonster = monster;
						mEventSystem.pushEvent(eventParam2, killer.getGUID());
					}
				}
			}

			// 已经没有怪物,并且已经刷新完了,则这一波结束,计算获得的经验,跳转到下一流程
			if (mMonsterList.count() == 0 && mMonsterGenerator.isSpawnFinish())
			{
				mEventSystem.pushEvent<EventWaveWillFinish>();
				cmdWaveFinish();
				if (mHasBossNextWave)
				{
					AT.SOUND_2D(SOUND_HOTFIX.NEXT_WAVE_BOSS);
				}
			}
		}
		// 检查怪物有没有结束死亡状态的,战斗结束后仍然要检测,否则最后死亡的怪物不会被销毁
		for (int i = 0; i < mDeadMonsterList.Count; ++i)
		{
			CharacterMonster monster = mDeadMonsterList[i];
			if (monster.isDestroy())
			{
				mDeadMonsterList.RemoveAt(i--);
				continue;
			}
			if (!monster.hasState<ActionDead>())
			{
				CmdGlobalDestroyMonster.execute(monster);
				mDeadMonsterList.RemoveAt(i--);
			}
		}

		// 建造CD
		tickTimerOnce(ref mBuildingCD, elapsedTime);
	}
	// 设置关卡配置数据
	public virtual void setLevelData(EDLevel levelData)
	{
		mLevelData = levelData;
		mMapConfig = mExcelMapConfig.query(levelData.mMapID);
		mHp = LEVEL_INIT_HP;
		mMonsterGenerator.init();

		// 检查关卡格子配置以及解析格子
		int pointsCount = mMapConfig.mPoints.Count;
		if (pointsCount != getMapHeight() * getMapWidth())
		{
			logError("关卡中配置的格子数量错误,配置的高:" + getMapHeight().IToS() + ", 宽:" + getMapWidth().IToS() + ",实际的格子数:" + pointsCount.IToS() + ",关卡ID:" + mLevelData.mID.IToS());
			return;
		}
		initMapDataInternal();
	}
	// 初始化关卡
	public virtual void initLevel()
	{
		initLevelPortalFromConfig();
		initGlobalBuff();
	}
	public int getMapID() { return mMapConfig?.mID ?? 0; }
	public int getMapWidth() { return mMapConfig.mWidth; }
	public int getMapHeight() { return mMapConfig.mHeight; }
	public GRID_TYPE getGridType() { return mMapConfig?.mGridDirection ?? GRID_TYPE.SIX; }
	public int getEndPoint() { return mMapConfig.mTargetPoint.get(0); }
	public List<int> getStartPoints() { return mMapConfig.mSpawnPoint; }
	// 获取地图某一行的数据
	public void getGridLine(int lineIndex, List<int> line)
	{
		line.Clear();
		for (int i = 0; i < getMapWidth(); ++i)
		{
			line.Add(mMapConfig.mPoints[lineIndex * getMapWidth() + i]);
        }
	}
	public void setBattleState(BATTLE_STATE state)
	{
		mState = state;
		if (mState == BATTLE_STATE.FIGHTING)
		{
			// 通知所有防御塔开始战斗
			mTowerList.For(tower => tower.notifyStartFight());
		}
	}
	public bool generateRoadPathAndRefresh()
	{
		bool success = true;
		int mMonsterRoadListCount = mMonsterRoadList.Count;
		for(int i = 0; i < mMonsterRoadListCount; ++i)
		{
			success &= generateRoadPath(i, -1, -1, mMonsterRoadList[i].mMonsterWalkRoadPoint, mMonsterRoadList[i].mMonsterFlyRoadPoint);
		}
		return success;
	}
	// 计算怪物路线,extraBlockIndex表示临时作为不可通过的格子下标,emptyIndex表示临时作为可通过的格子下标
	public bool generateRoadPath(int roadIndex, int extraBlockIndex, int emptyIndex, List<int> walkRoadList, List<int> flyRoadList = null)
	{
		MonsterRoad road = mMonsterRoadList[roadIndex];
		Point startPoint = road.mStartPoint;
		if (startPoint.Equals(mTargetPoint))
		{
			flyRoadList?.Add(startPoint.toIndex(getMapWidth()));
			walkRoadList?.Add(startPoint.toIndex(getMapWidth()));
			return true;
		}

		using var a = new ListScope<bool>(out var roadMap);
		int count = mGridList.Count;
		if (flyRoadList != null)
		{
			for (int i = 0; i < count; ++i)
			{
				bool isGridSelfFlyable = isGridStateFlyable(mGridList[i].getState());
				bool canFly = extraBlockIndex != i;
				roadMap.Add(isGridSelfFlyable && (emptyIndex == i || canFly));
			}
			if (getGridType() == GRID_TYPE.FOUR)
			{
				AStar4(roadMap, startPoint.toIndex(getMapWidth()), mTargetPoint.toIndex(getMapWidth()), getMapWidth(), flyRoadList);
			}
			else if (getGridType() == GRID_TYPE.SIX)
			{
                AStar6OddR(roadMap, startPoint.toIndex(getMapWidth()), mTargetPoint.toIndex(getMapWidth()), getMapWidth(), flyRoadList);
			}
		}

		roadMap.Clear();
		for (int i = 0; i < count; ++i)
		{
			LevelGrid grid = mGridList[i];
			bool canWalk = grid.getTower() == null &&  extraBlockIndex != i;
			roadMap.Add(isGridStateWalkable(grid.getState()) && (emptyIndex == i || canWalk));
		}
		if (getGridType() == GRID_TYPE.FOUR)
		{
			return AStar4(roadMap, startPoint.toIndex(getMapWidth()), mTargetPoint.toIndex(getMapWidth()), getMapWidth(), walkRoadList);
		}
		else if (getGridType() == GRID_TYPE.SIX)
		{
			return AStar6OddR(roadMap, startPoint.toIndex(getMapWidth()), mTargetPoint.toIndex(getMapWidth()), getMapWidth(), walkRoadList);
		}
		return false;
	}
	// 起点认为是一定可行走的
	public bool generateWalkRoadPathCustom(int start, List<int> walkRoadList, int extraBlockIndex)
	{
		if (start < 0)
		{
			return false;
		}
		using var a = new ListScope<bool>(out var roadMap);
		int count = mGridList.Count;
		for (int i = 0; i < count; ++i)
		{
			LevelGrid grid = mGridList[i];
			roadMap.Add(isGridStateWalkable(grid.getState()) &&
						((grid.getTower() == null && 
						extraBlockIndex != i) || 
						i == start));
		}
		int end = getTargetPointIndex();
		if (start == end)
		{
			walkRoadList?.Add(start);
			return true;
		}
		if (getGridType() == GRID_TYPE.FOUR)
		{
			return AStar4(roadMap, start, end, getMapWidth(), walkRoadList);
		}
		else if (getGridType() == GRID_TYPE.SIX)
		{
			return AStar6OddR(roadMap, start, end, getMapWidth(), walkRoadList);
		}
		return false;
	}
	// 起点认为是一定可行走的
	public bool generateFlyRoadPathCustom(int start, List<int> flyRoadList, int extraBlockIndex)
	{
		using var a = new ListScope<bool>(out var roadMap);
		int count = mGridList.Count;
		for (int i = 0; i < count; ++i)
		{
			bool isFlyable = isGridStateFlyable(mGridList[i].getState());
			roadMap.Add((isFlyable && extraBlockIndex != i) || (i == start));
		}
		int end = getTargetPointIndex();
		if (start == end)
		{
			flyRoadList?.Add(start);
			return true;
		}
		if (getGridType() == GRID_TYPE.FOUR)
		{
			return AStar4(roadMap, start, end, getMapWidth(), flyRoadList);
		}
		else if (getGridType() == GRID_TYPE.SIX)
		{
			return AStar6OddR(roadMap, start, end, getMapWidth(), flyRoadList);
		}
		return false;
	}
	// 添加怪物至怪物列表
	public void addMonster(CharacterMonster monster)
	{
		if (!monster.getMonsterData().mFlyable)
		{
			mWalkMonsterList.Add(monster);
		}
		else
		{
			mFlyMonsterList.Add(monster);
		}
		mMonsterList.add(monster);
	}
	// 交换两个角色的位置
	public void swapCharacterGrid(CharacterGame character0, CharacterGame character1, int newIndex0, int newIndex1)
	{
		if (newIndex0 < 0 || newIndex0 >= mGridList.Count ||
			newIndex1 < 0 || newIndex1 >= mGridList.Count)
		{
			logError("角色的下标错误");
			return;
		}
		mGridList[newIndex0].setMainCharacter(character0);
		mGridList[newIndex1].setMainCharacter(character1);
	}
	public void setCharacterGridIndex(CharacterGame character, int index)
	{
		if (index < 0 || index >= mGridList.Count)
		{
			logError("角色的下标错误,index:" + index.IToS());
			return;
		}
		if (mGridList[index].hasItem())
		{
			logError("该格子有物品,无法放置角色,index:" + index.IToS());
			return;
		}
		mGridList[index].setMainCharacter(character);
	}
	public void setPortalGridIndex(CharacterPortal character, int index)
	{
		if (index < 0 || index >= mGridList.Count)
		{
			logError("角色的下标错误,index:" + index.IToS());
			return;
		}
		if (mGridList[index].hasItem())
		{
			logError("该格子有物品,无法放置角色,index:" + index.IToS());
			return;
		}
		mGridList[index].setPortal(character);
	}
	public void addTower(CharacterTower tower)
	{
		if (!mTowerList.addUnique(tower))
		{
			logError("重复加入塔");
		}
	}
	public void addPortal(CharacterPortal portal)
	{
		if (!mPortalList.addUnique(portal))
		{
			logError("重复加入传送门");
		}
	}
	public void removeTower(CharacterTower tower)
	{
		if (mSelectedTowerScene == tower)
		{
			mSelectedTowerScene = null;
		}
		mTowerList.Remove(tower);
		int index = tower.getTowerData().mGridIndex;
		if (index < 0 || index >= mGridList.Count)
		{
			return;
		}
		if (mGridList[index].getTower() != tower)
		{
			logError("移除场景中的塔错误:" + index.IToS() + ",当前数量:" + mGridList.Count);
			return;
		}
		mGridList[index].setMainCharacter(null);
	}
	public GRID_STATE getGridState(int index)				{ return mGridList.get(index)?.getState() ?? GRID_STATE.NONE; }
	public void setCurExp(int exp)							{ mCurExp = exp; }
	public void setSelectedTowerScene(CharacterTower tower)	{ mSelectedTowerScene = tower; }
	public void setWaveIndex(int index)						{ mWaveIndex = index; }
	public void setHp(int hp)								{ mHp = hp; }
	public void startBuildingCD()							{ mBuildingCD = BUILDING_CD; }
	public List<Vector2Int> getMonsterDisplay()				{ return mMonsterDisplay; }
	public List<LevelGrid> getGridList()					{ return mGridList; }
	public int getWaveIndex()								{ return mWaveIndex; }
	public int getCurExp()									{ return mCurExp; }
	public EDLevel getLevelData()							{ return mLevelData; }
	public int getLevelNeedExp()							{ return mLevelData.mNeedExp; }
	public int getLevelUsePower()							{ return mLevelData.mPowerUse; }
	public string getLevelName()							{ return mLevelData?.mName; }
	public int getLevelMusic()								{ return mLevelData?.mMusic ?? 0; }
	public int getLevelID()									{ return mLevelData?.mID ?? 0; }
	public bool isLevelValid()								{ return mLevelData != null; }
	public Vector3 getGridRootPos()							{ return mMapConfig?.mGridRootPos ?? Vector3.zero; }
	public float getCameraBattlePos()						{ return mMapConfig?.mCameraBattlePos ?? 1.0f; }
	public int getMapTheme()								{ return mMapConfig?.mTheme ?? -1; }
	public string getMapSceneName()							{ return getFileNameNoSuffixNoDir(mMapConfig?.mSceneName); }
	public List<MonsterRoad> getMonsterRoadList()			{ return mMonsterRoadList; }
	public List<int> getMonsterWalkRoadPoint(int roadIndex)	{ return mMonsterRoadList[roadIndex].mMonsterWalkRoadPoint; }
	public List<int> getMonsterFlyRoadPoint(int roadIndex)	{ return mMonsterRoadList[roadIndex].mMonsterFlyRoadPoint; }
	public int getHp()										{ return mHp; }
	public CharacterTower getSelectedTowerScene()			{ return mSelectedTowerScene; }
	public SafeList<CharacterMonster> getMonsterList()		{ return mMonsterList; }
	public List<CharacterMonster> getMonsterMainList()		{ return mMonsterList.getMainList(); }
	public bool isContinuityWin()							{ return mStreakCount >= 3; }
	public bool isContinuityLose()							{ return mStreakCount <= -3; }
	public MonsterGenerator getMonsterGenerator()			{ return mMonsterGenerator; }
	public BATTLE_MODE getBattleMode()						{ return mMode; }
	public Point getTargetPoint()							{ return mTargetPoint; }
	public int getTargetPointIndex()						{ return mTargetPoint.toIndex(getMapWidth()); }
	public int getStartPointIndex(int roadIndex)			{ return mMonsterRoadList[roadIndex].mStartPoint.toIndex(getMapWidth()); }
	public CharacterGame getGlobalCharacter()				{ return mGlobalCharacter; }
	public float getBuildingCD()							{ return mBuildingCD; }
	public List<CharacterTower> getTowerList()				{ return mTowerList; }
	public int getTypeTowerCount(TOWER_TYPE type)
	{
		int towerCount = 0;
		foreach (CharacterTower tower in mTowerList)
		{
			if (tower.getTowerType() == type)
			{
				++towerCount;
			}
		}
		return towerCount;
	}
	public int getMonsterTypeBuffCount(Type state)
	{
		int buffCount = 0;
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (monster.hasState(state))
			{
				++buffCount;
			}
		}
		return buffCount;
	}
	public void getMonsterWithTypeBuffInRange(Vector3 pos, float range, Type state, List<CharacterMonster> resultList)
	{
		resultList.Clear();
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (lengthLessEqualIgnoreY(monster.getPosition() - pos, range) && monster.hasState(state))
			{
				resultList.Add(monster);
			}
		}
	}
	public void getInvisibleMonsterInRange(Vector3 pos, float range, List<CharacterMonster> resultList)
	{
		resultList.Clear();
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (monster.getMonsterData().mIsInvisible > 0 && lengthLessEqualIgnoreY(monster.getPosition() - pos, range))
			{
				resultList.Add(monster);
			}
		}
	}
	public CharacterMonster getMonsterByCollider(Collider collider)
	{
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (monster.getCollider() == collider)
			{
				return monster;
			}
		}
		return null;
	}
	public bool hasMonsterAtGrid(int gridIndex)
	{
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (monster.getComMovement().getGridIndex() == gridIndex)
			{
				return true;
			}
		}
		return false;
	}
	public void setWin(bool win)
	{
		if (mStreakCount == 0)
		{
			mStreakCount += win ? 1 : -1;
		}
		else if (mStreakCount > 0)
		{
			mStreakCount = win ? mStreakCount + 1 : -1;
		}
		else
		{
			mStreakCount = win ? 1 : mStreakCount - 1;
		}
	}
	public bool hasItemAtGrid(int index) { return mGridList.get(index)?.hasItem() ?? false; }
	public bool hasPortalAtGrid(int index) { return mGridList.get(index)?.hasPortal() ?? false; }
	public CharacterTower getTowerAtGrid(int index) { return mGridList.get(index)?.getTower(); }
	public CharacterPortal getPortalAtGrid(int index) { return mGridList.get(index)?.getPortal(); }
	public CharacterTower getHighestAttackTower()
	{
		CharacterTower maxTower = null;
		foreach (CharacterTower tower in mTowerList)
		{
			if (maxTower == null || maxTower.getAttack() < tower.getAttack())
			{
				maxTower = tower;
			}
		}
		return maxTower;
	}
	// 搜寻附近一定范围内的所有塔
	public void getTowersInRange(Vector3 pos, float range, List<CharacterTower> resultList)
	{
		resultList.Clear();
		foreach (CharacterTower tower in mTowerList)
		{
			resultList.addIf(tower, lengthLessEqualIgnoreY(tower.getPosition() - pos, range));
		}
	}
	// 附近一定范围内是否存在怪物
	public bool hasMonstersInRange(Vector3 pos, float range)
	{
		float squaredRange = range * range;
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (monster.getMonsterData().mHP <= 0 || monster.getMonsterData().mIsInvisible > 0)
			{
				continue;
			}
			if (getSquaredLengthIgnoreY(monster.getPosition() - pos) < squaredRange)
			{
				return true;
			}
		}
		return false;
	}
	public static bool lengthLessEqualIgnoreY(Vector3 vec, float length) { return vec.x * vec.x + vec.z * vec.z <= length * length; }
	public static float getSquaredLengthIgnoreY(Vector3 vec) { return vec.x * vec.x + vec.z * vec.z; }
	// 搜寻附近一定范围内的所有地面行走怪物
	public void getWalkMonstersInRange(Vector3 pos, float range, List<CharacterMonster> resultList)
	{
		resultList.Clear();
		foreach (CharacterMonster monster in mWalkMonsterList)
		{
			if (monster.getMonsterData().mHP > 0 && lengthLessEqualIgnoreY(monster.getPosition() - pos, range))
			{
				resultList.Add(monster);
			}
		}
	}
	// 搜寻附近一定范围内的空中飞行怪物
	public void getFlyMonstersInRange(Vector3 pos, float range, List<CharacterMonster> resultList)
	{
		resultList.Clear();
		foreach (CharacterMonster monster in mFlyMonsterList)
		{
			if (monster.getMonsterData().mHP > 0 && lengthLessEqualIgnoreY(monster.getPosition() - pos, range))
			{
				resultList.Add(monster);
			}
		}
	}
	// 搜寻附近一定范围内的所有怪物
	public void getMonstersInRange(Vector3 pos, float range, List<CharacterMonster> resultList)
	{
		resultList.Clear();
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (monster.getMonsterData().mHP > 0 && lengthLessEqualIgnoreY(monster.getPosition() - pos, range))
			{
				resultList.Add(monster);
			}
		}
	}
	// 查找指定位置的一定范围内最近的怪物
	public CharacterMonster getNearestMonsterInRange(Vector3 pos, float range)
	{
		float squaredRange = range * range;
		CharacterMonster nearestMonster = null;
		float nearestDis = 0.0f;
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (monster.getMonsterData().mHP <= 0 || monster.getMonsterData().mIsInvisible > 0)
			{
				continue;
			}
			float curDis = getSquaredLengthIgnoreY(monster.getPosition() - pos);
			if (curDis > squaredRange)
			{
				continue;
			}
			if (nearestMonster == null || curDis < nearestDis)
			{
				nearestDis = curDis;
				nearestMonster = monster;
			}
		}
		return nearestMonster;
	}
	// 查找指定位置的一定范围内最近的陆地行走怪物
	public CharacterMonster getNearestWalkMonsterInRange(Vector3 pos, float range)
	{
		float squaredRange = range * range;
		CharacterMonster nearestMonster = null;
		float nearestDis = 0.0f;
		foreach (CharacterMonster monster in mWalkMonsterList)
		{
			if (monster.getMonsterData().mHP <= 0 || monster.getMonsterData().mIsInvisible > 0)
			{
				continue;
			}
			float curDis = getSquaredLengthIgnoreY(monster.getPosition() - pos);
			if (curDis > squaredRange)
			{
				continue;
			}
			if (nearestMonster == null || curDis < nearestDis)
			{
				nearestDis = curDis;
				nearestMonster = monster;
			}
		}
		return nearestMonster;
	}
	// 查找指定位置的一定范围内最近的空中飞行怪物
	public CharacterMonster getNearestFlyMonsterInRange(Vector3 pos, float range)
	{
		float squaredRange = range * range;
		CharacterMonster nearestMonster = null;
		float nearestDis = 0.0f;
		foreach (CharacterMonster monster in mFlyMonsterList)
		{
			if (monster.getMonsterData().mHP <= 0 || monster.getMonsterData().mIsInvisible > 0)
			{
				continue;
			}
			float curDis = getSquaredLengthIgnoreY(monster.getPosition() - pos);
			if (curDis > squaredRange)
			{
				continue;
			}
			if (nearestMonster == null || curDis < nearestDis)
			{
				nearestDis = curDis;
				nearestMonster = monster;
			}
		}
		return nearestMonster;
	}
	// 查找指定位置的一定范围内最近的怪物
	public CharacterMonster getNearestMonsterInRange(Vector3 pos, float minRange, float maxRange)
	{
		float squaredRangeMin = minRange * minRange;
		float squaredRangeMax = maxRange * maxRange;
		CharacterMonster nearestMonster = null;
		float nearestDis = 0.0f;
		foreach (CharacterMonster monster in mMonsterList.getMainList())
		{
			if (monster.getMonsterData().mHP <= 0 || monster.getMonsterData().mIsInvisible > 0)
			{
				continue;
			}
			float curDis = getSquaredLengthIgnoreY(monster.getPosition() - pos);
			if (curDis < squaredRangeMin || curDis > squaredRangeMax)
			{
				continue;
			}
			if (nearestMonster == null || curDis < nearestDis)
			{
				nearestDis = curDis;
				nearestMonster = monster;
			}
		}
		return nearestMonster;
	}
	// 查找指定位置的一定范围内最近的地面怪物
	public CharacterMonster getNearestWalkMonsterInRange(Vector3 pos, float minRange, float maxRange)
	{
		float squaredRangeMin = minRange * minRange;
		float squaredRangeMax = maxRange * maxRange;
		CharacterMonster nearestMonster = null;
		float nearestDis = 0.0f;
		foreach (CharacterMonster monster in mWalkMonsterList)
		{
			if (monster.getMonsterData().mHP <= 0 || monster.getMonsterData().mIsInvisible > 0)
			{
				continue;
			}
			float curDis = getSquaredLengthIgnoreY(monster.getPosition() - pos);
			if (curDis < squaredRangeMin || curDis > squaredRangeMax)
			{
				continue;
			}
			if (nearestMonster == null || curDis < nearestDis)
			{
				nearestDis = curDis;
				nearestMonster = monster;
			}
		}
		return nearestMonster;
	}
	// 查找指定位置的一定范围内最近的飞行怪物
	public CharacterMonster getNearestFlyMonsterInRange(Vector3 pos, float minRange, float maxRange)
	{
		float squaredRangeMin = minRange * minRange;
		float squaredRangeMax = maxRange * maxRange;
		CharacterMonster nearestMonster = null;
		float nearestDis = 0.0f;
		foreach (CharacterMonster monster in mFlyMonsterList)
		{
			if (monster.getMonsterData().mHP <= 0 || monster.getMonsterData().mIsInvisible > 0)
			{
				continue;
			}
			float curDis = getSquaredLengthIgnoreY(monster.getPosition() - pos);
			if (curDis < squaredRangeMin || curDis > squaredRangeMax)
			{
				continue;
			}
			if (nearestMonster == null || curDis < nearestDis)
			{
				nearestDis = curDis;
				nearestMonster = monster;
			}
		}
		return nearestMonster;
	}
	public void generateWaveMonster()
	{
		mMonsterGenerator.generateMonsters();
		refreshMonsterDisplaySkill();
	}
	public void regenerateWaveMonster(int monsterID, int count)
	{
		mMonsterGenerator.regenerateMosnterDataList(monsterID, count);
		refreshMonsterDisplaySkill();
	}
	public virtual bool isFighting()
	{
		return getCurScene().atProcedure<GameSceneBattleGamingFight>();
	}
	public virtual bool isEnded() { return mCurExp >= getLevelNeedExp(); }
	public abstract Type getSetupTowerProcedure();
	public abstract void cmdSellTower(CharacterTower tower);
	public abstract void cmdSelectItemOwned(WindowRecyclableUGUI item);
	public abstract void cmdPutTower(CharacterTower tower, int gridIndex, int propIndex);
	public virtual void cmdWaveFinish()
	{
		setBattleState(BATTLE_STATE.WAIT_FINISH);
		if (mLevelData != null)
		{
			if (mHp > 0)
			{
				if (atProcedure<GameSceneBattleGamingFight>())
				{
					waveFinish(true);
				}
			}
			else
			{
                if (mMode == BATTLE_MODE.ROGUE_LIKE)
                {
                    LT.LOAD<UILevelFaild>();
                }
            }
        }
	}
	public virtual float getWaveIntensity(int level, int waveIndex, int monsterID)
	{
		return mExcelWaveConfig.getWaveMonsterIncreaseValue(level, waveIndex, monsterID);
	}
	public int getTowersPower()
	{
		float sum = 0;
		foreach (CharacterTower tower in mTowerList)
		{
			sum += tower.generatPower();
		}
		return sum.round();
	}
	public void notifyWaveChanged()
	{
		foreach (CharacterTower tower in mTowerList)
		{
			tower.getComSkill().notifyWaveChanged();
		}
	}
	public void setFocusedMonster(CharacterMonster monster) { mBeenFocusedMonster = monster; }
	public CharacterMonster getFocusedMonster() { return mBeenFocusedMonster; }
	//------------------------------------------------------------------------------------------------------------------------------
	protected void refreshMonsterDisplaySkill()
	{
		mMonsterDisplay.Clear();
		mHasBossNextWave = false;
		foreach (MonsterSpawnInfo info in mMonsterGenerator.getMonsterGenerateList())
		{
			foreach (int id in info.mMonsters)
			{
				mHasBossNextWave |= mExcelMonster.query(id).mStrength == MONSTER_STRENGTH.BOSS;
				// 按顺序放入列表中
				bool find = false;
				for (int i = 0; i < mMonsterDisplay.Count; ++i)
				{
					if (mMonsterDisplay[i].x == id)
					{
						mMonsterDisplay[i] = new(id, mMonsterDisplay[i].y + 1);
						find = true;
						break;
					}
				}
				if (!find)
				{
					mMonsterDisplay.Add(new(id, 1));
				}
			}
		}
	}
	// 初始化全局buff
	protected void initGlobalBuff(){}
	// 初始化关卡传送门
	protected void initLevelPortalFromConfig()
	{
		if (mLevelData == null)
		{
			return;
		}
		foreach (EDMapPortal data in mExcelMapPortal.getMapPortals(mLevelData.mMapID).safe())
		{
			CmdGlobalCreateOrSetPortal.execute(data, data.mStart, true);
			foreach (int item in data.mEndList)
			{
				CmdGlobalCreateOrSetPortal.execute(data, item, false);
			}
		}
	}
	protected void initMapDataInternal()
	{
		mGlobalCharacter = mCharacterManager.createCharacter<CharacterGame>("global");
		int gridCount = getMapWidth() * getMapHeight();		// 地图格子数量
		Span<GRID_STATE> pointStateList = stackalloc GRID_STATE[gridCount];
		using var a = new ListScope<int>(out var gridLine);
		int curStateCount = 0;
		for (int i = 0; i < getMapHeight(); ++i)
		{
			getGridLine(i, gridLine);
			foreach (int value in gridLine)
			{
				pointStateList[curStateCount++] = (GRID_STATE)value;
			}
		}
		// 解析关卡中格子点信息
		for (int i = 0; i < gridCount; ++i)
		{
			var grid = mGridList.add(CLASS<LevelGrid>());
			grid.setState(pointStateList[i]);
			grid.setIndex(i);
		}
		// 获取出生点
		List<int> spawnPoints = getStartPoints();
		int spawnPointsCount = spawnPoints.Count;
		if (spawnPointsCount == 0)
		{
			logError("MapConfig ID = " + getMapID() + " 没配置出生点");
			return;
		}
		for (int i = 0; i < spawnPointsCount; ++i)
		{
			int spawnIndex = spawnPoints[i];
			if (spawnIndex < 0 || spawnIndex >= pointStateList.Length)
			{
				logError("MapConfig ID = " + getMapID() + " , SpawnPoint = " + spawnIndex + " 下标超出范围");
				return;
			}
			if (pointStateList[spawnIndex] != GRID_STATE.WALK_FLY_UNTRAP_UNTOWER)
			{
				logError("MapConfig ID = " + getMapID() + " , SpawnPoint = " + spawnIndex + " 对应 Points 的点位下标必须是8");
				return;
			}
			mMonsterRoadList.add(new()).mStartPoint = Point.fromIndex(spawnIndex, getMapWidth());
		}
		// 获取终点
		int targetIndex = getEndPoint();
		if (targetIndex == 0)
		{
			logError("MapConfig ID = " + getMapID() + " 没配置终点");
			return;
		}
		if (targetIndex < 0 || targetIndex >= pointStateList.Length)
		{
			logError("MapConfig ID = " + getMapID() + " , TargetPoint = " + targetIndex + " 下标超出范围");
			return;
		}
		if (pointStateList[targetIndex] != GRID_STATE.WALK_FLY_UNTRAP_UNTOWER)
		{
			logError("MapConfig ID = " + getMapID() + " , TargetPoint = " + targetIndex + " 对应 Points 的点位下标必须是8");
			return;
		}
		mTargetPoint = Point.fromIndex(targetIndex, getMapWidth());

		mEventSystem?.unlistenEvent(this);
	}
	// 从怪物表中删除
	protected void removeMonster(CharacterMonster monster)
	{
		if (mBeenFocusedMonster == monster)
		{
			mBeenFocusedMonster = null;
		}
		if (!monster.getMonsterData().mFlyable)
		{
			mWalkMonsterList.Remove(monster);
		}
		else
		{
			mFlyMonsterList.Remove(monster);
		}
		mMonsterList.remove(monster);
	}
}