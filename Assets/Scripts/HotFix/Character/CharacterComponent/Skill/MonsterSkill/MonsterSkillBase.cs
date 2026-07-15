
// 怪物技能基类
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