using static StringUtility;

// 参数
public class BuffVertigoParam : CharacterBuffParamT<BuffVertigoParam>
{
	public float mBossPercent;
	public override void registeAllParam()
	{
		registeParam((param) => { mBossPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffVertigoParam other)
	{
		mBossPercent = other.mBossPercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mBossPercent = 0.0f;
	}
}

// 【代表样例：控制类 Buff】眩晕
// 【效果】"不允许移动/攻击"不是本类直接做的——靠眩晕 Buff 所属的 StateGroup
//   (NotAllowMove/NotAllowAttack)让组件禁用行为；本类只负责：
//   ① Boss 减控：若是 BOSS，持续时间 × mBossPercent(抗控)；
//   ② 挂 ActionVertigo 动作状态播眩晕动画，并借此中断正在进行的其他动作状态。
// 【范式】控制 Buff = 数值/状态组禁用行为 + 动作状态表现，两件事分开做。
public class BuffVertigo : CharacterBuffT<BuffVertigoParam>
{
	public override void enter()
	{
		base.enter();
		if (mCharacterGame is CharacterMonster monster && monster.getMonsterData().mTableData.mStrength == MONSTER_STRENGTH.BOSS)
		{
			mStateTime *= mCustomParam.mBossPercent;
		}
		// 通过添加眩晕行为来播放眩晕动画,这样可以中断正在进行的其他行为状态
		using var a = new ClassScope<StateParam>(out var param);
        param.mBuffTime = mStateTime;
        mCharacterGame.getStateMachine().addState<ActionVertigo>(param);
	}
}