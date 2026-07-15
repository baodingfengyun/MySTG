using static StringUtility;
using static GBR;
using static GDR;

// 参数
public class TriggerBuffToRangeMonsterOnceParam : CharacterTriggerParamT<TriggerBuffToRangeMonsterOnceParam>
{
	public float mRange;           // 范围半径,单位是格子大小
	public override void registeAllParam()
	{
		base.registeAllParam();
		registeParam((param) => { mRange = param.SToF(); });
	}
	public override void resetProperty()
	{
		base.resetProperty();
		mRange = 0.0f;
	}
	protected override void copyInternal(TriggerBuffToRangeMonsterOnceParam other)
	{
		base.copyInternal(other);
		mRange = other.mRange;
	}
}

// 给范围内的所有怪物附加buff,只在进入状态时添加一次,并且不会主动移除
public class TriggerBuffToRangeMonsterOnce : CharacterTriggerT<TriggerBuffToRangeMonsterOnceParam>
{
	public override void enter()
	{
		base.enter();
		using var a = new ListScope<CharacterMonster>(out var monsterList);
		mTowerDefenceSystem.getMonstersInRange(mCharacterGame.getPosition(), mCustomParam.mRange * GRID_SIZE, monsterList);
		foreach (CharacterMonster monster in monsterList)
		{
			addBuff(monster);
		}
	}
}