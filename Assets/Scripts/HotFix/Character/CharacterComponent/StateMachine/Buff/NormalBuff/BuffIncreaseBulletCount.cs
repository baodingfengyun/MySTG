using static FrameBaseHotFix;

// 参数
public class BuffIncreaseBulletCountParam : CharacterBuffParamT<BuffIncreaseBulletCountParam>
{
	public int mIncreaseCount;
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseCount = param.SToI(); });
	}
	protected override void copyInternal(BuffIncreaseBulletCountParam other)
	{
		mIncreaseCount = other.mIncreaseCount;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
	}
}

// 增加子弹数量
public class BuffIncreaseBulletCount : CharacterBuffT<BuffIncreaseBulletCountParam>
{
	protected int mIncreaseCount;
	public override void enter()
	{
		base.enter();
		mIncreaseCount = mCustomParam.mIncreaseCount;
		mEventSystem.listenEvent<EventTowerSkillChanged>(mCharacter.getGUID(), onTowerSkillChange, this);
		doIncrease(mIncreaseCount);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		doIncrease(-mIncreaseCount);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerSkillChange(EventTowerSkillChanged param)
	{
		doIncrease(mIncreaseCount);
	}
	protected void doIncrease(int increaseCount)
	{
		if (mCharacterGame is CharacterTower tower)
		{
			TowerSkill curSkill = tower.getComSkill().getCurSkill();
			curSkill.setBulletIncreaseCount(curSkill.getBulletIncreaseCount() + increaseCount);
		}
	}
}