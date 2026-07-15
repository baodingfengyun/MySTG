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

// 眩晕,不允许释放技能,不允许移动
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