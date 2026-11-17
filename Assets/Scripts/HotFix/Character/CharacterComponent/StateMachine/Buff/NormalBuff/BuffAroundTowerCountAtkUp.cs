using static FrameBaseHotFix;
using static GameUtilityHotFix;
using static StringUtility;
using static GBR;

// 参数
public class BuffAroundTowerCountAttackUpParam : CharacterBuffParamT<BuffAroundTowerCountAttackUpParam>
{
	public float mPercent;      // 每个塔增加的攻击力百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffAroundTowerCountAttackUpParam other)
	{
		mPercent = other.mPercent;
	}
	public override void check() { }
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
	}
}

// 周围塔数加成：统计本塔相邻六边形格子上的塔数量来提升攻击（AtkSpeedUp 同构，改攻速）。
// 与 BuffTypeTowerIncreaseSelfAttack 的区别：那个数"全图某类型塔"，这个只数"身边一圈"。
// 同样走"事件置脏 + 每帧重算"：getHexDiagonalGird(grid,1) 取相邻6格，hasTowerAtGrid 逐格判断。
// 用 mCount 记录上次计数，只补差额 mPercent*(new-old)；leave 减掉 mPercent*mCount。
// 注意类名是 AttackUp，原单行注释只写"攻击力提升"过于笼统，此处补全语义。
public class BuffAroundTowerCountAttackUp : CharacterBuffT<BuffAroundTowerCountAttackUpParam>
{
	public float mPercent;      // 每个塔增加的攻击力百分比
	public int mCount;          // 周围的塔数量
	public bool mTowerChanged;  // 场上的塔是否有改变,每帧检测一次,因为一帧里面可能会改变多次
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0.0f;
		mCount = 0;
		mTowerChanged = false;
	}
	public override void enter()
	{
		base.enter();
		mPercent = mCustomParam.mPercent;
		mEventSystem.listenEvent<EventGridTowerChange>(onTowerChanged, this);
		mTowerChanged = true;
	}
	public override void update(float elapsedTime)
	{
		base.update(elapsedTime);
		if (mTowerChanged)
		{
			mTowerChanged = false;
			int newCount = 0;
			foreach (int grid in getHexDiagonalGird((mCharacterGame as CharacterTower).getGridIndex(), 1))
			{
				newCount += mTowerDefenceSystem.hasTowerAtGrid(grid) ? 1 : 0;
			}
			if (newCount != mCount)
			{
				mCharacterGame.getGameData().mIncreaseAttackPercent += mPercent * newCount - mPercent * mCount;
				mCount = newCount;
			}
		}
	}
	public override void leave(bool isBreak, bool willDestroy, string param)
	{
		base.leave(isBreak, willDestroy, param);
		mCharacterGame.getGameData().mIncreaseAttackPercent -= mPercent * mCount;
	}
	//------------------------------------------------------------------------------------------------------------------------------
	protected void onTowerChanged(EventGridTowerChange eventParam)
	{
		mTowerChanged = true;
	}
}