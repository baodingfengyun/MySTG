using static FrameBaseHotFix;

// 参数
public class BuffAttackDownThenUpByHitSameMonsterParam : CharacterBuffParamT<BuffAttackDownThenUpByHitSameMonsterParam>
{
	public float mDownPercent;	// 降低的攻击力百分比
	public float mUpPercent;	// 每次上升的攻击力百分比
	public int mMaxLayer;		// 上限层数
	public override void registeAllParam()
	{
		registeParam((param) => { mDownPercent = param.SToF(); });
		registeParam((param) => { mUpPercent = param.SToF(); });
		registeParam((param) => { mMaxLayer = param.SToI(); });
	}
	protected override void copyInternal(BuffAttackDownThenUpByHitSameMonsterParam other)
	{
		mDownPercent = other.mDownPercent;
		mUpPercent = other.mUpPercent;
		mMaxLayer = other.mMaxLayer;
	}
	public override void check() {}
	public override void resetProperty()
	{
		base.resetProperty();
		mDownPercent = 0.0f;
		mUpPercent = 0.0f;
		mMaxLayer = 0;
	}
}

// 攻击力降低，每次攻击提高攻击力。上限n层，切换目标时重置
public class BuffAttackDownThenUpByHitSameMonster : CharacterBuffT<BuffAttackDownThenUpByHitSameMonsterParam>
{
	public float mDownPercent;	// 降低的攻击力百分比
	public float mUpPercent;	// 每次上升的攻击力百分比
	public int mMaxLayer;		// 上限层数
	public int mCurLayer;		// 当前层数
	public long mMonsterID;		// 当前击中的怪物，如果改变了就要重置
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventHitCharacter>(mCharacterGame.getGUID(), onHitMonster, this);
		mDownPercent = mCustomParam.mDownPercent;
		mUpPercent = mCustomParam.mUpPercent;
		mMaxLayer = mCustomParam.mMaxLayer;
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mDownPercent;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		resetUp();
		mCharacterGame.getGameData().mIncreaseAttackPercent += mDownPercent;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mDownPercent = 0.0f;
		mUpPercent = 0.0f;
		mMaxLayer = 0;
		mCurLayer = 0;
		mMonsterID = 0L;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onHitMonster(EventHitCharacter eventParam)
	{
		long id = eventParam.mTarget.getGUID();
		if (id != mMonsterID)
		{
			resetUp();
			mMonsterID = id;
		}
		if (mCurLayer < mMaxLayer)
		{
			++mCurLayer;
			mCharacterGame.getGameData().mIncreaseAttackPercent += mUpPercent;
		}
	}
	protected void resetUp()
	{
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mUpPercent * mCurLayer;
		mCurLayer = 0;
	}
}