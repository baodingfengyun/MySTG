using System.Collections.Generic;
using static GBR;

// 描述文本注册器基类:维护"数值ID → 生成描述文本的回调"字典,子类在 registeAllInternal 中登记。
// 提供 talentData/bulletData 便捷方法查询天赋表与子弹表,供描述回调拼文案时取数据。
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