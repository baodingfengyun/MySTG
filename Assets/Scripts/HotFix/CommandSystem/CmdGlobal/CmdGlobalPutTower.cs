using static FrameBaseHotFix;
using static GBR;
using static GDR;

// 放置塔到场景中的命令基类,提供公共函数
public class CmdGlobalPutTower
{
	// 简单设置两个场景中已经存在的塔的位置,塔可以为空
	public static void swapCharacter(CharacterGame tower0, CharacterGame tower1, int newIndex0, int newIndex1)
	{
		mTowerDefenceSystem.swapCharacterGrid(tower0, tower1, newIndex0, newIndex1);
		tower0?.setGridIndexAndPosition(newIndex0);
		tower1?.setGridIndexAndPosition(newIndex1);
		mEventSystem.pushEvent<EventGridTowerChange>();
	}
	//------------------------------------------------------------------------------------------------------------------------------
	// 将一个新的塔放置到指定格子,并且添加到塔列表中,显示放置特效
	protected static void putTower(CharacterTower tower, int gridIndex)
	{
		tower.setGridIndexAndPosition(gridIndex);
		mTowerDefenceSystem.addTower(tower);
		mTowerDefenceSystem.setCharacterGridIndex(tower, gridIndex);
		// 放置塔以后就需要激活战斗
		tower.notifyStartFight();

		// 创建特效显示
		mEffectManager.playEffectAsync(mExcelEffect.query(TOWER_PLACE_EFFECT_ID).mPath, tower, 2.6f, true, 0);
		AT.SOUND_2D(SOUND_HOTFIX.BUILD_TOWER);

		// 广播事件
		using var a = new ClassScope<EventTowerPut>(out var param);
		param.mTower = tower;
		mEventSystem.pushEvent(param, tower.getGUID());

		mEventSystem.pushEvent<EventGridTowerChange>();
	}
	protected static void postPutTower(int gridIndex)
	{
		mTowerDefenceSystem.generateRoadPathAndRefresh();
		mBattleScene.showAllPath();
		// 由于现在怪物路径是固定的,所以可以判断防御塔有没有放在路径上,如果没有,则不需要重新计算路径
		bool needRegeneratePath = false;
		foreach (MonsterRoad road in mTowerDefenceSystem.getMonsterRoadList())
		{
			if (road.mMonsterWalkRoadPoint.Contains(gridIndex))
			{
				needRegeneratePath = true;
				break;
			}
		}
		// 可能还有一些怪物由于被击退或者其他原因有自己独立的路线,也要判断一下是否在这些怪的路线上
		if (!needRegeneratePath)
		{
			foreach (CharacterMonster monster in mTowerDefenceSystem.getMonsterMainList())
			{
				if (monster.getComMovement().getRoadPointList().Contains(gridIndex))
				{
					needRegeneratePath = true;
					break;
				}
			}
		}
		if (needRegeneratePath)
		{
			// 只刷新受到影响的怪物的路线
			foreach (CharacterMonster monster in mTowerDefenceSystem.getMonsterMainList())
			{
				if (monster.getComMovement().getRoadPointList().Contains(gridIndex))
				{
					monster.getComMovement().regenerateRoadList();
				}
			}
		}
	}
}