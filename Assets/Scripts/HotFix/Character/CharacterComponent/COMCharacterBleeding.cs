
// 流血(DEBUFF)组件：怪物每移动 BLEED_MOVE_DISTANCE 距离触发一次流血伤害。
// 自身不 tick——提供一个 getListenFunction() 回调注册到 COMMonsterMovement 的移动回调上，
// 由移动组件每帧把位移传进来累加(onMoving)。这是"组件间通过回调解耦"的范例。
// addBleeding 用 clampMin 只保留最大流血百分比(同类效果不叠加，取最强)。
public class COMCharacterBleeding : GameComponent
{
	protected float mMaxBleeding;						// 最大的出血百分比
	protected const float BLEED_MOVE_DISTANCE = 1.0f;   // 出血一次所需要移动的距离
	protected float mCurMoveDistance;                   // 当前移动距离
	protected FloatCallback mListenFunction;			// 监听移动的委托
	public COMCharacterBleeding()
	{
		mListenFunction = onMoving;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMaxBleeding = 0.0f;
		mCurMoveDistance = 0.0f;
		// mListenFunction不重置
		// mListenFunction = null;
	}
	// 只保留最大的流血百分比
	public void addBleeding(float value)
	{
		mMaxBleeding = mMaxBleeding.clampMin(value);
	}
	public void removeBleeding(float value)
	{
		if (value.isEqual(mMaxBleeding))
		{
			mMaxBleeding = 0.0f;
		}
	}
	public FloatCallback getListenFunction() { return mListenFunction; }
	//------------------------------------------------------------------------------------------------------------------------------
	// DEBUFF效果：移动（距离累加）引发出血逻辑
	protected void onMoving(float distance)
	{
		if (mMaxBleeding <= 0.0f)
		{
			return;
		}
		mCurMoveDistance += distance;
		if (mCurMoveDistance >= BLEED_MOVE_DISTANCE)
		{
			mCurMoveDistance -= BLEED_MOVE_DISTANCE;
			// 出血伤害一次
			if (mComponentOwner is CharacterMonster monster)
			{
				// 伤害 = 流血百分比 × 当前血量。注意这里用的是实时 HP 而非最大 HP，
				// 所以血量越低每跳伤害越低（递减流血），与 BuffBurn 的递减思路一致。
				int damage = (mMaxBleeding * monster.getMonsterData().mHP).ceil();
				// 执行怪物设置血量
				CmdMonsterSetHP.execute(monster, null, monster.getMonsterData().mHP - damage, -damage, true, HP_DELTA.DEBUFF);
			}
		}
	}
}