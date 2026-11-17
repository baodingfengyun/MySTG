using static GBR;

// 塔天赋表：Rogue 三选一里的"塔强化"词条，本质是挂一个 Buff 到指定类型的塔上。
// 校验：mBuff 指向的 buff 细节存在、塔类型枚举合法、互斥天赋 mMutexTalent 存在（同一天赋不可重复叠）。
public class ExcelTowerTalent : ExcelTableT<EDTowerTalent>
{
    // auto generate start
	protected override void checkAllDataDefault()
	{
		foreach (EDTowerTalent item in queryAll())
		{
			mExcelBuffDetail.checkData(item.mBuff, item.mID, this);
			checkEnum(item.mTowerType, "mTowerType", item.mID);
			mExcelTowerTalent.checkData(item.mMutexTalent, item.mID, this);
		}
	}
    // auto generate end
}