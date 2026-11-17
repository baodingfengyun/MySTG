using System.Collections.Generic;
using static GBR;

// Buff 参数对象：创建/挂载一个 Buff 时随附的"上下文"，走对象池复用
// 【关键字段分三类】
//   ① 表格数据：mBuffData(Buff 类型表)、mBuffDetailData(Buff 细分表)——数值来源；
//   ② 来源信息：mBullet(哪发子弹挂的)、mSkill(哪个技能挂的)、mBuffTrigger(哪个触发器挂的)——
//      让 Buff 能追溯来源，比如"攻击力提升 X% 来源于某塔"；
//   ③ 可改写的伤害：mDamage——命中链里 Buff 可在此改伤害值(扣血前)。
// 【mTriggerAssignID】触发器的实例 ID，配合回调校验，防止旧触发器误重置新触发器的 CD。
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