using Obfuz;
using UnityEngine;
using static FrameBaseHotFix;
using static GBR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIMonsterQueue.prefab
// ===== 出场怪物队列 UI：屏幕上方显示"这一波有哪些怪"(普通怪列表 + Boss 列表) =====
// 【两个对象池】MonsterQueueItem(普通怪，显示图标+数量) 和 MonsterBossQueueItem(Boss，带血条)。
// 【数据来源 refresh】读 TowerDefenceSystem.getMonsterDisplay()——一组 (怪物ID, 数量) 对，
//   查 Monster 表按 mStrength 分流：BOSS 进 Boss 池、其余进普通池，再 autoGridHorizontal 排布。
// 【事件驱动】监听 EventSpawnMonster：Boss 出场时把队列项绑定真实怪物实体(trySetCharacter，
//   之后血条靠 updateHpBar 按 GUID 更新)；监听 EventMonsterDestroy：Boss 死了回收对应项。
// 【注】普通怪的"数量"是波次预览，Boss 项才跟踪实体血量——所以只有 Boss 池需要事件联动。
[ObfuzIgnore(ObfuzScope.TypeName)]
public class UIMonsterQueue : LayoutScript
{
	protected WindowStructPool<MonsterQueueItem> mMonsterQueueItemPool;
	protected WindowStructPool<MonsterBossQueueItem> mMonsterBossQueueItemPool;
    // auto generate member end
	public UIMonsterQueue()
	{
		// auto generate constructor start
		mMonsterQueueItemPool = new(this);
		mMonsterBossQueueItemPool = new(this);
		// auto generate constructor end
	}
	public override void assignWindow()
	{
		// auto generate assignWindow start
		newObject(out myUGUIObject monsterListRoot, "MonsterListRoot", false);
		newObject(out myUGUIObject monsterRoot, monsterListRoot, "MonsterRoot", false);
		mMonsterQueueItemPool.assignTemplate(monsterRoot, "MonsterQueueItem");
		newObject(out myUGUIObject bossRoot, monsterListRoot, "BossRoot", false);
		mMonsterBossQueueItemPool.assignTemplate(bossRoot, "MonsterBossQueueItem");
		// auto generate assignWindow end
	}
	public override void init()
	{
		base.init();
		// auto generate init start
		// auto generate init end
		mEventSystem.listenEvent<EventSpawnMonster>(onRefreshBossList, this);
		mEventSystem.listenEvent<EventMonsterDestroy>(onDestroyMonster, this);
	}
	public override void onGameState()
	{
		base.onGameState();
        mMonsterQueueItemPool.unuseAll();
		mMonsterBossQueueItemPool.unuseAll();
	}
	public void updateHpBar(long guid, float hpBar)
	{
		foreach(var each in mMonsterBossQueueItemPool.getUsedList().safe())
		{
			if(each.getMonsterGUID() == guid)
			{
				each.updateHpBar(hpBar);
				break;
			}
		}
	}
	public void refresh()
	{
		mMonsterQueueItemPool.unuseAll();
		mMonsterBossQueueItemPool.unuseAll();
		foreach (Vector2Int item in mTowerDefenceSystem.getMonsterDisplay())
		{
			EDMonster monster = mExcelMonster.query(item.x);
			if(monster.mStrength == MONSTER_STRENGTH.BOSS)
			{
				mMonsterBossQueueItemPool.newItem().setData(monster);
			}
			else
			{
				mMonsterQueueItemPool.newItem().setIcon(monster, item.y);
			}
		}
        mMonsterQueueItemPool.autoGridHorizontal(true);
        mMonsterBossQueueItemPool.autoGridHorizontal();
	}
	public void setActiveOnlyFirstMonster(out Vector3 pos)
	{
		MonsterQueueItem item = mMonsterQueueItemPool.getUsedList().get(0);
		if (item == null)
		{
			pos = Vector3.zero;
			return;
		}
		pos = item.getRoot().getWorldPosition();
		mGlobalTouchSystem.setActiveOnlyObject(item.getRoot());
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onRefreshBossList(EventSpawnMonster eventParam)
	{
		CharacterMonster monster = eventParam.mMonster;
		foreach(var each in mMonsterBossQueueItemPool.getUsedList().safe())
		{
			if(each.trySetCharacter(monster))
			{
				break;
			}
		}
        mMonsterBossQueueItemPool.autoGridHorizontal();
	}
	protected void onDestroyMonster(EventMonsterDestroy eventParam)
	{
		CharacterMonster monster = eventParam.mMonster;
		if (monster.getMonsterData().mTableData.mStrength == MONSTER_STRENGTH.BOSS)
		{
			MonsterBossQueueItem find = null;
			foreach (MonsterBossQueueItem each in mMonsterBossQueueItemPool.getUsedList().safe())
			{
				if (each.getMonsterGUID() == monster.getGUID())
				{
					find = each;
					break;
				}
			}
			if(find != null)
			{
				mMonsterBossQueueItemPool.unuseItem(find);
			}
            mMonsterBossQueueItemPool.autoGridHorizontal();
		}
	}
}