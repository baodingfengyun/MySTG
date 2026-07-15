using static FrameBaseHotFix;

// 参数
public class BuffBulletAttackUpHitMonsterParam : CharacterBuffParamT<BuffBulletAttackUpHitMonsterParam>
{
	public float mPercent;			// 每次穿过提高攻击百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffBulletAttackUpHitMonsterParam other)
	{
		mPercent = other.mPercent;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
}

// 波动塔的子弹每穿过1个敌方单位，攻击提高
public class BuffBulletAttackUpHitMonster : CharacterBuffT<BuffBulletAttackUpHitMonsterParam>
{
	public float mPercent;			// 每次穿过提高攻击百分比
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventHitCharacter>(mCharacterGame.getGUID(), onHitCharacter, this);
		mPercent = mCustomParam.mPercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
	protected void onHitCharacter(EventHitCharacter eventParam)
	{
		eventParam.mBullet.setAttackPercent(eventParam.mBullet.getAttackPercent() + mPercent);
	}
}