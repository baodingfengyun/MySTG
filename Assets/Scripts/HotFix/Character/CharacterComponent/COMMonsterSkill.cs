using System;
using System.Collections.Generic;
using static FrameUtility;
using static GBR;

// 怪物的技能容器：按技能ID从 ExcelMonsterSkill 表 + MonsterSkillRegister 反射工厂造出技能实例。
// 维护两份列表——mSkillList(全部：主动+被动)、mActiveSkillList(仅主动，需每帧 update 与按状态开关)。
// 关键设计：监听怪物状态机的 setStateChangedCallback，状态一变就重算"是否允许攻击"
// (是否处于 StateGroupNotAllowAttack，如眩晕)，批量开关所有主动技能，而非每个技能各自判断。
// 技能销毁在 destroy 里显式逐个调用（先于 Avatar 销毁），保证技能挂在角色上的特效先清理。
public class COMMonsterSkill : GameComponent
{
	protected List<MonsterSkillBase> mSkillList = new();		// 技能列表,包含mActiveSkillList
	protected List<MonsterActiveSkill> mActiveSkillList = new();		// 主动技能的列表
	protected CharacterMonster mMonster;						// 所属的怪物
	protected bool mStateChanged;								// 状态是否有改变
	public override void init(ComponentOwner owner)
	{
		base.init(owner);
		mMonster = mComponentOwner as CharacterMonster;
		mMonster.getStateMachine().setStateChangedCallback(() =>
		{
			mStateChanged = true;
		});
	}
	public override void destroy()
	{
		foreach (MonsterSkillBase skill in mSkillList)
		{
			skill.destroy();
		}
		UN_CLASS_LIST(mSkillList);
		base.destroy();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mSkillList.Clear();
		mActiveSkillList.Clear();
		mMonster = null;
		mStateChanged = false;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mStateChanged)
		{
			mStateChanged = false;
			bool allowSkill = !mMonster.getStateMachine().hasStateGroup<StateGroupNotAllowAttack>();
			foreach (MonsterActiveSkill skill in mActiveSkillList)
			{
				skill.setActive(allowSkill);
			}
		}
		foreach (MonsterActiveSkill skill in mActiveSkillList)
		{
			skill.update(elapsedTime);
		}
	}
	// 反射工厂：先查表拿配置，再问 MonsterSkillRegister 该ID是否有特例类(如自爆)，
	// 没有则按 mIsPassive 选 MonsterPassiveSkill(被动)或 MonsterActiveSkill(主动)。
	// 造出的实例 setCharacter 绑定怪物、initData 读表；主动技能额外进 mActiveSkillList 接受每帧更新。
	public MonsterSkillBase addSkill(int skillID)
	{
		EDMonsterSkill skillData = mExcelMonsterSkill.query(skillID);
		Type classType = MonsterSkillRegister.getMonsterType(skillID);
		classType ??= skillData.mIsPassive ? typeof(MonsterPassiveSkill) : typeof(MonsterActiveSkill);
		var skill = CLASS(classType) as MonsterSkillBase;
		skill.setCharacter(mMonster);
		skill.initData(skillData);
		mActiveSkillList.addNotNull(skill as MonsterActiveSkill);
		return mSkillList.add(skill);
	}
	public void firePassiveSkill()
	{
		foreach (MonsterSkillBase skill in mSkillList)
		{
			if (skill is MonsterPassiveSkill passiveSkill)
			{
				passiveSkill.fire();
			}
		}
	}
}