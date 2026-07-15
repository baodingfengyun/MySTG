using static GBR;

// 卡池表
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