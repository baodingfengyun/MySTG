using static GBR;

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