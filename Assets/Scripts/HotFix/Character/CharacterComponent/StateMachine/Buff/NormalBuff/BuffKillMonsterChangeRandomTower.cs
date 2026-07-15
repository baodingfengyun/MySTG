using System.Collections.Generic;
using static FrameBaseHotFix;
using static FrameUtility;

// 参数
public class BuffKillMonsterChangeRandomTowerParam : CharacterBuffParamT<BuffKillMonsterChangeRandomTowerParam>
{
	public int mNeedCount;		// 需要击杀的数量
	public bool mSaveLevel;		// 是否保留等级
	public List<TOWER_TYPE> mTowers = new();	// 可以变成的塔
	public override void registeAllParam()
	{
		registeParam((param) => { mNeedCount = param.SToI(); });
		registeParam((param) => { mSaveLevel = param.SToI() == 1; });
		registeParam((param) =>
		{
			foreach (var each in param.split(','))
			{
				mTowers.Add((TOWER_TYPE)each.SToI());
			}
		});
	}
	protected override void copyInternal(BuffKillMonsterChangeRandomTowerParam other)
	{
		mNeedCount = other.mNeedCount;
		mSaveLevel = other.mSaveLevel;
		mTowers.addRange(other.mTowers);
	}
	public override void check()
	{
		foreach(var each in mTowers)
		{
			checkEnum(each);
		}
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mSaveLevel = false;
		mTowers.Clear();
	}
}

// 击杀n个敌人后，随机变成一个塔，保留等级
public class BuffKillMonsterChangeRandomTower : CharacterBuffT<BuffKillMonsterChangeRandomTowerParam>
{
	protected int mNeedCount;		// 需要击杀的数量
	protected bool mSaveLevel;		// 是否保留等级
	protected int mCurCount;		// 该塔当前击杀的个数
	protected List<TOWER_TYPE> mTowers = new();	// 可以变成的塔
	public override void enter()
	{
		base.enter();
		mEventSystem.listenEvent<EventKillMonster>(mCharacterGame.getGUID(), onKillMonster, this);
		mNeedCount = mCustomParam.mNeedCount;
		mSaveLevel = mCustomParam.mSaveLevel;
		mTowers.addRange(mCustomParam.mTowers);
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mNeedCount = 0;
		mSaveLevel = false;
		mCurCount = 0;
		mTowers.Clear();
	}
	protected void onKillMonster(EventKillMonster eventParam)
	{
		if (++mCurCount >= mNeedCount)
		{
			CmdGlobalRandomChangeTowerRogue.execute(mCharacterGame as CharacterTower, mSaveLevel, mTowers);
		}
	}
}