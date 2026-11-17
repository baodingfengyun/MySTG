using static FrameUtility;
using static StringUtility;

// 参数
public class BuffDamageOverTimeParam : CharacterBuffParamT<BuffDamageOverTimeParam>
{
	public int mEachDamage;         // 伤害值
	public float mInterval;         // 伤害间隔
	public override void registeAllParam()
	{
		registeParam((param) => { mEachDamage = param.SToI(); });
		registeParam((param) => { mInterval = param.SToF(); });
	}
	protected override void copyInternal(BuffDamageOverTimeParam other)
	{
		mEachDamage = other.mEachDamage;
		mInterval = other.mInterval;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mEachDamage = 0;
		mInterval = 0.0f;
	}
}

// 无属性持续伤害(DoT)族代表，与 BuffPoison/BuffBurn 的区别是它不依赖元素克制字段。
// 范式：enter 读参数，update 用 tickTimerLoop 按 mInterval 周期触发，每跳 CmdMonsterSetHP 扣固定伤害，
// 类型标记 HP_DELTA.NORMAL_DAMAGE（毒/燃烧则走 DEBUFF，会被易伤/免伤字段修正）。
// 只有 target 是 CharacterMonster 才扣血——说明 DoT 是"挂在怪物身上"的 Buff。
// 注意它是"持续到 Buff 结束"的周期伤害，与 BuffDamageOnce(仅 enter 扣一次)形成对照。
public class BuffDamageOverTime : CharacterBuffT<BuffDamageOverTimeParam>
{
	public int mDamage;					// 伤害
	protected float mCurTime;			// 当前计时
	protected float mInterval;			// 伤害间隔
	public override void resetProperty()
	{
		base.resetProperty();
		mDamage = 0;
		mCurTime = 0.0f;
		mInterval = 0.0f;
	}
	public override void enter()
	{
		base.enter();
		mDamage = mCustomParam.mEachDamage;
		mInterval = mCustomParam.mInterval;
	}
	public override void update(float elapsedTime)
	{
		if (mCharacter is CharacterMonster monster && tickTimerLoop(ref mCurTime, elapsedTime, mInterval))
		{
			CmdMonsterSetHP.execute(monster, null, monster.getMonsterData().mHP - mDamage, -mDamage, true, HP_DELTA.NORMAL_DAMAGE);
		}
		base.update(elapsedTime);
	}
}