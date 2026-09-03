using System;
using System.Collections.Generic;
using UnityEngine;
using static GBR;

// 如何管理一整局战斗？流程只回答“现在应该做什么”。需要一个贯穿整场战斗的统一入口解决。
// TowerDefenceSystem: 对外统一入口（它的目的是让外围代码不与具体模式绑定）
// |- BattleModeBase: 一局战斗的公共数据与规则
//    |- BattleModeRogue: 肉鸽特有数据与行为
// 战斗逻辑系统,位于最顶层,管理战斗场景,战斗中的所有角色单元,战斗逻辑状态以及数据
public class TowerDefenceSystem : FrameSystem
{
	protected BattleModeBase mBattleModeInstance;                               // 当前战斗实例
	// 退出战斗时的清理
	public void clear() 
	{
		mBattleModeInstance.clear();
		mBattleModeInstance = null;
	}
	// 一波结束时的清理
	public void clearWave() { mBattleModeInstance.clearWave(); }
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		mBattleModeInstance?.update(elapsedTime);
	}
	public void setBattleMode(BATTLE_MODE mode, bool isEndless)
	{
		if (mode == BATTLE_MODE.ROGUE_LIKE)
		{
			mBattleModeInstance = new BattleModeRogue();
		}
		else
		{
			mBattleModeInstance = null;
		}
	}
	public void setGoldCoinRogue(int coin)								{ (mBattleModeInstance as BattleModeRogue)?.setGoldCoin(coin); }
	public void setAllowSelectPropListRogue(List<ExcelData> list)		{ (mBattleModeInstance as BattleModeRogue)?.setAllowSelectPropList(list); }
	public bool isAllPropUsedRogue()									{ return (mBattleModeInstance as BattleModeRogue)?.isAllPropUsed() ?? false; }
	public void initLevel()												{ mBattleModeInstance.initLevel(); }
	public void setLevelData(EDLevel levelData)							
	{
		if (mBattleModeInstance == null)
		{
			setBattleMode(levelData.mMode, levelData.mEndless);
		}
		mBattleModeInstance.setLevelData(levelData); 
	}
	public void setBattleState(BATTLE_STATE state)						{ mBattleModeInstance.setBattleState(state); }
	public void setCurExp(int exp)										{ mBattleModeInstance.setCurExp(exp); }
	public void setSelectedTowerScene(CharacterTower tower)				{ mBattleModeInstance.setSelectedTowerScene(tower); }
	public void setWaveIndex(int index)									{ mBattleModeInstance.setWaveIndex(index); }
	public void setWin(bool win)										{ mBattleModeInstance.setWin(win); }
	public void setHp(int hp)											{ mBattleModeInstance.setHp(hp); }
	public void addMonster(CharacterMonster monster)					{ mBattleModeInstance.addMonster(monster); }
	public void swapCharacterGrid(CharacterGame tower0, CharacterGame tower1, int newIndex0, int newIndex1)	{ mBattleModeInstance.swapCharacterGrid(tower0, tower1, newIndex0, newIndex1); }
	public void setCharacterGridIndex(CharacterGame tower, int index)	{ mBattleModeInstance.setCharacterGridIndex(tower, index); }
	public void setPortalGridIndex(CharacterPortal portal, int index)	{ mBattleModeInstance.setPortalGridIndex(portal, index); }
	public void addTower(CharacterTower tower)							{ mBattleModeInstance.addTower(tower); }
	public void addPortal(CharacterPortal portal)						{ mBattleModeInstance.addPortal(portal); }
	public void notifyWaveChanged()										{ mBattleModeInstance.notifyWaveChanged(); }
	public void removeTower(CharacterTower tower)						{ mBattleModeInstance.removeTower(tower); }
	public void startBuildingCD()										{ mBattleModeInstance.startBuildingCD(); }
	public void setFocusedMonster(CharacterMonster monster)				{ mBattleModeInstance.setFocusedMonster(monster); }
	public CharacterMonster getFocusedMonster()							{ return mBattleModeInstance.getFocusedMonster(); }
	public void cmdSelectItemOwned(WindowRecyclableUGUI towerItem)		{ mBattleModeInstance.cmdSelectItemOwned(towerItem); }
	public void cmdPutTower(CharacterTower tower, int gridIndex, int propIndex)					{ mBattleModeInstance.cmdPutTower(tower, gridIndex, propIndex); }
	public void cmdSellTower(CharacterTower tower)						{ mBattleModeInstance.cmdSellTower(tower); }
	public int getGoldCoinRogue()										{ return (mBattleModeInstance as BattleModeRogue).getGoldCoin(); }
	public int getAllowSelectPropListRogueCount()						{ return (mBattleModeInstance as BattleModeRogue).getAllowSelectPropListCount(); }
	public List<AllowSelectProp> getAllowSelectPropListRogue()			{ return (mBattleModeInstance as BattleModeRogue).getAllowSelectPropList(); }
	public void clearAllowSelectPropListRogue()							{ (mBattleModeInstance as BattleModeRogue)?.clearAllowSelectPropList(); }
	public bool isEnded()												{ return mBattleModeInstance.isEnded(); }
	public bool generateRoadPathAndRefresh()							{ return mBattleModeInstance.generateRoadPathAndRefresh(); }
	public bool generateWalkRoadPathCustom(int start, List<int> walkRoadList, int extraBlockIndex) { return mBattleModeInstance.generateWalkRoadPathCustom(start, walkRoadList, extraBlockIndex); }
	public bool generateFlyRoadPathCustom(int start, List<int> flyRoadList, int extraBlockIndex) { return mBattleModeInstance.generateFlyRoadPathCustom(start, flyRoadList, extraBlockIndex); }
	public GRID_STATE getGridState(int index)							{ return mBattleModeInstance.getGridState(index); }
	public bool hasItemAtGrid(int index)								{ return mBattleModeInstance.hasItemAtGrid(index); }
	public bool hasTowerAtGrid(int index)								{ return mBattleModeInstance.getTowerAtGrid(index) != null; }
	public bool hasPortalAtGrid(int index)								{ return mBattleModeInstance.hasPortalAtGrid(index); }
	public CharacterTower getTowerAtGrid(int index)						{ return mBattleModeInstance.getTowerAtGrid(index); }
	public CharacterPortal getPortalAtGrid(int index)					{ return mBattleModeInstance.getPortalAtGrid(index); }
	public BATTLE_MODE getBattleMode()									{ return mBattleModeInstance.getBattleMode(); }
	public BattleModeBase getBattleModeInstance()						{ return mBattleModeInstance; }
	public BattleModeRogue getBattleModeRogue()							{ return mBattleModeInstance as BattleModeRogue; }
	public CharacterMonster getMonsterByCollider(Collider collider)		{ return mBattleModeInstance.getMonsterByCollider(collider); }
	public List<CharacterMonster> getMonsterMainList()					{ return mBattleModeInstance.getMonsterMainList(); }
	public List<Vector2Int> getMonsterDisplay()							{ return mBattleModeInstance.getMonsterDisplay(); }
	public List<LevelGrid> getGridList()								{ return mBattleModeInstance.getGridList(); }
	public List<CharacterTower> getTowerList()							{ return mBattleModeInstance.getTowerList(); }
	public int getTypeTowerCount(TOWER_TYPE type)						{ return mBattleModeInstance.getTypeTowerCount(type); }
	public float getWaveIntensity(int monsterID)						{ return mBattleModeInstance.getWaveIntensity(getLevelID(), getWaveIndex(), monsterID);  }
	public int getMonsterTypeBuffCount(Type state)						{ return mBattleModeInstance.getMonsterTypeBuffCount(state); }
	public void getMonsterWithTypeBuffInRange(Vector3 pos, float range, Type state, List<CharacterMonster> resultList) { mBattleModeInstance.getMonsterWithTypeBuffInRange(pos, range, state, resultList); }
	public void getInvisibleMonsterInRange(Vector3 pos, float range, List<CharacterMonster> resultList) { mBattleModeInstance.getInvisibleMonsterInRange(pos, range, resultList); }
	public int getWaveIndex()											{ return mBattleModeInstance.getWaveIndex(); }
	public int getCurExp()												{ return mBattleModeInstance.getCurExp(); }
	public EDLevel getLevelData()										{ return mBattleModeInstance.getLevelData(); }
	public int getLevelNeedExp()										{ return mBattleModeInstance.getLevelNeedExp(); }
	public int getLevelID()												{ return mBattleModeInstance.getLevelID(); }
	public bool isLevelValid()											{ return mBattleModeInstance.isLevelValid(); }
	public string getLevelName()										{ return mBattleModeInstance.getLevelName(); }
	public int getLevelMusic()											{ return mBattleModeInstance.getLevelMusic(); }
	public int getLevelUsePower()										{ return mBattleModeInstance.getLevelUsePower(); }
	public EDWaveConfig getWaveData()									
	{
		if (getLevelID() > 0)
		{
			return mExcelWaveConfig.getWaveConfig(getLevelID(), getWaveIndex());
		}
		else
		{
			return mExcelWaveConfig.query(2101001);
		}
	}
	public int getMapTheme()											{ return mBattleModeInstance.getMapTheme(); }
	public float getCameraBattlePos()									{ return mBattleModeInstance.getCameraBattlePos(); }
	public Vector3 getGridRootPos()										{ return mBattleModeInstance.getGridRootPos(); }
	public string getMapSceneName()										{ return mBattleModeInstance.getMapSceneName(); }
	public GRID_TYPE getGridType()										{ return mBattleModeInstance.getGridType(); }
	public int getLevelWidth()											{ return mBattleModeInstance.getMapWidth(); }
	public int getLevelHeight()											{ return mBattleModeInstance.getMapHeight(); }
	public List<MonsterRoad> getMonsterRoadList()						{ return mBattleModeInstance.getMonsterRoadList(); }
	public List<int> getMonsterWalkRoadPoint(int roadIndex)				{ return mBattleModeInstance.getMonsterWalkRoadPoint(roadIndex); }
	public List<int> getMonsterFlyRoadPoint(int roadIndex)				{ return mBattleModeInstance.getMonsterFlyRoadPoint(roadIndex); }
	public bool hasMonsterAtGrid(int gridIndex)							{ return mBattleModeInstance.hasMonsterAtGrid(gridIndex); }
	public int getHp()													{ return mBattleModeInstance.getHp(); }
	public int getTargetPointIndex()									{ return mBattleModeInstance.getTargetPointIndex(); }
	public int getStartPointIndex(int roadIndex)						{ return mBattleModeInstance.getStartPointIndex(roadIndex); }
	public CharacterTower getSelectedTowerScene()						{ return mBattleModeInstance.getSelectedTowerScene(); }
	public bool isContinuityWin()										{ return mBattleModeInstance.isContinuityWin(); }
	public bool isContinuityLose()										{ return mBattleModeInstance.isContinuityLose(); }
	public MonsterGenerator getMonsterGenerator()						{ return mBattleModeInstance.getMonsterGenerator(); }
	public Type getSetupTowerProcedure()								{ return mBattleModeInstance.getSetupTowerProcedure(); }
	public float getBuildingCD()										{ return mBattleModeInstance.getBuildingCD(); }
	public bool isBuildingCDing()										{ return mBattleModeInstance.getBuildingCD() > 0.0f; }
	public CharacterGame getGlobalCharacter()							{ return mBattleModeInstance.getGlobalCharacter(); }
	public CharacterTower getHighestAttackTower()													{ return mBattleModeInstance.getHighestAttackTower(); }
	public CharacterMonster getNearestWalkMonsterInRange(Vector3 pos, float range)					{ return mBattleModeInstance.getNearestWalkMonsterInRange(pos, range); }
	public CharacterMonster getNearestFlyMonsterInRange(Vector3 pos, float range)					{ return mBattleModeInstance.getNearestFlyMonsterInRange(pos, range); }
	public CharacterMonster getNearestMonsterInRange(Vector3 pos, float maxRange)					{ return mBattleModeInstance.getNearestMonsterInRange(pos, maxRange);  }
	public CharacterMonster getNearestWalkMonsterInRange(Vector3 pos, float min, float max)			{ return mBattleModeInstance.getNearestWalkMonsterInRange(pos, min, max); }
	public CharacterMonster getNearestFlyMonsterInRange(Vector3 pos, float min, float max)			{ return mBattleModeInstance.getNearestFlyMonsterInRange(pos, min, max); }
	public CharacterMonster getNearestMonsterInRange(Vector3 pos, float min, float max)				{ return mBattleModeInstance.getNearestMonsterInRange(pos, min, max);  }
	public void getWalkMonstersInRange(Vector3 pos, float range, List<CharacterMonster> resultList) { mBattleModeInstance.getWalkMonstersInRange(pos, range, resultList); }
	public void getFlyMonstersInRange(Vector3 pos, float range, List<CharacterMonster> resultList)	{ mBattleModeInstance.getFlyMonstersInRange(pos, range, resultList); }
	public void getMonstersInRange(Vector3 pos, float range, List<CharacterMonster> resultList)		{ mBattleModeInstance.getMonstersInRange(pos, range, resultList); }
	public void getTowersInRange(Vector3 pos, float range, List<CharacterTower> resultList)			{ mBattleModeInstance.getTowersInRange(pos, range, resultList); }
	public void generateWaveMonster()																{ mBattleModeInstance.generateWaveMonster(); }
}
