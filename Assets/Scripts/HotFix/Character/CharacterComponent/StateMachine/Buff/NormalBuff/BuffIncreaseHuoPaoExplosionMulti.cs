using static StringUtility;
using static FrameBaseHotFix;

// 参数
public class BuffIncreaseHuoPaoExplosionMultiParam : CharacterBuffParamT<BuffIncreaseHuoPaoExplosionMultiParam>
{
	public int mIncreaseCount;						// 多爆炸的次数
	public float mIncreaseChance;					// 多爆炸的概率
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseCount = param.SToI(); });
		registeParam((param) => { mIncreaseChance = param.SToF(); });
	}
	protected override void copyInternal(BuffIncreaseHuoPaoExplosionMultiParam other)
	{
		mIncreaseCount = other.mIncreaseCount;
		mIncreaseChance = other.mIncreaseChance;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
		mIncreaseChance = 0.0f;
	}
}

// 增加火炮塔子弹爆炸次数
public class BuffIncreaseHuoPaoExplosionMulti : CharacterBuffT<BuffIncreaseHuoPaoExplosionMultiParam>
{
	protected int mIncreaseCount;                   // 多爆炸的次数
	protected float mIncreaseChance;				// 多爆炸的概率
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventTowerSkillChanged>(mCharacter.getGUID(), onTowerSkillChanged, this);
		mIncreaseCount = mCustomParam.mIncreaseCount;
		mIncreaseChance = mCustomParam.mIncreaseChance;
		doIncrease(mIncreaseCount, mIncreaseChance);
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		doIncrease(-mIncreaseCount, -mIncreaseChance);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
		mIncreaseChance = 0.0f;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerSkillChanged(EventTowerSkillChanged param)
	{
		doIncrease(mIncreaseCount, mIncreaseChance);
	}
	protected void doIncrease(int increaseCount, float increaseChance)
	{
		if ((mCharacterGame as CharacterTower).getComSkill().getCurSkill() is TowerSkill_HuoPao skill)
		{
			skill.addIncreaseExplosionTimes(increaseCount);
			skill.addIncreaseExplosionChance(increaseChance);
		}
	}
}