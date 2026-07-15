using static FrameUtility;
using static StringUtility;

// 参数
public class BuffChangeSearchTargetTypeParam : CharacterBuffParamT<BuffChangeSearchTargetTypeParam>
{
	public SEARCH_TARGET mSearchType;			// 寻敌方式
	public bool mForceNewTarget;				// 改为强制选择新目标
	public override void registeAllParam()
	{
		registeParam((param) => { mSearchType = (SEARCH_TARGET)param.SToI(); });
		registeParam((param) => { mForceNewTarget = param.SToI() != 0; });
	}
	protected override void copyInternal(BuffChangeSearchTargetTypeParam other)
	{
		mSearchType = other.mSearchType;
		mForceNewTarget = other.mForceNewTarget;
	}
	public override void check()
	{
		checkEnum(mSearchType);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mSearchType = SEARCH_TARGET.NONE;
		mForceNewTarget = false;
	}
}

// 修改寻敌方式
public class BuffChangeSearchTargetType : CharacterBuffT<BuffChangeSearchTargetTypeParam>
{
	public SEARCH_TARGET mSearchType;			// 寻敌方式
	public bool mForceNewTarget;				// 改为强制选择新目标
	public override void resetProperty()
	{
		base.resetProperty();
		mSearchType = 0;
		mForceNewTarget = false;
	}
	public override void enter()
	{
		base.enter();
		mSearchType = mCustomParam.mSearchType;
		mForceNewTarget = mCustomParam.mForceNewTarget;
		if (mCharacterGame is CharacterTower tower)
		{
			TowerSkill skill = tower.getComSkill().getCurSkill();
			skill.addSearchTarget(mSearchType);
			if(mForceNewTarget)
			{
				skill.setForceNewTarget(skill.getForceNewTarget() + 1);
			}
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		if (mCharacterGame is CharacterTower tower)
		{
			TowerSkill skill = tower.getComSkill().getCurSkill();
			skill.removeSearchTarget(mSearchType);
			if (mForceNewTarget)
			{
				skill.setForceNewTarget(skill.getForceNewTarget() - 1);
			}
		}
	}
}