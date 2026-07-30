using static GBR;

// 参数
public class BuffRangeDamageOnceParam : CharacterBuffParamT<BuffRangeDamageOnceParam>
{
	public float mPercent;         // 攻击力百分比
	public override void registeAllParam()
	{
		registeParam((param) => { mPercent = param.SToF(); });
	}
	protected override void copyInternal(BuffRangeDamageOnceParam other)
	{
		mPercent = other.mPercent;
	}
	public override void check(){}
	public override void resetProperty()
	{
		base.resetProperty();
		mPercent = 0;
	}
}

// 造成一次范围攻击力百分比伤害
public class BuffRangeDamageOnce : CharacterBuffT<BuffRangeDamageOnceParam>
{
	public override void enter()
	{
		base.enter();
		if (mCharacterGame is CharacterTower tower)
		{
			int damage = (tower.getAttack() * mCustomParam.mPercent).round();
			using var a = new ListScope<CharacterMonster>(out var monsterList);
			mTowerDefenceSystem.getMonstersInRange(tower.getPosition(), tower.getRange(), monsterList);
			foreach (CharacterMonster monster in monsterList)
			{
				CmdMonsterSetHP.execute(monster, mCharacterGame, monster.getMonsterData().mHP - damage, -damage, true);
			}
		}
	}
}