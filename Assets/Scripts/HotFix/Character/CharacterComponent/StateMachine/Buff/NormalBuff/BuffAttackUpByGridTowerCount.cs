using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static GBR;

// 参数
public class BuffAttackUpByGridTowerCountParam : CharacterBuffParamT<BuffAttackUpByGridTowerCountParam>
{
	public float mAttack;						// 增加的攻击力百分比
	public int mCount;							// 所需要的塔数量
	public int mRange;							// 附近几格
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mAttack = param.SToF(); });
		registeParam((param) => { mCount = param.SToI(); });
		registeParam((param) => { mRange = param.SToI(); });
	}
	protected override void copyInternal(BuffAttackUpByGridTowerCountParam other)
	{
		mAttack = other.mAttack;
		mCount = other.mCount;
		mRange = other.mRange;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mAttack = 0.0f;
		mCount = 0;
		mRange = 0;
	}
}

// 根据场上防御塔数量，提升自身攻击力和暴击率
public class BuffAttackUpByGridTowerCount : CharacterBuffT<BuffAttackUpByGridTowerCountParam>
{
	public float mAttackAdded;					// 已经增加的
	public float mAttack;						// 增加的攻击力百分比
	public int mCount;							// 所需要的塔数量
	public int mRange;							// 附近几格
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventGridTowerChange>(onGridTowerChange, this);
		mAttack = mCustomParam.mAttack;
		mCount = mCustomParam.mCount;
		mRange = mCustomParam.mRange;
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mAttackAdded;
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mAttackAdded = 0.0f;
		mAttack = 0.0f;
		mCount = 0;
		mRange = 0;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void addBuffs()
	{
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mAttackAdded;
		using var a = new ListScope<int>(out var girds);
		getHexAroundGird(mCharacterGame.getGridIndex(), mRange, girds);
		int count = 0;
		foreach (int each in girds)
		{
			CharacterTower tower = mTowerDefenceSystem.getTowerAtGrid(each);
			if (tower != null)
			{
				count++;
			}
		}
		if(count >= mCount)
		{
			mAttackAdded = mAttack;
			mCharacterGame.getGameData().mIncreaseAttackPercent += mAttackAdded;
		}
		else
		{
			mAttackAdded = 0;
		}
	}
	protected void onGridTowerChange(EventGridTowerChange param)
	{
		addBuffs();
	}
}