using static StringUtility;

// 参数
public class BuffBulletFlyDisIncreaseDamageParam : CharacterBuffParamT<BuffBulletFlyDisIncreaseDamageParam>
{
	public float mIncreasePercent;         // 每飞行一个格子的距离增加的伤害百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mIncreasePercent = param.SToF(); });
	}
	protected override void copyInternal(BuffBulletFlyDisIncreaseDamageParam other)
	{
		mIncreasePercent = other.mIncreasePercent;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercent = 0.0f;
	}
}

// 子弹伤害随飞行距离增加（子弹属性族代表，同族 BuffBulletFlyDisIncreaseExploRange/BulletSpeedUp/
// BulletDamageUpInExploRange 等，命名以 BuffBullet 开头）。
// 范式仍是 enter/leave 改 GameData 字段——本类改 mDamageIncreaseByFlyDis。
// 关键在消费端：子弹飞行时按已飞距离取该系数加到伤害上(见 SkillBullet/generateDamage)，
// 所以"越远越疼"这类词条也是纯填表改字段，无需写新伤害逻辑。
public class BuffBulletFlyDisIncreaseDamage : CharacterBuffT<BuffBulletFlyDisIncreaseDamageParam>
{
	protected float mIncreasePercent;      // 每飞行一个格子的距离增加的伤害百分比
	public override void enter()
	{
		base.enter();
		mIncreasePercent = mCustomParam.mIncreasePercent;
		mCharacterGame.getGameData().mDamageIncreaseByFlyDis += mIncreasePercent;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mDamageIncreaseByFlyDis -= mIncreasePercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mIncreasePercent = 0.0f;
	}
}