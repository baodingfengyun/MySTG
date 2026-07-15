using static FrameBaseHotFix;

// 参数
public class BuffXiangQianTaPurpleGemTargetCountUpParam : CharacterBuffParamT<BuffXiangQianTaPurpleGemTargetCountUpParam>
{
	public int mIncreaseCount;         // 增加的数量
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreaseCount = param.SToI(); });
	}
	protected override void copyInternal(BuffXiangQianTaPurpleGemTargetCountUpParam other)
	{
		mIncreaseCount = other.mIncreaseCount;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
	}
}

// 镶嵌塔紫宝石技能目标个数增加
public class BuffXiangQianTaPurpleGemTargetCountUp : CharacterBuffT<BuffXiangQianTaPurpleGemTargetCountUpParam>
{
	protected int mIncreaseCount;		// 增加的数量
	public override void enter()
	{
		base.enter();
		mIncreaseCount = mCustomParam.mIncreaseCount;
		if (mCharacterGame is CharacterTower tower &&
			tower.getTowerType() == TOWER_TYPE.XIANG_QIAN_TA)
		{
			mEventSystem.listenEvent<EventBulletWillFire>(tower.getGUID(), onBulletWillFire, this);
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreaseCount = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onBulletWillFire(EventBulletWillFire param)
	{
		if (param.mBullet is not SkillBulletLinkLine bullet)
		{
			return;
		}
		bullet.setTargetCount(bullet.getTargetCount() + mIncreaseCount);
	}
}