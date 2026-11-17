using static FrameUtility;

// 怪物的存活计时器：mLifeTime 初始 -1 表示"永久存活，不计时"。
// setLifeTime 设置倒计时后，每帧 tickTimerOnce 递减，到点置 mLifeDone=true。
// 典型用途是召唤物/临时怪物——BattleModeBase.update 检查 isLifeDone() 后走超时消亡分支。
// 注意它只打标记，不自己销毁怪物（与移动组件到达终点同理，统一由战斗模块处理）。
public class COMMonsterLifeTime : GameComponent
{
	protected CharacterMonster mMonster;	// 怪物对象
	protected float mLifeTime;              // 剩余生命时间
	protected bool mLifeDone;				// 生命是否已经结束
	public COMMonsterLifeTime()
	{
		mLifeTime = -1.0f;
	}
	public override void init(ComponentOwner owner)
	{
		base.init(owner);
		mMonster = mComponentOwner as CharacterMonster;
		mLifeTime = -1.0f;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mMonster = null;
		mLifeTime = -1.0f;
		mLifeDone = false;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (!mLifeDone)
		{
			mLifeDone = tickTimerOnce(ref mLifeTime, elapsedTime);
		}
	}
	public void setLifeTime(float time) { mLifeTime = time; }
	public bool isLifeDone() { return mLifeDone; }
}