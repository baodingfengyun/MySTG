using static FrameBaseHotFix;
using static FrameUtility;

// 技能基类
public class CharacterSkill : DelayCmdWatcher
{
	protected CharacterGame mCharacter;			// 技能所属的角色
	protected float mRemainCD;                  // 剩余的CD时间
	protected int mRemainWaveCD;				// 剩余的波次CD时间
	protected long mFireID;						// 技能释放的唯一ID，每次释放都会生成一个新的唯一ID
	public override void resetProperty()
	{
		base.resetProperty();
		mCharacter = null;
		mRemainCD = 0.0f;
		mRemainWaveCD = 0;
		mFireID = 0L;
	}
	public virtual void update(float elapsedTime)
	{
		tickTimerOnce(ref mRemainCD, elapsedTime);
	}
	public override void destroy()
	{
		base.destroy();
		breakSkill();
	}
	public virtual void breakSkill()
	{
		interruptAllCommand();
	}
	public virtual void setCharacter(CharacterGame character) { mCharacter = character; }
	public CharacterGame getCharacter() { return mCharacter; }
	public bool isCoolDown() { return mRemainCD <= 0.0f && mRemainWaveCD <= 0; }
	public void resetCD()
	{
		mRemainCD = 0.0f;
		mRemainWaveCD = 0;
	}
	public long getFireID() { return mFireID; }
	public virtual void notifyWaveChanged()
	{
		if (mRemainWaveCD > 0)
		{
			--mRemainWaveCD;
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onPreFireSkill()
	{
		++mFireID;
		using var a = new ClassScope<EventPreFireSkill>(out var param);
		param.mSkill = this;
		mEventSystem.pushEvent(param, mCharacter.getGUID());
	}
	protected void onPostFireSkill()
	{
		using var a = new ClassScope<EventPostFireSkill>(out var param);
		param.mSkill = this;
		mEventSystem.pushEvent(param, mCharacter.getGUID());
	}
}