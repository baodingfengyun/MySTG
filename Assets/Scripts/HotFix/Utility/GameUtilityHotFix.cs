using System;
using System.Collections.Generic;
using UnityEngine;
using static MathUtility;
using static FrameBaseHotFix;
using static FrameUtility;
using static UnityUtility;
using static StringUtility;
using static GDR;
using static GBR;

// 热更中使用的工具函数类
public class GameUtilityHotFix
{
	private static Vector2Int[] mHexDiagonalVector2Int = new Vector2Int[6];
	public static int generateDamage(CharacterGame target, CharacterGame attacker, SkillBullet bullet, out bool isHit, out bool isCritical, out HP_DELTA deltaType)
	{
		CharacterGameData attackerData = attacker.getGameData();
		CharacterGameData targetData = target.getGameData();
		isHit = true;
		deltaType = HP_DELTA.NORMAL_DAMAGE;
		// 是否暴击
		isCritical = attackerData.mAlwaysCriticalHit > 0 || randomHit(attackerData.getCritical() - targetData.mAntiCritical);
		// 判断是否闪避
		if (randomHit(targetData.mEvasion))
		{
			isHit = false;
			return 0;
		}

		int bulletAttack = bullet.getBulletData().mAttack;
		float bulletAttackPercent = bullet.getAttackPercent();
		float skillDamage = attacker.getAttack() * bulletAttackPercent + bulletAttack;
		float finalDamage = 0.0f;
		// 计算对怪物伤害增幅
		if(attacker is CharacterTower tower && target is CharacterMonster monster)
		{
			MONSTER_STRENGTH strength = monster.getMonsterData().mTableData.mStrength;
			float strengthPercent = 0.0f;
			if(strength == MONSTER_STRENGTH.ELITE)
			{
				strengthPercent = tower.getTowerData().getEliteMonsterDamageIncrease();
			}
			else if(strength == MONSTER_STRENGTH.BOSS)
			{
				strengthPercent = tower.getTowerData().getBossMonsterDamageIncrease();
			}
			skillDamage *= 1.0f + strengthPercent;
		}
		// 计算属性伤害增幅
		DAMAGE_ELEMENT damageElement = bullet.getBulletData().mElementType;
		// 无属性伤害
		if (damageElement == DAMAGE_ELEMENT.NONE)
		{
			if(target.getGameData().mImmunityPhysicDamage > 0)
			{
				// 免疫物理伤害
				return 0;
			}
			finalDamage = skillDamage * (1.0f - targetData.mDefence.divide(50 + targetData.mDefence));
		}
		// 属性伤害
		else
		{
			if (damageElement == DAMAGE_ELEMENT.FIRE)
			{
				finalDamage = skillDamage * (1.0f - targetData.mAntiFireElement);
				finalDamage *= 1.0f + targetData.mBeenFireElementDamageIncrease;
				deltaType = HP_DELTA.DEBUFF;
			}
			else if (damageElement == DAMAGE_ELEMENT.ICE)
			{
				finalDamage = skillDamage * (1.0f - targetData.mAntiIceElement);
				finalDamage *= 1.0f + targetData.mBeenIceElementDamageIncrease;
				deltaType = HP_DELTA.DEBUFF;
			}
			else if (damageElement == DAMAGE_ELEMENT.DARK)
			{
				finalDamage = skillDamage * (1.0f - targetData.mAntiDarkElement);
				finalDamage *= 1.0f + targetData.mBeenDarkElementDamageIncrease;
			}
			else if (damageElement == DAMAGE_ELEMENT.LIGHT)
			{
				finalDamage = skillDamage * (1.0f - targetData.mAntiLightElement);
				finalDamage *= 1.0f + targetData.mBeenLightElementDamageIncrease;
			}
			else if (damageElement == DAMAGE_ELEMENT.POISION)
			{
				finalDamage = skillDamage * (1.0f - targetData.mAntiPoisonElement);
				finalDamage *= 1.0f + targetData.mBeenPoisonElementDamageIncrease;
				deltaType = HP_DELTA.DEBUFF;
			}
			else if (damageElement == DAMAGE_ELEMENT.LIGHTNING)
			{
				finalDamage = skillDamage * (1.0f - targetData.mAntiLightningElement);
				finalDamage *= 1.0f + targetData.mBeenLightningElementDamageIncrease;
				deltaType = HP_DELTA.DEBUFF;
			}
		}
		// 伤害增幅
		finalDamage *= 1.0f + targetData.mBeenDamageIncrease;
		// 暴击
		if (isCritical)
		{
			finalDamage *= 1.0f + attackerData.getCriticalDamage();
		}
		finalDamage *= 1.0f + attackerData.mDamageIncrease;
		// 最终伤害上下浮动5%
		finalDamage *= 1.0f + (randomFloat(0.0f, 0.1f) - 0.05f);
		return (int)finalDamage.clampMin(1.0f);
	}
	public static void tip(string text, params string[] param)
	{
		using var a = new ListScope<string>(out var list);
		mUITip.showTip(text, list.addRange(param));
	}
	public static void tip(string text)
	{
		mUITip.showTip(text, null);
	}
	public static void tip(string text, List<string> param)
	{
		mUITip.showTip(text, param);
	}
	public static void dialogTip()
	{
		LT.HIDE<UIDialogTip>();
	}
	public static void dialogTip(string info)
	{
		LT.LOAD_TOP<UIDialogTip>(1203);
		mUIDialogTip.setInfo(info);
	}
	public static void dialogYesNo(string info, OnDialogOKCallback confirmCallback)
	{
		dialogYesNo(info, EMPTY, EMPTY, null, confirmCallback);
	}
	public static void dialogYesNo(string info, string param0, OnDialogOKCallback confirmCallback)
	{
		dialogYesNo(info, param0, EMPTY, null, confirmCallback);
	}
	public static void dialogYesNo(string info, string param0, string param1, OnDialogOKCallback confirmCallback)
	{
		dialogYesNo(info, param0, param1, null, confirmCallback);
	}
	public static void dialogYesNo(string info, OnDialogYesNoCallback callback)
	{
		dialogYesNo(info, EMPTY, EMPTY, callback, null);
	}
	// 不传任何参数就是关闭对话框
	public static void dialogYesNo()
	{
		mUIDialogYesNo?.setCallback(null);
		mUIDialogYesNo?.setConfirmCallback(null);
		LT.HIDE<UIDialogYesNo>();
	}
	// 显示一个对话框,有确认和取消按钮
	public static void dialogYesNo(string info, string param0, string param1, OnDialogYesNoCallback callback, OnDialogOKCallback confirmCallback)
	{
		if (info != null)
		{
			LT.LOAD_TOP<UIDialogYesNo>(1203);
			mUIDialogYesNo.setInfo(info, param0, param1);
			mUIDialogYesNo.setCallback(callback);
			mUIDialogYesNo.setConfirmCallback(confirmCallback);
		}
		else
		{
			mUIDialogYesNo?.setCallback(callback);
			mUIDialogYesNo?.setConfirmCallback(confirmCallback);
			LT.HIDE<UIDialogYesNo>();
		}
	}
	public static void dialogOK(string info, OnDialogOKCallback callback = null)
	{
		dialogOK(info, EMPTY, EMPTY, callback);
	}
	public static void dialogOK(string info, string param, OnDialogOKCallback callback = null)
	{
		dialogOK(info, param, EMPTY, callback);
	}
	public static void dialogOK(string info, string param0, string param1, OnDialogOKCallback callback = null)
	{
		if (info != null)
		{
			LT.LOAD_TOP<UIDialogOK>(1204);
			mUIDialogOK.setInfo(info, param0, param1);
			mUIDialogOK.setOKCallback(callback);
		}
		else
		{
			mUIDialogOK?.setOKCallback(callback);
			LT.HIDE<UIDialogOK>();
		}
	}
	// 不传任何参数就是关闭对话框
	public static void dialogOK()
	{
		dialogOK(null, null);
	}
	public static CharacterBuff characterAddBuff(int buffDetailID, Character target, Character source, SkillBullet bullet = null, CharacterSkill skill = null, INT damage = null)
	{
		using var a = new BuffParamScope(out CharacterBuffParam buffParam, buffDetailID);
		buffParam.mSource = source;
		buffParam.mBullet = bullet;
		buffParam.mSkill = skill;
		buffParam.mDamage = damage;
		Type stateType = mStateManager.getStateType(buffParam.mBuffData.mID);
		return target.getStateMachine().addState(stateType, buffParam, 0) as CharacterBuff;
	}
	// 场景中的塔是否可以拖拽到指定位置
	public static bool checkCanMoveTowerTo(int roadIndex, int index, int emptyIndex, List<int> walkRoadList)
	{
		BattleModeBase modeInstance = mTowerDefenceSystem.getBattleModeInstance();
		if (index < 0 ||
			!canGridStatePlaceTower(modeInstance.getGridState(index)) ||
			modeInstance.hasPortalAtGrid(index) ||
			!modeInstance.generateRoadPath(roadIndex, index, emptyIndex, walkRoadList))
		{
			return false;
		}
		// 放置塔以后,如果使任意怪物找不到路线到达终点,也不允许放置
		foreach (CharacterMonster monster in modeInstance.getMonsterMainList())
		{
			if (!monster.getComMovement().checkPath(index))
			{
				return false;
			}
		}
		return true;
	}
	// 是否可以在指定位置放一个新的塔
	public static bool checkCanPutTower(int roadIndex, int index, List<int> walkRoadList = null)
	{
		BattleModeBase modeInstance = mTowerDefenceSystem.getBattleModeInstance();
		if (index < 0 ||
			!canGridStatePlaceTower(modeInstance.getGridState(index)) ||
			modeInstance.hasItemAtGrid(index) ||
			!modeInstance.generateRoadPath(roadIndex, index, -1, walkRoadList))
		{
			return false;
		}
		// 放置塔以后,如果使任意怪物找不到路线到达终点,也不允许放置
		foreach (CharacterMonster monster in modeInstance.getMonsterMainList())
		{
			if (!monster.getComMovement().checkPath(index))
			{
				return false;
			}
		}
		return true;
	}
	public static int xyToIndex(int x, int y)
	{
		int levelWidth = mTowerDefenceSystem.getLevelWidth();
		int levelHeight = mTowerDefenceSystem.getLevelHeight();
		if (x < 0 || x >= levelWidth ||
			y < 0 || y >= levelHeight)
		{
			return -1;
		}
		return x + y * levelWidth;
	}
	public static int indexToX(int index)
	{
		return index % mTowerDefenceSystem.getLevelWidth();
	}
	public static int indexToY(int index)
	{
		return index / mTowerDefenceSystem.getLevelWidth();
	}
	// 获取六边形每条边对应的斜线上的格子
	public static int[] getHexDiagonalGird(int gridIndex, int range)
	{
		int hexCount = HEX_AROUND_GRID0.Length;
		int[] ret = new int[range * hexCount];
		int x = indexToX(gridIndex);
		int y = indexToY(gridIndex);
		int remainder = y & 1;

		for (int i = 0; i < mHexDiagonalVector2Int.Length; ++i)
		{
			mHexDiagonalVector2Int[i].x = 0;
			mHexDiagonalVector2Int[i].y = 0;
		}
		int index = 0;
		for (int r = 0; r < range; ++r)
		{
			for (int i = 0; i < hexCount; ++i)
			{
				Vector2Int[] grids = ((r + remainder) & 1) == 0 ? HEX_AROUND_GRID0 : HEX_AROUND_GRID1;
				mHexDiagonalVector2Int[i] += grids[i];
				ret[index++] = xyToIndex(x + mHexDiagonalVector2Int[i].x, y + mHexDiagonalVector2Int[i].y);
			}
		}
		return ret;
	}
	// 获取六边形半径内的格子
	public static void getHexAroundGird(int gridIndex, int range, List<int> ret)
	{
		int hexCount = HEX_AROUND_GRID0.Length;
		int x = indexToX(gridIndex);
		int y = indexToY(gridIndex);
		int remainder = y & 1;
		// 清理
		for (int i = 0; i < mHexDiagonalVector2Int.Length; ++i)
		{
			mHexDiagonalVector2Int[i].x = 0;
			mHexDiagonalVector2Int[i].y = 0;
		}
		Vector2Int tempHexEdge = new(0, 0);
		// 添加范围内的格子
		for (int r = 0; r < range; ++r)
		{
			for (int hexIndex = 0; hexIndex < hexCount; ++hexIndex)
			{
				// hexIndex是半径方向，0是六边形的左上角
				Vector2Int[] offset = ((r + remainder) & 1) == 0 ? HEX_AROUND_GRID0 : HEX_AROUND_GRID1;
				mHexDiagonalVector2Int[hexIndex] += offset[hexIndex];
				tempHexEdge.Set(x + mHexDiagonalVector2Int[hexIndex].x, y + mHexDiagonalVector2Int[hexIndex].y);
				ret.Add(xyToIndex(tempHexEdge.x, tempHexEdge.y));
				// 顺时针偏移两个角度格子，也就是半径方向偏移120度对应的那条边
				int cycleHexIndex = hexIndex + 2;
				if (cycleHexIndex >= hexCount)
				{
					cycleHexIndex -= hexCount;
				}
				// 每个边 向顶点到边 的方向，遍历 边长 次
				for (int edgeIndex = 0; edgeIndex < r; ++edgeIndex)
				{
					offset = (tempHexEdge.y & 1) == 0 ? HEX_AROUND_GRID0 : HEX_AROUND_GRID1;
					tempHexEdge += offset[cycleHexIndex];
					ret.Add(xyToIndex(tempHexEdge.x, tempHexEdge.y));
				}
			}
		}
	}
	public static bool isGridStateFlyable(GRID_STATE state)
	{
		return state != GRID_STATE.NONE && state != GRID_STATE.EMPTY;
	}
	public static bool isGridStateWalkable(GRID_STATE state)
	{
		return state == GRID_STATE.WALK_FLY_UNTRAP_UNTOWER || 
			   state == GRID_STATE.WALKABLE || 
			   state == GRID_STATE.WALK_FLY_TRAP_UNTOWER;
	}
	public static bool canGridStatePlaceTower(GRID_STATE state)
	{
		return state == GRID_STATE.WALKABLE || state == GRID_STATE.UNWALK_FLY_UNTRAP_TOWER;
	}
	public static int getLevelStar(int level)
	{
		return getLevelStarFromHp(level, mClientSystem.getCOMLevel().getLevelGreatHp(level));
	}
	public static int getLevelStarFromHp(int level, int hp)
	{
		if (level <= 0)
		{
			logWarning("关卡ID不能为0");
			return 0;
		}
		EDLevel levelData = mExcelLevel.query(level);
		if (hp >= LEVEL_INIT_HP)
		{
			return 3;
		}
		if (hp >= levelData.mStar2)
		{
			return 2;
		}
		if (hp >= levelData.mStar1)
		{
			return 1;
		}
		return 0;
	}
	public static Vector3 generateOffset(Vector3 pos)
	{
		float minOffset = 0.5f;
		float maxOffset = 1.2f;
		// 获取最底部中间的格子坐标为基准位置,根据当前位置到基准位置计算偏移量
		int mapWidth = mTowerDefenceSystem.getLevelWidth();
		int mapHeight = mTowerDefenceSystem.getLevelHeight();
		Vector3 originPos = mBattleScene.getGridPosition(intPosToIndex(mapWidth >> 1, mapHeight - 1, mapWidth));
		float dis = (pos - originPos).getLength();
		float maxDis = (mapHeight >> 1) * GRID_SIZE;
		Vector3 delta = pos - originPos;
		delta.z = delta.z.clampMin();
		return pos + lerp(minOffset, maxOffset, dis.divide(maxDis)) * delta.normalize();
	}
	public static void waveFinish(bool waveWin)
	{
		mEventSystem.pushEvent<EventWaveFinish>();
		mTowerDefenceSystem.clearWave();
		// 已经通关或者波次失败了,就进入结算流程
		if (!waveWin || mTowerDefenceSystem.isEnded())
		{
			changeProcedure<GameSceneBattleGamingLevelFinish>();
		}
		// 未通关就进入下一次放塔
		else
		{
			changeProcedure(mTowerDefenceSystem.getSetupTowerProcedure());
		}
	}
	public static void exitToLobbyOrMapEditor()
	{
		GameSceneLobbyLoading.mNextProcedureType = typeof(GameSceneLobbySelectLevel);
		if(mTowerDefenceSystem.getLevelData()?.mEndless ?? false)
		{
			GameSceneLobbySelectLevel.mBackToEndless = mTowerDefenceSystem.getLevelData().mID;
		}
		enterScene<GameSceneLobby>();
	}
}