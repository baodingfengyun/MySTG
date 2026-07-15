using static FrameUtility;

// 管理怪物的生命时间
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