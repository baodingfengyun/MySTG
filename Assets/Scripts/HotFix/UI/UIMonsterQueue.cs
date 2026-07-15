using Obfuz;
using UnityEngine;
using static FrameBaseHotFix;
using static GBR;

// auto generate member start
// generate from:Assets/GameResources/UI/UIPrefab/UIMonsterQueue.prefab
// 出场怪物信息
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