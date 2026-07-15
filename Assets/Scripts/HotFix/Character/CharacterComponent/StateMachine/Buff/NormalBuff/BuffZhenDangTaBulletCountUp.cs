using static StringUtility;

// 参数
public class BuffZhenDangTaBulletCountUpParam : CharacterBuffParamT<BuffZhenDangTaBulletCountUpParam>
{
	public int mIncreaseCount;         // 增加的数量
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseCount = param.SToI(); });
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
	}
	protected override void copyInternal(BuffZhenDangTaBulletCountUpParam other)
	{
		mIncreaseCount = other.mIncreaseCount;
	}
}

// 震荡塔子弹个数增加
public class BuffZhenDangTaBulletCountUp : CharacterBuffT<BuffZhenDangTaBulletCountUpParam>
{
	protected int mIncreaseCount;		// 增加的数量
	public override void enter()
	{
		base.enter();
		mIncreaseCount = mCustomParam.mIncreaseCount;
		if (mCharacterGame is CharacterTower tower &&
			tower.getTowerType() == TOWER_TYPE.ZHEN_DANG_TA)
		{
			var skill = tower.getComSkill().getCurSkill() as TowerSkill_ZhenDang;
			skill.setMaxBulletCount(skill.getMaxBulletCount() + mIncreaseCount);
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		if (mCharacterGame is CharacterTower tower &&
			tower.getTowerType() == TOWER_TYPE.ZHEN_DANG_TA)
		{
			var skill = tower.getComSkill().getCurSkill() as TowerSkill_ZhenDang;
			skill.setMaxBulletCount(skill.getMaxBulletCount() - mIncreaseCount);
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
	}
}