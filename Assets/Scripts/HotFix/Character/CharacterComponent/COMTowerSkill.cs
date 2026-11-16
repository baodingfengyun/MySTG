using System.Collections.Generic;
using static FrameUtility;
using static GBR;

// 管理塔的技能：通用攻击节奏（全链路 ④战斗 的"起搏器"）
// 职责边界很清晰：本组件只回答"什么时候尝试攻击"，不关心打谁、怎么打。
// 每帧 update 做两件事：
//   1. 驱动每个 TowerSkill 自己的 update（主要是转向目标、朝向动画）
//   2. 判断四个开火前提：场上有怪 + 有当前技能 + 技能冷却完毕 + 塔不在"禁止攻击"状态组
//      满足就调 mCurSkill.fire()，索敌与发射的细节交给 TowerSkill。
// mComSkill.setActive 由 CharacterTower.notifyStartFight 控制：布阵阶段关闭，开战才激活。
public class COMTowerSkill : GameComponent
{
	protected List<TowerSkill> mSkillList = new();  // 技能列表,一般只有一个技能
	protected TowerSkill mCurSkill;					// 当前使用的技能
	protected CharacterTower mTower;                // 所属的塔
	public override void init(ComponentOwner owner)
	{
		base.init(owner);
		mTower = mComponentOwner as CharacterTower;
	}
	public override void destroy()
	{
		base.destroy();
		clearSkills();
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mSkillList.Clear();
		mCurSkill = null;
		mTower = null;
	}
	// COMTowerSkill只决定什么时候尝试攻击（"打不打"）；"打谁/打几发/什么形态"全在 TowerSkill.fire()
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		foreach (TowerSkill skill in mSkillList)
		{
			skill.update(elapsedTime);
		}

		// 战场上存在怪物，当前技能有效，技能冷却完成，塔没有“不允许攻击”状态
		if (mTowerDefenceSystem.getMonsterMainList().Count > 0 && 
			mCurSkill != null && 
			mCurSkill.isCoolDown() &&
			!mTower.hasStateGroup<StateGroupNotAllowAttack>())
		{
			mCurSkill.fire();
		}
	}
	public TowerSkill getCurSkill() { return mCurSkill; }
	public int getCurSkillID() { return mCurSkill?.getSkillData().mID ?? 0; }
	public void removeSkill(int skillID)
	{
		if (mCurSkill != null && mCurSkill.getSkillData().mID == skillID)
		{
			mCurSkill = null;
		}
		int count = mSkillList.Count;
		for (int i = 0; i < count; ++i)
		{
			TowerSkill skill = mSkillList[i];
			if (skill.getSkillData().mID == skillID)
			{
				skill.destroy();
				UN_CLASS(ref skill);
				mSkillList.RemoveAt(i);
				break;
			}
		}
	}
	public TowerSkill addSkill(int skillID)
	{
		EDTowerSkill skillData = mExcelTowerSkill.query(skillID);
		var skill = CLASS(TowerSkillRegister.getSkillType(skillID)) as TowerSkill;
		skill.setCharacter(mTower);
		skill.initData(skillData, TowerSkillRegister.getSkillParam(skillData));
		if (mTower.isModelLoaded())
		{
			skill.onModelLoaded();
		}
		return mSkillList.add(skill);
	}
	public void clearSkills()
	{
		foreach (TowerSkill skill in mSkillList)
		{
			skill.destroy();
		}

		UN_CLASS_LIST(mSkillList);
		mCurSkill = null;
	}
	public void setCurSkill(int skillID)
	{
		mCurSkill = null;
		if (skillID == 0)
		{
			return;
		}
		foreach (TowerSkill skill in mSkillList)
		{
			if (skill.getSkillData().mID == skillID)
			{
				mCurSkill = skill;
				break;
			}
		}
	}
	public void onModelLoaded()
	{
		foreach (TowerSkill skill in mSkillList)
		{
			skill.onModelLoaded();
		}
	}
	public void notifyWaveChanged()
	{
		foreach (TowerSkill skill in mSkillList)
		{
			skill.notifyWaveChanged();
		}
	}
}