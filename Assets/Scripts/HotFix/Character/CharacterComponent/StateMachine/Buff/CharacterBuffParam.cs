using System.Collections.Generic;
using static GBR;

// buff参数对象
public abstract class CharacterBuffParam : StateParam
{
	public CharacterTrigger mBuffTrigger;           // 如果是由触发类buff添加的此buff,则表示触发类的buff
	public AddBuffCallback mCallback;               // 添加buff时的回调
	public EDBuffDetail mBuffDetailData;            // buff细分表中的表格数据
	public EDBuff mBuffData;                        // buff类型表中的表格数据
	public SkillBullet mBullet;                     // 由子弹附加的buff会记录子弹对象
	public CharacterSkill mSkill;                   // 由技能释放或者技能子弹附加的buff会记录技能对象
	public INT mDamage;                             // 伤害值,可以对伤害进行修改
	public long mTriggerAssignID;                   // 触发类buff的唯一分配ID,用于校验mBuffTrigger是否有效
	// 放回对象池时重置
	public override void resetProperty()
	{
		base.resetProperty();
		mBuffDetailData = null;
		mCallback = null;
		mBuffTrigger = null;
		mBuffData = null;
		mBullet = null;
		mSkill = null;
		mDamage = null;
		mTriggerAssignID = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void checkDataRefByBuffDetail(ExcelTable table, int id)
	{
		table.checkData(id, mBuffDetailData.mID, mExcelBuffDetail);
	}
	protected void checkDataRefByBuffDetail(ExcelTable table, List<int> idList)
	{
		table.checkData(idList, mBuffDetailData.mID, mExcelBuffDetail);
	}
}