using static FrameBaseHotFix;

// 参数
public class BuffIncreaseBulletCountPercentParam : CharacterBuffParamT<BuffIncreaseBulletCountPercentParam>
{
	public int mIncreasePercent;
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreasePercent = param.SToI(); });
	}
	protected override void copyInternal(BuffIncreaseBulletCountPercentParam other)
	{
		mIncreasePercent = other.mIncreasePercent;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercent = 0;
	}
}

// 增加子弹数量
public class BuffIncreaseBulletCountPercent : CharacterBuffT<BuffIncreaseBulletCountPercentParam>
{
	protected int mIncreasePercent;
	public override void enter()
	{
		base.enter();
		mIncreasePercent = mCustomParam.mIncreasePercent;
		mEventSystem.listenEvent<EventTowerSkillChanged>(mCharacter.getGUID(), onTowerSkillChanged, this);
		doIncrease(mIncreasePercent);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		doIncrease(-mIncreasePercent);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercent = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerSkillChanged(EventTowerSkillChanged param)
	{
		doIncrease(mIncreasePercent);
	}
	protected void doIncrease(int increasePercent)
	{
		if (mCharacterGame is CharacterTower tower)
		{
			TowerSkill curSkill = tower.getComSkill().getCurSkill();
			curSkill.setBulletIncreasePercent(curSkill.getBulletIncreasePercent() + increasePercent);
		}
	}
}