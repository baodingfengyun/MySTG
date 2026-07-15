using System;
using System.Collections.Generic;
using static FrameUtility;
using static GBR;

// 管理怪物的技能
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