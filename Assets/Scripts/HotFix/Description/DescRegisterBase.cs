using System.Collections.Generic;
using static GBR;

public abstract class DescRegisterBase
{
	protected Dictionary<int, ItemDescRegisteCallback> mRegisteCallbackList = new(1024);
	//------------------------------------------------------------------------------------------------------------------------------
	protected abstract void registeAllInternal();
	protected void registeDescriptionCallback(int id, ItemDescRegisteCallback callback)
	{
		mRegisteCallbackList.Add(id, callback);
	}
	protected static EDTowerTalent talentData(int id) { return mExcelTowerTalent.query(id); }
	protected static EDSkillBullet bulletData(int id) { return mExcelSkillBullet.query(id); }
}