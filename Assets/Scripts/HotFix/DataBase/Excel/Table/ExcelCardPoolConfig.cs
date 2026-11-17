using static GBR;

// 卡池表：Rogue 三选一抽卡的候选池，每行是一个池，内含"普通项"和"必选项"两组。
// 每项用 (ID, 类型) 表示一个道具，类型决定去哪张表校验 ID 合法性：
//   TOWER_TALENT -> 塔天赋表，TOWER -> 防御塔表，BATTLE_PROP -> 战斗道具（暂无校验）。
// 本类没有查询缓存，核心职责是 checkAllData 的引用完整性校验：
// checkListPair 保证每个并行 List 等长（ID/类型/权重一一对应），再按类型逐级查表。
public class ExcelCardPoolConfig : ExcelTableT<EDCardPoolConfig>
{
    public override void checkAllData()
    {
        foreach (EDCardPoolConfig item in queryAll())
        {
            checkListPair(item.mItemID, item.mItemType, item.mID);
            checkListPair(item.mItemID, item.mItemWeight, item.mID);
            checkListPair(item.mMustItemID, item.mMustItemType, item.mID);
            int count = item.mItemID.Count;
            for (int i = 0; i < count; ++i)
            {
                if (item.mItemType[i] == BATTLE_ITEM_TYPE.TOWER_TALENT)
                {
                    mExcelTowerTalent.checkData(item.mItemID[i], item.mID, this);
                }
                else if (item.mItemType[i] == BATTLE_ITEM_TYPE.BATTLE_PROP)
                {
                    ;
                }
                else if (item.mItemType[i] == BATTLE_ITEM_TYPE.TOWER)
                {
                    mExcelTower.checkData(item.mItemID[i], item.mID, this);
                }
            }
            int mustCount = item.mMustItemID.Count;
            for (int i = 0; i < mustCount; ++i)
            {
                if (item.mMustItemType[i] == BATTLE_ITEM_TYPE.TOWER_TALENT)
                {
                    mExcelTowerTalent.checkData(item.mMustItemID[i], item.mID, this);
                }
                else if (item.mMustItemType[i] == BATTLE_ITEM_TYPE.TOWER)
                {
                    mExcelTower.checkData(item.mMustItemID[i], item.mID, this);
                }
            }
        }
    }
	// auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDCardPoolConfig item in queryAll())
		{
			checkEnum(item.mItemType, "mItemType", item.mID);
			checkEnum(item.mMustItemType, "mMustItemType", item.mID);
		}
	}
	// auto generate end
}