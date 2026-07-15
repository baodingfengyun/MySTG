using static FrameBaseHotFix;

// 参数
public class BuffIncreaseBulletCountHuoPaoParam : CharacterBuffParamT<BuffIncreaseBulletCountHuoPaoParam>
{
	public int mIncreaseCount;
	public float mFireInterval;
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseCount = param.SToI(); });
		registeParam((param) => { mFireInterval = param.SToF(); });
	}
	protected override void copyInternal(BuffIncreaseBulletCountHuoPaoParam other)
	{
		mIncreaseCount = other.mIncreaseCount;
		mFireInterval = other.mFireInterval;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
		mFireInterval = 0.0f;
	}
}

// 增加火炮塔子弹数量
public class BuffIncreaseBulletCountHuoPao : CharacterBuffT<BuffIncreaseBulletCountHuoPaoParam>
{
	protected int mIncreaseCount;
	protected float mFireInterval;
	public override void enter()
	{
		base.enter();
		mIncreaseCount = mCustomParam.mIncreaseCount;
		mFireInterval = mCustomParam.mFireInterval + 0.2f;
		mEventSystem.listenEvent<EventTowerSkillChanged>(mCharacter.getGUID(), onTowerSkillChanged, this);
		doIncrease();
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		if (mCharacterGame is CharacterTower tower && tower.getComSkill().getCurSkill() is TowerSkill_HuoPao skill)
		{
			skill.removeBulletCount(mIncreaseCount);
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
		mFireInterval = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerSkillChanged(EventTowerSkillChanged param)
	{
		doIncrease();
	}
	protected void doIncrease()
	{
		if (mCharacterGame is CharacterTower tower && tower.getComSkill().getCurSkill() is TowerSkill_HuoPao skill)
		{
			skill.addBulletCount(mIncreaseCount, mFireInterval);
		}
	}
}