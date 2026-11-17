
// 怪物技能基类：主动/被动技能的公共部分——数据表 + 归属怪物 + 释放资格
// 【两条支线】MonsterActiveSkill(主动：血量阈值/冷却触发，会发子弹加 Buff) 和
//   MonsterPassiveSkill(被动：出生触发，给自身挂触发器)。
// 【canFire】默认要求"技能激活中(mActive) 且 蓝量够(怪物 MP ≥ 技能 MP)"；
//   子类可叠加条件(主动技能再加 CD 判断)。
// 【isPassive】由表里 mIsPassive 决定；setCharacter 时把角色转成 CharacterMonster 存起来。
public class MonsterSkillBase : CharacterSkill
{
	protected EDMonsterSkill mSkillData;			// 技能的表格数据
	protected CharacterMonster mMonster;			// 拥有此技能的怪物
	protected bool mActive = true;					// 是否可以释放此技能
	public virtual void initData(EDMonsterSkill skillData)
	{
		mSkillData = skillData;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mSkillData = null;
		mMonster = null;
		mActive = true;
	}
	public void setActive(bool active) { mActive = active; }
	public EDMonsterSkill getSkillData() { return mSkillData; }
	public bool isPassive() { return mSkillData.mIsPassive; }
	public virtual bool canFire() { return mActive && mSkillData.mMP <= mMonster.getMP(); }
	public override void setCharacter(CharacterGame character)
	{
		base.setCharacter(character);
		mMonster = character as CharacterMonster;
	}
}